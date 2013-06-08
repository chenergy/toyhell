using UnityEngine;
using System.Collections;

public class GameData {
	public static GameData instance = new GameData();
	
	private GameObject hooker;
	private int hooker_currenthp = 100;
	private int hooker_maxhp = 100;
	private int hooker_lives = 3;
	private GameObject hookerDeathParts;
	
	private GameObject robot;
	private int robot_currenthp = 100;
	private int robot_maxhp = 100;
	private int robot_lives = 3;
	private GameObject robotDeathParts;
	
	private GameObject lastCheckpoint;
	private GameObject respawnParticles;
	private GameObject ui;
	private float partsFadeTime = 3.0f;
	
	private GameData(){
		this.hooker = GameObject.Find("Hooker");
		this.robot  = GameObject.Find("Robot");
		this.ui		= GameObject.Find("PlayerUI");
		this.robotDeathParts = (GameObject)Resources.LoadAssetAtPath("Assets/Prefabs/DeathParts/RobotParts.prefab", typeof(GameObject));
		this.hookerDeathParts = (GameObject)Resources.LoadAssetAtPath("Assets/Prefabs/DeathParts/HookerParts.prefab", typeof(GameObject));
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
		player.collider.enabled = false;
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
			instance.robot_lives--;
			if (instance.robot_lives > 0) {
				instance.robot_currenthp = 0;
				instance.robot.collider.enabled = false;
				deathParts = (GameObject)GameObject.Instantiate(instance.robotDeathParts, player.transform.position, Quaternion.identity);
				GameObject.Destroy(deathParts, instance.partsFadeTime);
				instance.ui.GetComponent<PlayerRespawnTimer>().StartTimer(player); 
			}else{
				instance.robot.transform.position = instance.hooker.transform.position;
				instance.robot.transform.parent = instance.hooker.transform;
			}
		}
		else if (player.name == "Hooker"){
			instance.hooker_lives--;
			if (instance.hooker_lives > 0) {
				instance.hooker_currenthp = 0;
				instance.hooker.collider.enabled = false;
				deathParts = (GameObject)GameObject.Instantiate(instance.hookerDeathParts, player.transform.position, Quaternion.identity);
				GameObject.Destroy(deathParts, instance.partsFadeTime);
				instance.ui.GetComponent<PlayerRespawnTimer>().StartTimer(player); 
			}else{
				instance.hooker.transform.position = instance.robot.transform.position;
				instance.hooker.transform.parent = instance.robot.transform;
			}
		}
		
		if (instance.robot_lives <= 0 && instance.hooker_lives <= 0){
			GameObject.Find("Fader").GetComponent<fadeToBlackScript>().SetLoadLevel("LoseScreen");
			GameObject.Find("Fader").GetComponent<fadeToBlackScript>().FadeOut();
		}
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
		set { instance.hooker = value; }
	}
	
	public static GameObject Robot {
		get { return instance.robot; }
		set { instance.robot = value; }
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
	
	public static float RobotRespawnTime{
		get { return instance.ui.GetComponent<PlayerRespawnTimer>().respawnTime -
			instance.ui.GetComponent<PlayerRespawnTimer>().playerStats[instance.robot].timer; }
	}
	
	public static float HookerRespawnTime{
		get { return instance.ui.GetComponent<PlayerRespawnTimer>().respawnTime -
			instance.ui.GetComponent<PlayerRespawnTimer>().playerStats[instance.hooker].timer; }
	}
	
	public static int RobotLives{
		get { return instance.robot_lives; }
	}
	
	public static int HookerLives{
		get { return instance.hooker_lives; }
	}
	
	public static GameObject UI{
		get { return instance.ui; }
		set { instance.ui = value; }
	}
}
