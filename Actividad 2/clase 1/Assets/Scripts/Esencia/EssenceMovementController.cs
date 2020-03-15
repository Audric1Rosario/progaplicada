using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssenceMovementController : MonoBehaviour
{
    // Start is called before the first frame update
    public float _XAcceleration = -9.8f;
    private float _XCurrentSpeed = 0;
    private Vector3 _deltaPos;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _deltaPos = new Vector3(_XCurrentSpeed * Time.deltaTime + (_XAcceleration * Mathf.Pow(Time.deltaTime, 2.0f) * 0.5f) , 0, 0);
        gameObject.transform.Translate(_deltaPos);
        _XCurrentSpeed += _XAcceleration * Time.deltaTime;
    }
}
