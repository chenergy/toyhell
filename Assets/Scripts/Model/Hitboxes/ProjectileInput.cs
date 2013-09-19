using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ToyHell;

public class ProjectileInput : MonoBehaviour
{
	public GameObject 	hitboxObject;
	public float		speed;
	
	[HideInInspector]
	public Vector3		increment;
	//[HideInInspector]
	//public HitBox 		hitbox;
	
	void Update(){
		this.transform.Translate(increment);
	}
}

