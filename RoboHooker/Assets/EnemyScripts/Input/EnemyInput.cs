using UnityEngine;
using System.Collections.Generic;
using FSM;
using Actors;

[RequireComponent (typeof(CharacterController))]

public class EnemyInput : MonoBehaviour
{
	public GameObject 	patrolPoint1;
	public GameObject 	patrolPoint2;
	public GameObject	hitbox;
	public bool			isStatic		= false;
	public float		moveSpeed 		= 2.0f;
	public float 		turnSpeed 		= 5.0f;
	public float 		agroRange 		= 0.0f; 
	public float		patrolPauseTime = 3.0f;
	public float		attackTime		= 1.0f;
	public float		attackLength	= 1.0f;
	public float		attackRange		= 1.0f;
	public int			damage			= 10;
	public int			maxHP			= 100;
	public int			currentHP		= 100;
	
	private GameObject	hooker;
	private CharacterController	controller;
	private Dictionary<string, object> attributes;
	private bool		isFalling = false;
	private	Enemy 		enemy;
	
	void Start(){
		hooker = GameObject.Find("Hooker");
		currentHP = 100;
		
		// Turn off hitbox and patrol point rendering
		patrolPoint1.renderer.enabled = false;
		patrolPoint2.renderer.enabled = false;
		hitbox.renderer.enabled = false;
		hitbox.collider.enabled = false;
		hitbox.gameObject.GetComponent<HitboxScript>().Damage = damage;
		Physics.IgnoreCollision(hitbox.collider, this.collider);
		
		controller = this.GetComponent<CharacterController>();
		
		attributes = new Dictionary<string, object>();
		attributes["gameObject"] = this.gameObject;
		attributes["controller"] = controller;
		attributes["hooker"] = hooker;
		attributes["moveSpeed"] = moveSpeed;
		attributes["turnSpeed"] = turnSpeed;
		attributes["patrolPoint1"] = patrolPoint1;
		attributes["patrolPoint2"] = patrolPoint2;
		attributes["patrolTarget"] = patrolPoint1;
		attributes["targetPosition"] = controller.transform.position;
		attributes["targetRotation"] = controller.transform.rotation;
		attributes["actionTimer"] = 0.0f;
		attributes["attackTime"] = attackTime;
		attributes["attackLength"] = attackLength;
		attributes["attackRange"] = attackRange;
		attributes["damage"] = damage;
		attributes["maxHP"] = maxHP;
		attributes["currentHP"] = currentHP;
		attributes["animation"] = gameObject.animation;
		
		attributes["hasAttacked"] = false;
		attributes["hitbox"] = hitbox;
		
		enemy = new Enemy(attributes); // Pass the dictionary to the enemy
	}
	
	void Update (){
		// Update User Attributes
		this.currentHP 	= this.enemy.CurrentHP;
		this.maxHP		= this.enemy.MaxHP;
		enemy.MoveSpeed = moveSpeed;
		enemy.TurnSpeed = turnSpeed;
		enemy.Position 	= enemy.controller.transform.position;
		
		Vector3 targetPos 	= hooker.transform.position;
		float 	targetDist 	= (targetPos - enemy.Position).magnitude;
		Vector3 targetDir 	= (targetPos - enemy.Position).normalized;
		
		float range = ((CharacterController)hooker.collider).radius + enemy.controller.radius + enemy.AttackRange;
		
		if (!isStatic){
			// Move or Attack to player based on agro range
			if (targetDist > range && targetDist <= agroRange ){	// Hooker is within agro range, move toward the Hooker
				//targetPos = new Vector3(hooker.transform.position.x, this.transform.position.y, hooker.transform.position.z);
				enemy.MoveToPosition(targetPos);
			}
			else{
				enemy.Patrol(patrolPauseTime);		// Just Patrol
			}
			applyGravity();
		}
		
		if (targetDist < range){		// Within attack range, attack
			enemy.Attack(targetPos);
		}
		
		enemy.Update();
	}
	
	/*
	void OnTriggerEnter(Collider other){
		GameObject gobj = other.gameObject;
		if (gobj.tag == "Hitbox"){
			Debug.Log("Hit by Attack.");
		}
	}
	*/
	
	void applyGravity()
    {
        if (!enemy.controller.isGrounded)
        {
            enemy.controller.Move(new Vector3(0.0f, (Physics.gravity.y * Time.deltaTime), 0.0f));
        }
	}
}