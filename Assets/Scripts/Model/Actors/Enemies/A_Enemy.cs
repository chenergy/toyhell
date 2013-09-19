using UnityEngine;
using System;
using System.Collections.Generic;
using FSM;
using ToyHell;

namespace ToyHell
{
	public abstract class A_Enemy : A_Actor
	{
		//protected GameObject 			gobj;
		//protected CharacterController 	controller;
		//protected Vector3				targetPosition;
		//protected Quaternion			targetRotation;
		//protected float					jumpPower;
		//protected float					moveSpeed;
		//protected float					turnSpeed;
		//protected float					actionTimer;
		protected float					attackSpeed;
		protected GameObject			deathParts;
		protected float					knockbackStrength;
		protected bool					hasAttacked;
		protected float					fadeTime;
		protected float					agroRange;
		protected float					attackRange;
		protected float					damage;
		//protected float				attackTime;
		protected GameObject			drop;
		protected GameObject			hitbox;
		
		//protected bool					isFlying;
		//protected bool					isRanged;
		//protected bool					isStatic;
		
		//protected GameObject			patrolPoint1;
		//protected GameObject			patrolPoint2;
		//protected GameObject			patrolTarget;
		//protected GameObject			targetPlayer;
		
		
		protected float					currentHp;
		protected float					maxHp;
		
		//protected FSMContext 			fsmc;
		
		//protected float 				jumpStrength;
		protected bool					canJump;
		
		//protected Dictionary<FighterAnimation, string> animationNameMap;
		
		protected A_Enemy( GameObject gobj ) : base( gobj ) { }
		
		// Checks if self is close enough to attack
		protected bool IsFighterInAttackRange(Fighter fighter){
			float distance = Mathf.Abs( this.gobj.transform.position - fighter.gobj.transform.position );
			return (distance < this.attackRange);
		}
		
		// Checks if self should pursue player
		protected bool IsFighterInAgroRange(Fighter fighter){
			float distance = Mathf.Abs( this.gobj.transform.position - fighter.gobj.transform.position );
			return (distance < this.agroRange);
		}
		
		public string StateName{
			get{ return fsmc.CurrentState.Name; }
		}
		
		protected void Attack(Vector3 targetPosition){
			fsmc.dispatch("attack", this);
		}
		
		protected void Death(){
			fsmc.dispatch("death", this);
		}
		
		protected void Hurt(){
			fsmc.dispatch("hurt", this);
		}
		
		
	}
}

