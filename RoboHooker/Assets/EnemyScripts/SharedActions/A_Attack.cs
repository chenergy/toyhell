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
			float animationLength = 3.0f;
			
			if (actor.Animation){
				if (actor.Animation["Attack"]){
					animationLength = actor.Animation["Attack"].clip.length;
					actor.Animation.CrossFade("Attack");
				}
			}
			// Quick Rotation
			actor.controller.transform.LookAt(actor.TargetPosition);
			Debug.DrawLine(actor.Position, actor.TargetPosition);
			
			if (!actor.HasAttacked && (actor.ActionTimer > actor.AttackTime)){
				actor.Hitbox.collider.enabled = true;
				//actor.Hitbox.renderer.enabled = true; // Enable for Debugging
				actor.HasAttacked = true;
				//Debug.Log(actor.Hitbox.GetComponent<HitboxScript>().Damage);
				//Debug.Break();
			}
			
			if ((actor.Hitbox.collider.enabled == true) && (actor.ActionTimer > (actor.AttackTime + actor.AttackLength))){
				actor.Hitbox.collider.enabled = false;
				actor.Hitbox.renderer.enabled = false;
			}
			
			if (actor.ActionTimer > animationLength){
				fsmc.dispatch("idle", o);
			}
			actor.ActionTimer += Time.deltaTime;
        }
    }
}