using UnityEngine;
using System.Collections;

public class WeaponHitbox : MonoBehaviour
{
	public int damage = 10;
	public float lifetime = 1.0f;
	public GameObject attackParticles;
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	
	void OnTriggerEnter(Collider other){
		GameObject enemy = other.gameObject;
		if (enemy.tag == "Enemy"){
			this.collider.enabled = false;
			this.renderer.enabled = false;
			/*
			Vector3 knockbackDirection 	= new Vector3(this.source.gameObject.transform.forward.x, 0.0f, 0.0f);
			Vector3 launchDirection		= new Vector3(0.0f, 1.0f, 0.0f);
			Vector3.Normalize(knockbackDirection);
			knockbackDirection *= this.source.KnockbackStrength;
			launchDirection *= 0.0f;
			
			//Debug.Log("Knockback Direction: " + (knockbackDirection + launchDirection));
			//player.GetComponent<PlayerCharacter>().Knockback = knockbackDirection + launchDirection;
			player.GetComponent<PlayerInput>().Knockback = knockbackDirection + launchDirection;
			player.GetComponent<PlayerInput>().Hurt();
			*/
			enemy.transform.parent.gameObject.GetComponent<EnemyInput>().DamageEnemy(this.damage);
			
			// Create Particles
			Vector3 enemyPosition = other.collider.transform.position;
			GameObject newParticle = (GameObject)GameObject.Instantiate(attackParticles, new Vector3(enemyPosition.x, this.transform.position.y, enemyPosition.z), Quaternion.identity);
			//newParticle.transform.parent = player.transform;
			GameObject.Destroy(newParticle, lifetime);
		}
	}
}

