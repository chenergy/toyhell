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
			
			if (actor.SocketedWeapon != null){
				Weapon weapon = actor.SocketedWeapon.gameObject.GetComponent<Weapon>();
				GameObject model = weapon.model;
				float attackDelay = weapon.attackDelay;
				float attackSpeed = weapon.attackSpeed;
				GameObject hitbox = weapon.hitbox;
				int attacks = weapon.attacks;
				bool canKnockback = weapon.canKnockback;
				
				if (actor.Animation){
					if (actor.Animation["SocketAttack"]){
						animationLength = actor.Animation["SocketAttack"].clip.length * (1.0f/attackSpeed);
						actor.Animation["SocketAttack"].speed = attackSpeed;
						actor.Animation.CrossFade("SocketAttack");
					}
				}
				
				
				if (model.animation){
					if (model.animation["Attack"]){
						model.animation["Attack"].speed = attackSpeed;
						model.animation.CrossFade("Attack");
					}
				}
				
				if (!actor.HasAttacked && (actor.ActionTimer > attackDelay)){
					//hitbox.collider.enabled = true;
					//hitbox.renderer.enabled = true;
					weapon.StartAttack();
					actor.HasAttacked = true;
				}
				
				if (actor.ActionTimer > animationLength/attacks){
					//hitbox.collider.enabled = false;
					//hitbox.renderer.enabled = false;
					weapon.EndAttack();
					actor.HasAttacked = false;
					actor.ActionTimer = 0.0f;
					actor.attackCounter++;
				}
				
				if (actor.attackCounter >= attacks){
					fsmc.dispatch("idle", o);
				}
				
			}
			
			if (!actor.IsGrounded){
				actor.isFrozen = false;
			}
			/*
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
			*/
			if (actor.ActionTimer > (animationLength)){
				//actor.Hitbox.collider.enabled = false;
				//actor.Hitbox.renderer.enabled = false;
				fsmc.dispatch("idle", o);
			}
			actor.ActionTimer += Time.deltaTime;
        }
    }
}