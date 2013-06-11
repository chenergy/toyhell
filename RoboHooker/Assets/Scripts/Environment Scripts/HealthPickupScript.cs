using UnityEngine;
using System.Collections;

public class HealthPickupScript : MonoBehaviour
{
	public int healAmount = 25;
	public float particleLifetime = 1.0f;
	public GameObject particles;
	
	// Use this for initialization
	void Start ()
	{
		this.collider.isTrigger = true;
	}
	
	void OnTriggerEnter(Collider other){
		GameObject player = other.gameObject;
		if (player.tag == "Player"){
			GameData.GainHp(player, healAmount);
			
			// Create Particles
			GameObject newParticle = (GameObject)GameObject.Instantiate(particles, this.transform.position, Quaternion.identity);
			GameObject.Destroy(newParticle, particleLifetime);
			
			// Destroy Self
			GameObject.Destroy(this.gameObject);
		}
	}
}

