using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using ToyHell;
using FSM;

[System.Serializable]
public class PlayerControls{
	public KeyCode Attack 				= KeyCode.C;
	public KeyCode Activate				= KeyCode.F;
	public KeyCode Block				= KeyCode.B;
	public KeyCode Jump					= KeyCode.V;
	public KeyCode SwitchLeft			= KeyCode.Q;
	public KeyCode SwitchRight			= KeyCode.E;
	
	public KeyCode AttackJoystick		= KeyCode.Joystick1Button0;
	public KeyCode ActivateJoystick		= KeyCode.Joystick1Button3;
	public KeyCode BlockJoystick		= KeyCode.Joystick1Button2;
	public KeyCode JumpJoystick			= KeyCode.Joystick1Button1;
	public KeyCode SwitchLeftJoystick	= KeyCode.Joystick1Button4;
	public KeyCode SwitchRightJoystick	= KeyCode.Joystick1Button5;
	
	public KeyCode Left					= KeyCode.A;
	public KeyCode Right				= KeyCode.D;
	public KeyCode Up					= KeyCode.W;
	public KeyCode Down					= KeyCode.S;
}

public class GlobalInputListener : MonoBehaviour {
	public PlayerControls p1Controls;
	public PlayerControls p2Controls;
	public PlayerControls p3Controls;
	public PlayerControls p4Controls;
	private PlayerControls[] controls;
	
	void Awake(){
		this.controls = new PlayerControls[] { p1Controls, p2Controls, p3Controls, p4Controls };
	}
	
	void Update(){
		if (Input.anyKey){
			if (Input.GetKeyDown(KeyCode.Return)){
				GameManager.CreateFighter("Barbie", 1);
			}
		}
		
		GameManager.ProcessInput();
		GameManager.Update();
	}
	
	public PlayerControls GetControls( int playerNumber ){
		return this.controls[playerNumber-1];
	}
}