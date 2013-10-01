using UnityEngine;
using System;
using System.Collections.Generic;
using ToyHell;

[System.Serializable]
public class EnemyAnimations{
	public AnimationClip Idle;
	public AnimationClip Walk;
	public AnimationClip Attack;
	public AnimationClip Jump;
	public AnimationClip Hurt;
}

public abstract class EnemyInput : MonoBehaviour
{
	public GameObject			deathParts;
	public GameObject			drop;
	public string				name;
	public float				attackSpeed 		= 1.0f;
	public float				knockbackStrength 	= 1.0f;
	public float				fadeTime 			= 1.0f;
	public float				attackRange 		= 1.0f;
	public float				damage 				= 10.0f;
	public EnemyAnimations 		animations;
	
	[HideInInspector]
	public Dictionary<FighterAnimation, string> animationNameMap;
	
	void Awake(){
		this.animationNameMap = new Dictionary<FighterAnimation, string>();
		
		this.animationNameMap[EnemyAnimation.IDLE] 			= this.animations.Idle.name;
		this.animationNameMap[EnemyAnimation.WALK] 			= this.animations.Walk.name;
		this.animationNameMap[EnemyAnimation.ATTACK] 		= this.animations.Attack.name;
		this.animationNameMap[EnemyAnimation.JUMP]		 	= this.animations.Jump.name;
		this.animationNameMap[EnemyAnimation.HURT]		 	= this.animations.Hurt.name;
		
	}
}

