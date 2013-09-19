using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ToyHell;
using FSM;

namespace ToyHell
{
	public class Fighter : A_Actor
	{
		//protected FSMContext 					fsmc;
		//public string							name;
		//public GameObject 						gobj;
		public Player							player;
		public Transform						socketJoint;
		public int								playerNumber;
		//public float							moveSpeed;
		//public float							globalActionTimer;
		//public Vector3							movement;
		//public float							jumpHeight;
		//public float							radius;
		//public CharacterController				controller;
		//public Vector3							globalForwardVector;
		public GameObject						weaponPickup;
		
		public Weapon							currentWeapon;
		public ActionCommand 					currentAction;
		public MoveCommand						currentMovement;
		
		private bool							jumpPressed;
		
		//public Dictionary<FighterAnimation, string> animationNameMap;
		
		public Fighter(GameObject gobj, Player player) : base(gobj)
		{
			FighterInput input		= gobj.GetComponent<FighterInput>();
			input.fighter			= this;
			this.controller			= gobj.GetComponent<CharacterController>();
			this.animationNameMap	= input.animationNameMap;
			this.moveSpeed			= input.moveSpeed;
			this.name				= input.name;
			this.socketJoint		= input.socketJoint;
			this.jumpHeight			= input.jumpHeight;
			
			this.weaponPickup		= null;
			//this.gobj 				= gobj;
			this.player				= player;
			this.playerNumber		= player.PlayerNumber;
			this.currentAction 		= ActionCommand.NONE;
			this.currentMovement	= MoveCommand.NONE;
			this.currentWeapon		= null;
			this.globalActionTimer	= 0.0f;
			this.movement			= Vector3.zero;
			
			this.jumpPressed		= false;
			
			this.InitStateMachine();
		}
		/*
		public void Update()
		{
			Debug.Log(fsmc.CurrentState.Name);
			//Debug.Log("Jump Pressed: " + this.jumpPressed);
			this.AddGravity();
			this.ApplyMovement();
			this.fsmc.CurrentState.update(fsmc, this);
			this.ReCenter();
		}
		*/
		
		public override void Update(){
			this.AddGravity();
			base.Update();
		}
		
		public void DoActionCommand( ActionCommand ac ){
			//Debug.Log(this.name + " Action: " + ac.ToString());
			
			this.currentAction = ac;
			
			if ( ac != ActionCommand.NONE ){
				switch(ac){
				case ActionCommand.ACTIVATE:
					this.PickUpWeapon();
					break;
				case ActionCommand.ATTACK:
					this.fsmc.dispatch("attack", this);
					break;
				case ActionCommand.BLOCK:
					this.fsmc.dispatch("block", this);
					break;
				case ActionCommand.JUMP:
					if (!this.jumpPressed){
						this.AddMovement(new Vector3(0, this.jumpHeight, 0));
						this.jumpPressed = true;
					}
					break;
				default:
					break;
				}
			}
		}
		
		public void DoMoveCommand( MoveCommand mc ){
			//Debug.Log(this.name + " Move: " + mc.ToString());
			this.currentMovement = mc;
			if (mc != MoveCommand.NONE){
				if (mc == MoveCommand.LEFT || mc == MoveCommand.RIGHT){
					if ( this.IsGrounded ){
						if (this.fsmc.CurrentState.Name != "attack"){
							this.Move(mc);
							this.fsmc.dispatch("walk", this);
						}
					}else{
						this.Move(mc);
						this.fsmc.dispatch("walk", this);
					}
				}
				if (mc == MoveCommand.UP){
				}
				else if (mc == MoveCommand.DOWN){
				}
			}
		}
		
		protected override void Move (MoveCommand direction){
			if(direction == MoveCommand.LEFT)
			{
				this.gobj.transform.position -= new Vector3(this.moveSpeed * Time.deltaTime, 0, 0);
				this.gobj.transform.LookAt(this.gobj.transform.position + new Vector3(-1, 0, 0));
				this.globalForwardVector = new Vector3(-1, 0, 0);
			}
			else if(direction == MoveCommand.RIGHT)
			{
				this.gobj.transform.position += new Vector3(this.moveSpeed * Time.deltaTime, 0, 0);
				this.gobj.transform.LookAt(this.gobj.transform.position + new Vector3(1, 0, 0));
				this.globalForwardVector = new Vector3(1, 0, 0);
			}
		}
		
		public override void TakeDamage(int damage, Vector3 direction){
			if (this.fsmc.CurrentState.Name != "block"){
				this.player.currentHp -= damage;
				if (this.player.currentHp <= 0){
					this.player.currentHp = 0;
					this.fsmc.dispatch("death", this);
				}
				else{
					this.movement = direction * 0.1f;
					this.fsmc.dispatch( "takeDamage", this );
				}
			}
			else{
				this.player.currentHp -= (int)(damage * 0.1f);
				if (this.player.currentHp <= 0){
					this.player.currentHp = 0;
					this.fsmc.dispatch("death", this);
				}
				else{
					this.movement = direction * 0.05f;
				}
			}
		}
		/*
		public bool IsGrounded{
			get { return Physics.Raycast( this.controller.transform.position, new Vector3(0.0f, -1.0f, 0.0f), 0.2f ); }
		}
		
		public void AddMovement( Vector3 movement ){
			this.movement += movement;
		}
		*/
		
		// Private helper functions
		private void PickUpWeapon(){
			if (this.weaponPickup != null){
				WeaponPickupInput input = this.weaponPickup.GetComponent<WeaponPickupInput>();
				this.player.PickUpWeapon(input.weaponName);
				GameObject.Destroy(this.weaponPickup);
				this.weaponPickup = null;
			}
		}
		/*
		private void AddGravity(){
			if (!this.controller.isGrounded){
				this.movement.y += Physics.gravity.y * 0.1f;
			}
			else{
				if (this.jumpPressed == false){
					this.gobj.transform.position = new Vector3(this.gobj.transform.position.x, 0.0f, this.gobj.transform.position.z);
					this.movement.y = 0.0f;
				}
			}
			
		}
		
		private void ApplyMovement(){
			if (this.movement.magnitude > 0.001f){
				this.controller.Move(this.movement * Time.deltaTime);
				this.movement = Vector3.Lerp( this.movement, Vector3.zero, Time.deltaTime );
				
				if ( this.movement.magnitude < 0.001f ){
					this.movement = Vector3.zero;
				}
			}
			if (this.IsGrounded){
				this.jumpPressed = false;
			}
		}
		
		private void ReCenter(){
			if (this.gobj.transform.position.z != 0.0f) 
				this.gobj.transform.position = new Vector3(this.gobj.transform.position.x, this.gobj.transform.position.y, 0.0f);
		}
		*/
		protected override void InitStateMachine()
		{
			State S_idle 		= new State("idle", new Action_IdleEnter(), new Action_IdleUpdate(), new Action_IdleExit());
			State S_walk 		= new State("walk", new Action_WalkEnter(), new Action_WalkUpdate(), new Action_WalkExit());
			State S_attack 		= new State("attack",new Action_AttackEnter(), new Action_AttackUpdate(), new Action_AttackExit());
			State S_takeDamage 	= new State("takeDamage",new Action_TakeDamageEnter(), new Action_TakeDamageUpdate(),new Action_TakeDamageExit());
			State S_block		= new State("block",new Action_BlockEnter(), new Action_BlockUpdate(),new Action_BlockExit());
			State S_death		= new State("death" , new Action_DeathEnter(), new Action_DeathUpdate(),new Action_DeathExit());
			State S_climb		= new State("climb" , new Action_ClimbEnter(), new Action_ClimbUpdate(),new Action_ClimbExit());
			
			Transition T_idle 		= new Transition(S_idle, new Action_None());
			Transition T_walk 		= new Transition(S_walk, new Action_None());
			Transition T_attack 	= new Transition(S_attack, new Action_None());
			Transition T_takeDamage = new Transition(S_takeDamage, new Action_None());
			Transition T_block 		= new Transition(S_block, new Action_None());
			Transition T_death		= new Transition(S_death, new Action_None());
			Transition T_climb		= new Transition(S_climb, new Action_None());
			
			S_idle.addTransition(T_walk, "walk");
			S_idle.addTransition(T_attack,"attack");
			S_idle.addTransition(T_takeDamage,"takeDamage");
			S_idle.addTransition(T_idle,"idle");
			S_idle.addTransition(T_block, "block");
			S_idle.addTransition(T_death, "death");
			S_idle.addTransition(T_climb, "climb");
			
			S_walk.addTransition(T_idle, "idle");
			S_walk.addTransition(T_walk,"walk");
			S_walk.addTransition(T_takeDamage,"takeDamage");
			S_walk.addTransition(T_attack,"attack");
			S_walk.addTransition(T_block, "block");
			S_walk.addTransition(T_death, "death");
			
			S_attack.addTransition(T_idle,"idle");
			S_attack.addTransition(T_takeDamage,"takeDamage");
			S_attack.addTransition(T_death, "death");
			
			S_takeDamage.addTransition(T_idle,"idle");
			S_takeDamage.addTransition(T_takeDamage,"takeDamage");
			S_takeDamage.addTransition(T_death, "death");
			
			S_block.addTransition(T_idle, "idle");
			S_block.addTransition(T_walk, "walk");
			S_block.addTransition(T_death, "death");
			
			S_climb.addTransition(T_idle, "idle");
			
			this.fsmc = FSM.FSM.createFSMInstance(S_idle, new Action_None(), this);
		}
	}
}