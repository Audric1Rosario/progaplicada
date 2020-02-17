using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    Flowfree gameComponent;
    void Start()
    {
        gameComponent = GameObject.Find("Game").GetComponent<Flowfree>();        
        StartCoroutine("CheckScore");
    }

    IEnumerator CheckScore()
    {
        while (!gameComponent.isWin())
        {
            gameObject.GetComponent<TextMesh>().text = gameComponent.getActions().ToString();
            yield return new WaitForSeconds(0.2f);
        }
        gameObject.GetComponent<TextMesh>().text = gameComponent.getActions().ToString();
        yield return null;
    }
}
