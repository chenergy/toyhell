using UnityEngine;
using System.Collections;

public class CreditsScript : MonoBehaviour {
	
	public Vector3 end = new Vector3(0, 0, -17.0f);
	private string endText = "Created by:\n\nShaik Ramzan, Masie Huynh,\n\nNicholas Kirlis, Fern Hertz,\n\nJonathan Chien and Trevor Payne\n";
	private float toPosRate = 0.0005f;
	
	private Color off = new Color(0,0.5f,0,0);
	public Color on1 = new Color(0.8f,1.0f,0.9f,1.0f);
	public Color on2 = new Color(0.4f,0.8f,0.9f,1.0f);
	private float delay = 2.0f;
	
	public GUIStyle endTextGUI;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position != end)
			transform.position = Vector3.Lerp(transform.position, end,toPosRate);
		
		if (Input.GetKey(KeyCode.Escape))
			Application.Quit();
		
		if (Input.GetKey(KeyCode.Return)) 
			Application.LoadLevel("cine1");
	}
	
	void OnGUI()
	{
		if (Time.time < delay*2.0f)
			endTextGUI.normal.textColor = Color.Lerp(off, on1, Time.time-delay);
		else
			endTextGUI.normal.textColor = Color.Lerp(on1, on2, Mathf.Cos(Time.time*5)*0.5f + 0.5f);
		
		endTextGUI.fontSize = (int)(Screen.width * .05f);
		GUI.Label(new Rect(Screen.width*(2/9.0f), Screen.height*(1.0f/8.0f) ,100, 100), endText, endTextGUI);
	}
}
