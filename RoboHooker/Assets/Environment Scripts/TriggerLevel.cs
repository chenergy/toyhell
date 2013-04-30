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
        //this is probably going to need some more work
        //I'll make an input handling class tonight to help avoid this -Fern
		if (Input.GetButton("P1interact")){
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
