using UnityEngine;
using System;
using System.Collections.Generic;
using FSM;
using Actors;

namespace Actors
{
	public abstract class Actor
	{
		protected FSMContext fsmc;
		protected Dictionary<string, object> attributes;
		
		
		public void MoveToPosition(Vector3 targetPosition){
			// Do not consider y in the target location
			this.TargetPosition = new Vector3(targetPosition.x, this.Position.y, targetPosition.z);
			
			//this.TargetPosition = targetPosition;
			this.TargetRotation = Quaternion.LookRotation(this.TargetPosition - this.Position);

			if (fsmc.CurrentState.Name != "moveToPosition")
				fsmc.dispatch("moveToPosition", this);
		}
		
		
		public void Attack(){
			if (fsmc.CurrentState.Name != "attack")
				fsmc.dispatch("attack", this);
		}
		
		public GameObject gameObject{
			get{ return (GameObject)attributes["gameObject"]; }
			set{ attributes["gameObject"] = value; }
		}
		
		public CharacterController controller{
			get{ return (CharacterController)attributes["controller"]; }
			set{ attributes["controller"] = value; }
		}
		
		public string StateName{
			get{ return fsmc.CurrentState.Name; }
		}
		
		public Vector3 Position{
			get{ return (Vector3)this.controller.transform.position; }
			set{ this.controller.transform.position = value; }
		}
		
		public Quaternion Rotation{
			get{ return (Quaternion)this.gameObject.transform.rotation; }
			set{ this.gameObject.transform.rotation = value; }
		}
		
		public Vector3 TargetPosition{
			get{ return (Vector3)attributes["targetPosition"]; }
			set{ attributes["targetPosition"] = value; }
		}
		
		public Quaternion TargetRotation{
			get{ return (Quaternion)attributes["targetRotation"]; }
			set{ attributes["targetRotation"] = value; }
		}
		
		public float MoveSpeed{
			get{ return (float)attributes["moveSpeed"]; }
			set{ attributes["moveSpeed"] = value; }
		}
		
		public float TurnSpeed{
			get{ return (float)attributes["turnSpeed"]; }
			set{ attributes["turnSpeed"] = value; }
		}
		
		public float ActionTimer{
			get{ return (float)attributes["actionTimer"]; }
			set{ attributes["actionTimer"] = value; }
		}
		
		public float AttackTime{
			get{ return (float)attributes["attackTime"]; }
			set{ attributes["attackTime"] = value; }
		}
		
		public float AttackLength{
			get{ return (float)attributes["attackLength"]; }
			set{ attributes["attackLength"] = value; }
		}
		
		public bool HasAttacked{
			get{ return (bool)attributes["hasAttacked"]; }
			set{ attributes["hasAttacked"] = value; }
		}
		
		public GameObject Hitbox{
			get{ return (GameObject)attributes["hitbox"]; }
			set{ attributes["hitbox"] = value; }
		}
		
		public Vector3 AttackOffset{
			get{ return (Vector3)attributes["attackOffset"]; }
		}
		
		public int Damage{
			get{ return (int)attributes["damage"]; }
			set{ attributes["damage"] = value; }
		}
		
		public void Update(){
			fsmc.CurrentState.update(fsmc, this);
		}
	}
}

