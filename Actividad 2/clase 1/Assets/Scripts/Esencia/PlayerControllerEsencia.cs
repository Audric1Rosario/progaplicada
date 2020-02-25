using System.Collections; 
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerEsencia : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMesh PlayerLifesText;
    private GameObject GameOverText;
    private AudioManager AudioManagerText;
    public bool isGameOver = false;
    const float Y_MIN_LIMIT = -4.5f;
    const float Y_MAX_LIMIT = 4.5f;
    public Vector3 MovementSpeed = new Vector3(0, 1);
    private Vector3 _deltaPos;
    private int _lives = 3;
    private ScoreController score;
    // Update is called once per frame
    private void Awake()
    {
        AudioManagerText = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        GameOverText = GameObject.Find("GameOverText");
        GameOverText.SetActive(false);
        score = GameObject.Find("GlobalScriptsText").GetComponent<ScoreController>();
    }

    private void Start()
    {
        PlayerLifesText.text = _lives.ToString();
    }
    void Update()
    {
        if (!isGameOver) { 
            _deltaPos = MovementSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
            gameObject.transform.Translate(_deltaPos);
            gameObject.transform.position = new Vector3(gameObject.transform.position.x,
                                                        Mathf.Clamp(gameObject.transform.position.y, Y_MIN_LIMIT, Y_MAX_LIMIT),
                                                        gameObject.transform.position.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isGameOver)
            return;
        switch (other.gameObject.tag)
        {
            case "Blue":
                score.IncrementScore(EssenceType.Blue);
                break;
            case "Green":
                score.IncrementScore(EssenceType.Green);
                break;
            case "Orange":
                score.IncrementScore(EssenceType.Orange);
                break;
            case "Purple":
                score.IncrementScore(EssenceType.Purple);
                break;
            case "Red":
                score.IncrementScore(EssenceType.Red);
                break;
            case "Yellow":
                score.IncrementScore(EssenceType.Yellow);
                break;
            case "Enemy":
                _lives = _lives - 1 > 0 ? _lives - 1 : 0;
                PlayerLifesText.text = _lives.ToString();
                if (_lives <= 0)
                {
                    // GameOver
                    isGameOver = true;
                    GameOverText.SetActive(true);
                }
                AudioManagerText.PlaySoundEffect(AudioManager.SoundEffect.Explote);
                return;
        }
        AudioManagerText.PlaySoundEffect(AudioManager.SoundEffect.Capture);
        Destroy(other.gameObject);
    }
}
 