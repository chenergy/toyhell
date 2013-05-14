using UnityEngine;
using System.Collections;

public class GameData {
	public static GameData instance = new GameData();
	
	private GameObject hooker;
	private int hooker_currenthp = 100;
	private int hooker_maxhp = 100;
	
	private GameObject robot;
	private int robot_currenthp = 100;
	private int robot_maxhp = 100;
	
	private GameData(){
		this.hooker = GameObject.Find("Hooker");
		this.robot = GameObject.Find ("Robot");
	}
	
	public static void LoseHp(GameObject player, int damage){
		if (player == instance.hooker){
			instance.hooker_currenthp -= damage;
		}
		else if (player == instance.robot){
			instance.robot_currenthp -= damage;
		}
	}
	
	public static int HookerHp{
		get { return instance.hooker_currenthp; }
	}
	
	public static int RobotHp{
		get { return instance.robot_currenthp; }
	}
	
	public static int HookerMaxHp{
		get { return instance.hooker_maxhp; }
	}
	
	public static int RobotMaxHp{
		get { return instance.robot_maxhp; }
	}
}
