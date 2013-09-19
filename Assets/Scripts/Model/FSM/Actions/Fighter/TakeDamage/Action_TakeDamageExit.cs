using System;
using System.Collections.Generic;
using ToyHell;
using FSM;
using UnityEngine;

namespace FSM
{
	public class Action_TakeDamageExit:FSMAction
	{
		public override void execute(FSMContext c, object o){
			Fighter fighter;
			fighter = (Fighter)o;
			GameObject gobj = fighter.gobj;
			
			//fighter.hurtLocation = Location.NONE;
			fighter.globalActionTimer = 0.0f;
		}
	}
}

