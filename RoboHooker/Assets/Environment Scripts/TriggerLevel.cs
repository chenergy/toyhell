using UnityEngine;
using System.Collections;

public class TriggerLevel : MonoBehaviour
{
	//public GameObject Platform;
	private bool colliding = false;
	private bool activated = false;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton("Fire2")){
			if (colliding && !activated){
				Debug.Log("loading next level");
				Application.LoadLevel("level1");
			}
		}
	}
	
	void OnTriggerEnter(Collider player) 
	{
		if (player.tag == "Player")
		{
			colliding = true;
		}
		
    }
	
	void OnTriggerExti(Collider player){
		colliding = false;
	}
}
