using UnityEngine;
using System.Collections;

public class TriggerPlatforms : MonoBehaviour 
{
	//public GameObject Platform;
	private bool colliding = false;
	private bool activated = false;
	private KeyCode hookerActivateKey;
	private KeyCode hookerActivateJoystick;
	private KeyCode robotActivateKey;
	private KeyCode robotActivateJoystick;
	
	
	// Use this for initialization
	void Start () {
		this.hookerActivateKey = GameData.Hooker.GetComponent<PlayerInput>().controls.ActivateKey;
		this.hookerActivateJoystick = GameData.Hooker.GetComponent<PlayerInput>().controls.ActivateJoystick;
		this.robotActivateKey = GameData.Robot.GetComponent<PlayerInput>().controls.ActivateKey;
		this.robotActivateJoystick = GameData.Robot.GetComponent<PlayerInput>().controls.ActivateJoystick;
	}
	
	// Update is called once per frame
	void Update () {
        // use GamePadManager.Activate to determine the interaction
        // it'll return true when ever either player hits the interact button
		if (Input.GetKeyDown(this.hookerActivateKey) || Input.GetKeyDown(this.hookerActivateJoystick) ||
			Input.GetKeyDown(this.robotActivateKey) || Input.GetKeyDown(this.robotActivateJoystick)){
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
	
	void OnTriggerExit(Collider player){
		colliding = false;
	}
}
