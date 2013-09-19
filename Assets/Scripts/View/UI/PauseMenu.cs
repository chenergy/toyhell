using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (Time.timeScale == 1.0F){
                Time.timeScale = 0.0F;
			}
			else{
				Time.timeScale = 1.0f;
			}
            Time.fixedDeltaTime = 0.02F * Time.timeScale;
			Debug.Log(Time.timeScale);
        }
    }
}