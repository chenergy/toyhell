using UnityEngine;
using System.Collections;

public class TriggerPlatforms : MonoBehaviour 
{
	//public GameObject Platform;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider player) 
	{
		if (player.tag == "Player")
		{
			Debug.Log("playing platform animation");
			GameObject platform = GameObject.Find("Platform");
			platform.animation["platform_animations"].speed = .5f;
			platform.animation.Play("platform_animations");
		}
		
    }
}

