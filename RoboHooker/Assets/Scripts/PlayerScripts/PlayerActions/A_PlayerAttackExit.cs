using System;
using System.Collections.Generic;
using UnityEngine;
using Actors;

namespace FSM
{
    public class A_PlayerAttackExit:FSMAction
    {
        public override void execute(FSMContext fsmc, object o)
        {
			Player actor = (Player) o;
			actor.ActionTimer = 0.0f;
			actor.isFrozen = false;
			actor.HasAttacked = false;
			actor.attackCounter = 0;
			
			if (actor.Animation) 
				actor.Animation.Stop();
			if (actor.SocketedWeapon != null){
				Weapon weapon = actor.SocketedWeapon.gameObject.GetComponent<Weapon>();
				GameObject model = weapon.model;
				
				if (model.animation){
					model.animation.Stop();
				}
			}
        }
    }
}