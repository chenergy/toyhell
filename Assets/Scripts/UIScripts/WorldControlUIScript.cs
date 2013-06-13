using UnityEngine;
using System.Collections.Generic;

public class WorldControlUIScript : MonoBehaviour {
	
	public Texture cubeMap;
	private List<GameObject> renderables;
	
	// Use this for initialization
	void Start () {
		this.renderables = new List<GameObject>();
		GameObject[] objects = (GameObject[])GameObject.FindObjectsOfType(typeof(GameObject));
		foreach ( GameObject gobj in objects ){
			if (gobj.renderer){
				if (gobj.renderer.material){
					Debug.Log(gobj.renderer.material.shader.ToString());
					if (gobj.renderer.material.shader.ToString() == "Custom/Self-Illumin Lighted Outline (UnityEngine.Shader)"){
						renderables.Add(gobj);
						gobj.renderer.material.shader = Shader.Find("Self-Illumin/Bumped Diffuse");
					}
					if (gobj.renderer.material.shader.ToString() == "Custom/Fur (UnityEngine.Shader)"){
						renderables.Add(gobj);
					}
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI(){
		float width  		= Screen.width * 0.2f;
		float height 		= Screen.height * 0.1f;
		float top			= Screen.height - height;
		float numButtons	= 4.0f;
		
		if (GUI.Button(new Rect(0, top, width, height), "Normal World")){
			
			foreach (GameObject gobj in this.renderables){
				gobj.renderer.material.shader = Shader.Find("Self-Illumin/Bumped Diffuse");
			}
		}
		
		if (GUI.Button(new Rect((Screen.width * 1/(numButtons - 1)) - (width * 0.5f), top, width, height), "Shiny World")){
			foreach (GameObject gobj in this.renderables){
				gobj.renderer.material.shader = Shader.Find("Custom/Reflective");
				
				if (this.cubeMap){
					gobj.renderer.material.SetTexture("_Cube", cubeMap);
				}
			}
		}
		
		if (GUI.Button(new Rect((Screen.width * 2/(numButtons - 1)) - (width * 0.5f), top, width, height), "Bright World")){
			foreach (GameObject gobj in this.renderables){
				gobj.renderer.material.shader = Shader.Find("Custom/Fur");
				gobj.renderer.material.SetFloat("_FurLength", 0.1f);
				
			}
		}
		
		if (GUI.Button(new Rect((Screen.width) - width, top, width, height), "Toon World")){
			foreach (GameObject gobj in this.renderables){
				gobj.renderer.material.shader = Shader.Find("Custom/Self-Illumin Lighted Outline");
			}
		}
	}
}
