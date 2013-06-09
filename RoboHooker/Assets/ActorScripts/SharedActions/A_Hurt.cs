using System;
using System.Collections.Generic;
using UnityEngine;
using Actors;

namespace FSM
{
    public class A_Hurt:FSMAction
    {
        public override void execute(FSMContext fsmc, object o)
        {
			Actor actor = (Actor) o;
			float animationLength = 3.0f;
			
			if (actor.Animation){
				if (actor.Animation["Hurt"]){
					animationLength = actor.Animation["Hurt"].clip.length;
					actor.Animation.CrossFade("Hurt");
				}
			}
			
			if (actor.ActionTimer > animationLength){
				fsmc.dispatch("idle", o);
			}
			actor.ActionTimer += Time.deltaTime;
        }
    }
}