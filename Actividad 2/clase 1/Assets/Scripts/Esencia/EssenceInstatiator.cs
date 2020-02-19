using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EssenceType
{
    Blue = 0,
    Green,
    Orange,
    Purple,
    Red,
    Yellow,

}

public class EssenceInstatiator : MonoBehaviour
{
    // Start is called before the first frame update
    private float _lastSpawnedTime;
    private Dictionary<EssenceType, GameObject> EssencePrefabs;
    public float _spawnDeltaTime = 0.5f;
    public GameObject BlueEssencePrefab,
                      GreenEssencePrefab,
                      OrangeEssencePrefab,
                      PurpleEssencePrefab,
                      RedEssencePrefab,
                      YellowEssencePrefab;
    void Start()
    {
        _lastSpawnedTime = Time.time;
        EssencePrefabs = new Dictionary<EssenceType, GameObject> {
            { EssenceType.Blue,     BlueEssencePrefab   },
            { EssenceType.Green,    GreenEssencePrefab  },
            { EssenceType.Orange,   OrangeEssencePrefab },
            { EssenceType.Purple,   PurpleEssencePrefab },
            { EssenceType.Red,      RedEssencePrefab    },
            { EssenceType.Yellow,   YellowEssencePrefab },
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - _lastSpawnedTime > _spawnDeltaTime)
        {
            _lastSpawnedTime = Time.time;
            InstantiateEssence();            
        }       
    }

    void InstantiateEssence()
    {
        Instantiate(EssencePrefabs[(EssenceType)Random.Range(0, 6)], new Vector3(10, Random.Range(-4, 4), 0), Quaternion.identity);
    }
}
