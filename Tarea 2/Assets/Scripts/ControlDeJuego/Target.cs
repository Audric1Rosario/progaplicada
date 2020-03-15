using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    // Start is called before the first frame update

    GameSetTarget summitPoints;
    const float value = 20f;
    public float result = 1f;
    void Start()
    {
        summitPoints = GameObject.Find("GameSetText").GetComponent<GameSetTarget>();
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine("Autodestruction");        
    }

    IEnumerator Autodestruction()
    {
        yield return new WaitForSeconds(0.5f);
        summitPoints.reduceAndAdd(result * value);
        Destroy(gameObject);
    }
}
