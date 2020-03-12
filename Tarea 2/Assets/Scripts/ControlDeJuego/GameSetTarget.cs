using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetTarget : MonoBehaviour
{
    // Start is called before the first frame update
    public int level;
    public GameObject winSprite;
    public GameObject Player;
    public int numTargets;
    WebService summit;
    ParabolicThrow _player;
    TextMesh write;
    float points;
    bool check = true, isFinish;
    void Start()
    {
        isFinish = false;
        winSprite.SetActive(false);
        write = GetComponent<TextMesh>();
        points = 0f;
        write.text = ((int)points).ToString();
        _player = Player.GetComponentInChildren<ParabolicThrow>();
        points = 0f; // Los objetivos actualizaran este dato.

        // WebService
        summit = GetComponent<WebService>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isFinish)
        {
            isFinish = false;
            _player.canAttack = false;            
            winSprite.SetActive(true);
            // Enviar los datos.
            StartCoroutine(summit.SendWebRequest("Unity Player", level, (int)points));
        }  
    }

    public void reduceAndAdd(float p)
    {
        --numTargets;
        if (numTargets <= 0)
            isFinish = true;
        points += p;
        write.text = ((int)points).ToString();
    }

    //bool targetInScene()
    //{
    //    GameObject[] targets = GameObject.FindGameObjectsWithTag("target");
    //    return targets.Length > 0;
    //}

    //IEnumerator CheckifThereAreTargets()
    //{
    //    yield return new WaitForSeconds(1.5f);
    //    check = true;
    //    isFinish = targetInScene();
    //}
}
