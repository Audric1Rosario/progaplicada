using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolicThrowTest : MonoBehaviour
{
    private Vector3 _mouse;
    
    public GameObject ProjectilePrefab;
    public AudioClip Shoot;
    GameObject _projectile;
    Animator _attackMotions;

    // Variables con tiro parabolico
    float _xVelo, _yVelo, _throwAngle;
    bool _canAttack;     //Para limitar las veces que dispara.

    void Start() {
        _attackMotions = GetComponentInParent<Animator>();        
        _mouse = new Vector3();
        _canAttack = true;
    }

    void Update() {
        if (Input.GetButtonDown("Fire1") && _canAttack)
        {
            StartCoroutine("Attack");
        }
    }

    IEnumerator Attack()
    {
        _canAttack = false;
        _mouse = Input.mousePosition;
        _mouse = Camera.main.ScreenToWorldPoint(_mouse);
        float xdistance = _mouse.x - gameObject.transform.position.x;
        float ydistance = _mouse.y - gameObject.transform.position.y;

        // Encontrar angulo.
        if (xdistance < 0)
            _throwAngle = Mathf.PI;
        else
            _throwAngle = Mathf.Atan((ydistance - Physics.gravity.y / 2) / xdistance);

        // Encontrar modulo de la velocidad.
        float totalVelo = xdistance / Mathf.Cos(_throwAngle);

        _xVelo = totalVelo * Mathf.Cos(_throwAngle);
        _yVelo = totalVelo * Mathf.Sin(_throwAngle);

        // Reproducir animacion
        _attackMotions.SetBool("isAttacking", true);
        yield return new WaitForSeconds(0.7f);
        _attackMotions.SetBool("isAttacking", false);

        // Instanciar proyectil
        _projectile = Instantiate(ProjectilePrefab, transform.TransformPoint(Vector3.zero),
                        Quaternion.Euler(new Vector3(0, 0, _throwAngle * Mathf.Rad2Deg)));
        _projectile.GetComponent<Rigidbody>().velocity = new Vector3(_xVelo, _yVelo, 0);
        GetComponent<AudioSource>().PlayOneShot(Shoot);

        yield return new WaitForSeconds(0.1f);

        // Puede volver a atacar.
        _canAttack = true;
    }

    public Vector3 speedVector()
    {
        return new Vector3(_xVelo, _yVelo);
    }
    
}
