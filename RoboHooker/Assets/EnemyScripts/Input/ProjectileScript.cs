using UnityEngine;
using System.Collections;
using Actors;

public class ProjectileScript : MonoBehaviour {
	
	public GameObject attackParticles;
	public float particleLifetime = 1.0f;
	private int damage = 0;
	private Vector3 direction;
	private float speed;
	
	private Enemy source;
	
	void Start(){
	}
	
	void Update () {
		this.collider.transform.position += direction * speed;
		Debug.Log(direction);
		Debug.Log(speed);
	}
	
	public int Damage{
		get{ return damage; }
		set{ this.damage = value; }
	}
	
	public float Speed{
		get{ return speed; }
		set{ this.speed = value; }
	}
	
	public Vector3 Direction{
		get{ return direction; }
		set{ this.direction = value; }
	}
	
	public Enemy Source{
		get{ return source; }
		set{ this.source = value; }
	}
	
	void OnTriggerEnter(Collider other){
		GameObject player = other.gameObject;
		Vector3 playerPosition = player.transform.position;
		if (player.tag == "Player"){
			
			Vector3 knockbackDirection 	= new Vector3(this.source.gameObject.transform.forward.x, 0.0f, 0.0f);
			//Vector3 knockbackDirection 	= new Vector3(player.transform.forward.x * -1.0f, 0.0f, 0.0f);
			Vector3 launchDirection		= new Vector3(0.0f, 1.0f, 0.0f);
			Vector3.Normalize(knockbackDirection);
			knockbackDirection *= this.source.KnockbackStrength;
			launchDirection *= 0.0f;
			
			Debug.Log("Knockback Direction: " + (knockbackDirection + launchDirection));
			player.GetComponent<PlayerCharacter>().Knockback = knockbackDirection + launchDirection;
			
			GameData.LoseHp(player, damage);
			
			GameObject newParticle = (GameObject)GameObject.Instantiate(attackParticles, new Vector3(playerPosition.x, this.transform.position.y, playerPosition.z), Quaternion.identity);
			GameObject.Destroy(newParticle, particleLifetime);
			GameObject.Destroy(this.gameObject);
		}
	}
}
