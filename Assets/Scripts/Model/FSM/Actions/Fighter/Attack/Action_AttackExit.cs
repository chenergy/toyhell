using System;
using System.Collections.Generic;
using ToyHell;
using FSM;
using UnityEngine;

namespace FSM
{
	public class Action_AttackExit:FSMAction
	{
		public override void execute(FSMContext c, object o){
			Fighter fighter = (Fighter)o;
			fighter.gobj.animation.Stop();
			
			if (fighter.currentWeapon != null){
				fighter.currentWeapon.StopAnimation();
				fighter.currentWeapon.Reset();
			}
			fighter.globalActionTimer = 0.0f;
		}
	}
}

