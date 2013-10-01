using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ToyHell;
using FSM;

namespace ToyHell
{
	public abstract class A_Actor
	{
		protected FSMContext 					fsmc;
		public string							name;
		public GameObject 						gobj;
		public float							moveSpeed;
		public float							globalActionTimer;
		public Vector3							movement;
		public float							jumpHeight;
		public CharacterController				controller;
		public Vector3							globalForwardVector;
		public Dictionary<FighterAnimation, string> animationNameMap;
		
		protected A_Actor( GameObject gobj ){
			this.gobj 				= gobj;
			this.controller			= gobj.GetComponent<CharacterController>();
		}
		
		public virtual void Update()
		{
			Debug.Log(fsmc.CurrentState.Name);
			//Debug.Log("Jump Pressed: " + this.jumpPressed);
			//this.AddGravity();
			this.ApplyMovement();
			this.fsmc.CurrentState.update(fsmc, this);
			this.ReCenter();
		}
		
		protected void AddGravity(){
			if (!this.controller.isGrounded){
				this.movement.y += Physics.gravity.y * 0.1f;
			}
			/*
			else{
				if (this.jumpPressed == false){
					this.gobj.transform.position = new Vector3(this.gobj.transform.position.x, 0.0f, this.gobj.transform.position.z);
					this.movement.y = 0.0f;
				}
			}
			*/
		}
		
		public bool IsGrounded{
			get { return Physics.Raycast( this.controller.transform.position, new Vector3(0.0f, -1.0f, 0.0f), 0.2f ); }
		}
		
		protected void ApplyMovement(){
			if (this.movement.magnitude > 0.001f){
				this.controller.Move(this.movement * Time.deltaTime);
				this.movement = Vector3.Lerp( this.movement, Vector3.zero, Time.deltaTime );
				
				if ( this.movement.magnitude < 0.001f ){
					this.movement = Vector3.zero;
				}
			}
		}
		
		public void AddMovement( Vector3 movement ){
			this.movement += movement;
		}
		
		protected void ReCenter(){
			if (this.gobj.transform.position.z != 0.0f) 
				this.gobj.transform.position = new Vector3(this.gobj.transform.position.x, this.gobj.transform.position.y, 0.0f);
		}
		
		protected virtual void InitStateMachine() {}
		public abstract void TakeDamage ( int damage, Vector3 direction );
	}
}

