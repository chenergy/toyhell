using UnityEngine;
using System.Collections;

public class TriggerBox : MonoBehaviour {
	
	// public enum Location { TOP, BOTTOM };
	
	public string 	UI_Text 			= "Trigger Text";
	public int 		UI_FontSize 		= 15;
	// public Location UI_Location;
	public int 		UI_Width 			= 200;
	public int 		UI_OffSet 			= 10;
	public float 	UI_DisplayDuration	= 5.0f;
	
	private bool 	triggered = false;
	private int 	UI_X;
	private int 	UI_Y;
	private int 	UI_Height;
	private GUIStyle centeredTextStyle;
	private GUIContent UI_Content;
	private GameObject player;
	private float timer;
	
	// Use this for initialization
	void Start () {
		this.renderer.enabled = false;
		UI_Content = new GUIContent(UI_Text);
	}
	
	// Update is called once per frame
	void Update () {
		if (triggered){
			if (timer > UI_DisplayDuration){
				triggered = false;
				timer = 0.0f;
			}
			else{
				timer += Time.deltaTime;
			}
		}
	}
	
	void OnTriggerEnter(Collider other){
		
		if (other.tag == "Player"){
			triggered = true;
			player = other.gameObject;
		}
	}
	
	void OnTriggerExit(Collider other){ }
	
	void OnGUI(){
		if (triggered){
			centeredTextStyle = new GUIStyle("label");
			centeredTextStyle.alignment = TextAnchor.MiddleCenter;
			centeredTextStyle.fontSize = UI_FontSize;
			
			UI_Height = (int)(centeredTextStyle.CalcHeight(UI_Content, UI_Width));
			
			Vector3 screenPos = Camera.mainCamera.WorldToScreenPoint(player.transform.position);
			UI_X = (int)(screenPos.x - UI_Width/2);
			UI_Y = (int)(-screenPos.y + (UI_OffSet * -0.01 * Screen.height) + Screen.height);
			
			// Positioning the UI on the Top or Bottom of the Screen
			// UI_X = Screen.width/2 - UI_Width/2;
			// UI_Y = (UI_Location == Location.TOP) ? UI_OffSet : (Screen.height - UI_OffSet - UI_Height);
			
			GUI.Box (new Rect (UI_X, UI_Y - UI_Height, UI_Width + 5, UI_Height + 5), "");
			GUI.Label (new Rect (UI_X, UI_Y - UI_Height, UI_Width + 5, UI_Height + 5), UI_Content.text, centeredTextStyle);
		}
	}
}
