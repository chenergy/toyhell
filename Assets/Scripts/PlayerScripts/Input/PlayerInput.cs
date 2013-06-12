using UnityEngine;
using System;
using System.Collections.Generic;
using FSM;
using Actors;

public enum PlayerNumber { P1, P2, P3, P4 }

[System.Serializable]
public class GamePad{
	public PlayerNumber	player;
	public KeyCode		JumpKey;
	public KeyCode		ActivateKey;
	public KeyCode		AttackKey;
	public KeyCode		SwapKey;
	public KeyCode		LeftKey;
	public KeyCode		RightKey;
	public KeyCode		UpKey;
	public KeyCode		DownKey;
	
	public KeyCode		JumpJoystick;
	public KeyCode		ActivateJoystick;
	public KeyCode		AttackJoystick;
	public KeyCode		SwapJoystick;
}

public class PlayerInput : MonoBehaviour
{
	public GameObject 	gobj;
	public GameObject	deathParts;
	public GameObject	socketJoint;
	public float		jumpPower			= 20.0f;
	public float		moveSpeed 			= 2.0f;
	public float 		turnSpeed 			= 5.0f;
	public float		attackSpeed			= 1.0f;
	public float		knockbackStrength	= 35.0f;
	public int			maxHP				= 100;
	public int			currentHP			= 100;
	public float		fadeTime			= 3.0f;
	public GamePad		controls;
	
	[HideInInspector]
	public string		XAxis;
	[HideInInspector]
	public string		YAxis;
	
	private List<KeyCode> buttons;
	private GameObject	socketedWeapon;
	private float		zPlane;
	private CharacterController	controller;
	private Dictionary<string, object> attributes;
	private Vector3 	forward;
	private	Player 		player;
	
	private Vector3 knockback;
	
	void Start(){
		this.SetControls();
		XAxis = controls.player.ToString() + "moveX";
		YAxis = controls.player.ToString() + "moveY";
		zPlane = this.gameObject.transform.position.z;
		controller = this.gobj.GetComponent<CharacterController>();
		attributes = new Dictionary<string, object>();
		forward = new Vector3(1.0f, 0.0f, 0.0f);
		knockback = Vector3.zero;
		
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
		attributes["forward"] = forward;
		#endregion
		
		player = new Player(attributes); // Pass the dictionary to the enemy
		GameData.CreateWeapons();
	}
	
	private void SetControls(){
		//Debug.Log(Application.platform.ToString().Substring(0, 3));
		if (Application.platform.ToString().Substring(0, 3) == "OSX"){
			string thisPlayer = controls.player.ToString();
			controls.JumpJoystick = (KeyCode) Enum.Parse(typeof(KeyCode), "Joystick" + thisPlayer[1] + "Button16");
			controls.ActivateJoystick = (KeyCode) Enum.Parse(typeof(KeyCode), "Joystick" + thisPlayer[1] + "Button19");
			controls.AttackJoystick = (KeyCode) Enum.Parse(typeof(KeyCode), "Joystick" + thisPlayer[1] + "Button17");
			controls.SwapJoystick = (KeyCode) Enum.Parse(typeof(KeyCode), "Joystick" + thisPlayer[1] + "Button18");
		}
		
		buttons = new List<KeyCode>();
		buttons.Add(controls.JumpKey);
		buttons.Add(controls.ActivateKey);
		buttons.Add(controls.AttackKey);
		buttons.Add(controls.SwapKey);
		buttons.Add(controls.LeftKey);
		buttons.Add(controls.RightKey);
		buttons.Add(controls.UpKey);
		buttons.Add(controls.DownKey);
		buttons.Add(controls.JumpJoystick);
		buttons.Add(controls.ActivateJoystick);
		buttons.Add(controls.AttackJoystick);
		buttons.Add(controls.SwapJoystick);
	}
	
	void Update (){
		Debug.Log(controls.ActivateJoystick);
		if (this.gobj != null){
			// Update User Attributes
			this.currentHP 	= this.player.CurrentHP;
			this.maxHP		= this.player.MaxHP;
			player.MoveSpeed = moveSpeed;
			player.TurnSpeed = turnSpeed;
			player.Position  = this.controller.transform.position;
			
			if (knockback.magnitude > 0.1f) { 			//Added knockback to control movement after being hit
				player.Knockback(knockback);
				knockback *= 0.9f;
			}
			else{
				knockback = Vector3.zero;
			}
			
			
			// Check if any key in the given keys is being pressed
			bool someKeyPressed = false;
			if (Input.anyKey){
				foreach (KeyCode key in this.buttons){
					Debug.Log("Key: " + key);
					if (Input.GetKey(key)){
						someKeyPressed = true;
					}
				}
			}
			
			if (!player.isFrozen){
				float directionX = Input.GetAxisRaw(XAxis);
				float directionY = Input.GetAxisRaw(YAxis);
				
				if ((Mathf.Abs(directionX) > 0) || (Input.GetKey(controls.LeftKey)) || (Input.GetKey(controls.RightKey))){
					if (Input.GetKey(controls.LeftKey)){
						directionX = -1;
					}
					else if (Input.GetKey(controls.RightKey)){
						directionX = 1;
					}
					player.MoveX(directionX);
				}
				
				if ((Mathf.Abs(directionY) > 0) || (Input.GetKey(controls.UpKey)) || (Input.GetKey(controls.DownKey))){
					if (player.isClimbing){
						if (Input.GetKey(controls.DownKey)){
							directionY = -1;
						}
						else if (Input.GetKey(controls.UpKey)){
							directionY = 1;
						}
						player.MoveY(directionY);
					}
				}
				
				if (someKeyPressed || (directionX != 0) || (directionY != 0)){
					if (Input.GetKeyDown(controls.AttackKey) || Input.GetKeyDown(controls.AttackJoystick)){
						player.Attack(player.Forward);
					}
					else if (Input.GetKeyDown(controls.JumpKey) || Input.GetKeyDown(controls.JumpJoystick)){
						player.Jump();
					}
				}
				else{
					player.isMoving = false;
				}
			}
			
			// Parent to other object if dead
			if (this.gobj == GameData.Hooker){
				if (GameData.HookerLives <= 0){
					this.gobj.transform.position = GameData.Robot.transform.position;
				}
			}
			else if (this.gobj == GameData.Robot){
				if (GameData.RobotLives <= 0){
					this.gobj.transform.position = GameData.Hooker.transform.position;
				}
			}
			
			
			if (!player.isMoving && player.IsGrounded){
				player.Idle();
			}
			
			player.Update();
			
			if (player.IsGrounded){
				player.AllowJump();
			}
			
			if(this.transform.position.z != this.zPlane){
				this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.zPlane);
			}
		}
	}
	
	public void Idle(){
		player.Idle();
	}
	
	public void Hurt(){
		player.Hurt();
	}
	
	public void SwapWeapons(GameObject weapon){
		float rotationModifier = 0.0f;
		if (player.gameObject.transform.forward.x < 0){
			rotationModifier = 180.0f;
		}
		
		Debug.Log(rotationModifier);
		
		if (player.SocketedWeapon == null){
			//player.SocketedWeapon = (GameObject)GameObject.Instantiate(weapon, this.socketJoint.transform.position, Quaternion.Euler(weapon.transform.rotation.x, weapon.transform.rotation.y + rotationModifier, weapon.transform.rotation.z ));
			player.SocketedWeapon = (GameObject)GameObject.Instantiate(weapon, this.socketJoint.transform.position, weapon.transform.rotation );
			player.SocketedWeapon.transform.RotateAround(new Vector3(0, 1, 0), rotationModifier);
			//player.SocketedWeapon.transform.LookAt(weapon.transform.position + player.Forward);
			
			player.SocketedWeapon.transform.parent = this.socketJoint.transform;
		}
		else{
			// Drop the weapons
			string socketName = player.SocketedWeapon.GetComponent<Weapon>().model.name;
			//Debug.Log("Assets/Prefabs/WeaponPickupPrefabs/" + socketName + "Pickup.prefab");
			GameObject newPickup = (GameObject)Resources.LoadAssetAtPath("Assets/Prefabs/WeaponPickupPrefabs/" + socketName + "Pickup.prefab", typeof(GameObject));
			GameObject.Instantiate(newPickup, player.Position + new Vector3(0.0f, 2.0f, 0.0f), Quaternion.identity);
			GameObject.Destroy(player.SocketedWeapon);
			
			// Acquire new weapon
			player.SocketedWeapon = (GameObject)GameObject.Instantiate(weapon, this.socketJoint.transform.position, weapon.transform.rotation);
			player.SocketedWeapon.transform.RotateAround(new Vector3(0, 1, 0), rotationModifier);
			player.SocketedWeapon.transform.parent = this.socketJoint.transform;
		}
	}
	
    public bool Climbing
    {
		get { return player.isClimbing; }
		set { player.isClimbing = value; }
    }
	
	public bool Frozen{
		get { return player.isFrozen; }
		set { player.isFrozen = value; }
	}
	
	public Vector3 Knockback{
		get { return knockback; }
		set { knockback = value; }
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