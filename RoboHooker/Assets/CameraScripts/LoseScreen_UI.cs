using UnityEngine;
using System.Collections;

public class LoseScreen_UI : MonoBehaviour {
	
	public Vector3 end = new Vector3(75.0f, -112.2f, -293.5f);
	private string endText = "Press ENTER \nto Restart";
	private float toPosRate = 0.03f;
	
	private Color off = new Color(0,0.5f,0,0);
	private Color on1 = new Color(0.8f,1.0f,0.9f,1.0f);
	private Color on2 = new Color(0.4f,0.8f,0.5f,1.0f);
	private float delay = 2.0f;
	
	public GUIStyle endTextGUI;
	
	// Use this for initialization
	void Start () {
		endTextGUI = new GUIStyle();
		endTextGUI.alignment = TextAnchor.MiddleCenter;
		endTextGUI.normal.textColor = Color.white;
		
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position != end)
			transform.position = Vector3.Lerp(transform.position, end,toPosRate);
		
		if (Input.GetKey(KeyCode.Escape))
			Application.Quit();
		
		if (Input.GetKey(KeyCode.Return)) {
			GameData.RestartStats();
			Application.LoadLevel(GameData.LastLevel);
		}
	}
	
	void OnGUI()
	{
		if (Time.time < delay*2.0f)
			endTextGUI.normal.textColor = Color.Lerp(off, on1, Time.time-delay);
		else
			endTextGUI.normal.textColor = Color.Lerp(on1, on2, Mathf.Cos(Time.time*5)*0.5f + 0.5f);
		
		endTextGUI.fontSize = (int)(Screen.width * .1f);
		GUI.Label(new Rect(Screen.width*(4/9.0f), Screen.height*(1.0f/8.0f) ,100, 100), endText, endTextGUI);
	}
}
