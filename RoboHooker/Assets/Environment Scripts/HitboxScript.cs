using UnityEngine;
using System.Collections;

public class HitboxScript : MonoBehaviour {
	
	public int damage;
	public float attackLength;
	private float timer = 0.0f;
	
	void Update () {
		if (timer > attackLength){
			GameObject.Destroy(this.gameObject);
		}
		timer += Time.deltaTime;
	}
}
