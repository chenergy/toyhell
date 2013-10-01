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
		protected float 		patrolPauseTime;
		protected GameObject	currentPatrolTarget;
		protected float			agroRange;
		protected bool			canJump;
		
		public Enemy_Moving ( GameObject gobj ) : base( gobj )
		{
			MovingEnemyInput input 	= gobj.GetComponent<MovingEnemyInput>();
			this.controller			= gobj.GetComponent<CharacterController>();
			this.animationNameMap	= input.animationNameMap;
			this.moveSpeed			= input.moveSpeed;
			this.name				= input.name;
			this.socketJoint		= input.socketJoint;
			this.jumpHeight			= input.jumpHeight;
			this.agroRange			= input.agroRange;
			this.attackRange		= input.attackRange;
			this.damage				= input.damage;
			this.knockbackStrength	= input.knockbackStrength;
			this.patrolPoint1		= input.patrolPoint1;
			this.patrolPoint2		= input.patrolPoint2;
			this.patrolPauseTime	= input.patrolPauseTime;
			
			this.canJump 				= false;
			this.currentPatrolTarget 	= patrolPoint1;
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
						if (fighter.gobj.transform.position.x > this.gobj.transform.position.x){
							this.Move( MoveCommand.RIGHT );
						}
						else{
							this.Move( MoveCommand.LEFT );
						}
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
			
			bool shouldJump = !this.controller.isGrounded;
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
			this.AddGravity();
			base.Update();
		}
		
		/*public void MoveToPosition(Vector3 targetPosition){
			if (fsmc.CurrentState.Name != "attack"){
				// Do not consider y in the target location
				this.TargetPosition = new Vector3(targetPosition.x, this.Position.y, targetPosition.z);
				this.TargetRotation = Quaternion.LookRotation(this.TargetPosition - this.Position);
				
				fsmc.dispatch("moveToPosition", this);
			}
		}*/
		
		// Checks if self should pursue player
		protected bool IsFighterInAgroRange(Fighter fighter){
			float distance = (this.gobj.transform.position - fighter.gobj.transform.position).magnitude;
			return (distance < this.agroRange);
		}
		
		protected void Move (MoveCommand direction)
		{
			if (mc != MoveCommand.NONE && this.fsmc.CurrentState.Name != "attack"){
				if(direction == MoveCommand.LEFT)
				{
					this.controller.transform.Translate(-new Vector3(this.moveSpeed * Time.deltaTime, 0, 0));
					this.gobj.transform.LookAt(this.gobj.transform.position + new Vector3(-1, 0, 0));
					this.globalForwardVector = new Vector3(-1, 0, 0);
				}
				else if(direction == MoveCommand.RIGHT)
				{
					this.controller.transform.Translate(new Vector3(this.moveSpeed * Time.deltaTime, 0, 0));
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
		
		public void Patrol(){
			// Do not consider y in the target location
			Vector3 patrolTargetPosition = new Vector3(this.currentPatrolTarget.transform.position.x, this.gobj.transform.position.y, this.currentPatrolTarget.transform.position.z);
			float distance = (patrolTargetPosition - this.gobj.transform.position).magnitude;
			
			if (distance < 0.2f){
				if (this.globalActionTimer < this.patrolPauseTime){	// Gets within distance of a patrol point and idles
					this.globalActionTimer += Time.deltaTime;
					this.fsmc.dispatch("idle", this);
				}
				
				else{
					this.globalActionTimer = 0.0f;
					// Move target between patrol points 1 and 2
					this.currentPatrolTarget = (this.currentPatrolTarget == this.patrolPoint1) ? this.patrolPoint2 : this.patrolPoint1;
				}
			}
			else{
				//this.MoveToPosition(this.PatrolTarget.transform.position);
				MoveCommand direction = (this.currentPatrolTarget.transform.position.x > this.gobj.transform.position.x) ? MoveCommand.RIGHT : MoveCommand.LEFT;
				this.Move( direction );
			}
		}
		
		protected override void InitStateMachine(){
			FSMAction noAction 		= new A_None();
			
			State S_Idle 			= new State("idle", new A_IdleEnter(), new A_Idle(), new A_IdleExit());
			/*
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
			*/
			this.fsmc = FSM.FSM.createFSMInstance(S_Idle, noAction);
		}
	}
}

