using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
	public GameObject 	enemyPrefab;
	public float 		spawnTimeInterval = 5.0f;
	
	private float timer = 0.0f;
	private bool isOn = true;
	// Use this for initialization
	void Start ()
	{
		this.renderer.enabled = false;
		this.collider.enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (this.isOn){
			if (timer >= spawnTimeInterval){
				GameObject.Instantiate(enemyPrefab, this.transform.position, Quaternion.identity);
				timer = 0.0f;
			}
			else{
				timer += Time.deltaTime;
			}
		}
	}
	
	public void TurnOn(){
		this.isOn = true;
	}
	
	public void TurnOff(){
		this.isOn = false;
	}
}

