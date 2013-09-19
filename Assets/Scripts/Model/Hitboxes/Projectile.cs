using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ToyHell;

namespace ToyHell
{
	public class Projectile
	{
		public GameObject 	gobj;
		public Fighter	owner;
		public string		projectileName;
		
		public Projectile(string ProjectileName, Fighter owner){
			this.gobj = gobj;
			this.owner = owner;
		}
	}
}