using UnityEngine;
using System.Collections;

public class HitboxScript : MonoBehaviour {
	
	private int damage = 0;
	
	void Start(){
	}
	
	void Update () {
	}
	
	public int Damage{
		get{ return damage; }
		set{ this.damage = value; }
	}
	
	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player"){
			//Debug.Break();
			this.collider.enabled = false;
			this.renderer.enabled = false;
			GameData.LoseHp(other.gameObject, damage);
		}
	}
}
