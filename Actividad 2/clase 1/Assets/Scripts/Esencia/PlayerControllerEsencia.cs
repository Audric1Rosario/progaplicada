using System.Collections; 
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerEsencia : MonoBehaviour
{
    // Start is called before the first frame update
    const float Y_MIN_LIMIT = -4.5f;
    const float Y_MAX_LIMIT = 4.5f;
    public Vector3 MovementSpeed = new Vector3(0, 1);
    private Vector3 _deltaPos;
    private ScoreController score;
    // Update is called once per frame
    private void Awake()
    {
        score = GameObject.Find("GlobalScriptsText").GetComponent<ScoreController>();
    }
    void Update()
    {
        _deltaPos = MovementSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
        gameObject.transform.Translate(_deltaPos);
        gameObject.transform.position = new Vector3(gameObject.transform.position.x,
                                                    Mathf.Clamp(gameObject.transform.position.y, Y_MIN_LIMIT, Y_MAX_LIMIT),
                                                    gameObject.transform.position.z);        
    }

    private void OnTriggerEnter(Collider other)
    {
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
        }
        Destroy(other.gameObject);
    }
}
