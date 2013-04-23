using UnityEngine;
using System.Collections.Generic;
using FSM;
using Actors;

[RequireComponent (typeof (CharacterController))]

public class EnemyInput : MonoBehaviour
{
	public GameObject 	patrolPoint1;
	public GameObject 	patrolPoint2;
	public GameObject	player;
	public float		moveSpeed 		= 2.0f;
	public float 		turnSpeed 		= 5.0f;
	public float 		agroRange 		= 0.0f; 
	public float		patrolPauseTime = 5.0f;
	
	private Enemy 		enemy;
	private GameObject	patrolTarget;
	private Dictionary<string, object> attributes;
	
	void Start(){
		patrolPoint1.renderer.enabled = false;
		patrolPoint2.renderer.enabled = false;
		patrolTarget = patrolPoint1;
		
		attributes = new Dictionary<string, object>();
		attributes["gameObject"] = this.gameObject;
		attributes["controller"] = this.GetComponent<CharacterController>();
		attributes["player"] = player;
		attributes["patrolPoint1"] = patrolPoint1;
		attributes["patrolPoint2"] = patrolPoint2;
		attributes["patrolTarget"] = patrolPoint1;
		attributes["position"] = this.transform.position;
		attributes["rotation"] = this.transform.rotation;
		attributes["targetPosition"] = this.transform.position;
		attributes["targetRotation"] = this.transform.rotation;
		attributes["actionTimer"] = 0.0f;
		
		enemy = new Enemy(attributes); // Pass the dictionary to the enemy
	}
	
	void Update (){
		// Update User Attributes
		enemy.MoveSpeed = moveSpeed;
		enemy.TurnSpeed = turnSpeed;
		enemy.Position = this.transform.position;
		
		//Vector3 currentPos = this.transform.position;
		Vector3 targetPos = player.transform.position;
		float targetDist = (targetPos - enemy.Position).magnitude;
		Vector3 targetDir = (targetPos - enemy.Position).normalized;
		
		//if(attributes.Health > 0){
		float range = ((CharacterController)player.collider).radius + enemy.controller.radius + 0.5f;
		//}
		
		// Move or Attack to player based on agro range
		if (targetDist > range && targetDist <= agroRange ){
			enemy.MoveToPosition(targetPos);
		}
		else{
			enemy.Patrol(patrolPauseTime);
		}
		
		Debug.Log("Player Position: " + targetDist);
		Debug.Log("AgroRange: " + agroRange);
		
		if (targetDist < range){				
			enemy.Attack();
		}
		
		enemy.Update();
	}
	
	void OnCollisionEnter(Collision collision){
		GameObject gobj = collision.gameObject;
		if (gobj.tag == "Projectile"){
			
		}
	}
}