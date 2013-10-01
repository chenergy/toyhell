using UnityEngine;
using System;
using System.Collections.Generic;
using FSM;
using ToyHell;

namespace ToyHell
{
	public abstract class A_Enemy : A_Actor
	{
		// From EnemyInput
		protected GameObject			deathParts;
		protected GameObject			drop;
		protected float					attackSpeed;
		protected float					knockbackStrength;
		protected float					fadeTime;
		protected float					attackRange;
		protected float					damage;	
		
		protected bool					hasAttacked;
		protected float					currentHp;
		protected float					maxHp;

		protected A_Enemy( GameObject gobj ) : base( gobj ) { }
		
		// Checks if self is close enough to attack
		protected bool IsFighterInAttackRange(Fighter fighter){
			float distance = ( this.gobj.transform.position - fighter.gobj.transform.position ).magnitude;
			return (distance < this.attackRange);
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

