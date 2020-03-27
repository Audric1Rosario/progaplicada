using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerExplorationLevel : MonoBehaviour
{
    Vector3 _movementSpeed = new Vector3(5, 5), _runningSpeed = new Vector3(15, 15);
    Vector3 _currentSpeed = new Vector3();
    Vector3 _newPosition = new Vector3();
    Rigidbody _rigidbody;
    Animator _animator;
    SpriteRenderer _renderer;
    bool _isEnemy;
    const float _ENEMYMOVEDISTANCE = 5f, _ENEMYATTACKDISTANCE = 2f, _ENEMYRUNNINGSPEED = 10f;
    GameObject _player;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
        _player = GameObject.FindGameObjectWithTag("Player");
        Physics.IgnoreLayerCollision(8, 10);

    }

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _isEnemy = gameObject.tag == "Enemy";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!_isEnemy)
        {   // Controles del jugador
            _currentSpeed.x = Input.GetAxis("Horizontal") * (Input.GetButton("Fire3") ? _runningSpeed.x : _movementSpeed.x);
            _currentSpeed.y = Input.GetAxis("Vertical") * (Input.GetButton("Fire3") ? _runningSpeed.y : _movementSpeed.x);
            // Atacar
            _animator.SetBool("attack", Input.GetButton("Fire1"));
            // Bloquear
            _animator.SetBool("block", Input.GetButton("Fire2"));
            // Velocidad
            _animator.SetFloat("velocity", _currentSpeed.magnitude);
            // Desplazarse
            if (_currentSpeed != Vector3.zero)
            {
                //_rigidbody.MovePosition(transform.position + _currentSpeed * Time.deltaTime);
                _rigidbody.velocity = _currentSpeed;
            }
            _renderer.flipX = _currentSpeed.x < -0.1f;
        } else
        {
            if (Vector3.Distance(gameObject.transform.position, _player.transform.position) < _ENEMYMOVEDISTANCE)
            {
                // Moverse hacia el jugador
                _newPosition = Vector3.MoveTowards(gameObject.transform.position, _player.transform.position,
                    _ENEMYRUNNINGSPEED * Time.deltaTime);
                // Flip
                _renderer.flipX = _newPosition.x < transform.position.x;
                // Velocidad
                _animator.SetFloat("velocity", _ENEMYRUNNINGSPEED);
                // Desplazarse
                _rigidbody.MovePosition(_newPosition);
                //_rigidbody.velocity = _newPosition;
            } else
                _animator.SetFloat("velocity", 0f);
            
            // Atacar
            if (Vector3.Distance(gameObject.transform.position, _player.transform.position) < _ENEMYATTACKDISTANCE)
            {
                _animator.SetBool("attack", true);
            }
            else
                _animator.SetBool("attack", false);
           
        }
    }
}
