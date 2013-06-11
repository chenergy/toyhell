using UnityEngine;
using System.Collections;

public class WeaponPickup : MonoBehaviour {
	public GameObject gobj;
	public GameObject prefab;
	private float offsetY;
	// Use this for initialization
	void Start () {
		this.collider.isTrigger = true;
		this.offsetY = this.transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		this.gobj.transform.Rotate(new Vector3(0.0f, 1.0f, 0.0f) * Time.deltaTime);
		this.gobj.transform.position = new Vector3(this.gobj.transform.position.x, this.offsetY + Mathf.Sin(Time.frameCount * 0.05f), this.gobj.transform.position.z);
		
		if (this.gobj.animation){
			if (this.gobj.animation["Attack"]){
				this.gobj.animation.CrossFade("Attack");
			}
		}
	}
	
	void OnTriggerStay(Collider other){
		if (other.gameObject.tag == "Player"){
			GameObject player = other.gameObject;
			PlayerInput input = player.GetComponent<PlayerInput>();
			
			//if (Input.GetButtonDown(input.buttons.m_LeftEquipKey)){
			if (Input.GetKeyDown(input.SwapKey)){
				input.SwapWeapons(prefab);
				GameData.SaveWeapon(player, prefab);
				GameObject.Destroy(this.gameObject);
			}
		}
	}
}
