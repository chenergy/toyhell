using System;
using System.Collections.Generic;
using UnityEngine;
using Actors;

namespace FSM
{
    public class A_Attack:FSMAction
    {
        public override void execute(FSMContext fsmc, object o)
        {
			Actor actor = (Actor) o;
			
			if (actor.ActionTimer < 5.0f){
				fsmc.dispatch("idle", o);
			}
			
			Debug.Log("attacking");
        }
    }
}