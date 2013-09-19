using System;
using System.Collections.Generic;
using ToyHell;
using FSM;
using UnityEngine;

namespace FSM
{
	public class Action_AttackUpdate:FSMAction
	{
		public override void execute(FSMContext c, object o)
		{
			Fighter fighter 	= (Fighter)o;
			Weapon weapon		= fighter.currentWeapon;

			if (weapon != null){
				weapon.Execute();
				if( weapon.CheckComplete() ) { 
					c.dispatch("idle", o); 
				}
			}
			
			if (fighter.globalActionTimer >= fighter.gobj.animation["SocketAttack"].clip.length){
				c.dispatch("idle", o);
			}
			
			fighter.gobj.animation.CrossFade("SocketAttack");
			fighter.globalActionTimer += Time.deltaTime;
			
			/*
			A_Attack 	attack 	= fighter.currentAttack;
			
			foreach (A_HitBoxInstruction hbi in attack.instructions){
				if (attack.timer < hbi.startTime / attack.animationSpeed){
					hbi.hitbox.Disable();
				}
				else if ((attack.timer >= hbi.startTime / attack.animationSpeed) && (attack.timer <= hbi.endTime / attack.animationSpeed)){
					hbi.Execute();
				}
				else if (attack.timer >= hbi.endTime / attack.animationSpeed){
					hbi.Reset();
				}
			}
			//Debug.Log(attack.timer.ToString() + " > " + attack.attackLength.ToString());
			if(attack.timer >= attack.attackLength / attack.animationSpeed)
			{
				attack.timer = 0.0f;
				attack.Reset();
				c.dispatch("idle", o);
			}
			
			fighter.gobj.animation.CrossFade(attack.animationName);
			attack.Execute();
			*/
		}
	}
}