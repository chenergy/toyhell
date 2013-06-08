using UnityEngine;
using System.Collections;

public class fadeToBlackScript : MonoBehaviour {
	
	public bool triggered = false;
	public float fadeDelay = 1.0f;
	private float fade = 0;
	
	// Update is called once per frame
	void Update () {
		if (triggered)
		{
			if (fadeDelay > 0.0) fadeDelay -= Time.deltaTime;
			else 
			{
				fade += Time.deltaTime;
				renderer.material.color = new Color(0,0,0,fade);
			}
		}
	}
}
