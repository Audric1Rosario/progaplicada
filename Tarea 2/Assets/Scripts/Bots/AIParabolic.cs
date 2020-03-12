using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ParabolicData
{
    public float moduleVelocity;
    public float InitialXVelocity;
    public float InitialYVelocity;
    public float ThrowAngle;
    public float maxHeight;
    public float maxDistance;
}

public class AIParabolic : MonoBehaviour
{
    // Start is called before the first frame update
    public bool trigger;
    public GameObject Projectile;
    public int ValuesRange = 10;
    public int difficulty = 0; // 0 -> 3
    static float[] rngdifficulty = new float[] { 0.25f, 0.5f, 0.7f, 0.9f }; // Rango de exito que tiene.
    GameObject _projectile;
    GameObject _player;
    CharacterStatus status;
    // Efectos.
    Animator _animator;
    public AudioClip shooted;

    // Parabola parameters.
    Vector3 _playerPoint;
    Vector3 _maxObstacleLine;
    [SerializeField]
    Vector3 _lastvelocity;
    public bool canAttack = true;

    public static float[] Rngdifficulty => rngdifficulty;

    void Start()
    {
        status = gameObject.GetComponentInParent<CharacterStatus>();
        _animator = gameObject.GetComponentInParent<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player");

        if (_player.Equals(null))
            canAttack = false;
                                           //     (x1, y) ----------- (x2, y)
        _maxObstacleLine = FindMaxPoint(); // De la forma (X1, Y, X2) 
        // Para bajarle vida al jugador.
        updateHPdueDifficulty();
    }

    // Update is called once per frame
    void Update()
    {
        if (trigger)
        {
            trigger = false;
            if (canAttack && !status.isDeath)
                StartCoroutine("Attack");
        }
    }

    void updateHPdueDifficulty()
    {
        // Cambiar la vida segun la dificultad.
        switch (difficulty)
        {
            case 0:
                status.ChangeLife(40f);
                break;
            case 1:
                status.ChangeLife(60f);
                break;
            case 2:
                status.ChangeLife(80f);
                break;
            case 3:
                status.ChangeLife(120f);
                break;

            default:
                status.ChangeLife(50f);
                break;
        }
    }

    ParabolicData[] FindData(Vector3 LineSection)
    {
        List<ParabolicData> data = new List<ParabolicData>();
        // Encontrar rango de alturas del jugador.
        float[] heights = new float[ValuesRange];
        float hplayer = _player.transform.localScale.y;
        int successfulThrows = (int) Mathf.Ceil((float)ValuesRange * rngdifficulty[difficulty]);
        int unsuccessfulThrows = ValuesRange - successfulThrows;

        // Disparos exitosos primero.
        for (int i = 0; i < successfulThrows; i++)
        {
            heights[i] = _player.transform.position.y - hplayer / 2 + 
                (i+1)*(hplayer / successfulThrows);
        }

        // Disparos no exitosos. (mas atras de la cabeza del jugador).
        for (int i = 0; i < unsuccessfulThrows; i++)
        {
            // Saber aleatoriamente si ira mas arriba o mas abajo.
            if (Random.Range(-ValuesRange, ValuesRange) < 0)
            {
                // va mas abajo.
                heights[successfulThrows + i] = _player.transform.position.y -
                    0.1f * (i + 1) * (hplayer / unsuccessfulThrows);
            } else
            {
                // va mas arriba.
                heights[successfulThrows + i] = _player.transform.position.y +
                     0.1f * (i + 1) * (hplayer / unsuccessfulThrows);
            }
        }

        // Altura maxima del obstaculo...
        float hmin;
        if (LineSection.x != _player.transform.position.x || LineSection.y != _player.transform.position.y)
            hmin = LineSection.y + Mathf.Abs(LineSection.x - LineSection.y) + 5f;
        else
            hmin = _player.transform.position.y;

        // Empezar con los calculos.

        float xdistance = Mathf.Abs(_playerPoint.x - gameObject.transform.position.x);

        // Caja de pandora. Por favor escuche busque una explicacion alusiva al movimiento parabolico no simetrico.
        ParabolicData newElement = new ParabolicData();
        float tab, tc, vf, vfc;
        foreach (float h in heights)
        {
            if (h >= hmin || hmin == _player.transform.position.y) // No es necesario hacer un calculo especial 
            {
                // Calcular como si fuese a disparar hacia un punto.
                newElement.maxHeight = h - gameObject.transform.position.y;
                newElement.maxDistance = xdistance;
                newElement.ThrowAngle = Mathf.Atan((newElement.maxHeight - Physics.gravity.y / 2) / xdistance);
                // newElement.ThrowAngle = Mathf.Atan((newElement.maxHeight - Physics.gravity.y / 2) / xdistance) + Mathf.PI;
                /*if (_playerPoint.x < gameObject.transform.position.x)
                    newElement.ThrowAngle = Mathf.Atan((newElement.maxHeight - Physics.gravity.y / 2) / xdistance) + Mathf.PI;
                else
                    newElement.ThrowAngle = Mathf.Atan((newElement.maxHeight - Physics.gravity.y / 2) / xdistance);*/
                newElement.moduleVelocity = xdistance / Mathf.Cos(newElement.ThrowAngle);
                newElement.InitialXVelocity = newElement.moduleVelocity * Mathf.Cos(newElement.ThrowAngle);
                newElement.InitialYVelocity = newElement.moduleVelocity * Mathf.Sin(newElement.ThrowAngle);

            } else // Es necesario crear una parabola con hmax > hmin
            {   // Calcular tiro parabolico especial:
                // 3 puntos conocidos, 1 altura maxima, 1 distancia maxima, parabola no simetrica.
                newElement.maxHeight = Mathf.Abs(hmin + Random.Range(0f, 3f) - gameObject.transform.position.y);
                newElement.maxDistance = xdistance;
                vf = Mathf.Sqrt(-2 * Physics.gravity.y * newElement.maxHeight);
                tab = vf / (-Physics.gravity.y);
                vfc = Mathf.Sqrt(-2 * Physics.gravity.y * (newElement.maxHeight - h)); 
                //vfc = Mathf.Sqrt(-Physics.gravity.y * newElement.maxHeight);
                tc = vfc / (-Physics.gravity.y);
                newElement.InitialXVelocity = newElement.maxDistance / (tc + tab);
                newElement.InitialYVelocity = vf;
                //newElement.ThrowAngle = Mathf.Atan(newElement.InitialYVelocity / newElement.InitialXVelocity);
                if (_playerPoint.x < gameObject.transform.position.x)
                    newElement.ThrowAngle = Mathf.Atan(newElement.InitialYVelocity / newElement.InitialXVelocity) + Mathf.PI;
                else
                    newElement.ThrowAngle = Mathf.Atan(newElement.InitialYVelocity / newElement.InitialXVelocity);
                newElement.moduleVelocity = Mathf.Sqrt(Mathf.Pow(newElement.InitialXVelocity, 2f) + Mathf.Pow(newElement.InitialYVelocity, 2f));
            }
            data.Add(newElement);
        }

        return data.ToArray();
    }

    Vector3 FindMaxPoint()
    {
        _playerPoint = new Vector3(_player.GetComponentInParent<Transform>().position.x,
                                   _player.GetComponentInParent<Transform>().position.y);
        Vector3 point = new Vector3(_playerPoint.x, _playerPoint.y);        
        GameObject[] listObjects = GameObject.FindGameObjectsWithTag("Obstacle");
        GameObject maxO = null;
        // Encontrar punto de altura maxima entre los objetos.
        foreach (GameObject obstacle in listObjects)
        {           
            // Verificar que el objeto obstaculo este en el medio de los elementos.
            if (_playerPoint.x < transform.position.x && _playerPoint.x < obstacle.transform.position.x ||
                transform.position.x < _playerPoint.x && transform.position.x < obstacle.transform.position.x)
            {
                // Medir la altura.
                if (point.y < (obstacle.transform.position.y +
                    obstacle.transform.lossyScale.y / 2))
                {
                    point.x = obstacle.GetComponent<Transform>().position.x;
                    point.y = obstacle.GetComponent<Transform>().position.y;
                    maxO = obstacle;
                }
            }
        }

        if (maxO != null)
        {
            // Devolver de la siguiente manera: 
            // (Posicion en X del objeto - escala en x / 2, Posicion en Y mayor, 
            //  Posicion en X del objeto + escala en x / 2)
            point.x = maxO.transform.position.x - maxO.transform.lossyScale.x / 2;
            point.y = maxO.transform.position.y + maxO.transform.lossyScale.y / 2;
            point.z = maxO.transform.position.x + maxO.transform.lossyScale.x / 2;
        }
        
        // Si tiene la misma posicion del player, significa que no hay nada mas alto que el objeto.
        // Caso facil de calcular...
        return point;
    }

    IEnumerator Attack()
    {
        _playerPoint = _player.GetComponent<Transform>().position;

        // Obtener datos con los que va a disparar.
        ParabolicData randomData = FindData(FindMaxPoint())[Random.Range(0, ValuesRange)];
        
        if (_playerPoint.x < gameObject.transform.position.x)
        {
            randomData.InitialXVelocity = -Mathf.Abs(randomData.InitialXVelocity);
        } else
            randomData.InitialXVelocity = Mathf.Abs(randomData.InitialXVelocity);

        // Comprobar si el disparo sera exitoso o no
        if (Random.Range(0f, 1f) > rngdifficulty[difficulty])
        {
            randomData.InitialYVelocity = 0f;
        }

        _animator.SetBool("isAttacking", true);
        yield return new WaitForSeconds(0.7f);

        _projectile = Instantiate(Projectile, transform.TransformPoint(Vector3.zero),
                Quaternion.Euler(new Vector3(0, 0, randomData.ThrowAngle * Mathf.Rad2Deg)));
        _projectile.GetComponent<Rigidbody>().velocity = new Vector3(randomData.InitialXVelocity, 
            randomData.InitialYVelocity, 0);
        _projectile.GetComponent<ParabolicRotation>().myTag = gameObject.tag;

        _lastvelocity = new Vector3(randomData.InitialXVelocity,
            randomData.InitialYVelocity, 0);

        GetComponent<AudioSource>().PlayOneShot(shooted);

        yield return new WaitForSeconds(0.1f);
        _animator.SetBool("isAttacking", false);
    }
}
