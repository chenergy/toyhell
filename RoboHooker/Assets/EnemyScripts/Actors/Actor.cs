using UnityEngine;
using System;
using System.Collections.Generic;
using FSM;
using Actors;

namespace Actors
{
	public abstract class Actor
	{
		protected class PlayerData{
			public string		name;
			public Vector3 		position;
			public Vector3 		direction;
			public float 		distance;
			//public float		range;
			public GameObject 	player;
			
			public PlayerData(GameObject player){
				this.position 	= Vector3.zero;
				this.direction 	= Vector3.zero;
				this.distance 	= 0.0f;
				//this.range 		= 0.0f;
				this.player 	= player;
				this.name		= player.name;
			}
		}
		
		protected FSMContext fsmc;
		protected Dictionary<string, object> attributes;
		protected PlayerData HookerData;
		protected PlayerData RobotData;
		
		public void MoveToPosition(Vector3 targetPosition){
			if (fsmc.CurrentState.Name != "attack"){
				// Do not consider y in the target location
				this.TargetPosition = new Vector3(targetPosition.x, this.Position.y, targetPosition.z);
				this.TargetRotation = Quaternion.LookRotation(this.TargetPosition - this.Position);
				
				fsmc.dispatch("moveToPosition", this);
			}
		}
		
		public void Attack(Vector3 targetPosition){
			if (fsmc.CurrentState.Name != "attack"){
				// Do not consider y in the target location
				this.TargetPosition = new Vector3(targetPosition.x, this.Position.y, targetPosition.z);
				this.TargetRotation = Quaternion.LookRotation(this.TargetPosition - this.Position);
				
				fsmc.dispatch("attack", this);
			}
		}
		
		public void Death(){
			if (fsmc.CurrentState.Name != "death"){
				fsmc.dispatch("death", this);
			}
		}
		
		public abstract void Update();
			// todo: check to see the hooker or robot can be reassigned and reassign if possible
			// this will prevent the ai from breaking if the robot dies.
			
			// note: the way respawning is implemented - hooker and robot gameobjects are not destroyed, simply
			// rendering and collision is turned off, then relocated
	        
			// Setting Hooker Parameters for action checking
			/*
			Vector3 hookerPos = new Vector3();
	        
			if 	(hooker != null) 
					hookerPos = GameData.Hooker.transform.position;
	        else 	hookerPos = GameData.Robot.transform.position; //just using the position of the other character, assuming we don't want the ai to freeze when a character dies
			
			float 	hookerDist 	= (hookerPos - this.Position).magnitude;
			Vector3 hookerDir 	= (hookerPos - this.Position).normalized;
			float   hookerRange = ((CharacterController)hooker.collider).radius + this.controller.radius + this.AttackRange;
			
			// Setting Robot Parameters for action checking
			Vector3 robotPos = new Vector3();
	        
			if 	(robot != null) 
					robotPos = GameData.Robot.transform.position;
	        else 	robotPos = GameData.Hooker.transform.position;
			
			float 	robotDist 	= (robotPos - this.Position).magnitude;
			Vector3 robotDir 	= (robotPos - this.Position).normalized;
			float   robotRange = ((CharacterController)hooker.collider).radius + this.controller.radius + this.AttackRange;
			*/
			/*
			UpdatePlayerData(HookerData);
			UpdatePlayerData(RobotData);
			
			checkAttackRange(HookerData);
			checkAttackRange(RobotData);
			
			checkMovement(HookerData);
			checkMovement(RobotData);
			
			checkDeath();
			
			if (!this.IsFlying){
				applyGravity();
			}
			
			fsmc.CurrentState.update(fsmc, this);
			
		}*/
		
		
		protected void UpdatePlayerData(PlayerData data){
			if 	(data.player != null) {
				data.position = data.player.transform.position;
			}
			else{
				if (data.name == "Hooker"){
					data.position = this.Robot.transform.position; //just using the position of the other character, assuming we don't want the ai to freeze when a character dies
				}
				else if (data.name == "Robot"){
					data.position = this.Hooker.transform.position;
				}
			}
			
			data.distance	= (data.position - this.Position).magnitude;
			data.direction 	= (data.position - this.Position).normalized;
			//data.range		= ((CharacterController)data.player.collider).radius + this.controller.radius + this.AttackRange;
		}
		
		/*
		protected void checkMovement(PlayerData data){
			if (!this.IsStatic){ // ADD: AND NOT ALREADY ATTACKING
				// Move or Attack to player based on agro range
				if (data.distance > data.range && data.distance <= this.AgroRange ){	// Hooker is within agro range, move toward the Hooker
					this.MoveToPosition(data.position);
				}
			}
		}
		*/
		/*
		protected void checkAttackRange(PlayerData data){
			if (data.distance < this.AttackRange){		// Within attack range, attack
				this.Attack(data.position);
			}
		}
		*/
		protected void checkDeath(){
			if (this.CurrentHP <= 0){
				this.Death();
			}
		}
		
		protected void applyGravity(){
	        if (!this.controller.isGrounded)
	        {
	            this.controller.Move(new Vector3(0.0f, (Physics.gravity.y * Time.deltaTime), 0.0f));
	        }
		}
		
		#region attribute getters and setters
		public GameObject gameObject{
			get{ return (GameObject)attributes["gameObject"]; }
			set{ attributes["gameObject"] = value; }
		}
		
		public CharacterController controller{
			get{ return (CharacterController)attributes["controller"]; }
			set{ attributes["controller"] = value; }
		}
		
		public string StateName{
			get{ return fsmc.CurrentState.Name; }
		}
		
		public Vector3 Position{
			get{ return (Vector3)this.controller.transform.position; }
			set{ this.controller.transform.position = value; }
		}
		
		public Quaternion Rotation{
			get{ return (Quaternion)this.gameObject.transform.rotation; }
			set{ this.gameObject.transform.rotation = value; }
		}
		
		public Vector3 TargetPosition{
			get{ return (Vector3)attributes["targetPosition"]; }
			set{ attributes["targetPosition"] = value; }
		}
		
		public Quaternion TargetRotation{
			get{ return (Quaternion)attributes["targetRotation"]; }
			set{ attributes["targetRotation"] = value; }
		}
		
		public float MoveSpeed{
			get{ return (float)attributes["moveSpeed"]; }
			set{ attributes["moveSpeed"] = value; }
		}
		
		public float TurnSpeed{
			get{ return (float)attributes["turnSpeed"]; }
			set{ attributes["turnSpeed"] = value; }
		}
		
		public float ActionTimer{
			get{ return (float)attributes["actionTimer"]; }
			set{ attributes["actionTimer"] = value; }
		}
		
		public float AttackTime{
			get{ return (float)attributes["attackTime"]; }
			set{ attributes["attackTime"] = value; }
		}
		/*
		public float AttackLength{
			get{ return (float)attributes["attackLength"]; }
			set{ attributes["attackLength"] = value; }
		}
		*/
		public float AttackRange{
			get{ return (float)attributes["attackRange"]; }
			set{ attributes["attackRange"] = value; }
		}
		
		public bool HasAttacked{
			get{ return (bool)attributes["hasAttacked"]; }
			set{ attributes["hasAttacked"] = value; }
		}
		
		public GameObject Hitbox{
			get{ return (GameObject)attributes["hitbox"]; }
			set{ attributes["hitbox"] = value; }
		}

		public int Damage{
			get{ return (int)attributes["damage"]; }
			set{ attributes["damage"] = value; }
		}
		
		public int CurrentHP{
			get{ return (int)attributes["currentHP"]; }
			set{ attributes["currentHP"] = value; }
		}
		
		public int MaxHP{
			get{ return (int)attributes["maxHP"]; }
			set{ attributes["maxHP"] = value; }
		}
		
		public float FadeTime{
			get{ return (float)attributes["fadeTime"]; }
			set{ attributes["fadeTime"] = value; }
		}
		
		public GameObject DeathParts{
			get{ return (GameObject)attributes["deathParts"]; }
			set{ attributes["deathParts"] = value; }
		}
		
		public Animation Animation{
			get{ return (Animation)attributes["animation"]; }
			set{ attributes["animation"] = value; }
		}
		
		public float AgroRange{
			get{ return (float)attributes["agroRange"]; }
			set{ attributes["agroRange"] = value; }
		}
		
		public GameObject Projectile{
			get{ return (GameObject)attributes["projectile"]; }
		}
		
		public float ProjectileSpeed{
			get{ return (float)attributes["projectileSpeed"]; }
			set{ attributes["projectileSpeed"] = value; }
		}
		
		public bool IsRanged{
			get{ return (bool)attributes["isRanged"]; }
		}
		
		public bool IsFlying{
			get{ return (bool)attributes["isFlying"]; }
		}
		
		public bool IsStatic{
			get{ return (bool)attributes["isStatic"]; }
		}
		
		protected GameObject Hooker{
			get{ return (GameObject)attributes["hooker"]; }
		}
		
		protected GameObject Robot{
			get{ return (GameObject)attributes["robot"]; }
		}
		
		#endregion
	}
}

