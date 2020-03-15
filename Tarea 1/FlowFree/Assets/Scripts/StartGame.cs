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
        if (Nivel == 4)
            flowfree.loadLevel(new[] { 6, 6, 3, 4, 2, 1, 5, 0, 4, 4, 4, 2, 2, 3, 5, 5, 2, 2});
        if (Nivel == 5)
            flowfree.loadLevel(new[] { 7, 7, 0, 6, 3, 5, 2, 5, 0, 4, 4, 5, 2, 3, 0, 1, 5, 5, 1, 1, 5, 2, 6, 2, 0, 0, 5, 1, 2, 1 });
        if (Nivel == 6)
            flowfree.loadLevel(new[] { 8, 8, 1, 7, 2, 4, 6, 5, 1, 6, 1, 5, 4, 6, 6, 7, 4, 5, 6, 3, 4, 4, 5, 3, 6, 2, 1, 1, 6, 1, 0, 3, 3, 4 });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
