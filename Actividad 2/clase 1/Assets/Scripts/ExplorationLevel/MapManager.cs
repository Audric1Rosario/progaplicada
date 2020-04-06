using System.Collections;
using System.Collections.Generic;
using System;
using System.Xml;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    // Start is called before the first frame update
    public string currentLevel;
    public GameObject GrassA, GrassB, RoadC, RoadD, RoadE, 
        RoadF, RoadG, RoadH, RoadI, RoadJ, TreeK;
    public GameObject playerPrefab, morahPrefab, lionelPrefab, enemyPrefab;
    Transform cellsContainer, charactersContainer;
    XmlDocument xmlDoc;
    GameObject currentPrefab = null;
    /*Cargando el mapa*/
    XmlNode currentNode;
    XmlNodeList nodesList;

    // This is used in platflormer.
    [Header("Platformer")]
    public GameObject PlatformerGrass1, PlatformerGrass2, PlatformerGrass3, PlatformerGrass4, PlatformerGrass5,
                      PlatformerGrass6, PlatformerGrass7, PlatformerGrass8, PlatformerGrass9, PlatformerGrass10,
                      PlatformerGrass11, PlatformerGrass12, PlatformerGrass13, PlatformerGrass14, PlatformerGrass15,
                      PlatformerGrass16, PlatformerGrass17;

    private void Awake()
    {
        if (currentLevel.Equals("Level1")) { 
            cellsContainer = GameObject.Find("Celdas").transform;
            charactersContainer = GameObject.Find("Characters").transform;
        }
    }
    void Start()
    {
        xmlDoc = new XmlDocument();
        // -Para cargar el mapa
        //Debug.Log(Resources.Load<TextAsset>("Level1.xml").text);
        if (currentLevel.Equals("Level1"))
        {
            xmlDoc.LoadXml(Resources.Load<TextAsset>("Level1").text);
            LoadMap();
        }
        else { 
            xmlDoc.LoadXml(Resources.Load<TextAsset>("PlatformerLevel1").text);
            LoadPlatformerMap();
        }
        //Debug.Log(xmlDoc.InnerXml);
        
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

    void LoadPlatformerMap()
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
                        currentPrefab = PlatformerGrass1;
                        break;
                    case 'B':
                        currentPrefab = PlatformerGrass2;
                        break;
                    case 'C':
                        currentPrefab = PlatformerGrass3;
                        break;
                    case 'D':
                        currentPrefab = PlatformerGrass4;
                        break;
                    case 'E':
                        currentPrefab = PlatformerGrass5;
                        break;
                    case 'F':
                        currentPrefab = PlatformerGrass6;
                        break;
                    case 'G':
                        currentPrefab = PlatformerGrass7;
                        break;
                    case 'H':
                        currentPrefab = PlatformerGrass8;
                        break;
                    case 'I':
                        currentPrefab = PlatformerGrass9;
                        break;
                    case 'J':
                        currentPrefab = PlatformerGrass10;
                        break;
                    case 'K':
                        currentPrefab = PlatformerGrass11;
                        break;
                    case 'L':
                        currentPrefab = PlatformerGrass12;
                        break;
                    case 'M':
                        currentPrefab = PlatformerGrass13;
                        break;
                    case 'N':
                        currentPrefab = PlatformerGrass14;
                        break;
                    case 'O':
                        currentPrefab = PlatformerGrass15;
                        break;
                    case 'P':
                        currentPrefab = PlatformerGrass16;
                        break;
                    case 'Q':
                        currentPrefab = PlatformerGrass17;
                        break;
                    case 'R':
                        currentPrefab = PlatformerGrass1;
                        break;
                    default:
                        currentPrefab = null;
                        break;
                }
                if (currentPrefab != null)
                {
                    currentPrefab = Instantiate(currentPrefab, new Vector3(j, -i, 0), Quaternion.identity);
                    currentPrefab.transform.SetParent(cellsContainer);
                }
            }
        }
        //LoadCharacters();
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
