using UnityEngine;
using System.Collections;
using Actors;

public class HitboxScript : MonoBehaviour {
	
	public 	GameObject 	attackParticles;
	public	float		lifetime = 1.0f;
	public 	float	 	yOffset = 0.0f;
	
	private Enemy	source;
	private int 	damage = 0;
	private float	timer = 0.0f;
	//private bool	justHit = false;
	//private GameObject player;
	
	/*
	void Update(){
		
		if ((this.timer <= this.lifetime) && this.justHit && (this.player != null)){
			Vector3 knockbackDirection 	= this.source.gameObject.transform.forward;
			Vector3 launchDirection		= new Vector3(0.0f, 1.0f, 0.0f);
			Vector3.Normalize(knockbackDirection);
			knockbackDirection *= 2.0f;
			launchDirection *= 2.0f;
			
			Debug.Log("Knockback Direction: " + (knockbackDirection + launchDirection));
			this.player.GetComponent<PlayerCharacter>().Knockback(knockbackDirection + launchDirection);
			this.timer += Time.deltaTime;
		}
		else{
			this.player = null;
			this.justHit = false;
		}
	}
	*/
	
	public int Damage{
		get{ return damage; }
		set{ this.damage = value; }
	}
	
	public Enemy Source{
		get{ return source; }
		set{ this.source = value; }
	}
	
	void OnTriggerEnter(Collider other){
		GameObject player = other.gameObject;
		if (player.tag == "Player"){
			//justHit = true;
			//this.player = player;
			this.collider.enabled = false;
			this.renderer.enabled = false;
			
			Vector3 knockbackDirection 	= new Vector3(this.source.gameObject.transform.forward.x, 0.0f, 0.0f);
			//Vector3 knockbackDirection 	= new Vector3(player.transform.forward.x * -1.0f, 0.0f, 0.0f);
			Vector3 launchDirection		= new Vector3(0.0f, 1.0f, 0.0f);
			Vector3.Normalize(knockbackDirection);
			knockbackDirection *= this.source.KnockbackStrength;
			launchDirection *= 0.0f;
			
			//Debug.Log("Knockback Direction: " + (knockbackDirection + launchDirection));
			player.GetComponent<PlayerCharacter>().Knockback = knockbackDirection + launchDirection;
			//this.timer += Time.deltaTime;
			
			GameData.LoseHp(player, damage);
			
			// Create Particles
			Vector3 playerPosition = other.collider.transform.position;
			GameObject newParticle = (GameObject)GameObject.Instantiate(attackParticles, new Vector3(playerPosition.x, this.transform.position.y + yOffset, playerPosition.z), Quaternion.identity);
			newParticle.transform.parent = player.transform;
			GameObject.Destroy(newParticle, lifetime);
		}
	}
}
