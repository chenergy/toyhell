using UnityEngine;
using System.Collections;

public class DeathBox : MonoBehaviour {
	
	// Test using random parts. Will get parts from character in later trials
	//public float respawnTime = 3.0f;
	//private bool isHookerDead = false;
	//private bool isRobotDead = false;
	//private float hookerTimer = 0.0f;	// respawn timers can be placed on characters for data continuity
	//private float robotTimer = 0.0f;
	
	// Use this for initialization
	void Start () {
		this.renderer.enabled = false;
	}
	
	void Update(){
		/*
		if (isHookerDead){
			GameData.Hooker.transform.position = GameData.Robot.transform.position;
			if (hookerTimer > respawnTime){
				isHookerDead = false;
				hookerTimer = 0.0f;
				GameData.RespawnPlayer(GameData.Hooker);

			}
			hookerTimer += Time.deltaTime;
			Debug.Log("Respawn" + hookerTimer);
		}
		
		if (isRobotDead){
			GameData.Robot.transform.position = GameData.Hooker.transform.position;
			if (robotTimer > respawnTime){
				isRobotDead = false;
				robotTimer = 0.0f;
				GameData.RespawnPlayer(GameData.Robot);
				
			}
			robotTimer += Time.deltaTime;
			Debug.Log("Respawn" + robotTimer);
		}
		*/
	}
	
	// Update is called once per frame
	void OnTriggerEnter(Collider other){
		GameObject player = other.gameObject;
		
		if (player.tag == "Player"){
			GameData.KillPlayer(player);
			/*
			if (player.name == "Robot"){
				isRobotDead = true;
			}
			else{
				isHookerDead = true;
			}
			*/
		}
	}
}
