using UnityEngine;
using System;
using System.Collections.Generic;
using FSM;
using ToyHell;

namespace ToyHell
{
	public abstract class Enemy_Moving : A_Enemy
	{
		protected GameObject	patrolPoint1;
		protected GameObject	patrolPoint2;
		protected GameObject	patrolTarget;
		
		public Enemy_Moving ( GameObject gobj ) : base( gobj )
		{
			MovingEnemyInput input = gobj.GetComponent<MovingEnemyInput>();
			
		}
		
		public override void Update ()
		{
			//checkDeath();
			Debug.DrawRay(this.Position + (this.gameObject.transform.forward), new Vector3(0.0f, -1.0f, 0.0f));
			
			
			// Update information about the two players
			/*foreach (PlayerData data in this.playerData.Values){
				UpdatePlayerData(data);
			}*/
			
			// Get a player to target, if possible
			/*
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
			*/
			
			foreach (Player p in GameManager.Players){
				Fighter fighter = p.Fighter;
				if (fighter != null){
					if (this.IsFighterInAttackRange(fighter)){
						this.Attack(fighter.gobj.transform.position);
						break;
					}
					else if (this.IsFighterInAgroRange(fighter)){
						this.MoveToPosition(fighter.gobj.transform.position);
						break;
					}
					else{
						this.Patrol();
					}
				}
				else{
					this.Patrol();
				}
			}
			
			bool shouldJump = !this.IsGrounded;
			// If not, jump
			if(shouldJump){
				if (this.canJump){
					this.canJump = false;
					this.AddMovement(new Vector3(0, this.jumpHeight, 0));
				}
			}
			else{
				//this.jumpStrength = 0.0f;
				this.canJump = true;
			}
			
			// If it's non-static and non-flying, apply gravity-based logic
			
			//if (!this.IsFlying && !this.IsStatic){
				
				// Adjust Jump strength back to normal
				/*if (this.jumpStrength <= 0.1f){
					this.jumpStrength = 0.0f;
				}
				else{
					this.jumpStrength -= this.jumpStrength * 0.01f;
				}*/
				
				// Check if there is an existing collider in from of the enemy
				/*bool shouldJump = !(Physics.Raycast( this.Position + (this.gameObject.transform.forward), 
					new Vector3(0.0f, -1.0f, 0.0f), 
					1.0f )
				);
				
				// If not, jump
				if(shouldJump){
					if (this.canJump){
						this.canJump = false;
						this.AddMovement(new Vector3(0, this.jumpHeight, 0));
					}
				}
				else{
					//this.jumpStrength = 0.0f;
					this.canJump = true;
				}*/
				
				//Debug.Log("jump strength: " + this.jumpStrength);
				//this.applyGravity();
			//}

			//fsmc.CurrentState.update(fsmc, this);
		}
		
		/*public void MoveToPosition(Vector3 targetPosition){
			if (fsmc.CurrentState.Name != "attack"){
				// Do not consider y in the target location
				this.TargetPosition = new Vector3(targetPosition.x, this.Position.y, targetPosition.z);
				this.TargetRotation = Quaternion.LookRotation(this.TargetPosition - this.Position);
				
				fsmc.dispatch("moveToPosition", this);
			}
		}*/
		
		protected override void Move (MoveCommand direction)
		{
			if (mc != MoveCommand.NONE && this.fsmc.CurrentState.Name != "attack"){
				if(direction == MoveCommand.LEFT)
				{
					this.gobj.transform.position -= new Vector3(this.moveSpeed * Time.deltaTime, 0, 0);
					this.gobj.transform.LookAt(this.gobj.transform.position + new Vector3(-1, 0, 0));
					this.globalForwardVector = new Vector3(-1, 0, 0);
				}
				else if(direction == MoveCommand.RIGHT)
				{
					this.gobj.transform.position += new Vector3(this.moveSpeed * Time.deltaTime, 0, 0);
					this.gobj.transform.LookAt(this.gobj.transform.position + new Vector3(1, 0, 0));
					this.globalForwardVector = new Vector3(1, 0, 0);
				}
			}
			/*
				if (mc == MoveCommand.LEFT || mc == MoveCommand.RIGHT){
					if ( this.IsGrounded ){
						if (this.fsmc.CurrentState.Name != "attack"){
							this.Move(mc);
							this.fsmc.dispatch("walk", this);
						}
					}else{
						this.Move(mc);
						this.fsmc.dispatch("walk", this);
					}
				}
				if (mc == MoveCommand.UP){
				}
				else if (mc == MoveCommand.DOWN){
				}
			}
			*/
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
		
		protected override void InitFSM(){
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
	}
}

