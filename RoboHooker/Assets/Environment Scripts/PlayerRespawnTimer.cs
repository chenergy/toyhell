using UnityEngine;
using System.Collections.Generic;
using Actors;

public class PlayerRespawnTimer : MonoBehaviour
{
	public class PlayerRespawnStats{
		public float		timer;
		public bool			isAlive;
		public GameObject 	player;
	}
	
	public float respawnTime = 3.0f;
	public Dictionary<GameObject, PlayerRespawnStats> playerStats;
	
	// Use this for initialization
	void Start ()
	{
		PlayerRespawnStats robot = new PlayerRespawnStats();
		robot.timer 	= 0.0f;
		robot.isAlive 	= true;
		robot.player 	= GameData.Robot;
		
		PlayerRespawnStats hooker = new PlayerRespawnStats();
		hooker.timer	= 0.0f;
		hooker.isAlive	= true;
		hooker.player	= GameData.Hooker;
		
		this.playerStats = new Dictionary<GameObject, PlayerRespawnStats>();
		this.playerStats[GameData.Robot] = robot;
		this.playerStats[GameData.Hooker] = hooker;
	}
	
	// Update is called once per frame
	void Update ()
	{
		foreach (PlayerRespawnStats stats in playerStats.Values){
			if ( (stats.timer > respawnTime) && (!stats.isAlive) ){
				GameData.RespawnPlayer(stats.player);
				stats.isAlive = true;
			}
			else if (!stats.isAlive){
				stats.timer += Time.deltaTime;
				stats.player.transform.position = GameData.LastCheckpoint.transform.position;
			}
		}
	}
	
	public void StartTimer(GameObject player){
		this.playerStats[player].timer = 0.0f;
		this.playerStats[player].isAlive = false;
	}
}

