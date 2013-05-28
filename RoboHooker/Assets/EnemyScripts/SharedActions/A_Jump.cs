using System;
using System.Collections.Generic;
using UnityEngine;
using Actors;

namespace FSM
{
    public class A_Jump:FSMAction
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
				if (actor.IsRanged){
					if (actor.Projectile != null){
						GameObject attackProjectile = actor.Projectile;
						GameObject newProjectile = (GameObject)GameObject.Instantiate(attackProjectile, actor.Hitbox.transform.position + actor.gameObject.transform.forward, Quaternion.identity);
						newProjectile.GetComponent<ProjectileScript>().Damage = actor.Damage;
						newProjectile.GetComponent<ProjectileScript>().Direction = (actor.TargetPosition - actor.Position).normalized;
						newProjectile.GetComponent<ProjectileScript>().Speed = actor.ProjectileSpeed;
						GameObject.Destroy(newProjectile, 1.0f);
					}
					actor.HasAttacked = true;
				}
				else{
					actor.Hitbox.collider.enabled = true;
					actor.HasAttacked = true;
				}
			}
			
			if ((actor.Hitbox.collider.enabled == true) && (actor.ActionTimer > (actor.AttackTime + animationLength))){
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