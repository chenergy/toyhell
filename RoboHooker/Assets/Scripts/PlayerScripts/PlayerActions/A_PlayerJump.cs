using System;
using System.Collections.Generic;
using UnityEngine;
using Actors;

namespace FSM
{
    public class A_PlayerJump:FSMAction
    {
        public override void execute(FSMContext fsmc, object o)
        {
			Player actor = (Player) o;
			
			if (actor.Animation){
				if (actor.Animation["Jump"]){
					actor.Animation.CrossFade("Jump");
				}
			}
			
			if (actor.IsGrounded){
				fsmc.dispatch("idle", o);
			}
        }
    }
}