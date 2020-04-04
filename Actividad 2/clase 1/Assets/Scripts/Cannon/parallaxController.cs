using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallaxController : MonoBehaviour
{
    MeshRenderer _renderer;
    float _maxSpeed = 0.02f, _currentSpeed;
    const float _MAXDISTANCE = 20f;
    void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();    
    }

    private void Start()
    {
        _currentSpeed = (1f - gameObject.transform.position.z / _MAXDISTANCE) * _maxSpeed;
    }

    // Update is called once per frame
    void Update()
    {        
        _renderer.material.mainTextureOffset = new Vector2(Camera.main.transform.position.x * _currentSpeed, 0f);
    }
}
