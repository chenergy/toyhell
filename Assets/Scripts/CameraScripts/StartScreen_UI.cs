using UnityEngine;
using System.Collections;

public class StartScreen_UI : MonoBehaviour {
	
	public Vector3 end = new Vector3(75.0f, -112.2f, -293.5f);
	private string startText = "Press Start \nto Begin";
	private float toPosRate = 0.03f;
	
	public KeyCode m_LeftKey;
    public KeyCode m_RightKey;
    public KeyCode m_JumpKey;
    public KeyCode m_FireSpecial;
    public KeyCode m_FireSocket;
	protected GamePadManager m_controller;
	public gamepad m_player;
	
	private Color off = new Color(0,0.5f,0,0);
	private Color on1 = new Color(0.8f,1.0f,0.9f,1.0f);
	private Color on2 = new Color(0.4f,0.8f,0.5f,1.0f);
	private float delay = 1.0f;
	
	public GUIStyle startTextGUI;
	
	// Use this for initialization
	void Start () {
		startTextGUI = new GUIStyle();
		startTextGUI.alignment = TextAnchor.MiddleCenter;
		startTextGUI.normal.textColor = Color.white;
		
		m_controller = new GamePadManager(m_player);
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position != end)
			transform.position = Vector3.Lerp(transform.position, end,toPosRate);
		
		if (Input.GetKey(KeyCode.Escape))
			Application.Quit();
		
		if (Input.GetKey(KeyCode.Joystick1Button7) || Input.GetKey(KeyCode.Joystick2Button7)) 
			Application.LoadLevel("cine1");
	}
	
	void OnGUI()
	{
		if (Time.time < delay*2.0f)
			startTextGUI.normal.textColor = Color.Lerp(off, on1, Time.time-delay);
		else
			startTextGUI.normal.textColor = Color.Lerp(on1, on2, Mathf.Cos(Time.time*5)*0.5f + 0.5f);
		
		startTextGUI.fontSize = (int)(Screen.width * .04f);
		GUI.Label(new Rect(Screen.width*(1/9.0f), Screen.height*(3.0f/8.0f) ,100, 100), startText, startTextGUI);
	}
}
