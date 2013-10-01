using UnityEngine;
using System;
using System.Collections.Generic;
using ToyHell;

public class StaticEnemyInput : EnemyInput
{
	[HideInInspector]
	public Enemy_Static	enemy;
	
	void Update(){
		enemy.Update();
	}
}

