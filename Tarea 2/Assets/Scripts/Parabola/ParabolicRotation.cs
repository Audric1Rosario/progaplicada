using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolicRotation : MonoBehaviour
{
    // Start is called before the first frame update
    // Update is called once per frame
    Rigidbody rigid;

	// Efecto de sonido.
	public AudioClip targetHit;
	public string myTag = "None";
	// Efectos de colision.
	bool collisionOccurred;
	[SerializeField]
	float life;
	float life_loss;
	public Color color = Color.white;

	// Efecto de rotacion.
	[SerializeField]
    Vector3 vel;
	[SerializeField]
	float angleZ, angleY;
	private void Start()
    {
        rigid = GetComponent<Rigidbody>();
		float duration = 5f;			// Segundos
		life_loss = 1f / duration;
		life = 1f;
	}

    void FixedUpdate()
    {
        if (rigid.velocity.magnitude > 0.5f)
        {
            //transform.rotation = Quaternion.LookRotation(rigid.velocity, new Vector3(0, 0, 1));
            vel = rigid.velocity;
			// Calcular la rotacion con el arctan de la velocidad en y y en x.
	
			if (vel.x > 0)
			{
				angleZ = Mathf.Atan2(vel.y, vel.x) * Mathf.Rad2Deg;
				angleY = Mathf.Atan2(vel.z, vel.x) * Mathf.Rad2Deg;
				
				transform.eulerAngles = new Vector3(0, -angleY, angleZ);
			} else
			{
				angleZ = Mathf.Atan2(vel.y, vel.x) * Mathf.Rad2Deg;
				angleY = Mathf.Atan2(vel.z, vel.x) * Mathf.Rad2Deg;
				transform.eulerAngles = new Vector3(180, -angleY, angleZ + 180);
			}
				
            
        }

		if (collisionOccurred)
		{
			// Hacer desaparecer flecha...
			life -= Time.deltaTime * life_loss;
			GetComponent<Renderer>().material.color = new Color(color.r, color.g, color.b, life);
			if (life <= 0f)
			{
				Destroy(gameObject);
			}
		}

	}

	void OnCollisionEnter(Collision other)
	{
		float y;
		int actScore = 0;

		if (collisionOccurred)
		{
			transform.position = new Vector3(other.transform.position.x, transform.position.y, transform.position.z);
			return;
		}
		if (!other.gameObject.tag.Contains("Flecha"))
		{
			GetComponent<AudioSource>().PlayOneShot(targetHit);
			rigid.velocity = Vector3.zero;
			rigid.isKinematic = true;
			rigid.constraints = RigidbodyConstraints.FreezeAll;
			collisionOccurred = true;
		}

		if (other.gameObject.tag.Equals("target"))
		{
		
			y = other.contacts[0].point.y;			 
			y = y - other.transform.position.y;		
			// Blanco...
			if (y < 1.48557f && y > -1.48691f)
				actScore = 10;
			// ... Negro ...
			if (y < 1.36906f && y > -1.45483f)
				actScore = 20;
			// ...  Azul  ...
			if (y < 0.9470826f && y > -1.021649f)
				actScore = 30;
			// ... or Rojo ...
			if (y < 0.6095f && y > -0.760f)
				actScore = 40;
			// ... En el centro !!!
			if (y < 0.34f && y > -0.53f)
				actScore = 50;
			
			other.gameObject.GetComponent<Target>().result = 1f + (float)actScore / 50f;
		}

	}
}
