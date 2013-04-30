using System;
using System.Collections.Generic;
using UnityEngine;
using Actors;

namespace FSM
{
    public class A_Attack:FSMAction
    {
        public override void execute(FSMContext fsmc, object o)
        {
			Actor actor = (Actor) o;
			
			if (!actor.HasAttacked && (actor.ActionTimer > actor.AttackTime)){
				
				GameObject hitbox = (GameObject)UnityEngine.Object.Instantiate(actor.Hitbox, actor.Position + actor.AttackOffset, Quaternion.identity);
				
				hitbox.GetComponent<HitboxScript>().damage = actor.Damage;
				hitbox.GetComponent<HitboxScript>().attackLength = actor.AttackLength;
				Debug.Log("Enemy weapon damage:" + (actor.Damage));
				
				Physics.IgnoreCollision(actor.controller, hitbox.collider);
				//Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Enemies"), LayerMask.NameToLayer("Projectiles"));
				
				actor.HasAttacked = true;
			}
			
			if (actor.ActionTimer > 5.0f){
				fsmc.dispatch("idle", o);
			}
			actor.ActionTimer += Time.deltaTime;
        }
    }
}