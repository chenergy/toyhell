using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using ToyHell;

namespace ToyHell{
	public class MeleeHitBoxInstruction : A_HitBoxInstruction
	{
		//private Vector3		offset;
		public MeleeHitBoxInstruction(Fighter fighter, int damage, float startTime, float endTime, Vector3 movement = default(Vector3), Vector3 knockback = default(Vector3)): base(fighter, damage, startTime, endTime, movement, knockback) {
		}
		/*
		public MeleeHitBoxInstruction( Fighter fighter, Weapon weapon, float radius, int damage, float startTime, float endTime, HitBox hitbox, Vector3 movement = default(Vector3), Vector3 knockback = default(Vector3)) : base(fighter, weapon, radius, damage, startTime, endTime, hitbox, movement, knockback){
		}
		*/
		/*public override void Init(){
		}*/
		
		public override void Start(){
			HitBox hitbox = this.fighter.currentWeapon.HitBox;
			hitbox.Enable();
			hitbox.damage = this.damage;
			hitbox.knockback = this.knockback;
			//this.hitbox.SetRadius(this.radius);
			//this.hitbox.SetKnockback(this.knockback);
			/*
			if (this.hitbox.damage != this.damage){ 
				this.hitbox.damage = this.damage; 
			}
			*/
			base.Start();
		}
		/*
		public override void Execute ()
		{
			base.Execute();
			hitbox.gobj.transform.position = joint.position + joint.TransformDirection(this.offset);
		}
		*/
		public override void Reset(){
			HitBox hitbox = this.fighter.currentWeapon.HitBox;
			base.Reset();
			hitbox.Reset();
		}
		
		public override void Disable(){
			HitBox hitbox = this.fighter.currentWeapon.HitBox;
			hitbox.Disable();
		}
	}
}