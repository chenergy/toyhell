using UnityEngine;
using System.Collections;


public class restartGameScript : MonoBehaviour {
	
	
	public float timerLimit = 4.0f;
	public string NextLevelName;
	private float timer = 0;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer > timerLimit) 
		{
			Debug.Log ("loading next level");
			Application.LoadLevel(NextLevelName);
		}
	}
}
