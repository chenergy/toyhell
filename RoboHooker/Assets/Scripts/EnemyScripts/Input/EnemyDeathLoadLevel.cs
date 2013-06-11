using UnityEngine;
using System.Collections;

[RequireComponent (typeof (EnemyInput))]
public class EnemyDeathLoadLevel : MonoBehaviour
{
	public string levelToLoad;
	private EnemyInput input;
	// Use this for initialization
	void Start ()
	{
		input = this.GetComponent<EnemyInput>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (levelToLoad != ""){
			if (input.currentHP <= 0){
				Application.LoadLevel(levelToLoad);
			}
		}
	}
}

