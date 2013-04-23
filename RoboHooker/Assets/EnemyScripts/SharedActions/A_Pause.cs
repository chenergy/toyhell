using System;
using System.Collections.Generic;
using UnityEngine;
using Actors;

namespace FSM
{
    public class A_Pause:FSMAction
    {
        public override void execute(FSMContext fsmc, object o)
        {
			Debug.Log("pausing");
        }
    }
}