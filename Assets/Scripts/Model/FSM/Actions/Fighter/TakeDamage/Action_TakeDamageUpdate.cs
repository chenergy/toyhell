using System;
using System.Collections.Generic;
using ToyHell;
using FSM;
using UnityEngine;

namespace FSM
{
	public class Action_TakeDamageUpdate:FSMAction
	{
		public override void execute(FSMContext c, object o){
			Fighter fighter;
			fighter = (Fighter)o;				
			GameObject gobj = fighter.gobj;
			
			FighterAnimation animation = FighterAnimation.HURT;
			
			if(fighter.globalActionTimer > gobj.animation[fighter.animationNameMap[animation]].length)
			{
				c.dispatch("idle", o);
			}
			
			fighter.globalActionTimer += UnityEngine.Time.deltaTime;
			gobj.animation.Play(fighter.animationNameMap[animation]);
				
		}
	}
}

