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
			
			// Quick Rotation
			actor.controller.transform.LookAt(actor.TargetPosition);
			
			if (!actor.HasAttacked && (actor.ActionTimer > actor.AttackTime)){
				actor.Hitbox.collider.enabled = true;
				actor.Hitbox.renderer.enabled = true;
				actor.HasAttacked = true;
				//Debug.Log(actor.Hitbox.GetComponent<HitboxScript>().Damage);
				//Debug.Break();
			}
			
			if ((actor.Hitbox.collider.enabled == true) && (actor.ActionTimer > (actor.AttackTime + actor.AttackLength))){
				actor.Hitbox.collider.enabled = false;
				actor.Hitbox.renderer.enabled = false;
			}
			
			if (actor.ActionTimer > 3.0f){
				fsmc.dispatch("idle", o);
			}
			actor.ActionTimer += Time.deltaTime;
        }
    }
}