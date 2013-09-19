using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ToyHell;
using FSM;

namespace ToyHell
{
	public class Player
	{
		public  GamePad			controls;
		public	int				currentHp;
		public	int				maxHp;
		private int				playerNumber;
		private Fighter			fighter;
		private	int				lives;
		private Weapon[]		weapons;
		private int				currentWeaponSlot;
		
		public Player (int playerNumber)
		{
			this.playerNumber 		= playerNumber;
			this.controls     		= new GamePad(this);
			this.fighter 			= null;
			this.lives				= 3;
			this.weapons			= new Weapon[3];
			this.currentWeaponSlot 	= 0;
		}
		
		public void Update(){
			if (this.fighter != null){
				this.fighter.Update();
			}
		}
		
		public void DoActionCommand( ActionCommand actionCommand ){
			if (this.fighter != null){
				if ( actionCommand != ActionCommand.NONE ){
					switch(actionCommand){
					case ActionCommand.SWITCH_LEFT:
						this.SwitchWeapon("left");
						break;
					case ActionCommand.SWITCH_RIGHT:
						this.SwitchWeapon("right");
						break;
					default:
						this.fighter.DoActionCommand( actionCommand );
						break;
					}
				}
				//Debug.Log("Attack Command: " + actionCommand.ToString());
			}
		}
		
		public void DoMoveCommand( MoveCommand moveCommand ){
			if (this.fighter != null){
				//Debug.Log("Move Command: " + moveCommand.ToString());
				this.fighter.DoMoveCommand( moveCommand );
			}
		}
		
		public int PlayerNumber{
			get { return this.playerNumber; }
		}
		
		public Fighter Fighter{
			get { return this.fighter; }
		}
		
		public void InstantiateFighter(string fighter, Vector3 position, Quaternion rotation){
			GameObject character = GameObject.Instantiate( Resources.Load("Characters/" + fighter, typeof(GameObject)),
				position, rotation ) as GameObject;
			character.transform.LookAt(character.transform.position + Vector3.right);
			
			switch (fighter){
			case "Robot":
				this.fighter = new Fighter(character, this);
				break;
			case "Barbie":
				this.fighter = new Fighter(character, this);
				break;
			default:
				break;
			}
		}
		
		public void RestartFighter(){
			string fighterName = this.Fighter.name;
			GameObject.Destroy( this.fighter.gobj );
			this.fighter = null;
			
			GameManager.CreateFighter( fighterName, this.playerNumber );
		}
		
		// Pick up a weapon from the ground
		public void PickUpWeapon( WeaponName weaponName ){
			if ( weaponName != WeaponName.None){
				string name = weaponName.ToString();
				
				if (this.weapons[this.currentWeaponSlot] != null){
					this.DropWeapon( this.weapons[this.currentWeaponSlot] );
				}
				
				GameObject gobj = this.InstantiateWeapon( name );
				Weapon newWeapon = null;
				
				switch (name){
				case "Arm":
					newWeapon = this.CreateArmWeapon( gobj );
					break;
				case "Paddle":
					newWeapon = this.CreatePaddleWeapon( gobj );
					break;
				case "Pencil":
					newWeapon = this.CreatePencilWeapon( gobj );
					break;
				case "Helicopter":
					newWeapon = this.CreateHelicopterWeapon( gobj );
					break;
				default:
					break;
				}
				
				//this.EquipWeapon( weaponName );
				if (newWeapon != null){
					this.fighter.currentWeapon = newWeapon;
					this.weapons[this.currentWeaponSlot] = newWeapon;
				}
			}
		}
		
		public Weapon GetWeapon( int slot ){
			return this.weapons[slot];
		}
		
		// Switch between weapons in your roster
		public void SwitchWeapon( string direction ){
			int newWeaponSlot = direction == "left" ? this.currentWeaponSlot - 1 : this.currentWeaponSlot + 1;
			
			if ((this.currentWeaponSlot != Mathf.Clamp(newWeaponSlot, 0, this.weapons.Length)) &&
				(newWeaponSlot >= 0) && (newWeaponSlot < this.weapons.Length) ){
				
				if (this.fighter.currentWeapon != null){
					GameObject.Destroy(this.fighter.currentWeapon.Gobj);
					this.fighter.currentWeapon = null;
				}
				
				this.currentWeaponSlot = newWeaponSlot;
				Weapon newWeapon = this.weapons[this.currentWeaponSlot];
				
				
				if ( this.weapons[this.currentWeaponSlot] != null ){
					newWeapon.Init( this.InstantiateWeapon( newWeapon.Name ) );
					this.fighter.currentWeapon = newWeapon;
					//this.EquipWeapon( this.weapons[this.currentWeaponSlot] );
				}
			}
		}
		
		// Create a weapon gameobject from resources
		private GameObject InstantiateWeapon(string name){
			GameObject gobj = GameObject.Instantiate( Resources.Load("Weapons/" + name, typeof(GameObject))) as GameObject;
			gobj.transform.parent = this.fighter.socketJoint;
			gobj.transform.localPosition = Vector3.zero;
			gobj.transform.localRotation = this.fighter.socketJoint.localRotation;
			return gobj;
		}
		
		// Equip an existing weapon
		/*
		private void EquipWeapon( WeaponName weapon ){
			if (weapon != WeaponName.None){
				this.fighter.currentWeapon.Gobj = this.InstantiateWeapon( weapon.ToString() );
			}
		}
		*/
		
		// Drop an existing weapon
		private void DropWeapon( Weapon weapon ){
			string name = weapon.Name;
			Vector3 dropLocation = this.fighter.controller.transform.position + (Vector3.right * this.fighter.globalForwardVector.x) + (Vector3.up * 1.5f);
			GameObject gobj = GameObject.Instantiate( Resources.Load("WeaponPickups/" + name + "Pickup", typeof(GameObject)), 
				dropLocation, Quaternion.identity ) as GameObject;
			
			if (this.fighter.currentWeapon != null){
				GameObject.Destroy(this.fighter.currentWeapon.Gobj);
			}
			
			this.fighter.currentWeapon = null;
			this.weapons[this.currentWeaponSlot] = null;
		}
		
		private Weapon CreateArmWeapon(GameObject gobj){
			WeaponInput input 	= gobj.GetComponent<WeaponInput>();
			//HitBox hitbox 		= new HitBox( this.fighter, input.hitbox, false );
			float animLength	= input.attackAnimation.length;
			
			Attack_Melee attack = new Attack_Melee( animLength, input.speed, this.fighter );
			attack.AddInstruction( new MeleeHitBoxInstruction (
				this.fighter,		// fighter
				//hitbox,				// hitbox
				10,					// damage
				0,					// startTime
				1,					// endTime
				Vector3.zero,		// fighter movement
				Vector3.left ));	// knockback
			
			return new Weapon( this.fighter, gobj, attack );
		}
		
		private Weapon CreatePaddleWeapon(GameObject gobj){
			WeaponInput input 	= gobj.GetComponent<WeaponInput>();
			//HitBox hitbox 		= new HitBox( this.fighter, input.hitbox, false );
			float animLength	= input.attackAnimation.length;
			
			Attack_Melee attack = new Attack_Melee( animLength, input.speed, this.fighter );
			attack.AddInstruction( new MeleeHitBoxInstruction (
				this.fighter,		// fighter
				//hitbox,				// hitbox
				10,					// damage
				0,					// startTime
				1,					// endTime
				Vector3.zero,		// fighter movement
				Vector3.left ));	// knockback
			
			return new Weapon( this.fighter, gobj, attack );
		}
		
		private Weapon CreatePencilWeapon(GameObject gobj){
			WeaponInput input 	= gobj.GetComponent<WeaponInput>();
			//HitBox hitbox 		= new HitBox( this.fighter, input.hitbox, false );
			float animLength	= input.attackAnimation.length;
			
			Attack_Melee attack = new Attack_Melee( animLength, input.speed, this.fighter );
			attack.AddInstruction( new MeleeHitBoxInstruction (
				this.fighter,		// fighter
				//hitbox,				// hitbox
				10,					// damage
				0,					// startTime
				1,					// endTime
				Vector3.zero,		// fighter movement
				Vector3.left ));	// knockback
			
			return new Weapon( this.fighter, gobj, attack );
		}
		
		private Weapon CreateHelicopterWeapon(GameObject gobj){
			WeaponInput input 	= gobj.GetComponent<WeaponInput>();
			//HitBox hitbox 		= new HitBox( this.fighter, input.hitbox, false );
			float animLength	= input.attackAnimation.length;
			
			Attack_Melee attack = new Attack_Melee( animLength, input.speed, this.fighter );
			attack.AddInstruction( new MeleeHitBoxInstruction (
				this.fighter,		// fighter
				//hitbox,				// hitbox
				10,					// damage
				0,					// startTime
				1,					// endTime
				Vector3.zero,		// fighter movement
				Vector3.left ));	// knockback
			
			return new Weapon( this.fighter, gobj, attack );
		}
	}
}