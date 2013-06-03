using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovingPlatform : MonoBehaviour
{
	private class PlayerStats{
		public float offset;
		public bool onPlatform;
		
		public PlayerStats(){
			this.offset = 0.0f;
			this.onPlatform = false;
		}
	}
	
	private Dictionary<GameObject, PlayerStats> playerStats;
	
	// Use this for initialization
	void Start ()
	{
		this.renderer.enabled = false;
		
		PlayerStats hookerStats = new PlayerStats();
		PlayerStats robotStats = new PlayerStats();
		
		playerStats = new Dictionary<GameObject, PlayerStats>();
		playerStats[GameData.Hooker] = hookerStats;
		playerStats[GameData.Robot] = robotStats;
	}
	
	// Update is called once per frame
	void Update ()
	{
		float P1_movement = Input.GetAxis("P1moveX");
        float P2_movement = Input.GetAxis("P2moveX");
		
		GameObject player1 = this.GetPlayerByGamepad(gamepad.one);
		GameObject player2 = this.GetPlayerByGamepad(gamepad.two);
		
		if (player1 != null){
			PlayerStats stats = this.playerStats[player1];
			
			if (P1_movement == 0){
				if (stats.onPlatform){
					player1.collider.transform.position = new Vector3(this.transform.position.x + stats.offset,
						player1.collider.transform.position.y,
						player1.collider.transform.position.z
						);
				}
			}
			else{
				stats.offset = player1.collider.transform.position.x - this.transform.position.x;
			}
		}
		
		
		if (player2 != null){
			PlayerStats stats = this.playerStats[player2];
			
			if (P2_movement == 0){
				if (stats.onPlatform){
					player2.collider.transform.position = new Vector3(this.transform.position.x + stats.offset,
						player2.collider.transform.position.y,
						player2.collider.transform.position.z
						);
				}
			}
			else{
				stats.offset = player2.collider.transform.position.x - this.transform.position.x;
			}
		}
	}
	
	
	private GameObject GetPlayerByGamepad(gamepad num){
		if (GameData.Hooker.GetComponent<PlayerCharacter>().m_player == num){
			return GameData.Hooker;
		}
		else if (GameData.Robot.GetComponent<PlayerCharacter>().m_player == num){
			return GameData.Robot;
		}
		else{
			return null;
		}
	}
	
	
	void OnTriggerEnter(Collider other){
		GameObject player = other.gameObject;
		
		if (player.tag == "Player"){
			PlayerStats stats = this.playerStats[player];
			
			if (stats != null){
				stats.offset = player.collider.transform.position.x - this.transform.position.x;
				stats.onPlatform = true;
			}
		}
	}
	
	void OnTriggerStay(Collider other){

	}
	
	void OnTriggerExit(Collider other){
		GameObject player = other.gameObject;
		
		if (player.tag == "Player"){
			PlayerStats stats = this.playerStats[player];
			
			if (stats != null){
				stats.offset = 0.0f;
				stats.onPlatform = false;
			}
		}
	}
}

