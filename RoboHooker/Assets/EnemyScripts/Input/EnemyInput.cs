using UnityEngine;
using System.Collections.Generic;
using FSM;
using Actors;

[RequireComponent (typeof(CharacterController))]

public class EnemyInput : MonoBehaviour
{
	public bool			isStatic		= false;
	public bool			isRanged		= false;
	public bool			isFlying		= false;
	public GameObject 	patrolPoint1;
	public GameObject 	patrolPoint2;
	public GameObject	hitbox;
	public GameObject	deathParts;
	public GameObject	socketedDrop;
	public GameObject	projectile;
	public float		projectileSpeed = 1.0f;
	public float		jumpPower		= 5.0f;
	public float		moveSpeed 		= 2.0f;
	public float 		turnSpeed 		= 5.0f;
	public float 		agroRange 		= 0.0f; 
	public float		attackRange		= 1.0f;
	public float		patrolPauseTime = 3.0f;
	public float		attackTime		= 0.0f;
	public float		fadeTime		= 3.0f;
	public int			damage			= 10;
	public int			maxHP			= 100;
	public int			currentHP		= 100;
	
	private GameObject	hooker;
	private GameObject	robot;
	private CharacterController	controller;
	private Dictionary<string, object> attributes;
	private	Enemy 		enemy;
	
	void Start(){
		hooker = GameObject.Find("Hooker");
		robot = GameObject.Find("Robot");
		
		// Turn off hitbox and patrol point rendering
		Physics.IgnoreCollision(hitbox.collider, this.collider);
		patrolPoint1.renderer.enabled = false;
		patrolPoint2.renderer.enabled = false;
		hitbox.renderer.enabled = false;
		hitbox.collider.enabled = false;
		if (hitbox != null) 
			hitbox.gameObject.GetComponent<HitboxScript>().Damage = damage;
		if (projectile != null) 
			projectile.gameObject.GetComponent<ProjectileScript>().Damage = damage;
		
		controller = this.GetComponent<CharacterController>();
		
		attributes = new Dictionary<string, object>();
		#region attribute dictionary assignments
		attributes["gameObject"] = this.gameObject;
		attributes["controller"] = controller;
		attributes["hooker"] = hooker;
		attributes["robot"] = robot;
		attributes["deathParts"] = deathParts;
		attributes["socketedDrop"] = socketedDrop;
		attributes["projectile"] = projectile;
		
		attributes["projectileSpeed"] = projectileSpeed;
		attributes["isStatic"] = isStatic;
		attributes["isRanged"] = isRanged;
		attributes["isFlying"] = isFlying;
		attributes["jumpPower"] = jumpPower;
		attributes["moveSpeed"] = moveSpeed;
		attributes["turnSpeed"] = turnSpeed;
		attributes["agroRange"] = agroRange;
		attributes["patrolPoint1"] = patrolPoint1;
		attributes["patrolPoint2"] = patrolPoint2;
		attributes["patrolTarget"] = patrolPoint1;
		attributes["patrolPauseTime"] = patrolPauseTime;
		attributes["targetPosition"] = controller.transform.position;
		attributes["targetRotation"] = controller.transform.rotation;
		attributes["actionTimer"] = 0.0f;
		attributes["attackTime"] = attackTime;
		//attributes["attackLength"] = attackLength;
		attributes["attackRange"] = attackRange;
		attributes["fadeTime"] = fadeTime;
		attributes["damage"] = damage;
		attributes["maxHP"] = maxHP;
		attributes["currentHP"] = currentHP;
		attributes["animation"] = gameObject.animation;
		
		attributes["hasAttacked"] = false;
		attributes["hitbox"] = hitbox;
		#endregion
		enemy = new Enemy(attributes); // Pass the dictionary to the enemy
	}
	
	void Update (){
		// Update User Attributes
		//Debug.Log(this.enemy.gameObject.name);
		this.currentHP 	= this.enemy.CurrentHP;
		this.maxHP		= this.enemy.MaxHP;
		enemy.MoveSpeed = moveSpeed;
		enemy.TurnSpeed = turnSpeed;
		enemy.Position 	= enemy.controller.transform.position;
		
		/*
		//todo: check to see the hooker or robot can be reassigned and reassign if possible
		//this will prevent the ai from breaking if the robot dies.
		
		// note: the way respawning is implemented - hooker and robot gameobjects are not destroyed, simply
		// rendering and collision is turned off
        Vector3 hookerPos = new Vector3();
        
		if 	(hooker != null) 
				hookerPos = hooker.transform.position;
        else 	hookerPos = robot.transform.position; //just using the position of the other character, assuming we don't want the ai to freeze when a character dies
		
		float 	hookerDist 	= (hookerPos - enemy.Position).magnitude;
		Vector3 hookerDir 	= (hookerPos - enemy.Position).normalized;
		float   hookerRange = ((CharacterController)hooker.collider).radius + enemy.controller.radius + enemy.AttackRange;
		
		// Setting Robot Parameters for action checking
		Vector3 robotPos = new Vector3();
        
		if 	(robot != null) 
				robotPos = robot.transform.position;
        else 	robotPos = hooker.transform.position;
		
		float 	robotDist 	= (robotPos - enemy.Position).magnitude;
		Vector3 robotDir 	= (robotPos - enemy.Position).normalized;
		float   robotRange = ((CharacterController)hooker.collider).radius + enemy.controller.radius + enemy.AttackRange;
		
		checkAttackRange(hookerPos, hookerDist, hookerRange);
		checkAttackRange(robotPos, robotDist, robotRange);
		
		checkMovement(hookerPos, hookerDist, hookerRange);
		checkMovement(robotPos, robotDist, robotRange);
		
		checkDeath();
		
		if (Input.GetKey(KeyCode.Tab)){ // Test case for enemy death
			enemy.CurrentHP = 0;
		}
		
		if (enemy.CurrentHP <= 0 ){	// True case
			isDead = true;
		}
		
		if (!isFlying){
			applyGravity();
		}
		*/
		
		if (Input.GetKey(KeyCode.Tab)){ // Test case for enemy death
			enemy.CurrentHP = 0;
		}
		
		enemy.Update();
		
	}
	/*
	void checkMovement(Vector3 targetPos, float targetDist, float range){
		if (!isStatic){
			// Move or Attack to player based on agro range
			if (targetDist > range && targetDist <= agroRange ){	// Hooker is within agro range, move toward the Hooker
				enemy.MoveToPosition(targetPos);
			}
			else{
				enemy.Patrol(patrolPauseTime);		// Just Patrol
			}
		}
	}
	
	
	void checkAttackRange(Vector3 targetPos, float targetDist, float range){
		if (targetDist < range){		// Within attack range, attack
			enemy.Attack(targetPos);
		}
	}
	
	void checkDeath(){
		if (isDead){
			enemy.Death();
		}
	}
	
	void applyGravity(){
        if (!enemy.controller.isGrounded)
        {
            enemy.controller.Move(new Vector3(0.0f, (Physics.gravity.y * Time.deltaTime), 0.0f));
        }
	}
	*/
}