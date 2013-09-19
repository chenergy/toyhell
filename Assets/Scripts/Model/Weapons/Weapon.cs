using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using ToyHell;

namespace ToyHell{
	public class Weapon
	{
		private string			name;
		private Fighter		owner;
		private GameObject		gobjParent;
		private GameObject		gobj;
		private Attack_Melee	attack;
		private bool			isProjectile;
		private	string		 	animationName;
		private HitBox			hitbox;
		
		public Weapon ( Fighter owner, GameObject gobj, Attack_Melee attack )
		{
			this.owner 				= owner;
			this.attack				= attack;
			
			this.Init(gobj);
			//this.hitboxJoint 		= weaponInput.hitboxJoint;
			//this.animation	 	= weaponInput.attackAnimation;
			//this.speed			= weaponInput.speed;
			//this.attack 			= new Attack_Melee(weaponInput.animation.name, weaponInput.speed, owner);
			
			//HitBoxInput hitboxInput = weaponInput.hitbox.GetComponent<HitBoxInput>();
			
			//this.timer			= 0.0f;
		}
		
		public void Execute(){
			this.gobj.animation.CrossFade(this.animationName);
			this.attack.Execute();
			//this.timer += Time.deltaTime;
		}
		
		public void Reset(){
			//this.timer = 0.0f;
			this.attack.Reset();
		}
		
		public bool CheckComplete(){
			return this.attack.CheckComplete();
		}
		
		public void StopAnimation(){
			this.gobj.animation.Stop();
		}
		
		public void Init(GameObject gobj){
			WeaponInput weaponInput = gobj.GetComponent<WeaponInput>();
			this.gobjParent			= gobj;
			this.gobj				= weaponInput.gobj;
			this.name				= weaponInput.weaponName.ToString();
			this.isProjectile 		= weaponInput.isProjectile;
			this.animationName		= weaponInput.attackAnimation.name;
			this.hitbox				= new HitBox( this.owner, weaponInput.hitbox, weaponInput.isProjectile );
		}
		
		public GameObject Gobj{
			get { return this.gobjParent; }
		}
		
		public string Name{
			get { return this.name; }
		}
		
		public HitBox HitBox{
			get { return this.hitbox; }
		}
	}
}

