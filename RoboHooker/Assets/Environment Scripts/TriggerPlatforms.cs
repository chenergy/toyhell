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
<<<<<<< HEAD
		if (Input.GetButton("Fire2")){
=======
//		if (Input.GetButton("P1interact")){
>>>>>>> 8fbe32a61e325182eaec12b923d0ef2c240b5ae4
			if (colliding && !activated){
				Debug.Log("playing platform animation");
				GameObject platform = GameObject.Find("Platform");
				GameObject trigger = GameObject.Find("trigger");
				platform.animation["platform_animations"].speed = .5f;
				platform.animation.Play("platform_animations");
				trigger.animation.Play("trigger");
				
				activated = true;
//			}
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

