using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollController : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 _scollingSpeed = new Vector3(1, 1);
    private Vector3 _currentPosition = new Vector3();
    private MeshRenderer _renderer;
    void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _currentPosition += _scollingSpeed * Time.deltaTime;
        _renderer.material.mainTextureOffset = _currentPosition;
    }
}
