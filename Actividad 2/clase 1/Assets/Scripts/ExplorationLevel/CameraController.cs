using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject _player;
    void Start()
    {
        StartCoroutine("LateStart");
    }

    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(0.5f);
        _player = GameObject.FindGameObjectWithTag("Player");
        gameObject.transform.SetParent(_player.transform);
        gameObject.transform.localPosition = new Vector3(0, 0, -10);
    }
}
