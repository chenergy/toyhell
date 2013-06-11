using UnityEngine;
using System.Collections;

public class CheckPoint : MonoBehaviour {
	private bool triggered = false;
	private GameObject particles;
	// Use this for initialization
	void Start () {
		this.particles = (GameObject)Resources.LoadAssetAtPath("Assets/Prefabs/Particles/RespawnParticles.prefab", typeof(GameObject));
		this.renderer.enabled = false;
	}
	
	// Update is called once per frame
	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player" & !this.triggered){
			GameData.LastCheckpoint = this.gameObject;
			GameObject newParticles = (GameObject)GameObject.Instantiate(this.particles, this.transform.position, Quaternion.identity);
			GameObject.Destroy(newParticles, 1.0f);
			this.triggered = true;
		}
	}
}
