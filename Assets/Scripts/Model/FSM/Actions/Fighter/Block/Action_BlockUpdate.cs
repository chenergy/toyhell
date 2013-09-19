using System;
using System.Collections.Generic;
using ToyHell;
using FSM;
using UnityEngine;

namespace FSM
{
	public class Action_BlockUpdate:FSMAction
	{
		public override void execute(FSMContext c, object o){
			Fighter fighter;
			fighter = (Fighter)o;
			
			fighter.gobj.animation[ fighter.animationNameMap[FighterAnimation.BLOCK] ].wrapMode = UnityEngine.WrapMode.ClampForever;
			fighter.gobj.animation.CrossFade(fighter.animationNameMap[FighterAnimation.BLOCK]);
			
			if (fighter.currentAction == ActionCommand.NONE){
				c.dispatch("idle", o);
			}
		}
	}
}

