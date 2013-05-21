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
	public GameObject	projectile;
	public float		projectileSpeed = 2.0f;
	public float		moveSpeed 		= 2.0f;
	public float 		turnSpeed 		= 5.0f;
	public float 		agroRange 		= 0.0f; 
	public float		patrolPauseTime = 3.0f;
	public float		attackTime		= 1.0f;
	public float		attackLength	= 1.0f;
	public float		attackRange		= 1.0f;
	public float		fadeTime		= 3.0f;
	public int			damage			= 10;
	public int			maxHP			= 100;
	public int			currentHP		= 100;
	
	private GameObject	hooker;
	private GameObject	robot;
	private CharacterController	controller;
	private Dictionary<string, object> attributes;
	private bool		isFalling 		= false;
	private bool		isDead			= false;
	private	Enemy 		enemy;
	
	void Start(){
		hooker = GameObject.Find("Hooker");
		robot = GameObject.Find("Robot");
		currentHP = 100;
		
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
		attributes["gameObject"] = this.gameObject;
		attributes["controller"] = controller;
		attributes["hooker"] = hooker;
		attributes["robot"] = robot;
		attributes["deathParts"] = deathParts;
		attributes["projectile"] = projectile;
		
		attributes["projectileSpeed"] = projectileSpeed;
		attributes["isRanged"] = isRanged;
		attributes["isFlying"] = isFlying;
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
		attributes["fadeTime"] = fadeTime;
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
		
		Vector3 hookerPos 	= hooker.transform.position;
		float 	hookerDist 	= (hookerPos - enemy.Position).magnitude;
		Vector3 hookerDir 	= (hookerPos - enemy.Position).normalized;
		
		Vector3 robotPos 	= robot.transform.position;
		float 	robotDist 	= (robotPos - enemy.Position).magnitude;
		Vector3 robotDir 	= (robotPos - enemy.Position).normalized;
		
		float hookerRange = ((CharacterController)hooker.collider).radius + enemy.controller.radius + enemy.AttackRange;
		float robotRange = ((CharacterController)hooker.collider).radius + enemy.controller.radius + enemy.AttackRange;
		
		checkMovement(hookerPos, hookerDist, hookerRange);
		checkAttackRange(hookerPos, hookerDist, hookerRange);
		checkMovement(robotPos, robotDist, robotRange);
		checkAttackRange(robotPos, robotDist, robotRange);
		checkDeath();
		
		if (Input.GetKey(KeyCode.Tab)){ // Test case for enemy death
			enemy.CurrentHP = 0;
		}
		
		if (enemy.CurrentHP <= 0 ){	// True case
			isDead = true;
		}
		
		enemy.Update();
	}
	
	void Jump(){
	}
	
	void checkMovement(Vector3 targetPos, float targetDist, float range){
		
		if (!isStatic){
			// Move or Attack to player based on agro range
			if (targetDist > range && targetDist <= agroRange ){	// Hooker is within agro range, move toward the Hooker
				enemy.MoveToPosition(targetPos);
			}
			else{
				enemy.Patrol(patrolPauseTime);		// Just Patrol
			}
			applyGravity();
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
}