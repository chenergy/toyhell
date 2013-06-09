using UnityEngine;
using System.Collections.Generic;
using FSM;
using Actors;

public class PlayerInput : MonoBehaviour
{
	public GameObject 	gobj;
	public GameObject	deathParts;
	public GameObject	socketedWeapon;
	public float		jumpPower			= 20.0f;
	public float		moveSpeed 			= 2.0f;
	public float 		turnSpeed 			= 5.0f;
	public float		attackSpeed			= 1.0f;
	public float		knockbackStrength	= 35.0f;
	public int			maxHP				= 100;
	public int			currentHP			= 100;
	public float		fadeTime			= 3.0f;
	
	private float		zPlane;
	private CharacterController	controller;
	private Dictionary<string, object> attributes;
	private	Player 		player;
	
	void Start(){
		zPlane = this.gameObject.transform.position.z;
		// Turn off rendering and colliders for hitbox and patrol point
		
		// Can Walk through characters
		
		controller = this.gobj.GetComponent<CharacterController>();
		attributes = new Dictionary<string, object>();
		
		#region attribute dictionary assignments
		attributes["gameObject"] = gobj;
		attributes["controller"] = controller;
		attributes["deathParts"] = deathParts;
		attributes["socketedWeapon"] = socketedWeapon;

		attributes["jumpPower"] = jumpPower;
		attributes["moveSpeed"] = moveSpeed;
		attributes["turnSpeed"] = turnSpeed;
		attributes["targetPosition"] = controller.transform.position;
		attributes["targetRotation"] = controller.transform.rotation;
		attributes["actionTimer"] = 0.0f;
		attributes["attackSpeed"] = attackSpeed;
		attributes["fadeTime"] = fadeTime;
		attributes["maxHP"] = maxHP;
		attributes["currentHP"] = currentHP;
		attributes["animation"] = this.gobj.animation;
		attributes["knockbackStrength"] = knockbackStrength;
		attributes["fadeTime"] = fadeTime;
		
		attributes["hasAttacked"] = false;
		#endregion
		player = new Player(attributes); // Pass the dictionary to the enemy

	}
	
	void Update (){
		if (this.gobj != null){
			/*
			// Update User Attributes
			this.currentHP 	= this.player.CurrentHP;
			this.maxHP		= this.player.MaxHP;
			player.MoveSpeed = moveSpeed;
			player.TurnSpeed = turnSpeed;
			player.Position  = this.controller.transform.position;
	
			if (Input.GetKey(KeyCode.Tab)){ // Test case for enemy death
				this.DamageEnemy(10);
			}
			*/
			player.Update();
			/*
			if(this.transform.position.z != this.zPlane){
				this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.zPlane);
			}
			*/
		}
	}
	/*
	public void KillEnemy(){
		this.enemy.CurrentHP = 0;
	}
	
	public void DamageEnemy(int damage){
		this.enemy.CurrentHP -= damage;
		this.enemy.Hurt();
	}
	*/
}