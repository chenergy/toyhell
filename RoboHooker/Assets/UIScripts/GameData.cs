using UnityEngine;
using System.Collections;

public class GameData {
	public static GameData instance = new GameData();
	
	private GameObject hooker;
	private int hooker_currenthp = 100;
	private int hooker_maxhp = 100;
	private GameObject hookerDeathParts;
	
	private GameObject robot;
	private int robot_currenthp = 100;
	private int robot_maxhp = 100;
	private GameObject robotDeathParts;
	
	private GameObject lastCheckpoint;
	private GameObject respawnParticles;
	
	private float partsFadeTime = 3.0f;
	
	private GameData(){
		this.hooker = GameObject.Find("Hooker");
		this.robot = GameObject.Find("Robot");
		this.robotDeathParts = (GameObject)Resources.LoadAssetAtPath("Assets/Prefabs/DeathParts/RobotParts.prefab", typeof(GameObject));
		this.hookerDeathParts = (GameObject)Resources.LoadAssetAtPath("Assets/Prefabs/DeathParts/RobotParts.prefab", typeof(GameObject));
		this.respawnParticles = (GameObject)Resources.LoadAssetAtPath("Assets/Prefabs/Particles/RespawnParticles.prefab", typeof(GameObject));
	}
	
	public static void GainHp(GameObject player, int hp){
		if (player == instance.hooker){
			if (instance.hooker_currenthp + hp >= 100){
				instance.hooker_currenthp = 100;
			}
			else{
				instance.hooker_currenthp += hp;
			}
		}
		else if (player == instance.robot){
			if (instance.robot_currenthp + hp >= 100){
				instance.robot_currenthp = 100;
			}
			else{
				instance.robot_currenthp += hp;
			}
		}
	}
	
	public static void LoseHp(GameObject player, int damage){
		if (player == instance.hooker){
			if (instance.hooker_currenthp - damage <= 0){
				instance.hooker_currenthp = 0;
				GameData.KillPlayer(instance.hooker);
			}
			else{
				instance.hooker_currenthp -= damage;
			}
		}
		else if (player == instance.robot){
			if (instance.robot_currenthp - damage <= 0){
				instance.robot_currenthp = 0;
				GameData.KillPlayer(instance.robot);
			}
			else{
				instance.robot_currenthp -= damage;
			}
		}
	}
	
	public static void KillPlayer(GameObject player){
		player.GetComponent<PlayerCharacter>().Frozen = true;
		foreach (SkinnedMeshRenderer smr in player.GetComponentsInChildren<SkinnedMeshRenderer>()){
			smr.enabled = false;
		}
		foreach (MeshRenderer mr in player.GetComponentsInChildren<MeshRenderer>()){
			mr.enabled = false;
		}
		// Test with assigned parts
		GameObject deathParts;
	
		if (player.name == "Robot"){
			instance.robot_currenthp = 0;
			instance.robot.collider.enabled = false;
			deathParts = (GameObject)GameObject.Instantiate(instance.robotDeathParts, player.transform.position, Quaternion.identity);
		}
		else{
			instance.hooker_currenthp = 0;
			instance.hooker.collider.enabled = false;
			deathParts = (GameObject)GameObject.Instantiate(instance.hookerDeathParts, player.transform.position, Quaternion.identity);
		}
		
		player.GetComponent<PlayerRespawnTimer>().StartTimer();
		GameObject.Destroy(deathParts, instance.partsFadeTime);
	}
	
	public static void RespawnPlayer(GameObject player){
		player.collider.enabled = true;
		player.transform.position = instance.lastCheckpoint.transform.position;
		player.GetComponent<PlayerCharacter>().Frozen = false;
		
		foreach (SkinnedMeshRenderer smr in player.GetComponentsInChildren<SkinnedMeshRenderer>()){
			smr.enabled = true;
		}
		foreach (MeshRenderer mr in player.GetComponentsInChildren<MeshRenderer>()){
			mr.enabled = true;
		}
		
		if (player.name == "Robot"){
			instance.robot_currenthp = 100;
		}
		else if (player.name == "Hooker"){
			instance.hooker_currenthp = 100;
		}
		
		GameObject newParticles = (GameObject)GameObject.Instantiate(instance.respawnParticles, player.transform.position, Quaternion.identity);
		GameObject.Destroy(newParticles, 1.0f);
	}

	public static GameObject Hooker {
		get { return instance.hooker; }
	}
	
	public static GameObject Robot {
		get { return instance.robot; }
	}
	
	public static int HookerHp{
		get { return instance.hooker_currenthp; }
	}
	
	public static int RobotHp{
		get { return instance.robot_currenthp; }
	}
	
	public static int HookerMaxHp{
		get { return instance.hooker_maxhp; }
	}
	
	public static int RobotMaxHp{
		get { return instance.robot_maxhp; }
	}
	
	public static GameObject LastCheckpoint{
		get { return instance.lastCheckpoint; }
		set { instance.lastCheckpoint = value; }
	}
}
