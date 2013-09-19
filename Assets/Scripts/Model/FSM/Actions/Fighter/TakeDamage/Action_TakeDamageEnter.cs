using System;
using System.Collections.Generic;
using ToyHell;
using FSM;
using UnityEngine;

namespace FSM
{
	public class Action_TakeDamageEnter:FSMAction
	{
		public override void execute(FSMContext c, object o){
			Fighter fighter = (Fighter)o;				
			GameObject gobj = fighter.gobj;
			
			fighter.globalActionTimer = 0.0f;
			
			gobj.animation.Stop();
		}
	}
}


