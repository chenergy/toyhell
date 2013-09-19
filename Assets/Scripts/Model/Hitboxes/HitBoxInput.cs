using UnityEngine;
using System.Collections;
using ToyHell;

public class HitBoxInput : MonoBehaviour
{
	[HideInInspector]
	public HitBox hitbox;
	
	void OnTriggerEnter( Collider other ){
		Debug.Log(hitbox.Gobj.name + " is Colliding with " + other.gameObject.name);
		/*
		if (other.tag == "HurtBox"){
			HurtBox hurtbox = other.GetComponent<HurtBoxInput>().hurtbox;
			if (hurtbox.owner.playerNumber != this.hitbox.owner.playerNumber){
				hurtbox.owner.TakeDamage(this.hitbox.damage, hurtbox, Vector3.left);
				this.hitbox.Disable();
				
				if (this.hitbox.isProjectile){
					GameObject.Destroy(this.transform.parent.gameObject);
				}
			}
		}
		
		else if (other.tag == "HitBox"){
			HitBox otherHitbox = other.GetComponent<HitBoxInput>().hitbox;
			if (otherHitbox.owner.playerNumber != this.hitbox.owner.playerNumber){
				if (this.hitbox.isProjectile && (this.tag != "SuperHitBox")){
					GameObject.Destroy(this.transform.parent.gameObject);
				}
			}
		}
		
		else if (other.tag == "SuperHitBox"){
			HitBox otherHitbox = other.GetComponent<HitBoxInput>().hitbox;
			if (otherHitbox.owner.playerNumber != this.hitbox.owner.playerNumber){
				if (this.hitbox.isProjectile){
					GameObject.Destroy(this.transform.parent.gameObject);
				}
			}
		}
		*/
	}
}

