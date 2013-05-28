using System;
using System.Collections.Generic;
using UnityEngine;
using Actors;

namespace FSM
{
    public class A_JumpEnter:FSMAction
    {
        public override void execute(FSMContext fsmc, object o)
        {
			Actor actor = (Actor) o;
			actor.ActionTimer = 0.0f;
			Debug.Log("jumping");
			//actor.controller.transform.LookAt(actor.TargetPosition);
        }
    }
}