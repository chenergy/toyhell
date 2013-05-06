using UnityEngine;
using System.Collections;

public class TriggerPlatforms : MonoBehaviour 
{
	//public GameObject Platform;
	private bool colliding = false;
	private bool activated = false;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetButton("P1interact")){
			if (colliding && !activated){
				Debug.Log("playing platform animation");
				GameObject platform = GameObject.Find("Platform");
				GameObject trigger = GameObject.Find("trigger");
				platform.animation["platform_animations"].speed = .5f;
				platform.animation.Play("platform_animations");
				trigger.animation.Play("trigger");
				
				activated = true;
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

