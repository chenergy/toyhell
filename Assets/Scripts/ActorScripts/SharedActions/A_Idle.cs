using System;
using System.Collections.Generic;
using UnityEngine;
using Actors;

namespace FSM
{
    public class A_Idle:FSMAction
    {
        public override void execute(FSMContext fsmc, object o)
        {
			Actor actor = (Actor) o;
			
			if (actor.Animation){
				if (!actor.IsGrounded){
					if (actor.Animation["Jump"]){
						actor.Animation.CrossFade("Jump");
						//Debug.Log("jumping");
					}
					else if (actor.Animation["Idle"]){
						actor.Animation.CrossFade("Idle");
					}
				}
				else if (actor.Animation["Idle"]){
					actor.Animation.CrossFade("Idle");
				}
			}
        }
    }
}