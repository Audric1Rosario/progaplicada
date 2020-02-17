using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChange : MonoBehaviour
{
    // Start is called before the first frame update
    public string toChange;
    private Vector3 origTam;
    void Start()
    {
        origTam = gameObject.transform.localScale;
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
        if (!toChange.Equals("Exit"))
            SceneManager.LoadScene(toChange);
        else
            Application.Quit();
    }
}
