using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolicThrowTest : MonoBehaviour
{
    private Vector3 _mouse;
    public Animator attackMotions;
    private Camera mainCamera;
    public GameObject ProjectilePrefab;
    private GameObject _projectile;
    public float xVelo = 1f, yVelo = 1f;
    // Variables con tiro parabolico actualizado
    public float timeTillHit = 1f;
    private bool canAttack;
    
    /*
     * Variables de tiro parabolico
    public float heightOffset;
    public float arrowSpeed = 1.0f;
    public float gravity = 9.81f;*/


    void Start() {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        _mouse = new Vector3();
        canAttack = true;
    }

    void Update() {
        if (Input.GetButtonDown("Fire1") && canAttack)
        {
            StartCoroutine("Attack");
        }
    }

    IEnumerator Attack()
    {
        canAttack = false;
        _mouse = Input.mousePosition;
        _mouse = mainCamera.ScreenToWorldPoint(_mouse);
        float xdistance = _mouse.x - gameObject.transform.position.x;
        float ydistance = _mouse.y - gameObject.transform.position.y;

        // Forma vieja de hacerlo
        float throwAngle = Mathf.Atan((ydistance + 4.905f)/xdistance); 
        //float throwAngle = Mathf.Atan((ydistance + 4.905f * (timeTillHit * timeTillHit)) / xdistance);
        // Forma vieja de hacerlo.
        float totalVelo = xdistance / Mathf.Cos(throwAngle);
        //float totalVelo = xdistance / (Mathf.Cos(throwAngle) * timeTillHit);
        float xVelo, yVelo;
        xVelo = totalVelo * Mathf.Cos(throwAngle);
        yVelo = totalVelo * Mathf.Sin(throwAngle);

        attackMotions.SetBool("isAttacking", true);
        yield return new WaitForSeconds(0.2f);
        _projectile = Instantiate(ProjectilePrefab, gameObject.transform.position,
                        Quaternion.Euler(new Vector3(0, 0, throwAngle * Mathf.Rad2Deg)));
        _projectile.GetComponent<Rigidbody>().velocity = new Vector3(xVelo, yVelo, 0);
        attackMotions.SetBool("isAttacking", false);
        yield return new WaitForSeconds(0.8f);

        canAttack = true;
    }
    
}
