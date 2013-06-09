using System;
using System.Collections.Generic;
using UnityEngine;
using Actors;

namespace FSM
{
    public class A_PlayerAttack:FSMAction
    {
        public override void execute(FSMContext fsmc, object o)
        {
			Player actor = (Player) o;
			float animationLength = 3.0f;
			/*
			if (actor.Animation){
				if (actor.Animation["Attack"]){
					animationLength = actor.Animation["Attack"].clip.length * (1.0f/actor.AttackSpeed);
					actor.Animation["Attack"].speed = actor.AttackSpeed;
					actor.Animation.CrossFade("Attack");
				}
			}
			
			// Quick Rotation
			actor.controller.transform.LookAt(actor.TargetPosition);
			Debug.DrawLine(actor.Position, actor.TargetPosition);
			
			// Check to see if the enemy has attacked yet
			if (!actor.HasAttacked && (actor.ActionTimer > actor.AttackTime)){
				if (actor.IsRanged){
					if (actor.Projectile != null){
						GameObject attackProjectile = actor.Projectile;
						GameObject newProjectile = (GameObject)GameObject.Instantiate(attackProjectile, actor.Hitbox.transform.position, Quaternion.identity);
						
						// Enemy Shoots Forward
						//newProjectile.GetComponent<ProjectileScript>().Direction = (actor.TargetPosition - actor.Position).normalized;
						
						// Enemy Shoots Toward Player
						// START
						newProjectile.transform.LookAt(actor.TargetPlayer.transform.position + new Vector3(0.0f, actor.controller.height/2.0f, 0.0f));
						newProjectile.GetComponent<ProjectileScript>().Direction = newProjectile.transform.forward;
						// END
						
						newProjectile.GetComponent<ProjectileScript>().Damage = actor.Damage;
						newProjectile.GetComponent<ProjectileScript>().Speed = actor.ProjectileSpeed;
						newProjectile.GetComponent<ProjectileScript>().Source = (Enemy)actor;
						GameObject.Destroy(newProjectile, actor.ProjectileDuration);
					}
					actor.HasAttacked = true;
				}
				else{
					actor.Hitbox.collider.enabled = true;
					actor.HasAttacked = true;
				}
			}
			
			if (actor.ActionTimer > (animationLength)){
				actor.Hitbox.collider.enabled = false;
				actor.Hitbox.renderer.enabled = false;
				fsmc.dispatch("idle", o);
			}
			actor.ActionTimer += Time.deltaTime;
			*/
        }
    }
}