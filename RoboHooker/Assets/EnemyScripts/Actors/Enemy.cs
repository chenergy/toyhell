using UnityEngine;
using System;
using System.Collections.Generic;
using FSM;
using Actors;

namespace Actors
{
	public class Enemy : Actor
	{
		public Enemy(Dictionary<string, object> attributes){
			FSMAction noAction 		= new A_None();
			
			State S_Idle 			= new State("idle", new A_IdleEnter(), new A_Idle(), new A_IdleExit());
			State S_MoveToPosition	= new State("moveToPosition", new A_MoveToPositionEnter(), new A_MoveToPosition(), new A_MoveToPositionExit());
			State S_Attack 			= new State("attack", new A_AttackEnter(), new A_Attack(), new A_AttackExit());
			State S_Death 			= new State("death", new A_DeathEnter(), new A_Death(), new A_DeathExit());
			State S_Jump			= new State("jump", new A_JumpEnter(), new A_Jump(), new A_JumpExit());
			
			Transition T_Idle 			= new Transition(S_Idle, noAction);
			Transition T_MoveToPosition	= new Transition(S_MoveToPosition, noAction);
			Transition T_Attack			= new Transition(S_Attack, noAction);
			Transition T_Death			= new Transition(S_Death, noAction);
			Transition T_Jump			= new Transition(S_Jump, noAction);
			
			S_Idle.addTransition(T_MoveToPosition, "moveToPosition");
			S_Idle.addTransition(T_Attack, "attack");
			S_Idle.addTransition(T_Death, "death");
			S_Idle.addTransition(T_Jump, "jump");
			
			S_MoveToPosition.addTransition(T_Idle, "idle");
			S_MoveToPosition.addTransition(T_Attack, "attack");
			S_MoveToPosition.addTransition(T_Death, "death");
			S_MoveToPosition.addTransition(T_MoveToPosition, "moveToPosition");
			S_MoveToPosition.addTransition(T_Jump, "jump");
			
			S_Attack.addTransition(T_Idle, "idle");
			S_Attack.addTransition(T_Death, "death");
			
			S_Jump.addTransition(T_Idle, "idle");
			
			this.HookerData = new PlayerData(GameData.Hooker);
			this.RobotData = new PlayerData(GameData.Robot);
			
			this.fsmc = FSM.FSM.createFSMInstance(S_Idle, noAction);
			this.attributes = attributes;
		}
		
		
		public override void Update ()
		{
			UpdatePlayerData(HookerData);
			UpdatePlayerData(RobotData);
			
			bool isMoving = false;
			
			if (checkAttack(HookerData)){
				this.Attack(HookerData.position);
				isMoving = true;
			}
			else if (checkMovement(HookerData) && !isMoving && !this.IsStatic){
				this.MoveToPosition(HookerData.position);
				isMoving = true;
			}
			else if (checkAttack(RobotData) && !isMoving){
				this.MoveToPosition(RobotData.position);
				isMoving = true;
			}
			else if (checkMovement(RobotData) && !isMoving && !this.IsStatic){
				this.MoveToPosition(RobotData.position);
				isMoving = true;
			}
			else if (!this.IsStatic){
				this.Patrol(this.PatrolPauseTime);
			}
			
			checkDeath();
			
			if (!this.IsFlying && !this.IsStatic){
				Debug.Log(this.gameObject.name + " is Flying: " + this.IsFlying);
				applyGravity();
			}
			
			fsmc.CurrentState.update(fsmc, this);
		}
		
		bool checkAttack(PlayerData data){
			// todo: check to see the hooker or robot can be reassigned and reassign if possible
			// this will prevent the ai from breaking if the robot dies.
			// note: the way respawning is implemented - hooker and robot gameobjects are not destroyed, simply
			// rendering and collision is turned off, then relocated
			
			if (data.distance < this.AttackRange){
				return true;
			}
			return false;
		}
		
		bool checkMovement(PlayerData data){
			if (!this.IsStatic){
				if (data.distance < this.AgroRange){
					return true;
				}
			}
			return false;
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
		
		public void Patrol(float patrolPauseTime){
			// Do not consider y in the target location
			Vector3 PatrolTargetPosition = new Vector3(this.PatrolTarget.transform.position.x, this.Position.y, this.PatrolTarget.transform.position.z);
			float distance = (PatrolTargetPosition - this.Position).magnitude;
			
			if (distance < 0.2f){
				
				if (this.ActionTimer < patrolPauseTime){
					this.ActionTimer += Time.deltaTime;
					this.fsmc.dispatch("idle", this);
				}
				
				else{
					this.ActionTimer = 0.0f;
					if (this.PatrolTarget == this.PatrolPoint1){
						Debug.Log("Changed To point2");
						this.PatrolTarget = this.PatrolPoint2;
					}
					else{
						Debug.Log("Changed To point1");
						this.PatrolTarget = this.PatrolPoint1;
					}
				}
			}
			else{
				this.MoveToPosition(this.PatrolTarget.transform.position);
			}
		}
	}
}