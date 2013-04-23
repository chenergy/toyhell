using System;
using System.Collections.Generic;
using UnityEngine;
using Actors;

namespace FSM
{
    public class A_MoveToPosition:FSMAction
    {
        public override void execute(FSMContext fsmc, object o)
        {
			Actor actor = (Actor) o;
			Vector3 direction = actor.TargetPosition - actor.Position;
			
			if (Quaternion.Angle(actor.Rotation, actor.TargetRotation) > 5.0f){
				actor.Rotation = Quaternion.Slerp(actor.Rotation, actor.TargetRotation, Time.deltaTime * 
					(actor.TurnSpeed * 3.0f));
			}
			
			if (direction.magnitude > 0.2f){
				actor.controller.Move(new Vector3((direction.normalized * (actor.MoveSpeed * 0.01f)).x, 0, 
					(direction.normalized * (actor.MoveSpeed * 0.01f)).z));
			}
			
			if ((Quaternion.Angle(actor.Rotation, actor.TargetRotation) < 5.0f) && (direction.magnitude < 0.1f))
			{
				actor.TargetPosition = actor.Position;
				actor.TargetRotation = actor.Rotation;
				fsmc.dispatch("idle", this);
			}
			
			//Debug.Log("distance: " + direction.magnitude);
			//fsmc.Attributes.PlayEnemy("run", this.moveSpeed);
			
        }
    }
}