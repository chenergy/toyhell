using UnityEngine;
using System;
using System.Collections.Generic;
using FSM;
using ToyHell;

namespace ToyHell
{
	public abstract class Enemy_Static : A_Enemy
	{
		public Enemy_Static ( GameObject gobj ) : base( gobj )
		{
		}
		
		public override void Update ()
		{
			base.Update ();
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

