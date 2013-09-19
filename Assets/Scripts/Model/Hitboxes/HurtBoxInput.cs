using UnityEngine;
using System.Collections;
using ToyHell;

public enum Location { HIGH, LOW, NONE };

public class HurtBoxInput : MonoBehaviour
{
	[HideInInspector]
	public HurtBox hurtbox;
	
	public Location location;
	
	/*
	void OnTriggerEnter( Collider other ){
		//Debug.Log(hitbox.gobj.name + " is Colliding with " + other.gameObject.name);
		if (other.tag == "HitBox"){
			HurtBox hurtbox = other.GetComponent<HurtBoxInput>().hurtbox;
			if (hurtbox.owner.playerNumber != hitbox.owner.playerNumber){
				hurtbox.owner.TakeDamage(hitbox.damage);
				hitbox.Disable();
				
				if (hitbox.isProjectile){
					GameObject.Destroy(this.transform.parent.gameObject);
				}
			}
		}
	}
	*/
}

