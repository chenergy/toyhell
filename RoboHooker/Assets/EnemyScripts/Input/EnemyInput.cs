using UnityEngine;
using System.Collections.Generic;
using FSM;
using Actors;

public class EnemyInput : MonoBehaviour
{
	public bool			isStatic		= false;
	public bool			isRanged		= false;
	public bool			isFlying		= false;
	public GameObject 	gobj;	
	public GameObject 	patrolPoint1;
	public GameObject 	patrolPoint2;
	public GameObject	hitbox;
	public GameObject	deathParts;
	public GameObject	socketedDrop;
	public GameObject	projectile;
	public float		projectileSpeed 	= 1.0f;
	public float		projectileDuration 	= 2.0f;
	public float		jumpPower			= 20.0f;
	public float		moveSpeed 			= 2.0f;
	public float 		turnSpeed 			= 5.0f;
	public float 		agroRange 			= 5.0f; 
	public float		attackRange			= 1.0f;
	public float		attackTime			= 0.0f;
	public float		knockbackStrength	= 35.0f;
	public float		patrolPauseTime 	= 3.0f;
	public float		fadeTime			= 3.0f;
	public int			damage				= 10;
	public int			maxHP				= 100;
	public int			currentHP			= 100;
	
	private GameObject	targetPlayer;
	private float		zPlane;
	private GameObject	hooker;
	private GameObject	robot;
	private CharacterController	controller;
	private Dictionary<string, object> attributes;
	private	Enemy 		enemy;
	
	void Start(){
		hooker = GameObject.Find("Hooker");
		robot = GameObject.Find("Robot");
		zPlane = this.gameObject.transform.position.z;
		// Turn off rendering and colliders for hitbox and patrol point
		Physics.IgnoreCollision(hitbox.collider, this.gobj.collider);
		
		// Can Walk through characters
		Physics.IgnoreCollision(this.gobj.collider, hooker.collider);
		Physics.IgnoreCollision(this.gobj.collider, robot.collider);
		
		patrolPoint1.renderer.enabled = false;
		patrolPoint2.renderer.enabled = false;
		hitbox.renderer.enabled = false;
		hitbox.collider.enabled = false;
		
		controller = this.gobj.GetComponent<CharacterController>();
		
		attributes = new Dictionary<string, object>();
		#region attribute dictionary assignments
		attributes["gameObject"] = this.gobj;
		attributes["controller"] = this.controller;
		attributes["hooker"] = hooker;
		attributes["robot"] = robot;
		attributes["deathParts"] = deathParts;
		attributes["socketedDrop"] = socketedDrop;
		attributes["projectile"] = projectile;
		
		attributes["projectileSpeed"] = projectileSpeed;
		attributes["projectileDuration"] = projectileDuration;
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
		attributes["targetPlayer"] = null;
		attributes["actionTimer"] = 0.0f;
		attributes["attackTime"] = attackTime;
		attributes["attackRange"] = attackRange;
		attributes["fadeTime"] = fadeTime;
		attributes["damage"] = damage;
		attributes["maxHP"] = maxHP;
		attributes["currentHP"] = currentHP;
		attributes["animation"] = this.gobj.animation;
		attributes["knockbackStrength"] = knockbackStrength;
		
		attributes["hasAttacked"] = false;
		attributes["hitbox"] = hitbox;
		#endregion
		enemy = new Enemy(attributes); // Pass the dictionary to the enemy
		
		if (hitbox != null){
			hitbox.gameObject.GetComponent<HitboxScript>().Damage = this.damage;
			hitbox.gameObject.GetComponent<HitboxScript>().Source = this.enemy;
		}
		if (projectile != null) {
			projectile.gameObject.GetComponent<ProjectileScript>().Damage = this.damage;
			projectile.gameObject.GetComponent<ProjectileScript>().Source = this.enemy;
		}
	}
	
	void Update (){
		// Update User Attributes
		this.currentHP 	= this.enemy.CurrentHP;
		this.maxHP		= this.enemy.MaxHP;
		enemy.MoveSpeed = moveSpeed;
		enemy.TurnSpeed = turnSpeed;
		enemy.Position 	= this.controller.transform.position;

		if (Input.GetKey(KeyCode.Tab)){ // Test case for enemy death
			this.DamageEnemy(10);
		}
		
		enemy.Update();
		
		if(this.transform.position.z != this.zPlane){
			this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.zPlane);
		}
	}
	
	public void KillEnemy(){
		this.enemy.CurrentHP = 0;
	}
	
	public void DamageEnemy(int damage){
		this.enemy.CurrentHP -= damage;
	}
}