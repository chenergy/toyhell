using System;
using System.Collections.Generic;
using UnityEngine;
using Actors;

namespace FSM
{
    public class A_HurtEnter:FSMAction
    {
        public override void execute(FSMContext fsmc, object o)
        {
			Actor actor = (Actor) o;
			actor.ActionTimer = 0.0f;
			Debug.Log("hurt");
			
			GameObject attackSparks = (GameObject)Resources.LoadAssetAtPath("Assets/Prefabs/Particles/AttackSparks.prefab", typeof(GameObject));
			GameObject newParticles = (GameObject)GameObject.Instantiate(attackSparks, actor.Position + new Vector3(0.0f, 2.0f, 0.0f), Quaternion.identity);
			GameObject.Destroy(newParticles, 1.0f);
        }
    }
}