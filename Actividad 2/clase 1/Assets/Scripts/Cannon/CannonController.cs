using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    // Start is called before the first frame update
    private float temp;
    public float SpeedCannonX = 40f;
    public float triggerSpeed = 10f, triggerAngle;    
    const float limitMinX = -8.0f, limitMaxX = 0;
    private Vector3 deltaPos, mousePosition;
    public GameObject CannonBallPrefab;
    public GameObject ProgressBar;
    DianaInstantiator _diana;
    Rigidbody _myRigid;
    public float power;
    void Start()
    {
        _myRigid = GetComponent<Rigidbody>();
        _diana = GameObject.Find("GlobalScriptsText").GetComponent<DianaInstantiator>();
        deltaPos = new Vector3();
    }

    // Update is called once per frame
    void Update()
    {
        if (_diana.myGame.Equals(eCannonState.playing))
        {
            // change position
            deltaPos.y = 0;
            deltaPos.z = 0;
            deltaPos.x = Input.GetAxis("Horizontal") * Time.deltaTime * SpeedCannonX;
            /*gameObject.transform.Translate(deltaPos);
            gameObject.transform.position = new Vector3(Mathf.Clamp(gameObject.transform.position.x, limitMinX, limitMaxX),
                                              gameObject.transform.position.y,
                                              gameObject.transform.position.z);*/
            if (Input.GetAxis("Horizontal") != 0)
                _myRigid.velocity = new Vector3(Input.GetAxis("Horizontal") * 100f * Time.deltaTime, _myRigid.velocity.y);
            // Otra forma 
            // GetComponent<Rigidbody>().AddForce(new Vector3() * 40f * Time.deltaTime, 0f);            
            if (Input.GetButtonDown("Jump"))
                GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, 10f);
            // Otra forma
            // GetComponent<Rigidbody>().AddForce(0f, new Vector3() * 40f * Time.deltaTime);            
            // Calculating angle
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(Input.mousePosition);
            Debug.Log(mousePosition);
            deltaPos.y = mousePosition.y - gameObject.transform.position.y;
            deltaPos.x = mousePosition.x - gameObject.transform.position.x;

            if (deltaPos.x < 0)
                triggerAngle = Mathf.PI / 2;
            else if (deltaPos.y < 0)
                triggerAngle = 0;
            else
                triggerAngle = Mathf.Atan(deltaPos.y / deltaPos.x);

            if (Input.GetButton("Fire1"))
            {
                /*
                Instantiate(CannonBallPrefab, gameObject.transform.position, Quaternion.identity).GetComponent<CannonBallBehaviour>
                    ().Shoot(triggerSpeed, triggerAngle);*/
                power = Mathf.PingPong(Time.time, 1);
                ProgressBar.GetComponent<ProgressBar>().BarValue = power * 100f;
            }
            else if (Input.GetButtonUp("Fire1"))
            {
                Instantiate(CannonBallPrefab, gameObject.transform.position, Quaternion.identity).GetComponent<CannonBallBehaviour>
                    ().Shoot(triggerSpeed * power, triggerAngle);
            }
            //temp = Mathf.PingPong(Time.time, 5f);

            Debug.Log("Radianes: " + triggerAngle.ToString());
            Debug.Log("Grados: " + (triggerAngle * Mathf.Rad2Deg).ToString());

        }
    }
}
