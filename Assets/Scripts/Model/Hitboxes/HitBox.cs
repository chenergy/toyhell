using UnityEngine;
using System.Collections;
using ToyHell;
using System.Collections.Generic;

namespace ToyHell
{
	public class HitBox
	{
		private GameObject 	gobj;
		private Fighter	owner;
		private bool		isProjectile;
		
		public int			damage;
		public Vector3		knockback;
		
		public HitBox(Fighter owner, GameObject gobj, bool isProjectile)
		{
			this.owner			= owner;
			this.gobj			= gobj;
			this.isProjectile 	= isProjectile;
			this.damage			= 0;
			this.knockback		= Vector3.zero;
			
			this.Reset();
			this.gobj.GetComponent<HitBoxInput>().hitbox = this;
		}
		
		public void Enable(){
			if (this.gobj != null){
				this.TurnOnCollider();
				this.TurnOnVisibility();
			}
			/*
			if (GameManager.UI.hitboxOn){
				this.TurnOnVisibility();
			}
			*/
		}
		
		public void Disable(){
			if (this.gobj != null){
				this.TurnOffCollider();
				this.TurnOffVisibility();
			}
		}
		
		public void Reset(){
			if (!this.isProjectile){
				/*if (this.gobj != null){
					this.gobj.transform.localPosition = Vector3.zero;
					this.gobj.transform.localScale = Vector3.one;
				}*/
				this.Disable();
			}
		}
		
		public void TurnOnCollider(){
			if (!this.gobj.collider.enabled){
				this.gobj.collider.enabled = true;
			}
		}
		
		public void TurnOffCollider(){
			if (this.gobj.collider.enabled){
				this.gobj.collider.enabled = false;
			}
		}
		
		public void TurnOnVisibility(){
			if (!this.gobj.renderer.enabled){
				this.gobj.renderer.enabled = true;
			}
		}
		
		public void TurnOffVisibility(){
			if (this.gobj.renderer.enabled){
				this.gobj.renderer.enabled = false;
			}
		}
		
		public GameObject Gobj{
			get { return this.gobj; }
		}
	}
}
