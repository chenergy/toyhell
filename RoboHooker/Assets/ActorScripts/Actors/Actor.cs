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
		
		protected 	float 	jumpStrength;
		//protected bool	canJump;
		public 		bool 	canJump;
		protected	Vector3 extraMovement;
		
		public void MoveToPosition(Vector3 targetPosition){
			if (fsmc.CurrentState.Name != "attack"){
				// Do not consider y in the target location
				this.TargetPosition = new Vector3(targetPosition.x, this.Position.y, targetPosition.z);
				this.TargetRotation = Quaternion.LookRotation(this.TargetPosition - this.Position);
				
				this.Forward = (targetPosition - this.Position).normalized;
				
				fsmc.dispatch("moveToPosition", this);
			}
		}
		
		public void Attack(Vector3 targetPosition){
			if (fsmc.CurrentState.Name != "attack"){
				// Use if you DO NOT consider y in the target location
				this.TargetPosition = new Vector3(targetPosition.x, this.Position.y, targetPosition.z);
				
				// Use if you DO consider y in target location
				//this.TargetPosition = targetPosition;
				this.TargetRotation = Quaternion.LookRotation(this.TargetPosition - this.Position);
				
				fsmc.dispatch("attack", this);
			}
		}
		
		public void Death(){
			if (fsmc.CurrentState.Name != "death"){
				fsmc.dispatch("death", this);
			}
		}
		
		public void Hurt(){
			fsmc.dispatch("hurt", this);
		}
		
		public abstract void Update();		

		protected void checkDeath(){
			if (this.CurrentHP <= 0){
				this.Death();
			}
		}
		
		protected virtual void applyGravity(){
	        if (!this.controller.isGrounded){
				this.extraMovement.y += Physics.gravity.y * Time.deltaTime;
	        }
			else{
				this.extraMovement.y = 0.0f;
			}
		}
		
		protected void ApplyExtraMovement(){
			this.controller.Move(this.extraMovement * Time.deltaTime);
		}
		
		#region attribute getters and setters
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
		
		public float JumpPower{
			get{ return (float)attributes["jumpPower"]; }
			set{ attributes["jumpPower"] = value; }
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
		
		public float AttackSpeed{
			get{ return (float)attributes["attackSpeed"]; }
			set{ attributes["attackSpeed"] = value; }
		}
		
		public int CurrentHP{
			get{ return (int)attributes["currentHP"]; }
			set{ attributes["currentHP"] = value; }
		}
		
		public int MaxHP{
			get{ return (int)attributes["maxHP"]; }
			set{ attributes["maxHP"] = value; }
		}
		
		public GameObject DeathParts{
			get{ return (GameObject)attributes["deathParts"]; }
			set{ attributes["deathParts"] = value; }
		}
		
		public Animation Animation{
			get{ return (Animation)attributes["animation"]; }
			set{ attributes["animation"] = value; }
		}
		
		public float KnockbackStrength{
			get{ return (float)attributes["knockbackStrength"]; }
			set{ attributes["knockbackStrength"] = value; }
		}
		
		public bool HasAttacked{
			get{ return (bool)attributes["hasAttacked"]; }
			set{ attributes["hasAttacked"] = value; }
		}
		
		public float FadeTime{
			get{ return (float)attributes["fadeTime"]; }
			set{ attributes["fadeTime"] = value; }
		}
		
		public Vector3 Forward{
			get{ return (Vector3)attributes["forward"]; }
			set{ attributes["forward"] = value; }
		}
		
		public Vector3 Knockback{
			get{ return (Vector3)attributes["knockback"]; }
			set{ attributes["knockback"] = value; }
		}
		
		#endregion
	}
}

