using UnityEngine;
using System.Collections;

public class HitboxScript : MonoBehaviour {
	
	private int damage;
	private float attackLength;
	private GameObject parent;
	
	void Start(){
		parent = this.transform.parent.gameObject;
		damage = parent.GetComponent<EnemyInput>().damage;
		Physics.IgnoreCollision(parent.collider, this.collider);
	}
	
	void Update () {
	}
	
	public int Damage{
		get{ return damage; }
	}
}
