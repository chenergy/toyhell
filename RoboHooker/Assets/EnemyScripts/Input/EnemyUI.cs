using UnityEngine;
using System.Collections;

public class EnemyUI : MonoBehaviour {
	
	public string 	name			= "Enemy Name";
	public float 	widthScale 		= 1.0f;
	public float	heightScale		= 1.0f;
	public int 		yOffSet 		= 40;
	public GUIStyle currentHpBar;
	public GUIStyle maxHpBar;
	
	private int 	UI_X;
	private int 	UI_Y;
	private int 	UI_Width;
	private int		UI_Height;
	private GameObject enemy;
	private float 	maxHP;
	private float	currentHP;
	private GUIStyle UI_Text;
	
	// Use this for initialization
	void Start () {
		enemy = this.gameObject;
		
		UI_Text = new GUIStyle();
		UI_Text.alignment = TextAnchor.MiddleCenter;
		UI_Text.normal.textColor = Color.white;
		
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnGUI(){
		UI_Width = (int)(Screen.width * 0.1f * widthScale);
		UI_Height = (int)(Screen.height * 0.035f * heightScale);
		
		Vector3 screenPos = Camera.mainCamera.WorldToScreenPoint(enemy.transform.position);
		UI_X = (int)(screenPos.x - UI_Width/2);
		UI_Y = (int)(
			-screenPos.y	 // Enemy Y Position in Screen point
			- yOffSet					// User defined Y Offset
			+ Screen.height				// Screen Height
			- UI_Height/2	  			// (Optional) Half UI Height
			);
		
		maxHP = enemy.GetComponent<EnemyInput>().maxHP;
		currentHP = enemy.GetComponent<EnemyInput>().currentHP;
		UI_Text.fontSize = (int)(Screen.width * .015f);
		
		GUI.Box (new Rect (UI_X, UI_Y, UI_Width, UI_Height), "", maxHpBar);
		GUI.Box (new Rect (UI_X, UI_Y, UI_Width * (currentHP/maxHP), UI_Height), "", currentHpBar);
		GUI.Label (new Rect (UI_X, UI_Y, UI_Width, UI_Height), name, UI_Text);
	}
}
