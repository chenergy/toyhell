using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using ToyHell;

[System.Serializable]
public class FighterAnimations{
	public AnimationClip Idle;
	public AnimationClip Walk;
	public AnimationClip SocketAttack;
	public AnimationClip SpecialAttack;
	public AnimationClip Jump;
	public AnimationClip Hurt;
	public AnimationClip Spawn;
	public AnimationClip Block;
}

public class FighterInput : MonoBehaviour
{
	public string 			name;
	public float 			moveSpeed;
	public float			jumpHeight;
	public Transform		socketJoint;
	public FighterAnimations animations;
	
	[HideInInspector]
	public Dictionary<FighterAnimation, string> animationNameMap;
	
	[HideInInspector]
	public Fighter 		fighter;	// assigned in Fighter
	
	void Awake(){
		this.animationNameMap = new Dictionary<FighterAnimation, string>();
		
		this.animationNameMap[FighterAnimation.IDLE] 			= this.animations.Idle.name;
		this.animationNameMap[FighterAnimation.WALK] 			= this.animations.Walk.name;
		this.animationNameMap[FighterAnimation.SOCKET_ATTACK] 	= this.animations.SocketAttack.name;
		this.animationNameMap[FighterAnimation.SPECIAL_ATTACK]	= this.animations.SpecialAttack.name;
		this.animationNameMap[FighterAnimation.JUMP]		 	= this.animations.Jump.name;
		this.animationNameMap[FighterAnimation.HURT]		 	= this.animations.Hurt.name;
		this.animationNameMap[FighterAnimation.SPAWN]		 	= this.animations.Spawn.name;
		this.animationNameMap[FighterAnimation.BLOCK]		 	= this.animations.Block.name;
	}
}