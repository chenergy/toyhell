using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ToyHell;

public class WeaponPickupInput : MonoBehaviour
{
	public GameObject	gobj;
	public WeaponName 	weaponName = WeaponName.None;
	
	private float radians = 0.0f;
	
	void Start(){
		this.collider.enabled = true;
		this.collider.isTrigger = true;
	}
	
	void Update(){
		this.radians += Time.deltaTime;
		
		if ( radians >= (Mathf.PI * 2) ){
			radians = 0.0f;
		}
		
		this.gobj.animation.Play();
		this.gobj.transform.position += new Vector3( 0.0f, Mathf.Sin(radians) * Time.deltaTime * 0.25f, 0.0f);
	}
	
	void OnTriggerEnter(Collider other){
		if (other.CompareTag("Player")){
			Fighter fighter = other.GetComponent<FighterInput>().fighter;
			fighter.weaponPickup = this.gameObject;
		}
	}
	
	void OnTriggerExit(Collider other){
		if (other.CompareTag("Player")){
			Fighter fighter = other.GetComponent<FighterInput>().fighter;
			fighter.weaponPickup = null;
		}
	}
}

