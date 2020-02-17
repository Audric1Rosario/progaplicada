using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecreaseLife : MonoBehaviour
{
    // Start is called before the first frame update
    private GameController game;
    void Start()
    {
        game = GameObject.Find("CubeGenerator").GetComponent<GameController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        game.decreaseLife();
    }
}
