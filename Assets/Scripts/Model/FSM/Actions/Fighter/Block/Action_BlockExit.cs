using System;
using System.Collections.Generic;
using ToyHell;
using FSM;

namespace FSM
{
	public class Action_BlockExit:FSMAction
	{
		public override void execute(FSMContext c, object o){
			Fighter fighter = (Fighter)o;
			
			fighter.gobj.animation[fighter.animationNameMap[FighterAnimation.BLOCK]].wrapMode = UnityEngine.WrapMode.Default;
		}
	}
}

