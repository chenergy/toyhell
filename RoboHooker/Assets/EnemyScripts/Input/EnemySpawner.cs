using UnityEngine;
using System.Collections;

[System.Serializable]
public class EnemyStats{
	public bool			isStatic		= false;
	public bool			isRanged		= false;
	public bool			isFlying		= false;
	public float		projectileSpeed 	= 1.0f;
	public float		projectileDuration 	= 2.0f;
	public float		jumpPower			= 20.0f;
	public float		moveSpeed 			= 2.0f;
	public float 		turnSpeed 			= 5.0f;
	public float 		agroRange 			= 5.0f; 
	public float		attackRange			= 1.0f;
	public float		attackTime			= 0.0f;
	public float		attackSpeed			= 1.0f;
	public float		knockbackStrength	= 35.0f;
	public float		patrolPauseTime 	= 3.0f;
	public float		fadeTime			= 3.0f;
	public int			damage				= 10;
	public int			maxHP				= 100;
	public int			currentHP			= 100;
}

public class EnemySpawner : MonoBehaviour
{
	public GameObject 	enemyPrefab;
	public GameObject	spawnPoint;
	public float 		spawnTimeInterval = 5.0f;
	public int			maxEnemies = 1000;
	public EnemyStats enemyStats;
	
	
	private float timer = 0.0f;
	private bool isOn = true;
	private int enemyCount = 0;
	// Use this for initialization
	void Start ()
	{
		this.spawnPoint.renderer.enabled = false;
		this.spawnPoint.collider.enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (this.isOn){
			if (timer >= spawnTimeInterval){
				GameObject instance = (GameObject)GameObject.Instantiate(enemyPrefab, this.spawnPoint.transform.position, Quaternion.identity);
				EnemyInput input = instance.GetComponent<EnemyInput>();
				
				input.isStatic				= this.enemyStats.isStatic;
				input.isRanged				= this.enemyStats.isRanged;
				input.isFlying				= this.enemyStats.isFlying;
				input.projectileSpeed 		= this.enemyStats.projectileSpeed;
				input.projectileDuration 	= this.enemyStats.projectileDuration;
				input.jumpPower				= this.enemyStats.jumpPower;
				input.moveSpeed 			= this.enemyStats.moveSpeed;
				input.turnSpeed 			= this.enemyStats.turnSpeed;
				input.agroRange 			= this.enemyStats.agroRange; 
				input.attackRange			= this.enemyStats.attackRange;
				input.attackTime			= this.enemyStats.attackTime;
				input.attackSpeed			= this.enemyStats.attackSpeed;
				input.knockbackStrength		= this.enemyStats.knockbackStrength;
				input.patrolPauseTime 		= this.enemyStats.patrolPauseTime;
				input.fadeTime				= this.enemyStats.fadeTime;
				input.damage				= this.enemyStats.damage;
				input.maxHP					= this.enemyStats.maxHP;
				input.currentHP				= this.enemyStats.currentHP;
				
				this.enemyCount++;
				this.timer = 0.0f;
				
				if (this.enemyCount >= this.maxEnemies){
					this.isOn = false;
				}
			}
			else{
				this.timer += Time.deltaTime;
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

