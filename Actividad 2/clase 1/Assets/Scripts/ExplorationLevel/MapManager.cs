using System.Collections;
using System.Collections.Generic;
using System;
using System.Xml;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject GrassA, GrassB, RoadC, RoadD, RoadE, 
        RoadF, RoadG, RoadH, RoadI, RoadJ, TreeK;
    public GameObject playerPrefab, morahPrefab, lionelPrefab, enemyPrefab;
    Transform cellsContainer, charactersContainer;
    XmlDocument xmlDoc;
    GameObject currentPrefab = null;
    /*Cargando el mapa*/
    XmlNode currentNode;
    XmlNodeList nodesList;

    private void Awake()
    {
        cellsContainer = GameObject.Find("Celdas").transform;
        charactersContainer = GameObject.Find("Characters").transform;
    }
    void Start()
    {
        xmlDoc = new XmlDocument();
        // -Para cargar el mapa
        //Debug.Log(Resources.Load<TextAsset>("Level1.xml").text);
        xmlDoc.LoadXml(Resources.Load<TextAsset>("Level1").text);
        //Debug.Log(xmlDoc.InnerXml);
        LoadMap();
        // Physics.IgnoreCollider
    }

    
    void LoadMap()
    {
        currentPrefab = null;
        /*Cargando el mapa*/
        nodesList = xmlDoc.SelectNodes("//level/map/row");
        for (int i = 0; i < nodesList.Count; i++)
        {
            currentNode = nodesList[i];
            for (int j = 0; j < currentNode.InnerText.Length; j++)
            {
                switch (currentNode.InnerText[j])
                {
                    case 'A':
                        currentPrefab = GrassA;
                        break;
                    case 'B':
                        currentPrefab = GrassB;
                        break;
                    case 'C':
                        currentPrefab = RoadC;
                        break;
                    case 'D':
                        currentPrefab = RoadD;
                        break;
                    case 'E':
                        currentPrefab = RoadE;
                        break;
                    case 'F':
                        currentPrefab = RoadF;
                        break;
                    case 'G':
                        currentPrefab = RoadG;
                        break;
                    case 'H':
                        currentPrefab = RoadH;
                        break;
                    case 'I':
                        currentPrefab = RoadI;
                        break;
                    case 'J':
                        currentPrefab = RoadJ;
                        break;
                    case 'K':
                        currentPrefab = TreeK;
                        break;
                    default:
                        currentPrefab = GrassA;
                        break;
                }
                currentPrefab = Instantiate(currentPrefab, new Vector3(j, -i, 0), Quaternion.identity);
                currentPrefab.transform.SetParent(cellsContainer);
            }
        }
        LoadCharacters();
    }

    void LoadCharacters()
    {
        GameObject newElement;
        currentPrefab = null;
        nodesList = xmlDoc.SelectNodes("//level/characters/character");
        
        foreach (XmlNode currentElement in nodesList)
        {
            switch(currentElement.Attributes["prefabName"].Value)
            {
                case "Player":
                    currentPrefab = playerPrefab;  // En vez de tree seria un caracter
                    break;

                case "Morah":
                    currentPrefab = morahPrefab;
                    break;
                
                case "Lionel":
                    currentPrefab = lionelPrefab;
                    break;

                case "Enemy":
                    currentPrefab = enemyPrefab;
                    break;

                default:
                    currentPrefab = TreeK;
                    break;                
            }
            newElement = Instantiate(currentPrefab, new Vector3(Convert.ToSingle(currentElement.Attributes["posX"].Value),
                                                   -Convert.ToSingle(currentElement.Attributes["posY"].Value)),
                                                   Quaternion.identity);
            newElement.name = currentElement.Attributes["uniqueObjectName"].Value;
            newElement.transform.SetParent(charactersContainer);
        }
    }
}
