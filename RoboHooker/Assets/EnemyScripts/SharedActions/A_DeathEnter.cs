using System;
using System.Collections.Generic;
using UnityEngine;
using Actors;

namespace FSM
{
    public class A_DeathEnter:FSMAction
    {
        public override void execute(FSMContext fsmc, object o)
        {
			Actor actor = (Actor) o;
			actor.ActionTimer = 0.0f;
			actor.gameObject.SetActive(false);
			
			GameObject deathParts = (GameObject)GameObject.Instantiate(actor.DeathParts, actor.Position, Quaternion.identity);
			GameObject.Destroy(deathParts, actor.FadeTime);
			Debug.Log("death");
        }
    }
}