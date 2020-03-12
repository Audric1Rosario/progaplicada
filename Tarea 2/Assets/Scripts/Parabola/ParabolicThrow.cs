using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolicThrow : MonoBehaviour
{
    // Start is called before the first frame update
    // Proyectil
    public GameObject Projectile;
    public GameObject PowerBar;
    GameObject _projectile;
    // Velocidades y magnitudes fisicas.
    public float ShootSpeedMax = 20f;
//    [SerializeField]
    float _shootSpeed, _throwAngle, _xSpeed, _ySpeed;
    // Detalles
    CharacterStatus status;
    Animator _animator;
    public AudioClip shooted;
    // Limitaciones
    public bool canAttack = true;
    public bool attackDone = false;
    float _power = 1f, _proportion = 0.05f;
    Vector3 _mouse, _deltaPos;
    void Start()
    {
        status = gameObject.GetComponentInParent<CharacterStatus>();
        _deltaPos = new Vector3();
        _animator = GetComponentInParent<Animator>();
        _shootSpeed =  ShootSpeedMax * _power;
        PowerBar.GetComponent<ProgressBar>().BarValue = _power * 100f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && canAttack && !status.isDeath)
        {
            StartCoroutine("Attack");
        }

        // Para incrementar potencia
        if (Input.GetButton("Fire2") && !status.isDeath)
        {
            _power = Mathf.PingPong(Time.time, 1f);
            PowerBar.GetComponent<ProgressBar>().BarValue = _power * 100f;
            //UpdatePower();
        }

        UpdatePhysics();
    }

    void UpdatePhysics()
    {
        _mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _deltaPos.y = _mouse.y - gameObject.transform.position.y;
        _deltaPos.x = _mouse.x - gameObject.transform.position.x;

        if (_deltaPos.x < 0)
            _throwAngle = Mathf.PI / 2;
        else if (_deltaPos.y < 0)
            _throwAngle = 2 * Mathf.PI + Mathf.Atan(_deltaPos.y / _deltaPos.x);
        else
            _throwAngle = Mathf.Atan(_deltaPos.y / _deltaPos.x);
        
        _shootSpeed = ShootSpeedMax * _power;
    }

    public Vector3 GetRelativeSpeed()
    {
        return new Vector3(_shootSpeed, _throwAngle);
    }

    IEnumerator Attack()
    {
        // Ataque basado en el poder del jugador.
        canAttack = false;
        
        if (_shootSpeed > 0)
        {
            // Coordenadas del cursor.            
            _animator.SetBool("isAttacking", true);
            yield return new WaitForSeconds(0.7f);
            _animator.SetBool("isAttacking", false);

            //Instanciar proyectil.
            _projectile = Instantiate(Projectile, transform.TransformPoint(Vector3.zero), 
                Quaternion.Euler(new Vector3(0, 0, _throwAngle * Mathf.Rad2Deg)));
            _projectile.GetComponent<ParabolicRotation>().myTag = gameObject.tag;       // Guardar el tag del lanzador (esto es para permitir fuego amigo)
            _projectile.GetComponent<Rigidbody>().velocity = new Vector3(_shootSpeed * Mathf.Cos(_throwAngle), _shootSpeed * Mathf.Sin(_throwAngle));
            GetComponent<AudioSource>().PlayOneShot(shooted);
            yield return new WaitForSeconds(0.1f);
            attackDone = true;
        }
        // Volver a poder atacar.
        canAttack = true;
    }
}
