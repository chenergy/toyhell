using UnityEngine;
using System.Collections.Generic;
using FSM;
using Actors;

[RequireComponent (typeof (CharacterController))]

public class EnemyInput : MonoBehaviour
{
	public GameObject 	patrolPoint1;
	public GameObject 	patrolPoint2;
	public GameObject	hooker;
	public GameObject	hitboxShape;
	public GameObject	hitboxPoint;
	public float		moveSpeed 		= 2.0f;
	public float 		turnSpeed 		= 5.0f;
	public float 		agroRange 		= 0.0f; 
	public float		patrolPauseTime = 3.0f;
	public float		attackTime		= 1.0f;
	public float		attackLength	= 1.0f;
	public int			damage			= 10;
	
	private Enemy 		enemy;
	private Dictionary<string, object> attributes;
	
	void Start(){
		patrolPoint1.renderer.enabled = false;
		patrolPoint2.renderer.enabled = false;
		CharacterController controller = this.GetComponent<CharacterController>();
		
		attributes = new Dictionary<string, object>();
		attributes["gameObject"] = this.gameObject;
		attributes["controller"] = this.GetComponent<CharacterController>();
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
		attributes["damage"] = damage;
		attributes["hasAttacked"] = false;
		attributes["hitbox"] = hitboxShape;
		attributes["attackOffset"] = hitboxPoint.transform.position - this.transform.position;
		
		enemy = new Enemy(attributes); // Pass the dictionary to the enemy
	}
	
	void Update (){
		// Update User Attributes
		enemy.MoveSpeed = moveSpeed;
		enemy.TurnSpeed = turnSpeed;
		enemy.Position 	= enemy.controller.transform.position;
		//enemy.Rotation 	= enemy.controller.transform.rotation;
		
		Vector3 targetPos 	= hooker.transform.position;
		float 	targetDist 	= (targetPos - enemy.Position).magnitude;
		Vector3 targetDir 	= (targetPos - enemy.Position).normalized;
		
		float range = ((CharacterController)hooker.collider).radius + enemy.controller.radius + 0.5f;
		
		// Move or Attack to player based on agro range
		if (targetDist > range && targetDist <= agroRange ){	// Hooker is within agro range, move toward the Hooker
			//targetPos = new Vector3(hooker.transform.position.x, this.transform.position.y, hooker.transform.position.z);
			enemy.MoveToPosition(targetPos);
		}
		else{
			enemy.Patrol(patrolPauseTime);		// Just Patrol
		}
		
		if (targetDist < range){		// Within attack range, attack
			enemy.Attack();
		}
		applyGravity();
		enemy.Update();
	}
	
	void OnCollisionEnter(Collision collision){
		GameObject gobj = collision.gameObject;
		if (gobj.tag == "Hitbox"){
			Debug.Log("Hit by Attack.");
		}
	}
	
	private void applyGravity()
    {
        if (!enemy.controller.isGrounded)
        {
            enemy.Position += new Vector3(0.0f, (Physics.gravity.y * Time.deltaTime), 0.0f);
        }
	}
}