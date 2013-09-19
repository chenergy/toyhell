using System;
using System.Collections.Generic;
using ToyHell;
using FSM;
using UnityEngine;
namespace FSM
{
	public class Action_WalkUpdate:FSMAction
	{
		public override void execute(FSMContext c, object o){
			Fighter fighter = (Fighter)o;				
			/*
			GameObject gobj = fighter.gobj;
			float moveSpeed = fighter.moveSpeed;
			
			if(fighter.currentMovement == MoveCommand.LEFT)
			{
				gobj.transform.position -= new Vector3(moveSpeed * Time.deltaTime, 0, 0);
				gobj.transform.LookAt(gobj.transform.position + new Vector3(-1, 0, 0));
				fighter.globalForwardVector = new Vector3(-1, 0, 0);
			}
			else if(fighter.currentMovement == MoveCommand.RIGHT)
			{
				gobj.transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
				gobj.transform.LookAt(gobj.transform.position + new Vector3(1, 0, 0));
				fighter.globalForwardVector = new Vector3(1, 0, 0);
			}
			*/
			if (fighter.IsGrounded){
				fighter.gobj.animation.CrossFade(fighter.animationNameMap[FighterAnimation.WALK]);
			}else{
				fighter.gobj.animation.CrossFade(fighter.animationNameMap[FighterAnimation.JUMP]);
			}
			
			if( fighter.currentMovement == MoveCommand.NONE)
			{
				c.dispatch("idle", o);
			}
		}
	}
}

