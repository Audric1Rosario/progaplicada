using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetControl : MonoBehaviour
{
    // Start is called before the first frame update
    BirdControl flappy;
    void Start()
    {
        flappy = GameObject.Find("FlappyBird").GetComponent<BirdControl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
