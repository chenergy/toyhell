using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
	public bool isProjectile;
	public bool canKnockback;
	public string weaponName;
	public GameObject model;
	public GameObject hitbox;
	public GameObject attackParticles;
	public float attackDelay = 0.0f;
	public float attackSpeed = 1.0f;
	public int damage = 10;
	public int attacks = 1;
	
	// Use this for initialization
	void Start ()
	{
		if (hitbox.GetComponent<WeaponHitbox>()){
			hitbox.GetComponent<WeaponHitbox>().damage = this.damage;
			hitbox.GetComponent<WeaponHitbox>().attackParticles = this.attackParticles;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	}
	
	public void Attack(){
		hitbox.collider.enabled = true;
		//hitbox.renderer.enabled = true;
	}
}

