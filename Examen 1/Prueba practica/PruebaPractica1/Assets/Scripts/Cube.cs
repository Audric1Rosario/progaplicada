using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    // Start is called before the first frame update
    public bool deletable;
    void Start()
    {        
        deletable = false;
    }
    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        MeshRenderer color = gameObject.GetComponent<MeshRenderer>();
        MeshRenderer rcolor = other.GetComponent<MeshRenderer>();
        if (rcolor != null)
        {
            if (rcolor.material.color.Equals(color.material.color))
                deletable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        MeshRenderer color = gameObject.GetComponent<MeshRenderer>();
        MeshRenderer rcolor = other.GetComponent<MeshRenderer>();
        if (rcolor != null)
        {
            //if (rcolor.material.Equals(color.material))
               // deletable = false;
        }
    }
}
