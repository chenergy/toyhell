using UnityEngine;
using System;
using System.Collections.Generic;
using FSM;
using Actors;

namespace Actors
{
	public class Player : Actor
	{
		public bool isClimbing;
		public bool isMoving;
		public int	attackCounter;
		
		public Player(Dictionary<string, object> attributes){
			FSMAction noAction 		= new A_None();
			
			State S_Idle 			= new State("idle", new A_PlayerIdleEnter(), new A_PlayerIdle(), new A_PlayerIdleExit());
			State S_MoveToPosition	= new State("move", new A_PlayerMoveEnter(), new A_PlayerMove(), new A_PlayerMoveExit());
			State S_Attack 			= new State("attack", new A_PlayerAttackEnter(), new A_PlayerAttack(), new A_PlayerAttackExit());
			State S_Death 			= new State("death", new A_PlayerDeathEnter(), new A_PlayerDeath(), new A_PlayerDeathExit());
			State S_Hurt			= new State("hurt", new A_HurtEnter(), new A_Hurt(), new A_HurtExit());
			State S_Jump 			= new State("jump", new A_PlayerJumpEnter(), new A_PlayerJump(), new A_PlayerJumpExit());
			
			Transition T_Idle 			= new Transition(S_Idle, noAction);
			Transition T_MoveToPosition	= new Transition(S_MoveToPosition, noAction);
			Transition T_Attack			= new Transition(S_Attack, noAction);
			Transition T_Death			= new Transition(S_Death, noAction);
			Transition T_Hurt			= new Transition(S_Hurt, noAction);
			Transition T_Jump			= new Transition(S_Jump, noAction);
			
			S_Idle.addTransition(T_MoveToPosition, "move");
			S_Idle.addTransition(T_Attack, "attack");
			S_Idle.addTransition(T_Death, "death");
			S_Idle.addTransition(T_Hurt, "hurt");
			S_Idle.addTransition(T_Jump, "jump");
			
			S_MoveToPosition.addTransition(T_Idle, "idle");
			S_MoveToPosition.addTransition(T_Attack, "attack");
			S_MoveToPosition.addTransition(T_Death, "death");
			S_MoveToPosition.addTransition(T_MoveToPosition, "move");
			S_MoveToPosition.addTransition(T_Hurt, "hurt");
			S_MoveToPosition.addTransition(T_Jump, "jump");
			
			S_Attack.addTransition(T_Idle, "idle");
			S_Attack.addTransition(T_Death, "death");
			S_Attack.addTransition(T_Hurt, "hurt");
			
			S_Hurt.addTransition(T_Idle, "idle");
			S_Hurt.addTransition(T_Death, "death");
			S_Hurt.addTransition(T_Hurt, "hurt");
			
			S_Jump.addTransition(T_Idle, "idle");
			S_Jump.addTransition(T_Death, "death");
			S_Jump.addTransition(T_Hurt, "hurt");
			
			this.fsmc = FSM.FSM.createFSMInstance(S_Idle, noAction);
			this.attributes = attributes;
			
			this.jumpStrength 	= 0.0f;
			this.isClimbing		= false;
			this.attackCounter	= 0;
		}
		
		
		public override void Update(){
			fsmc.CurrentState.update(fsmc, this);

			Debug.DrawLine(this.Position, this.Position + new Vector3(0.0f, -0.2f, 0.0f));
			//Debug.Log(this.gameObject.name + " State: " + fsmc.CurrentState.Name);

			if (!this.isClimbing && !this.IsGrounded){
				applyGravity();
			}
			
			if (this.isClimbing){ 
				if (!this.isMoving){
					this.extraMovement.y = 0;
				}
			}
			
			this.ApplyExtraMovement();
		}
		
		public void Jump(){	
			if (this.IsGrounded){
				if (this.extraMovement.y == 0.0f){
					this.extraMovement.y	= this.JumpPower;
					fsmc.dispatch("jump", this);
				}
			}
		}
		
		public void MoveX(float direction){
			Vector3 targetPosition = (this.Position + new Vector3(direction, 0, 0));
			Quaternion targetRotation = Quaternion.LookRotation((this.Position + new Vector3(direction, 0, 0)) - this.Position);
			
			// Quick Rotation
			this.controller.transform.LookAt(targetPosition);
			
			// Slow Rotation
			if (Quaternion.Angle(this.Rotation, targetRotation) > 5.0f){
				this.Rotation = Quaternion.Slerp(this.Rotation, targetRotation, Time.deltaTime * 
					(this.TurnSpeed));
			}
			fsmc.dispatch("move", this);
			
			this.isMoving = true;
			this.extraMovement.x = direction * this.MoveSpeed;
		}
		
		public void MoveY(float direction){
			Vector3 targetPosition = (this.Position + new Vector3(0, direction, 0));

			// Quick Rotation
			this.gameObject.transform.LookAt(this.Position + new Vector3(0.0f, 0.0f, 1.0f));

			fsmc.dispatch("move", this);
			
			this.isMoving = true;
			this.extraMovement.y = direction * this.MoveSpeed;
		}
		
		public void AllowJump(){
			this.extraMovement.y 	= 0.0f;
		}
		
		protected override void applyGravity(){
			this.extraMovement.y += Physics.gravity.y * Time.deltaTime;
		}
		
		public void Idle(){
			if (fsmc.CurrentState.Name != "attack"){
				fsmc.dispatch("idle", this);
			}
		}
		
		public void Knockback(Vector3 knockback){
			this.extraMovement.x = knockback.x;
		}
		
		#region attribute getters and setters
		
		public GameObject SocketedWeapon{
			get{ return (GameObject)attributes["socketedWeapon"]; }
			set{ attributes["socketedWeapon"] = value; }
		}
		
		#endregion
		
		
	}
}