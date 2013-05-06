using UnityEngine;
using System.Collections;

public class EnemyUI : MonoBehaviour {
	
	public float 	widthScale 		= 1.0f;
	public float	heightScale		= 1.0f;
	public int 		yOffSet 		= 15;
	public int		padding 		= 5;
	
	private int 	UI_X;
	private int 	UI_Y;
	private int 	UI_Width;
	private int		UI_Height;
	public GUIStyle currentHPBar;
	private GUIStyle maxHPBar;
	private GameObject enemy;
	//private float timer;
	private int 	maxHP;
	private int		currentHP;
	
	
	// Use this for initialization
	void Start () {
		
		enemy = this.gameObject;
		maxHP = enemy.GetComponent<EnemyInput>().maxHP;
		currentHP = maxHP;
		
		currentHPBar = new GUIStyle();
		maxHPBar = new GUIStyle();
		
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnGUI(){
		UI_Width = (int)(Screen.width * 0.1f * widthScale);
		UI_Height = (int)(Screen.height * 0.02f * heightScale);
		
		Vector3 screenPos = Camera.mainCamera.WorldToScreenPoint(enemy.transform.position);
		UI_X = (int)(screenPos.x - UI_Width/2);
		UI_Y = (int)(-screenPos.y + (yOffSet * -0.01 * Screen.height) + Screen.height) - UI_Height;
		
		GUI.Box (new Rect (UI_X, UI_Y, UI_Width + padding, UI_Height + padding), "");
		//GUI.Box (new Rect (UI_X, UI_Y, UI_Width + UI_Padding, UI_Height + UI_Padding), "");
	}
}
