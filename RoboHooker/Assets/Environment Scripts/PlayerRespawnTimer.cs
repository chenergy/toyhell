using UnityEngine;
using System.Collections;
using Actors;

public class PlayerRespawnTimer : MonoBehaviour
{
	public float respawnTime = 3.0f;
	public float timer = 0.0f;
	public bool isAlive = true;
	
	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
		if ( (timer > respawnTime) && (!isAlive) ){
			GameData.RespawnPlayer(this.gameObject);
			isAlive = true;
		}
		else if (!isAlive){
			timer += Time.deltaTime;
			if (this.gameObject.name == "Hooker"){
				GameObject robot = GameObject.Find("Robot");
				if (robot.GetComponent<PlayerRespawnTimer>().isAlive)
					this.gameObject.transform.position = robot.transform.position;
				else
					this.gameObject.transform.position = GameData.LastCheckpoint.transform.position;
			}
			else if (this.gameObject.name == "Robot"){
				GameObject hooker = GameObject.Find("Hooker");
				if (hooker.GetComponent<PlayerRespawnTimer>().isAlive)
					this.gameObject.transform.position = hooker.transform.position;
				else
					this.gameObject.transform.position = GameData.LastCheckpoint.transform.position;
			}
		}
	}
	
	public void StartTimer(){
		timer = 0.0f;
		isAlive = false;
	}
}

