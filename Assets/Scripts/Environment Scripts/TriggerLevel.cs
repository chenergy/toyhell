using UnityEngine;
using System.Collections;

public class TriggerLevel : MonoBehaviour
{
	//public GameObject Platform;
	private bool colliding = false;
	private bool activated = false;
	public string levelToLoad = "level";
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
        // use GamePadManager.Activate to determine the interaction
        // it'll return true when ever either player hits the interact butto
    
		if (colliding)
		{
			Debug.Log("loading next level");
			Application.LoadLevel(levelToLoad);
		}
	}
	
	void OnTriggerEnter(Collider player) 
	{
		if (player.gameObject.tag == "Player")
		{
			//colliding = true;
			Application.LoadLevel(levelToLoad);
		}
		
    }
	
	void OnTriggerExit(Collider player){
		colliding = false;
	}
}