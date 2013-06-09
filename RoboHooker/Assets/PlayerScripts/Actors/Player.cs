using UnityEngine;
using System;
using System.Collections.Generic;
using FSM;
using Actors;

namespace Actors
{
	public class Player : Actor
	{
		public Player(Dictionary<string, object> attributes){
			FSMAction noAction 		= new A_None();
			
			State S_Idle 			= new State("idle", new A_IdleEnter(), new A_Idle(), new A_IdleExit());
			State S_MoveToPosition	= new State("moveToPosition", new A_MoveToPositionEnter(), new A_MoveToPosition(), new A_MoveToPositionExit());
			State S_Attack 			= new State("attack", new A_PlayerAttackEnter(), new A_PlayerAttack(), new A_PlayerAttackExit());
			State S_Death 			= new State("death", new A_PlayerDeathEnter(), new A_PlayerDeath(), new A_PlayerDeathExit());
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
			
			this.fsmc = FSM.FSM.createFSMInstance(S_Idle, noAction);
			this.attributes = attributes;
			
			this.jumpStrength = 0.0f;
			this.canJump = false;
		}
		
		
		public override void Update(){
		}
		
		#region attribute getters and setters
		public GameObject SocketedWeapon{
			get{ return (GameObject)attributes["socketedWeapon"]; }
			set{ attributes["socketedWeapon"] = value; }
		}
		#endregion
		
		
	}
}