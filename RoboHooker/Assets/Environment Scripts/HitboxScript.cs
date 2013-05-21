using UnityEngine;
using System.Collections;

public class HitboxScript : MonoBehaviour {
	
	public 	GameObject 	attackParticles;
	public	float		lifetime = 1.0f;
	public 	Vector3 	offset = new Vector3(0.0f, 2.0f, 0.0f);
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
			GameObject newParticle = (GameObject)GameObject.Instantiate(attackParticles, other.collider.transform.position + offset, Quaternion.identity);
			GameObject.Destroy(newParticle, lifetime);
		}
	}
}
