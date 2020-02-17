using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RegScene : MonoBehaviour
{
    // Start is called before the first frame update
    public string toChange;
    private WebService sendScore; 
    private Vector3 origTam;
    private Vector3 origPos;
    private Flowfree gameComponent;
    void Start()
    {
        gameComponent = GameObject.Find("Game").GetComponent<Flowfree>();
        sendScore = GameObject.Find("SendWeb").GetComponent<WebService>();
        origTam = gameObject.transform.localScale;
        origPos = gameObject.transform.localPosition;
        gameObject.transform.localPosition = new Vector3(50, 50, 50);
        gameObject.GetComponent<TextMesh>().text = "";
        StartCoroutine("VerifyWinner");
    }

    IEnumerator VerifyWinner()
    {
        while (!gameComponent.isWin())
        {
            yield return new WaitForSeconds(0.2f);
        }
        gameObject.SetActive(true);
        gameObject.transform.localPosition = origPos;
        gameObject.GetComponent<TextMesh>().text = "You Win!";
        yield return null;
    }
    private void OnMouseExit()
    {
        gameObject.transform.localScale = origTam;
    }

    private void OnMouseEnter()
    {
        gameObject.transform.localScale *= 1.2f;
    }

    private void OnMouseDown()
    {
        StartCoroutine(sendScore.SendWebRequest("Unity Player", gameComponent.getActions()));
        SceneManager.LoadScene(toChange);
    }
}
