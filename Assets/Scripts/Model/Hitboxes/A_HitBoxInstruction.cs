using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using ToyHell;

namespace ToyHell{
	public abstract class A_HitBoxInstruction
	{
		protected Fighter	fighter;
		//protected HitBox	hitbox;
		protected float		startTime;
		protected float		endTime;
		protected Vector3	movement;
		protected Vector3	knockback;
		protected int		damage;
		protected bool		started;
		
		protected A_HitBoxInstruction(Fighter fighter, int damage, float startTime, float endTime, Vector3 movement, Vector3 knockback){
			this.fighter		= fighter;
			//this.weapon			= weapon;
			this.startTime 		= startTime;
			this.endTime 		= endTime;
			this.movement		= movement;
			this.knockback		= knockback;
			//this.hitbox			= hitbox;
			this.damage			= damage;
			this.started		= false;
		}
		
		//public abstract void Init();
		public abstract void Disable();
		
		public virtual void Start(){
			this.fighter.AddMovement( this.movement );
			this.started = true;
		}
		
		public virtual void Execute(){
			if (!this.started){
				this.Start();
			}
		}
		
		public virtual void Reset(){
			this.started = false;
		}
		
		public float StartTime{
			get { return this.startTime; }
		}
		
		public float EndTime{
			get { return this.endTime; }
		}
	}
}