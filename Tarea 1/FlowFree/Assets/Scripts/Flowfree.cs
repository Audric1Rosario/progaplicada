using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Flowfree : MonoBehaviour
{
    public Texture2D SpritesSource;
    [SerializeField] private Vector3 mousePosition;
    [SerializeField] private Vector2Int gridCoord;
    private float ortosize;
    private float aspect;
    private Color[] definedColors;
    private SpriteRenderer[] cellGrid;
    private Game _game;
    private Sprite[] _sprites;
    private float xHalfValue;
    private float yHalfValue;

    public int getActions() //para coger
    {
        if (_game == null)
            return 0;
        return _game.actions;
    }

    public bool isWin()
    {
        if (_game == null)
            return false;
        return _game.win;
    }

    public void loadLevel(int[] ar)
    {
        if ((ar.Length - 2) % 4 != 0)
        {
            Debug.LogError("Incorrect format");
            return;
        }

        Pair<Vector2Int, Vector2Int>[] flows = new Pair<Vector2Int, Vector2Int>[(ar.Length - 2) / 4];
        for (int i = 0; i < flows.Length; i++)
        {
            flows[i] = new Pair<Vector2Int, Vector2Int>(new Vector2Int(ar[i * 4 + 2], ar[i * 4 + 3]),
                new Vector2Int(ar[i * 4 + 4], ar[i * 4 + 5]));
        }

        StartGame(ar[0], ar[1], flows);
    }

    enum eColors
    {
        None,
        Red,
        Green,
        Blue,
        Yellow,
        Orange,
        Cyan,
        Pink,
        Brown,
        Purple,
        White,
    }

    enum eSprite
    {
        None,
        Start,
        Vertical,
        Horizontal,
        Center,
        End
    }

    // TODO: juego
    class Game
    {
        public bool win;
        public int x;
        public int y;
        public int actions;
        private eColors currentColor;
        public Dictionary<eColors, Stack<Vector2Int>> flows;
        public Dictionary<Vector2Int, eColors> starts;
        public Dictionary<Vector2Int, eColors> ends;
        private Dictionary<eColors, Vector2Int> inversedStarts;
        private Dictionary<eColors, Vector2Int> inversedEnd;
        public float escala;
        public eColors[] grid;
        private Vector2Int selected;
        private eColors selectedColor;


        public Game(int x, int y, float orto, float asp, Pair<Vector2Int, Vector2Int>[] flows)
        {
            this.win = false;
            this.x = x;
            this.y = y;
            this.actions = 0;
            this.grid = new eColors[x * y];
            this.flows = new Dictionary<eColors, Stack<Vector2Int>>();
            this.currentColor = eColors.None;
            this.escala = (2.0f * orto) / y;
            this.escala = Math.Min(this.escala, (2.0f * orto * asp) / x);
            this.starts = new Dictionary<Vector2Int, eColors>();
            this.inversedStarts = new Dictionary<eColors, Vector2Int>();
            this.ends = new Dictionary<Vector2Int, eColors>();
            this.inversedEnd = new Dictionary<eColors, Vector2Int>();
            this.selected = new Vector2Int();
            this.selectedColor = eColors.None;
            for (int i = 0; i < flows.Length; i++)
            {
                if (this.flows.Count < 10 && !starts.ContainsKey(flows[i].First) && !ends.ContainsKey(flows[i].Second))
                {
                    currentColor++;
                    this.flows.Add(currentColor, new Stack<Vector2Int>());
                    starts.Add(flows[i].First, currentColor);
                    inversedStarts.Add(currentColor, flows[i].First);
                    Debug.Log("Added " + currentColor + " (" + this.flows.Count + "/10)");
                    Debug.Log("    Start " + currentColor + " added " + flows[i].First);
                    ends.Add(flows[i].Second, currentColor);
                    inversedEnd.Add(currentColor, flows[i].Second);
                }
                else
                {
                    if (this.flows.Count == 10)
                    {
                        Debug.LogError("Limit of 10 reached.");
                        break;
                    }
                    else
                    {
                        if (starts.ContainsKey(flows[i].First))
                        {
                            Debug.LogError("Start at " + flows[i].First.ToString());
                        }

                        if (ends.ContainsKey(flows[i].Second))
                        {
                            Debug.LogError("End at " + flows[i].Second.ToString());
                        }
                    }
                }
            }
        }

        public Vector2Int getMouseGrid(float x, float y)
        {
            return new Vector2Int((int)Math.Floor((x + this.x * escala / 2.0f) / escala),
                (int)Math.Floor((y + this.y * escala / 2.0f) / escala));
        }

        public void selectCell(int x, int y)
        {
            if (win)
                return;
            if (this.grid[x + y * this.x] != eColors.None)
            {
                this.selectedColor = this.grid[x + y * this.x];
            }
            else if (this.starts.ContainsKey(new Vector2Int(x, y)))
            {
                this.selectedColor = starts[new Vector2Int(x, y)];
            }
            else
            {
                Debug.Log("Not selected");
                return;
            }

            this.selected.x = x;
            this.selected.y = y;
            Debug.Log("Selected" + this.selectedColor + "(" + this.selected + ")");
        }

        public void check(int x, int y)
        {
            Vector2Int temp = new Vector2Int(x, y);
            if (selectedColor == eColors.None || x < 0 || y < 0 || x >= this.x || y >= this.y)
            {
                return;
            }


            //Undo
            if ((grid[x + y * this.x] == selectedColor && flows[selectedColor].Peek() != temp))
            {
                var tempC = selectedColor;
                selectedColor = eColors.None;
                while (flows[tempC].Peek() != temp)
                {
                    grid[flows[tempC].Peek().x + flows[tempC].Peek().y * this.x] = eColors.None;
                    flows[tempC].Pop();
                }

                selectedColor = tempC;
                selected.x = x;
                selected.y = y;
                return;
            }

            if (selected.x == x && selected.y == y)
            {
                if (grid[selected.x + selected.y * this.x] != selectedColor && !flows[selectedColor].Contains(selected))
                {
                    grid[selected.x + selected.y * this.x] = selectedColor;
                    flows[selectedColor].Push(selected);
                }

                return;
            }

            float direction = Mathf.Atan2(y - selected.y, x - selected.x);
            if ((int)Math.Abs(direction * Mathf.Rad2Deg) % 90 != 0)
                return;
            move(selected.x + (int)Mathf.Cos(direction), selected.y + (int)Mathf.Sin(direction));
        }

        private void move(int x, int y)
        {
            Vector2Int temp = new Vector2Int(x, y);
            if (x < 0 || x >= this.x || y < 0 || y >= this.y || (this.selected.x == x && this.selected.y == y) ||
                selectedColor == eColors.None || selected == inversedEnd[selectedColor])
                return;
            if (starts.ContainsKey(temp) && this.grid[x + y * this.x] != selectedColor)
                return;
            if (ends.ContainsKey(temp))
                if (ends[temp] != selectedColor)
                    return;
            if (this.grid[x + y * this.x] != eColors.None)
                return;

            this.grid[x + y * this.x] = selectedColor;
            selected.x = x;
            selected.y = y;
            if (!flows[selectedColor].Contains(selected))
                flows[selectedColor].Push(selected);
            Debug.Log(selected);
        }

        public void deselectCell()
        {
            if (win)
                return;
            this.selectedColor = eColors.None;
            this.actions++;
            checkForWin();
        }

        public void checkForWin()
        {
            foreach (KeyValuePair<eColors, Stack<Vector2Int>> aux in flows)
            {
                if (aux.Value.Count == 0)
                    return;
                if (aux.Value.Peek() != inversedEnd[aux.Key])
                {
                    Debug.Log("meh");
                    return;
                }
            }

            foreach (eColors aux in grid)
            {
                if (aux == eColors.None)
                {
                    Debug.Log("neh");
                    return;
                }
            }

            win = true;
            Debug.Log("win");
        }
    }

    void StartGame(int x, int y, Pair<Vector2Int, Vector2Int>[] flows)
    {
        if (flows.Length > 10)
        {
            Debug.Log("Maximun of 10 flows");
            return;
        }

        int i, j;
        _game = new Game(x, y, ortosize * 0.9f, aspect, flows);
        foreach (Transform aux in transform)
        {
            Destroy(aux.gameObject);
        }

        xHalfValue = x * _game.escala / 2.0f;
        yHalfValue = y * _game.escala / 2.0f;
        GameObject temp;
        for (i = 0; i <= x; i++)
        {
            temp = new GameObject("line", typeof(LineRenderer));
            temp.GetComponent<LineRenderer>().material = new Material(Shader.Find("Unlit/Texture"));
            temp.GetComponent<LineRenderer>().SetWidth(0.04f, 0.04f);
            temp.GetComponent<LineRenderer>().SetColors(Color.white, Color.white);
            temp.GetComponent<LineRenderer>().SetPositions(new Vector3[]
            {
                new Vector3(i * _game.escala - xHalfValue, yHalfValue, 7.2f),
                new Vector3(i * _game.escala - xHalfValue, -yHalfValue, 7.2f)
            });
            temp.transform.parent = gameObject.transform;
        }

        for (i = 0; i <= y; i++)
        {
            temp = new GameObject("line", typeof(LineRenderer));
            temp.GetComponent<LineRenderer>().material = new Material(Shader.Find("Unlit/Texture"));
            temp.GetComponent<LineRenderer>().SetWidth(0.04f, 0.04f);
            temp.GetComponent<LineRenderer>().SetColors(Color.white, Color.white);
            temp.GetComponent<LineRenderer>().SetPositions(new Vector3[]
            {
                new Vector3(xHalfValue, i * _game.escala - yHalfValue, 7.2f),
                new Vector3(-xHalfValue, i * _game.escala - yHalfValue, 7.2f),
            });
            temp.transform.parent = gameObject.transform;
        }

        cellGrid = new SpriteRenderer[x * y];

        for (i = 0; i < x; i++)
        {
            for (j = 0; j < y; j++)
            {
                cellGrid[i + j * x] =
                    createTile("tile",
                            new Vector3((i + 0.5f) * _game.escala - xHalfValue,
                                (j + 0.5f) * _game.escala - yHalfValue, 5), _sprites[0], _game.escala)
                        .GetComponent<SpriteRenderer>();
            }
        }

        foreach (KeyValuePair<Vector2Int, eColors> aux in _game.starts)
        {
            createTile("tile",
                    new Vector3((aux.Key.x + 0.5f) * _game.escala - xHalfValue,
                        (aux.Key.y + 0.5f) * _game.escala - yHalfValue, 7), _sprites[(int)eSprite.Start], _game.escala)
                .GetComponent<SpriteRenderer>().color = definedColors[(int)aux.Value - 1];
        }

        foreach (KeyValuePair<Vector2Int, eColors> aux in _game.ends)
        {
            createTile("tile",
                    new Vector3((aux.Key.x + 0.5f) * _game.escala - xHalfValue,
                        (aux.Key.y + 0.5f) * _game.escala - yHalfValue, 7), _sprites[(int)eSprite.End], _game.escala)
                .GetComponent<SpriteRenderer>().color = definedColors[(int)aux.Value - 1];
        }
    }

    private GameObject createTile(string name, Vector3 p, Sprite sprite, float escala)
    {
        GameObject tile = new GameObject(name, typeof(SpriteRenderer));
        tile.GetComponent<SpriteRenderer>().sprite = sprite;
        tile.transform.position = p;
        tile.transform.localScale = new Vector3(escala, escala, 1.0f);
        tile.transform.parent = gameObject.transform;
        return tile;
    }


    void Awake()
    {
        _sprites = new Sprite[]
        {
            Sprite.Create(SpritesSource, new Rect(200, 0, 100, 100), new Vector2(0.5f, 0.5f)),
            Sprite.Create(SpritesSource, new Rect(0, 100, 100, 100), new Vector2(0.5f, 0.5f)),
            Sprite.Create(SpritesSource, new Rect(100, 100, 100, 100), new Vector2(0.5f, 0.5f)),
            Sprite.Create(SpritesSource, new Rect(0, 0, 100, 100), new Vector2(0.5f, 0.5f)),
            Sprite.Create(SpritesSource, new Rect(100, 0, 100, 100), new Vector2(0.5f, 0.5f)),
            Sprite.Create(SpritesSource, new Rect(200, 100, 100, 100), new Vector2(0.5f, 0.5f))
        };

        definedColors = new[]
        {
            Color.red,
            Color.green,
            Color.blue,
            Color.yellow,
            new Color(1, 0.5f, 0), //Orange
            Color.cyan,
            new Color(1f, 0.5f, 1f), //Pink
            new Color(0.5f, 0.2f, 0), //Brown
            new Color(0.5f, 0.1f, 1f), //Purple
            Color.white
        };

        ortosize = Camera.main.orthographicSize;
        aspect = Camera.main.aspect;

        Camera.main.backgroundColor = Color.black; //Me gusta lo prieto, no homo -Zhen Li 2020
        Camera.main.orthographicSize = 5;
        loadLevel(new[]
        {
            2, 1, 0, 0, 1, 0
        });
        // loadLevel(new []{5,5,0,2,3,4,4,4,3,3,1,1,2,3,4,0,3,1,0,1,4,1});
        // // StartGame(5, 5, new Pair<Vector2Int, Vector2Int>[]
        // // {
        // //     new Pair<Vector2Int, Vector2Int>(new Vector2Int(0, 2), new Vector2Int(3, 4)),
        // //     new Pair<Vector2Int, Vector2Int>(new Vector2Int(4, 4), new Vector2Int(3, 3)),
        // //     new Pair<Vector2Int, Vector2Int>(new Vector2Int(1, 1), new Vector2Int(2, 3)),
        // //     new Pair<Vector2Int, Vector2Int>(new Vector2Int(4, 0), new Vector2Int(3, 1)),
        // //     new Pair<Vector2Int, Vector2Int>(new Vector2Int(0, 1), new Vector2Int(4, 1))
        // // });
    }

    private void Start()
    {
        StartCoroutine(renderConn());
    }

    IEnumerator renderConn()
    {
        Vector2Int past = new Vector2Int(-1, -1);
        int i;
        while (_game == null)
        {
            yield return new WaitForSecondsRealtime(2.0f);
        }
        while (true)
        {
            if (_game != null)
            {
                for (i = 0; i < transform.childCount; i++)
                {
                    Transform child = transform.GetChild(i);
                    if (child.name == "fCon")
                        DestroyImmediate(child.gameObject);
                }

                foreach (KeyValuePair<eColors, Stack<Vector2Int>> flow in _game.flows)
                {
                    past = new Vector2Int(-1, -1);
                    foreach (Vector2Int element in flow.Value)
                    {
                        if (past.x > -1)
                        {
                            if (past.y == element.y)
                            {
                                createTile("fCon",
                                        new Vector3((Math.Min(past.x, element.x) + 1) * _game.escala - xHalfValue,
                                            (past.y + 0.5f) * _game.escala - yHalfValue, 5),
                                        _sprites[(int)eSprite.Horizontal], _game.escala)
                                    .GetComponent<SpriteRenderer>().color = definedColors[(int)flow.Key - 1];
                            }
                            else if (past.x == element.x)
                            {
                                createTile("fCon",
                                        new Vector3((past.x + 0.5f) * _game.escala - xHalfValue,
                                            (Math.Min(past.y, element.y) + 1) * _game.escala - yHalfValue, 5),
                                        _sprites[(int)eSprite.Vertical], _game.escala)
                                    .GetComponent<SpriteRenderer>().color = definedColors[(int)flow.Key - 1];
                            }
                        }

                        past = element;
                    }
                }
                // if (i < x - 1)
                // {
                //     hConnGrip[i + j * (x - 1)] =
                //         createTile(
                //                 new Vector3((i + 1) * _game.escala - xHalfValue,
                //                     (j + 0.5f) * _game.escala - yHalfValue, 6), _sprites[0], _game.escala)
                //             .GetComponent<SpriteRenderer>();
                // }
                //
                // if (j < y - 1)
                // {
                //     vConnGrip[i + j * (x - 1)] =
                //         createTile(
                //                 new Vector3((i + 0.5f) * _game.escala - xHalfValue,
                //                     (j + 1) * _game.escala - yHalfValue, 6), _sprites[0], _game.escala)
                //             .GetComponent<SpriteRenderer>();
                // }

                yield return new WaitForEndOfFrame();
            }
        }
    }

    private void LateUpdate() //updating grids
    {
        if (_game != null)
        {
            int i, j;
            for (i = 0; i < _game.x; i++)
            {
                for (j = 0; j < _game.y; j++)
                {
                    if (_game.grid[i + j * _game.x] != eColors.None)
                    {
                        cellGrid[i + j * _game.x].sprite = _sprites[(int)eSprite.Center];
                        cellGrid[i + j * _game.x].color = definedColors[(int)_game.grid[i + j * _game.x] - 1];
                    }
                    else
                    {
                        cellGrid[i + j * _game.x].sprite = _sprites[(int)eSprite.None];
                    }
                }
            }
        }
    }

    void Update()
    {
        // Otra idea 
        
        /*

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
           // Golpeo 
           
        }
        else
        {
            
        }
        */


        mousePosition = Input.mousePosition;
        if (_game != null)
        {
            gridCoord = _game.getMouseGrid(2 * ortosize * aspect * (mousePosition.x / Screen.width - 0.5f),
                2 * ortosize * (mousePosition.y / Screen.height - 0.5f));
            if (Input.GetMouseButtonDown(0))
                _game.selectCell(gridCoord.x, gridCoord.y);
            else if (Input.GetMouseButtonUp(0))
            {
                _game.deselectCell();
            }
            else
            {
                _game.check(gridCoord.x, gridCoord.y);
            }

            int c = 0;
            foreach (var VARIABLE in _game.grid)
            {
                if (VARIABLE != eColors.None)
                    c++;
            }
        }
    }
    //Cosas ajenas que no debería de tocar ya que no es mi código

    #region CosasAjenas

    public class Pair<T, U>
    {
        //Porque no hay pairs en C#? Es por eso que Java tiene más en ICPC que C#.
        public Pair()
        {
        }

        public Pair(T first, U second)
        {
            this.First = first;
            this.Second = second;
        }

        public T First { get; set; }
        public U Second { get; set; }
    };

    #endregion CosasAjenas
}