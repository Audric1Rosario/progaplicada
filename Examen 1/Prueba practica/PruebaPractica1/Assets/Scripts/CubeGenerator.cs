using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public Material red;
    public Material blue;
    public Material green;
    public GameObject cube;
    public bool go;
    private enum eColor
    {
        Red,
        Green,
        Blue
    };
    void Start()
    {
        go = false;
        StartCoroutine("generateCubeRandom");
    }

    // Update is called once per frame
    /*void Update()
    {
        
    }*/

    IEnumerator generateCubeRandom()
    {
        Debug.Log("Voy funcionando");

        while (!go)
        {
            for (int i = 0; i < 3; i++)
            {
                Debug.Log("es bucle funcionando");
                eColor e = (eColor) Random.Range((float)eColor.Red, (float)eColor.Blue + 1);
                switch (e)
                {
                    case eColor.Red:
                        cube.GetComponent<MeshRenderer>().material = red;
                        break;
                    case eColor.Green:
                        cube.GetComponent<MeshRenderer>().material = green;
                        break;
                    case eColor.Blue:
                        cube.GetComponent<MeshRenderer>().material = blue;
                        break;
                }
                Debug.Log("Estoy funcionando");
                Instantiate(cube, new Vector3((int)Random.Range(-12.0f, -2.0f), (int)Random.Range(4.0f, 6.0f), 0), Quaternion.identity);
            }

            yield return new WaitForSeconds(2.0f);
        }
    }
}
