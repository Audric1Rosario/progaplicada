using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoaderController : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetButtonDown("Submit"))
        {
            switch (gameObject.name)
            {
                case "Game1Loader":
                    SceneManager.LoadScene(0);
                    break;
                case "Game2Loader":
                    SceneManager.LoadScene(2);
                    break;
                case "Game3Loader":
                    SceneManager.LoadScene(3);
                    break;
                case "Game4Loader":
                    SceneManager.LoadScene(4);
                    break;
            }
        }
    }
}
