using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLoaderController : MonoBehaviour
{
    // Start is called before the first frame update
    Text _uitext;
    private void Start()
    {
        _uitext = GameObject.Find("TravelZoneText").GetComponent<Text>();
    }

    private void OnTriggerEnter(Collider other)
    {
        AudioManager.Instance.PlaySoundEffect(AudioManager.SoundEffect.Explote);
        switch (gameObject.name)
        {
            // Gravilota
            case "Game1Loader":
                _uitext.text = "Gravilota";
                break;

            // ExplorationLevel
            case "Game2Loader":
                _uitext.text = "ExplorationLevel";
                break;
            
            // Esencia
            case "Game3Loader":
                _uitext.text = "Esencia";
                break;

            // Cannon
            case "Game4Loader":
                _uitext.text = "Cannon";
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        AudioManager.Instance.PlaySoundEffect(AudioManager.SoundEffect.Capture);
        _uitext.text = "";
    }
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
