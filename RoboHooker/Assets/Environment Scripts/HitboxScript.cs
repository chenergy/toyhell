using UnityEngine;
using System.Collections;

public class HitboxScript : MonoBehaviour {
	
	public 	GameObject 	attackParticles;
	public	float		lifetime = 1.0f;
	public 	float	 	yOffset = 0.5f;
	private int 		damage = 0;
	
	void Start(){
	}
	
	void Update () {
	}
	
	public int Damage{
		get{ return damage; }
		set{ this.damage = value; }
	}
	
	void OnTriggerEnter(Collider other){
		GameObject player = other.gameObject;
		if (player.tag == "Player"){
			//Debug.Break();
			this.collider.enabled = false;
			this.renderer.enabled = false;
			GameData.LoseHp(player, damage);
			// Create Particles
			Vector3 playerPosition = other.collider.transform.position;
			GameObject newParticle = (GameObject)GameObject.Instantiate(attackParticles, new Vector3(playerPosition.x, this.transform.position.y + yOffset, playerPosition.z), Quaternion.identity);
			GameObject.Destroy(newParticle, lifetime);
		}
	}
}
