using System;
using System.Collections.Generic;
using ToyHell;
using FSM;
using UnityEngine;

namespace FSM
{
	public class Action_IdleUpdate:FSMAction
	{
		public override void execute(FSMContext c, object o){
			Fighter fighter = (Fighter)o;
			if (fighter.IsGrounded){
				fighter.gobj.animation.CrossFade(fighter.animationNameMap[FighterAnimation.IDLE]);
			}else{
				fighter.gobj.animation.CrossFade(fighter.animationNameMap[FighterAnimation.JUMP]);
			}
		}
	}
}

