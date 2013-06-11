using UnityEngine;
using System.Collections;

public class TimerUIScript : MonoBehaviour {
	public string	preTimerText		= "";
	public string	postTimerText		= "";
	public float	postTimerFadeDelay	= 0.0f;
	public int		secondsUntilTimeout = 120;
	public int 		UI_FontSize 		= 30;
	public int 		UI_Width 			= 200;
	public int 		UI_OffSet 			= 100;
	
	private int 	UI_X;
	private int 	UI_Y;
	private int 	UI_Height;
	private GUIStyle centeredTextStyle;
	private GUIContent UI_Content;
	private float 	timer;
	
	// Use this for initialization
	void Start () {
		UI_Content = new GUIContent();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnGUI(){
		
		int newTime = secondsUntilTimeout - (int)Time.timeSinceLevelLoad;

		newTime = (newTime < 0) ? 0 : newTime;
		int minute = ((int)(newTime / 60));
		int second = ((int)(newTime % 60));
		string newText = (second < 10) ? (minute.ToString() + ":" + "0" + second.ToString()) : (minute.ToString() + ":" + second.ToString());
		
		UI_Content.text = newText;
		centeredTextStyle = new GUIStyle("label");
		centeredTextStyle.alignment = TextAnchor.MiddleCenter;
		centeredTextStyle.fontSize = UI_FontSize;
		centeredTextStyle.normal.textColor = Color.red;
		
		UI_Height = (int)(centeredTextStyle.CalcHeight(UI_Content, UI_Width));
		
		UI_X = Screen.width/2 - UI_Width/2;
		UI_Y = UI_OffSet;
		
		if (newTime > 0){
			GUI.Label (new Rect (UI_X, UI_Y - UI_Height - (UI_OffSet * 0.4f), UI_Width + 5, UI_Height + 5), preTimerText, centeredTextStyle);
			GUI.Label (new Rect (UI_X, UI_Y - UI_Height, UI_Width + 5, UI_Height + 5), UI_Content.text, centeredTextStyle);
		}else if (newTime <= 0){
			timer += Time.deltaTime;
			if (timer < postTimerFadeDelay){
				GUI.Label (new Rect (UI_X, UI_Y - UI_Height - (UI_OffSet * 0.4f), UI_Width + 5, UI_Height + 5), postTimerText, centeredTextStyle);
			}
		}
	}
}
