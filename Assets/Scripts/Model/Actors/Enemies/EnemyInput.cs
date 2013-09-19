using UnityEngine;
using System.Collections;

public abstract class EnemyInput : MonoBehaviour
{
	protected float					attackSpeed;
		protected GameObject			deathParts;
		protected float					knockbackStrength;
		protected bool					hasAttacked;
		protected float					fadeTime;
		protected float					agroRange;
		protected float					attackRange;
		protected float					damage;
		//protected float				attackTime;
		protected GameObject			drop;
		protected GameObject			hitbox;
}

