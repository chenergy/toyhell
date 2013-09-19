using System;
using System.Collections.Generic;
using ToyHell;
using FSM;
using UnityEngine;
namespace FSM
{
	public class Action_ClimbUpdate:FSMAction
	{
		public override void execute(FSMContext c, object o){
			/*
			Fighter fighter = (Fighter)o;				
			GameObject gobj = fighter.gobj;
			float moveSpeed = fighter.moveSpeed;
			
			if(fighter.currentMovement == MoveCommand.LEFT)
			{
				gobj.transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
				gobj.transform.LookAt(gobj.transform.position + new Vector3(1, 0, 0));
				gobj.animation.CrossFade(fighter.animationNameMap[FighterAnimation.WALK]);
			}
			else if(fighter.currentMovement == MoveCommand.RIGHT)
			{
				gobj.transform.position -= new Vector3(moveSpeed * Time.deltaTime, 0, 0);
				gobj.transform.LookAt(gobj.transform.position + new Vector3(-1, 0, 0));
				gobj.animation.CrossFade(fighter.animationNameMap[FighterAnimation.WALK]);
			}
			
			
			if( fighter.currentMovement == MoveCommand.NONE)
			{
				c.dispatch("idle", o);
			}
			*/
		}
	}
}

