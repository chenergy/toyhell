using UnityEngine;
using System.Collections;

public class fadeToBlackScript : MonoBehaviour {
	
	public float 	fadeDelay 			= 1.0f;
	public bool 	loadLevelOnTrigger 	= false;
	public string 	levelToLoad 		= "";
	
	private float 	fade 		= 0;
	public bool 	triggered	= false;
	
	// Update is called once per frame
	void Update () {
		if (triggered)
		{
			if (fadeDelay <= 0.0){
				fade += Time.deltaTime;
				renderer.material.color = new Color(0,0,0,fade);
				if (fade >= 1.0f && loadLevelOnTrigger){
					Application.LoadLevel(levelToLoad);
				}
			}
			else 
			{
				fadeDelay -= Time.deltaTime;
			}
		}
	}
	
	public void FadeOut(){
		this.triggered = true;
	}
	
	public void SetLoadLevel(string level){
		this.loadLevelOnTrigger = true;
		this.levelToLoad = level;
	}
}
