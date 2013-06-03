using UnityEngine;
using System.Collections;

public class DeathBox : MonoBehaviour {

	void Start () {
		this.renderer.enabled = false;
	}
	
	// Update is called once per frame
	void OnTriggerEnter(Collider other){
		GameObject player = other.gameObject;
		
		if (player.tag == "Player"){
			GameData.KillPlayer(player);
		}
		else if (player.tag == "Enemy"){
			player.GetComponent<EnemyInput>().KillEnemy();
		}
	}
}
