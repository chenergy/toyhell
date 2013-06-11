using System;
using System.Collections.Generic;
using UnityEngine;
using Actors;

namespace FSM
{
    public class A_EnemyAttackEnter:FSMAction
    {
        public override void execute(FSMContext fsmc, object o)
        {
			Actor actor = (Actor) o;
			actor.ActionTimer = 0.0f;
			Debug.Log("attacking");
			
			if (actor.Animation){
				if (actor.Animation["Attack"]){
					actor.Animation["Attack"].speed = 1.0f;
				}
			}
        }
    }
}