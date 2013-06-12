using UnityEngine;
using System.Collections;

public class TriggerTrain : MonoBehaviour 
{
	//public GameObject Platform;
	private bool colliding = false;
	private bool activated = false;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        // use GamePadManager.Activate to determine the interaction
        // it'll return true when ever either player hits the interact button
		if (colliding && !activated){
			Debug.Log("playing train animation");
			GameObject train = GameObject.Find("Train");
			//GameObject trigger = GameObject.Find("trigger");
			//train.animation["train_anim"].speed = .5f;
			train.animation.Play("train_animation");
			//trigger.animation.Play("trigger");
			
			activated = true;
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
