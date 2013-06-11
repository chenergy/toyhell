using System;
using System.Collections.Generic;
using UnityEngine;
using FSM;
using Actors;

namespace FSM
{
	public class Repository
	{
		protected GameObject gobj;
		protected Dictionary<string, object> attributes;
		
		public Repository (Dictionary<string, object> attributes, GameObject gobj)
		{
			this.attributes = attributes;
			this.gobj = gobj;
		}
		
        public void Add(string name, object o)
        {
            if (!attributes.ContainsKey(name))
                attributes.Add(name, o);
            else
				Debug.Log("Attributes already contains "+name);
        }
		
		public bool ContainsAttribute(string name){
			if (attributes.ContainsKey(name))
				return true;
			return false;
		}
		
		public object Get(string key){
			return (object)attributes[key];
		}
		
		public GameObject Gobj{
			get { return (GameObject)gobj; }
		}
	}
}

