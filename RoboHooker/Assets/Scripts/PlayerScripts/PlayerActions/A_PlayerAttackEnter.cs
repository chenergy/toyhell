using System;
using System.Collections.Generic;
using UnityEngine;
using Actors;

namespace FSM
{
    public class A_PlayerAttackEnter:FSMAction
    {
        public override void execute(FSMContext fsmc, object o)
        {
			Player actor = (Player) o;
			actor.ActionTimer = 0.0f;
			actor.HasAttacked = false;
			actor.isFrozen = true;
			actor.attackCounter = 0;
			
			Debug.Log("attacking");
			
			if (actor.Animation){
				if (actor.Animation["Attack"]){
					actor.Animation["Attack"].speed = 1.0f;
				}
			}
        }
    }
}