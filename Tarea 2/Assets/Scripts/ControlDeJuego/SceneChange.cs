using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{

    public string toChange;
    Vector3 origTam;
    Vector3 origPos;
    void Start()
    {
        origTam = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
        origPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
    }

    private void OnMouseExit()
    {
        gameObject.transform.localScale = origTam;
        gameObject.transform.position = origPos;
    }

    private void OnMouseEnter()
    {
        gameObject.transform.localScale *= 1.2f;
        gameObject.transform.position = origPos;
    }

    private void OnMouseDown()
    {
        if (!toChange.Equals("Exit"))
            SceneManager.LoadScene(toChange);
        else
            Application.Quit();
    }
}
