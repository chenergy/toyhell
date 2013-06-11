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
	
	private GameObject player1;
	private GameObject player2;
	private PlayerInput P1_Input;
	private PlayerInput P2_Input;
	// Use this for initialization
	void Start ()
	{
		this.renderer.enabled = false;
		
		PlayerStats hookerStats = new PlayerStats();
		PlayerStats robotStats = new PlayerStats();
		
		playerStats = new Dictionary<GameObject, PlayerStats>();
		playerStats[GameData.Hooker] = hookerStats;
		playerStats[GameData.Robot] = robotStats;
		
		player1 = this.GetPlayerByPlayerNumber(PlayerNumber.P1);
		player2 = this.GetPlayerByPlayerNumber(PlayerNumber.P2);
		
		P1_Input = player1.GetComponent<PlayerInput>();
		P2_Input = player2.GetComponent<PlayerInput>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		bool P1_movement = false;
		bool P2_movement = false;
		/*
		GameObject player1 = this.GetPlayerByPlayerNumber(PlayerNumber.P1);
		GameObject player2 = this.GetPlayerByPlayerNumber(PlayerNumber.P2);
		
		PlayerInput P1_Input = player1.GetComponent<PlayerInput>();
		PlayerInput P2_Input = player2.GetComponent<PlayerInput>();
		*/
		if ((Mathf.Abs(Input.GetAxisRaw(P1_Input.XAxis)) > 0) || (Input.GetKey(P1_Input.controls.LeftKey)) || (Input.GetKey(P1_Input.controls.RightKey))){
			P1_movement = true;
		}
		
		if ((Mathf.Abs(Input.GetAxisRaw(P2_Input.XAxis)) > 0) || (Input.GetKey(P2_Input.controls.LeftKey)) || (Input.GetKey(P2_Input.controls.RightKey))){
			P2_movement = true;
		}
		
		if (this.player1 != null){
			PlayerStats stats = this.playerStats[player1];
			
			if (!P1_movement){
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
		
		
		if (this.player2 != null){
			PlayerStats stats = this.playerStats[player2];
			
			if (!P2_movement){
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
		// Will adjust for enemies, time allowing
		/*
		foreach (GameObject gobj in this.playerStats.Keys){
			if (gobj.tag == "Enemy"){
				PlayerStats stats = this.playerStats[gobj];
				if (stats.onPlatform){
					gobj.collider.transform.position = new Vector3(this.transform.position.x + stats.offset,
						gobj.collider.transform.position.y,
						gobj.collider.transform.position.z
						);
				}
				stats.offset = player2.collider.transform.position.x - this.transform.position.x;
			}
		}*/
	}
	
	
	private GameObject GetPlayerByPlayerNumber(PlayerNumber num){
		if (GameData.Hooker.GetComponent<PlayerInput>().controls.player == num){
			return GameData.Hooker;
		}
		else if (GameData.Robot.GetComponent<PlayerInput>().controls.player == num){
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
		
		// Will adjust for enemies, time allowing
		/*
		else if (player.tag == "Enemy"){
			PlayerStats stats = this.playerStats[player];
			
			if (stats != null){
				stats.offset = player.collider.transform.position.x - this.transform.position.x;
				stats.onPlatform = true;
			}
			else{
				PlayerStats enemyStats = new PlayerStats();
				this.playerStats[player] = enemyStats;
			}
		}*/
	}
	
	void OnTriggerStay(Collider other){

	}
	
	void OnTriggerExit(Collider other){
		GameObject player = other.gameObject;
		
		if (player.tag == "Player" || player.tag == "Enemy"){
			PlayerStats stats = this.playerStats[player];
			
			if (stats != null){
				stats.offset = 0.0f;
				stats.onPlatform = false;
			}
		}
	}
}

