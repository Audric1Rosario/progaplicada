using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Xml;

public class MapLoad : MonoBehaviour
{
    // Start is called before the first frame update
    XmlDocument xmlDoc;
    GameObject currentPrefab = null;
    /*Cargando el mapa*/
    XmlNode currentNode;
    XmlNodeList nodesList;
    void Start()
    {
        xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(Resources.Load<TextAsset>("Tubes").text);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
