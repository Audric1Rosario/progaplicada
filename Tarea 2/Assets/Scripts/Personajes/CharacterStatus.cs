using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatus : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject HealthBar;
    public bool isDeath = false;
    bool isInGround;
    public AudioClip deathSound;
    Animator _animator;
    ParabolicRotation arrow;
    public float life = 100f;
    float _lifeproportion;
    void Start()
    {
        // Poner el evento de la camara al canvas.
        Canvas setCanvas = gameObject.transform.Find("Canvas").GetComponent<Canvas>();
        setCanvas.renderMode = RenderMode.WorldSpace;
        setCanvas.worldCamera = Camera.main;

        // Verificar proporciones de vida.
        _lifeproportion = life;
        _animator = GetComponent<Animator>();
        HealthBar.GetComponent<ProgressBar>().BarValue = (life/_lifeproportion) * 100f;
    }

    public void ChangeLife(float newlife)
    {
        life = newlife;
        _lifeproportion = life;
        HealthBar.GetComponent<ProgressBar>().BarValue = (life / _lifeproportion) * 100f;
    }

    public float LostLife()
    {
        return _lifeproportion - life;
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (isDeath)
            return;

        bool isDamage = false;
        float damage = 0f;
        switch (other.gameObject.tag)
        {
            case "Flecha":
                damage = 10f;
                break;

            case "FlechaExp":
                damage = 25f;
                break;

            case "FlechaGuiada":
                damage = 15f;
                break;

            default:
                arrow = null;
                break;
        }

        if (other.gameObject.tag.Contains("Flecha"))
        {
            arrow = other.gameObject.GetComponent<ParabolicRotation>();
            if (!arrow.myTag.Equals(gameObject.tag) && !arrow.myTag.Equals("None"))
            {
                isDamage = true;
            }          
        }

        if (isDamage)
        {
            life -= damage;
            HealthBar.GetComponent<ProgressBar>().BarValue = (life / _lifeproportion) * 100f < 0f ? 
                                                               0f : (life / _lifeproportion) * 100f;
            _animator.SetBool("isHit", true);
            StartCoroutine("isGettingAnArrow");

            if (life <= 0f)
            {
                isDeath = true;
                StartCoroutine("dying");
            }
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isInGround)
            return;
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        isInGround = true;
    }

    IEnumerator dying()
    {
        _animator.SetBool("isAlive", false);
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<AudioSource>().PlayOneShot(deathSound);
    }

    IEnumerator isGettingAnArrow() {
        yield return new WaitForSeconds(0.25f);
        _animator.SetBool("isHit", false);
    }
}
