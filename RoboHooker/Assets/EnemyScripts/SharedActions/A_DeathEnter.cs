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
			
			GameObject deathParts = (GameObject)GameObject.Instantiate(actor.DeathParts, actor.Position, Quaternion.identity);
			GameObject.Destroy(deathParts, actor.FadeTime);
			
			Vector3 offset = new Vector3(0.0f, 2.0f, 0.0f);
			
			if (actor.SocketedDrop != null){
				GameObject socketedDrop = (GameObject)GameObject.Instantiate(actor.SocketedDrop, actor.Position + offset, Quaternion.identity);
			}
			
			if (actor.OnDeathLoadLevel){
				if (actor.LevelToLoad != ""){
					Application.LoadLevel(actor.LevelToLoad);
				}
			}
			
			Debug.Log("death");
			GameObject.Destroy(actor.gameObject);
        }
    }
}