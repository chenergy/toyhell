using System;
using System.Collections.Generic;
using UnityEngine;
using Actors;

namespace FSM
{
    public class A_EnemyAttackExit:FSMAction
    {
        public override void execute(FSMContext fsmc, object o)
        {
			Enemy actor = (Enemy) o;
			actor.ActionTimer = 0.0f;
			actor.HasAttacked = false;
			actor.TargetPlayer = null;
			if (actor.Animation) 
				actor.Animation.Stop();
        }
    }
}