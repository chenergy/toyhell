using UnityEngine;
using System;
using System.Collections.Generic;
using FSM;
using Actors;

namespace Actors
{
	public class Enemy : Actor
	{
		public class PlayerData{
			public string		name;
			public Vector3 		position;
			public Vector3 		direction;
			public float 		distance;
			public GameObject 	player;
			
			public PlayerData(GameObject player){
				this.position 	= Vector3.zero;
				this.direction 	= Vector3.zero;
				this.distance 	= 0.0f;
				this.player 	= player;
				this.name		= player.name;
			}
		}
		
		private Dictionary<GameObject, PlayerData> playerData;
		
		public Enemy(Dictionary<string, object> attributes){
			FSMAction noAction 		= new A_None();
			
			State S_Idle 			= new State("idle", new A_IdleEnter(), new A_Idle(), new A_IdleExit());
			State S_MoveToPosition	= new State("moveToPosition", new A_MoveToPositionEnter(), new A_MoveToPosition(), new A_EnemyMoveToPositionExit());
			State S_Attack 			= new State("attack", new A_EnemyAttackEnter(), new A_EnemyAttack(), new A_EnemyAttackExit());
			State S_Death 			= new State("death", new A_EnemyDeathEnter(), new A_EnemyDeath(), new A_EnemyDeathExit());
			State S_Hurt			= new State("hurt", new A_HurtEnter(), new A_Hurt(), new A_HurtExit());
			
			Transition T_Idle 			= new Transition(S_Idle, noAction);
			Transition T_MoveToPosition	= new Transition(S_MoveToPosition, noAction);
			Transition T_Attack			= new Transition(S_Attack, noAction);
			Transition T_Death			= new Transition(S_Death, noAction);
			Transition T_Hurt			= new Transition(S_Hurt, noAction);
			
			S_Idle.addTransition(T_MoveToPosition, "moveToPosition");
			S_Idle.addTransition(T_Attack, "attack");
			S_Idle.addTransition(T_Death, "death");
			S_Idle.addTransition(T_Hurt, "hurt");
			
			S_MoveToPosition.addTransition(T_Idle, "idle");
			S_MoveToPosition.addTransition(T_Attack, "attack");
			S_MoveToPosition.addTransition(T_Death, "death");
			S_MoveToPosition.addTransition(T_MoveToPosition, "moveToPosition");
			S_MoveToPosition.addTransition(T_Hurt, "hurt");
			
			S_Attack.addTransition(T_Idle, "idle");
			S_Attack.addTransition(T_Death, "death");
			S_Attack.addTransition(T_Hurt, "hurt");
			
			S_Hurt.addTransition(T_Idle, "idle");
			S_Hurt.addTransition(T_Death, "death");
			S_Hurt.addTransition(T_Hurt, "hurt");
			
			PlayerData HookerData = new PlayerData(GameData.Hooker);
			PlayerData RobotData = new PlayerData(GameData.Robot);
			
			this.fsmc = FSM.FSM.createFSMInstance(S_Idle, noAction);
			this.attributes = attributes;
			
			this.playerData = new Dictionary<GameObject, PlayerData>();
			this.playerData[GameData.Hooker] = HookerData;
			this.playerData[GameData.Robot] = RobotData;
			
			this.jumpStrength = 0.0f;
			this.canJump = false;
		}
		
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
		}
		
		
		public override void Update ()
		{
			checkDeath();
			Debug.DrawRay(this.Position + (this.gameObject.transform.forward), new Vector3(0.0f, -1.0f, 0.0f));
			
			
			// Update information about the two players
			foreach (PlayerData data in this.playerData.Values){
				UpdatePlayerData(data);
			}
			
			// Get a player to target, if possible
			if (this.TargetPlayer == null)
				this.TargetPlayer = this.GetPlayerInRange();	
			
			
			// Find out whether to attack or move toward the player
			if (this.TargetPlayer != null){
				bool playerIsAlive = GameObject.Find("PlayerUI").GetComponent<PlayerRespawnTimer>().playerStats[this.TargetPlayer].isAlive;
				
				if (playerIsAlive){
					if (CheckAttackPlayer(this.TargetPlayer)){
						this.Attack(this.TargetPlayer.transform.position);
					}
					else if (CheckMoveToPlayer(this.TargetPlayer) && !this.IsStatic){
						this.MoveToPosition(this.TargetPlayer.transform.position);
					}
				}
				else{
					this.Patrol(this.PatrolPauseTime);
				}
			}
			// As long as it's non-static, patrol
			else {
				if (!this.IsStatic){
					this.Patrol(this.PatrolPauseTime);
				}
			}
			
			
			// If it's non-static and non-flying, apply gravity-based logic
			if (!this.IsFlying && !this.IsStatic){
				
				// Adjust Jump strength back to normal
				if (this.jumpStrength <= 0.1f){
					this.jumpStrength = 0.0f;
				}
				else{
					this.jumpStrength -= this.jumpStrength * 0.01f;
				}
				
				// Check if there is an existing collider in from of the enemy
				bool shouldJump = !(Physics.Raycast( this.Position + (this.gameObject.transform.forward), 
					new Vector3(0.0f, -1.0f, 0.0f), 
					1.0f )
				);
				
				// If not, jump
				if(shouldJump){
					if (this.canJump){
						this.canJump = false;
						if (this.jumpStrength == 0.0f){
							this.jumpStrength = this.JumpPower;
						}
					}
				}
				else{
					this.jumpStrength = 0.0f;
					this.canJump = true;
				}
				
				//Debug.Log("jump strength: " + this.jumpStrength);
				this.applyGravity();
			}

			fsmc.CurrentState.update(fsmc, this);
		}
		
		
		// Finds the closest player
		private GameObject GetPlayerInRange(){
			PlayerData hookerData = this.playerData[GameData.Hooker];
			PlayerData robotData = this.playerData[GameData.Robot];
			
			if ((hookerData.distance < this.AgroRange) && (robotData.distance < this.AgroRange)){
				if (hookerData.distance < robotData.distance){
					return hookerData.player;
				}
				else{
					return robotData.player;
				}
			}
			else{
				foreach (PlayerData data in this.playerData.Values){
					if (data.distance < this.AgroRange){
						return data.player;
					}
				}
			}
			return null;
		}
		
		// Checks if self is close enough to attack
		private bool CheckAttackPlayer(GameObject player){
			// todo: check to see the hooker or robot can be reassigned and reassign if possible
			// this will prevent the ai from breaking if the robot dies.
			// note: the way respawning is implemented - hooker and robot gameobjects are not destroyed, simply
			// rendering and collision is turned off, then relocated
			PlayerData data = this.playerData[player];
			if (data.distance <= this.AttackRange){
				return true;
			}
			return false;
		}
		
		// Checks if self should pursue player
		private bool CheckMoveToPlayer(GameObject player){
			PlayerData data = this.playerData[player];
			if (data.distance <= this.AgroRange){
				return true;
			}
			return false;
		}
		
		public void Patrol(float patrolPauseTime){
			// Do not consider y in the target location
			Vector3 PatrolTargetPosition = new Vector3(this.PatrolTarget.transform.position.x, this.Position.y, this.PatrolTarget.transform.position.z);
			float distance = (PatrolTargetPosition - this.Position).magnitude;
			
			if (distance < 0.2f){
				if (this.ActionTimer < patrolPauseTime){	// Gets within distance of a patrol point and idles
					this.ActionTimer += Time.deltaTime;
					this.fsmc.dispatch("idle", this);
				}
				
				else{
					this.ActionTimer = 0.0f;
					if (this.PatrolTarget == this.PatrolPoint1){	// Move target between patrol points 1 and 2
						//Debug.Log("Changed To point2");
						this.PatrolTarget = this.PatrolPoint2;
					}
					else{
						//Debug.Log("Changed To point1");
						this.PatrolTarget = this.PatrolPoint1;
					}
				}
			}
			else{
				this.MoveToPosition(this.PatrolTarget.transform.position);
			}
		}
		
		#region attribute getters and setters
		public bool IsRanged{
			get{ return (bool)attributes["isRanged"]; }
		}
		
		public bool IsFlying{
			get{ return (bool)attributes["isFlying"]; }
		}
		
		public bool IsStatic{
			get{ return (bool)attributes["isStatic"]; }
		}
		
		public GameObject PatrolPoint1{
			get{ return (GameObject) attributes["patrolPoint1"]; }
			set{ attributes["patrolPoint1"] = value; }
		}
		
		public GameObject PatrolPoint2{
			get{ return (GameObject) attributes["patrolPoint2"]; }
			set{ attributes["patrolPoint2"] = value; }
		}
		
		public GameObject PatrolTarget{
			get{ return (GameObject) attributes["patrolTarget"]; }
			set{ attributes["patrolTarget"] = value; }
		}
		
		public float PatrolPauseTime{
			get{ return (float) attributes["patrolPauseTime"]; }
			set{ attributes["patrolPauseTime"] = value; }
		}
		
		public bool OnDeathLoadLevel{
			get{ return (bool)attributes["onDeathLoadLevel"]; }
		}
		
		public string LevelToLoad{
			get{ return (string)attributes["levelToLoad"]; }
		}
		
		public GameObject Projectile{
			get{ return (GameObject)attributes["projectile"]; }
		}
		
		public float ProjectileSpeed{
			get{ return (float)attributes["projectileSpeed"]; }
			set{ attributes["projectileSpeed"] = value; }
		}
		
		public float ProjectileDuration{
			get{ return (float)attributes["projectileDuration"]; }
			set{ attributes["projectileDuration"] = value; }
		}
		
		public GameObject TargetPlayer{
			get{ return (GameObject)attributes["targetPlayer"]; }
			set{ attributes["targetPlayer"] = value; }
		}
		
		public GameObject Hooker{
			get{ return (GameObject)attributes["hooker"]; }
			set{ attributes["hooker"] = value; }
		}
		
		public GameObject Robot{
			get{ return (GameObject)attributes["robot"]; }
			set{ attributes["robot"] = value; }
		}
		
		public float AgroRange{
			get{ return (float)attributes["agroRange"]; }
			set{ attributes["agroRange"] = value; }
		}
		
		public float AttackRange{
			get{ return (float)attributes["attackRange"]; }
			set{ attributes["attackRange"] = value; }
		}
		public int Damage{
			get{ return (int)attributes["damage"]; }
			set{ attributes["damage"] = value; }
		}
		
		public float AttackTime{
			get{ return (float)attributes["attackTime"]; }
			set{ attributes["attackTime"] = value; }
		}
		
		public GameObject SocketedDrop{
			get{ return (GameObject)attributes["socketedDrop"]; }
			set{ attributes["socketedDrop"] = value; }
		}
		
		public GameObject Hitbox{
			get{ return (GameObject)attributes["hitbox"]; }
			set{ attributes["hitbox"] = value; }
		}
		
		#endregion
		
		
	}
}