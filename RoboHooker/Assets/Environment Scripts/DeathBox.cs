using UnityEngine;
using System.Collections;

public class DeathBox : MonoBehaviour {
	
	// Test using random parts. Will get parts from character in later trials
	public GameObject parts;
	public float fadeTime = 3.0f;
	public float respawnTime = 3.0f;
	private bool isHookerDead = false;
	private bool isRobotDead = false;
	private float hookerTimer = 0.0f;	// respawn timers can be placed on characters for data continuity
	private float robotTimer = 0.0f;
	
	// Use this for initialization
	void Start () {
		this.renderer.enabled = false;
	}
	
	void Update(){
		if (isHookerDead){
			if (hookerTimer > respawnTime){
				isHookerDead = false;
				hookerTimer = 0.0f;
				GameData.Hooker.transform.position = GameData.LastCheckpoint.transform.position;
				GameData.Hooker.GetComponent<PlayerCharacter>().Frozen = false;
				GameData.Respawn("hooker");
				
				foreach (SkinnedMeshRenderer smr in GameData.Hooker.GetComponentsInChildren<SkinnedMeshRenderer>()){
					smr.enabled = true;
				}
				foreach (MeshRenderer mr in GameData.Hooker.GetComponentsInChildren<MeshRenderer>()){
					mr.enabled = true;
				}
			}
			hookerTimer += Time.deltaTime;
			Debug.Log("Respawn" + hookerTimer);
		}
		
		if (isRobotDead){
			if (robotTimer > respawnTime){
				isRobotDead = false;
				robotTimer = 0.0f;
				GameData.Robot.transform.position = GameData.LastCheckpoint.transform.position;
				GameData.Robot.GetComponent<PlayerCharacter>().Frozen = false;
				GameData.Respawn("robot");
				
				foreach (SkinnedMeshRenderer smr in GameData.Robot.GetComponentsInChildren<SkinnedMeshRenderer>()){
					smr.enabled = true;
				}
				foreach (MeshRenderer mr in GameData.Robot.GetComponentsInChildren<MeshRenderer>()){
					mr.enabled = true;
				}
			}
			robotTimer += Time.deltaTime;
			Debug.Log("Respawn" + robotTimer);
		}
	}
	
	// Update is called once per frame
	void OnTriggerEnter(Collider other){
		GameObject player = other.gameObject;
		player.GetComponent<PlayerCharacter>().Frozen = true;
		foreach (SkinnedMeshRenderer smr in player.GetComponentsInChildren<SkinnedMeshRenderer>()){
			smr.enabled = false;
		}
		foreach (MeshRenderer mr in player.GetComponentsInChildren<MeshRenderer>()){
			mr.enabled = false;
		}
		// Test with assigned parts
		GameObject deathParts = (GameObject)GameObject.Instantiate(parts, player.transform.position, Quaternion.identity);
		GameObject.Destroy(deathParts, fadeTime);
		
		if (player.tag == "Player"){
			if (GameObject.Find("Robot") == player){
				isRobotDead = true;
				GameData.SetDead("robot");
			}
			else{
				isHookerDead = true;
				GameData.SetDead("hooker");
			}
		}
	}
	
	void OnTriggerStay(Collider other){
		
		
	}
}
