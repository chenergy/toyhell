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
			actor.TargetPosition = new Vector3(actor.TargetPosition.x, actor.Position.y, actor.Position.z);
			Vector3 direction = actor.TargetPosition - actor.Position;
			
			//Debug.Log("Moving to: " + actor.TargetPosition);
			Debug.DrawLine(actor.Position, actor.TargetPosition);
			
			// Quick Rotation
			//actor.controller.transform.LookAt(actor.TargetPosition);
			
			// Slow Rotation
			if (Quaternion.Angle(actor.Rotation, actor.TargetRotation) > 5.0f){
				actor.Rotation = Quaternion.Slerp(actor.Rotation, actor.TargetRotation, Time.deltaTime * 
					(actor.TurnSpeed));
			}
			
			Debug.DrawRay(actor.Position, actor.controller.transform.forward);

			if (direction.magnitude > 0.2f){
				actor.controller.Move(new Vector3((direction.normalized * (actor.MoveSpeed * 0.01f)).x, 0, 
					(direction.normalized * (actor.MoveSpeed * 0.01f)).z));
			}
			/*else{
				fsmc.dispatch("idle", o);
			}*/
			
			if (actor.Animation){
				if (!actor.IsGrounded){
					if (actor.Animation["Jump"]){
						actor.Animation.CrossFade("Jump");
						Debug.Log("jumping");
					}
					else if (actor.Animation["Walk"]){
						actor.Animation.CrossFade("Walk");
					}
				}
				else if (actor.Animation["Walk"]){
					actor.Animation.CrossFade("Walk");
				}
			}
        }
    }
}