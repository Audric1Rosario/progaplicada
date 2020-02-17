using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    // Start is called before the first frame update

    public int Nivel;
    void Start()
    {
        Flowfree flowfree = GameObject.Find("Game").GetComponent<Flowfree>();
        if (Nivel == 1)
            flowfree.loadLevel(new[] { 5, 5, 0, 2, 3, 4, 4, 4, 3, 3, 1, 1, 2, 3, 4, 0, 3, 1, 0, 1, 4, 1 });
        if (Nivel == 2)
            flowfree.loadLevel(new[] { 5, 5, 0, 3, 2, 0, 1, 3, 3, 3, 0, 1, 3, 2, 0, 0, 3, 1 });
        if (Nivel == 3)
            flowfree.loadLevel(new[] { 5, 5, 0, 4, 1, 0, 1, 1, 3, 4, 2, 0, 4, 1, 2, 1, 4, 4, 2, 2, 3, 3 });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
