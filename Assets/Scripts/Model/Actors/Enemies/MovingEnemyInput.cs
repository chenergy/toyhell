using UnityEngine;
using System;
using System.Collections.Generic;
using ToyHell;

public class MovingEnemyInput : EnemyInput
{
	public GameObject 	patrolPoint1;
	public GameObject 	patrolPoint2;
	public float		moveSpeed 		= 1.0f;
	public float		agroRange 		= 5.0f;
	public float 		patrolPauseTime = 1.0f;
	
	[HideInInspector]
	public Enemy_Moving	enemy;
	
	void Update(){
		enemy.Update();
	}
}