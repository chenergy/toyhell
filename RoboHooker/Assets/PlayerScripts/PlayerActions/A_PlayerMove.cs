using System;
using System.Collections.Generic;
using UnityEngine;
using Actors;

namespace FSM
{
    public class A_PlayerMove:FSMAction
    {
        public override void execute(FSMContext fsmc, object o)
        {
			Player actor = (Player) o;
			
			if (actor.Animation){
				if (actor.IsGrounded){
					if (actor.Animation["Walk"]){
						actor.Animation.CrossFade("Walk");
					}
				}
				else{
					if (actor.Animation["Jump"]){
						actor.Animation.CrossFade("Jump");
					}
				}
			}
			
			if ( !actor.isMoving ){
				fsmc.dispatch("idle", o);
			}
        }
    }
}