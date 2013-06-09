using System;
using System.Collections.Generic;
using UnityEngine;
using Actors;

namespace FSM
{
    public class A_PlayerDeathEnter:FSMAction
    {
        public override void execute(FSMContext fsmc, object o)
        {
			Player actor = (Player) o;
			actor.ActionTimer = 0.0f;
			
			GameObject deathParts = (GameObject)GameObject.Instantiate(actor.DeathParts, actor.Position, Quaternion.identity);
			GameObject.Destroy(deathParts, actor.FadeTime);
			
			Vector3 offset = new Vector3(0.0f, 2.0f, 0.0f);
			
			/*
			if (actor.SocketedDrop != null){
				GameObject socketedDrop = (GameObject)GameObject.Instantiate(actor.SocketedDrop, actor.Position + offset, Quaternion.identity);
			}
			
			if (actor.OnDeathLoadLevel){
				GameObject.Find("Fader").GetComponent<fadeToBlackScript>().SetLoadLevel(actor.LevelToLoad);
				GameObject.Find("Fader").GetComponent<fadeToBlackScript>().FadeOut();
			}
			*/
			
			Debug.Log("death");
			GameObject.Destroy(actor.gameObject);
        }
    }
}