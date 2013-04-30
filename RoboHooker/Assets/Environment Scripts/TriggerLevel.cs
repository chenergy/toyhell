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
        // use GamePadManager.Activate to determine the interaction
        // it'll return true when ever either player hits the interact button
        if (GamePadManager.Activate)
        {
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
