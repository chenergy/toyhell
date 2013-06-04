using UnityEngine;
using System.Collections;
using System;

public class PlayerUIScript : MonoBehaviour {
	public GUIStyle currentHpBar;
	public GUIStyle maxHpBar;
	public float padding = 0.01f;
	
	private float 	hookerHp;
	private float 	hookerMaxHp;
	private int		hookerLives;
	private GUIStyle hookerText;
	
	private float 	robotHp;
	private float 	robotMaxHp;
	private int		robotLives;
	private GUIStyle robotText;
	
	private int UI_Width;
	private int UI_Height;
	
	void Start () {
		// Init Hooker Data
		hookerHp = (float)GameData.HookerHp;
		hookerMaxHp = (float)GameData.HookerMaxHp;
		hookerLives = GameData.HookerLives;
		hookerText = new GUIStyle();
		hookerText.alignment = TextAnchor.MiddleCenter;
		hookerText.normal.textColor = Color.white;
		
		// Init Robot Data
		robotHp = (float)GameData.RobotHp;
		robotMaxHp = (float)GameData.RobotMaxHp;
		robotLives = GameData.RobotLives;
		robotText = new GUIStyle();
		robotText.alignment = TextAnchor.MiddleCenter;
		robotText.normal.textColor = Color.white;
	}
	
	void OnGUI(){
		UI_Width = Screen.width/4;
		UI_Height = Screen.height/20;
		
		hookerLives = GameData.HookerLives;
		robotLives = GameData.RobotLives;
		
		// Robot UI
		robotHp = (float)GameData.RobotHp;
		robotText.fontSize = (int)(Screen.width * .02f);
		GUI.Box(new Rect(padding * Screen.width, padding * Screen.height, UI_Width, UI_Height), "", maxHpBar);
		GUI.Box(new Rect(padding * Screen.width, padding * Screen.height, UI_Width * (robotHp/robotMaxHp), UI_Height), "", currentHpBar);
		GUI.Label(new Rect(padding * Screen.width, padding * Screen.height, UI_Width, UI_Height), "Robot", robotText);
		
		robotText.fontSize = (int)(Screen.width * .015f);
		
		GUI.Label(new Rect(padding * Screen.width, 5 * padding * Screen.height, UI_Width, UI_Height), "Lives: " + robotLives, robotText);
		
		if (robotHp <= 0){
			int robotRespawnTime = (int)GameData.RobotRespawnTime;
			//string robotCountdown = "Respawning in: " + Math.Round(robotRespawnTime, 2);
			string robotCountdown = "Respawning in: " + robotRespawnTime;
			GUI.Label(new Rect(padding * Screen.width, 10 * padding * Screen.height, UI_Width, UI_Height), robotCountdown, robotText);
		}
		
		
		// Hooker UI
		hookerHp = (float)GameData.HookerHp;
		hookerText.fontSize = (int)(Screen.width * .02f);
		GUI.Box(new Rect(Screen.width - Screen.width/4 - (padding * Screen.width), padding * Screen.height, UI_Width, UI_Height), "", maxHpBar);
		GUI.Box(new Rect(Screen.width - Screen.width/4 - (padding * Screen.width), padding * Screen.height, UI_Width * (hookerHp/hookerMaxHp), UI_Height), "", currentHpBar);
		GUI.Label(new Rect(Screen.width - Screen.width/4 - (padding * Screen.width), padding * Screen.height, UI_Width, UI_Height), "Hooker", hookerText);
		
		hookerText.fontSize = (int)(Screen.width * .015f);
		
		GUI.Label(new Rect(Screen.width - Screen.width/4 - (padding * Screen.width), 5 * padding * Screen.height, UI_Width, UI_Height), "Lives: " + hookerLives, hookerText);
		
		if (hookerHp <= 0){
			int hookerRespawnTime = (int)GameData.HookerRespawnTime;
			//string hookerCountdown = "Respawning in: " + Math.Round(hookerRespawnTime, 2);
			string hookerCountdown = "Respawning in: " + hookerRespawnTime;
			GUI.Label(new Rect(Screen.width - Screen.width/4 - (padding * Screen.width), 10 * padding * Screen.height, UI_Width, UI_Height), hookerCountdown, hookerText);
		}
	}
}
