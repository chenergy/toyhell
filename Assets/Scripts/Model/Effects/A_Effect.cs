using UnityEngine;
using System.Collections;
using ToyHell;
using FSM;

namespace ToyHell
{
	public abstract class A_Effect
	{
		protected Vector3 position;
		protected Quaternion rotation;
		protected GameObject prefab;
		
		public abstract void Play();
		public abstract void DestroySelf();
	}
}

