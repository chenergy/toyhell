using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour {

	private int damage = 0;
	private Vector3 direction;
	private float speed;
	
	void Start(){
	}
	
	void Update () {
		this.collider.transform.position += direction * speed;
        if (!renderer.isVisible)
        {
            Debug.Log("clean up");
            Destroy(gameObject);
        }
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
	
	void OnTriggerEnter(Collider other){
		GameObject player = other.gameObject;
		if (player.tag == "Player"){
			GameData.LoseHp(player, damage);
			GameObject.Destroy(this.gameObject);
		}
	}
}
