using UnityEngine;
using System;
using System.Collections.Generic;
using FSM;
using Actors;

namespace Actors{
	class Enemy : Actor
	{
		public Enemy(Dictionary<string, object> attributes){
			FSMAction noAction 		= new A_None();
			
			State S_Idle 			= new State("idle", new A_IdleEnter(), new A_Idle(), new A_IdleExit());
			State S_MoveToPosition	= new State("moveToPosition", new A_MoveToPositionEnter(), new A_MoveToPosition(), new A_MoveToPositionExit());
			State S_Attack 			= new State("attack", new A_AttackEnter(), new A_Attack(), new A_AttackExit());
			State S_Death 			= new State("death", new A_DeathEnter(), new A_Death(), new A_DeathExit());
			
			Transition T_Idle 			= new Transition(S_Idle, noAction);
			Transition T_MoveToPosition	= new Transition(S_MoveToPosition, noAction);
			Transition T_Attack			= new Transition(S_Attack, noAction);
			Transition T_Death			= new Transition(S_Death, noAction);
			
			S_Idle.addTransition(T_MoveToPosition, "moveToPosition");
			S_Idle.addTransition(T_Attack, "attack");
			S_Idle.addTransition(T_Death, "death");
			
			S_MoveToPosition.addTransition(T_Idle, "idle");
			S_MoveToPosition.addTransition(T_Attack, "attack");
			S_MoveToPosition.addTransition(T_Death, "death");
			
			S_Attack.addTransition(T_Idle, "idle");
			S_Attack.addTransition(T_Death, "death");
			
			this.fsmc = FSM.FSM.createFSMInstance(S_Idle, noAction);
			this.attributes = attributes;
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
		
		public float AttackRange{
			get{ return (float)attributes["attackRange"]; }
			set{ attributes["attackRange"] = value; }
		}
		
		public void Patrol(float patrolPauseTime){
			// Do not consider y in the target location
			Vector3 PatrolTargetPosition = new Vector3(this.PatrolTarget.transform.position.x, this.Position.y, this.PatrolTarget.transform.position.z);
			float distance = (PatrolTargetPosition - this.Position).magnitude;
			
			if (distance < 0.2f){
				
				if (this.ActionTimer < patrolPauseTime){
					//Debug.Log(this.ActionTimer);
					this.ActionTimer += Time.deltaTime;
					this.fsmc.dispatch("idle", this);
				}
				
				else{
					//this.fsmc.dispatch("moveToPosition", this);
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
			
			//Debug.Log("Patrol Target: " + this.PatrolTarget.transform.position);
		}
	}
}