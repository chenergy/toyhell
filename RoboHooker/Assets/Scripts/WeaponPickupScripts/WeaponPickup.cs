using UnityEngine;
using System.Collections;

public class WeaponPickup : MonoBehaviour {
	public GameObject weapon;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerStay(Collider other){
		if (other.gameObject.tag == "Player"){
			GameObject player = other.gameObject;
			PlayerInput input = player.GetComponent<PlayerInput>();
			
			if (Input.GetButtonDown(input.buttons.m_LeftEquipKey)){
				
			}
		}
	}
}
