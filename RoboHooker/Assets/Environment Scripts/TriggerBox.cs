using UnityEngine;
using System.Collections;

public class TriggerBox : MonoBehaviour {
	
	public enum Location { TOP, BOTTOM };
	
	public string text;
	public Location UILocation;
	public int UI_Width = 200;
	public int UI_OffSet = 10;
	
	private bool triggered = false;
	private int UI_X;
	private int UI_Y;
	private int UI_Height;
	private GUIStyle centeredTextStyle;
	private GUIContent UI_Content;
	
	// Use this for initialization
	void Start () {
		this.GetComponent<MeshRenderer>().enabled = false;
		UI_Content = new GUIContent(text);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other){
		if (other == GameObject.FindGameObjectWithTag("Player").collider){
			triggered = true;
		}
	}
	
	void OnTriggerExit(Collider other){
		if (other == GameObject.FindGameObjectWithTag("Player").collider){
			triggered = false;
		}
	}
	
	void OnGUI(){
		if (triggered){
			centeredTextStyle = new GUIStyle("label");
			centeredTextStyle.alignment = TextAnchor.MiddleCenter;
			
			UI_Height = (int)(centeredTextStyle.CalcHeight(UI_Content, UI_Width));
			UI_X = Screen.width/2 - UI_Width/2;
			UI_Y = (UILocation == Location.TOP) ? UI_OffSet : (Screen.height - UI_OffSet - UI_Height);
			
			GUI.Box (new Rect (UI_X, UI_Y, UI_Width + 5, UI_Height + 5), "");
			GUI.Label (new Rect (UI_X, UI_Y, UI_Width + 5, UI_Height + 5), UI_Content.text, centeredTextStyle);
		}
	}
}
