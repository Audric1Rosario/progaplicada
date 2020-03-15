using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 _currentSpeed;
    bool _shooted;

    // Update is called once per frame
    void Update()
    {
        
        if (!_shooted)
            return;
        gameObject.transform.Translate(_currentSpeed * Time.deltaTime + Physics.gravity * Mathf.Pow(Time.deltaTime, 2) / 2);
        _currentSpeed += Physics.gravity * Time.deltaTime;

    }
    
    public void Shoot (float triggerSpeed, float triggerAngle)
    {        
        _currentSpeed = new Vector3(triggerSpeed * Mathf.Cos(triggerAngle), triggerSpeed * Mathf.Sin(triggerAngle));
        _shooted = true;
    }
}
