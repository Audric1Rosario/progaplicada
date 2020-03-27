using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdControl : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isAlive;
    public float jumpSpeed = 10f;
    public float YAcceleration = 15f;
    float YCurrentSpeed = 0f;
    Rigidbody rb;
    Animator myAnim;
    GameSetControl game;
    Vector3 deltaPos;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isAlive = true;        
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isAlive) // Si ha muerto.
            return;

        // Para saltar...
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myAnim.SetBool("isSpace", true);
            // Dar impulso a jugador...
            YCurrentSpeed = jumpSpeed; // Va a tomar una velocidad y el motor se encarga de bajarlo.
        }
        else
            myAnim.SetBool("isSpace", false);

        deltaPos = new Vector3(0f, YCurrentSpeed * Time.deltaTime - (YAcceleration * Mathf.Pow(Time.deltaTime, 2.0f) * 0.5f), 0);
        gameObject.transform.Translate(deltaPos);
        YCurrentSpeed -= YAcceleration * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        isAlive = false;
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }
}
