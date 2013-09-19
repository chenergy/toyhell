using UnityEngine;
using System.Collections;
using ToyHell;

public class WeaponInput : MonoBehaviour
{
	public WeaponName		weaponName;
	public GameObject		gobj;
	public bool				isProjectile = false;
	public GameObject 		hitbox;
	public int				attacks = 1;
	public AnimationClip	attackAnimation;
	public float			speed = 1.0f;
}