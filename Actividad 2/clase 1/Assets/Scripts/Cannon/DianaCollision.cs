using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DianaCollision : MonoBehaviour
{
    DianaInstantiator _diana;
    private void Start()
    {
        _diana = GameObject.Find("GlobalScriptsText").GetComponent<DianaInstantiator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            _diana.deleteDiana();
            Destroy(other.gameObject);
            AudioManager.Instance.PlaySoundEffect(AudioManager.SoundEffect.Capture);
            StartCoroutine(destroyObject());
        }
    }

    IEnumerator destroyObject()
    {
        yield return new WaitForSeconds(0.5f);
        
        // Eliminar objeto
        Destroy(gameObject);
    }
}
