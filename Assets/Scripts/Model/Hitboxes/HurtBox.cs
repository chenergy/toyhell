using UnityEngine;
using System.Collections;
using ToyHell;
using System.Collections.Generic;

namespace ToyHell
{
	public class HurtBox
	{
		public Fighter 	owner;
		public GameObject 	gobj;
		public Location		location;
		
		public HurtBox(Fighter owner, GameObject gobj)
		{
			this.owner = owner;
			this.gobj = gobj;
			this.location = gobj.GetComponent<HurtBoxInput>().location;
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
	}
}
