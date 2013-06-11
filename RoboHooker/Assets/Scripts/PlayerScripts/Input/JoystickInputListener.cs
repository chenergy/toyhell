using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using FSM;
using Actors;

public class JoystickInputListener : MonoBehaviour {
	/*
	// Private class to handle input values
	private class JoyStick
	{
		public int player;
		public string[] ButtonAxes;
		public string[] DirectionalAxes;
		
		public JoyStick(int player){
			this.player = player;
			
			string playerPostfix = "P" + player;
			
			this.DirectionalAxes = new string[2];
			this.DirectionalAxes[0] = playerPostfix + "moveX";
			this.DirectionalAxes[1] = playerPostfix + "moveY";
			
			this.ButtonAxes = new string[4];
			this.ButtonAxes[0] = "RegularAttack" + playerPostfix;
			this.ButtonAxes[1] = "UniqueAttack" + playerPostfix;
			this.ButtonAxes[2] = "SpecialAttack" + playerPostfix;
			this.ButtonAxes[3] = "Block" + playerPostfix;
		}
	}
	
	private JoyStick[] joysticks;
	
	void Start () {
		JoyStick playerOneStick = new JoyStick(1);
		JoyStick playerTwoStick = new JoyStick(2);
		
		this.joysticks = new JoyStick[2];
		this.joysticks[0] = playerOneStick;
		this.joysticks[1] = playerTwoStick;
	}

	void Update ()
	{
		for (int i = 0; i < this.joysticks.Length; i++){
			Vector2 inputDirection = this.GetInputDirection(joysticks[i].DirectionalAxes[0], joysticks[i].DirectionalAxes[1]);
			string controllerDirection = this.GetControllerDirection(inputDirection);
			
			if (controllerDirection != "none")
				Debug.Log("Direction: " + controllerDirection);
			
			List<string> pressedButtons = new List<string>();
			if (Input.anyKeyDown){
				for (int j = 0; j < this.joysticks[i].ButtonAxes.Length; j++){
					string axis = this.joysticks[i].ButtonAxes[j];
					//Debug.Log("Button: " + axis);
					
					//if (Mathf.Abs(Input.GetAxisRaw(axis)) > 0){
					if (Input.GetButtonDown(axis)){
						//Debug.Log("Hit: " + axis);
						pressedButtons.Add(axis);
					}
				}
			}
			//GameManager.ProcessInput(controllerDirection, pressedButtons);
		}
		//GameManager.Update();
	}
	
	private Vector2 GetInputDirection(string hAxis, string vAxis)
	{
		return new Vector2(Input.GetAxis(hAxis),Input.GetAxisRaw(vAxis));
	}
	
	private float GetInputAngle(Vector2 forward, Vector2 inputDirection)
	{
		if(inputDirection.y>0)
			return Mathf.Acos(Vector2.Dot(inputDirection.normalized,forward));
		else if(inputDirection.y<0)
			return Mathf.Acos(Vector2.Dot(inputDirection.normalized,-forward)) + halfCircle;
		else if (inputDirection.normalized == -forward)
			return halfCircle;
		else
			return 0;
	}
	
	private string GetControllerDirection(Vector2 inputDirection)
	{
		
		if (inputDirection.x == 0 && inputDirection.y == 0)
		{
			return "none";
		}
		else
		{
			//Vector3 forward = GameManager.P1.ForwardVector;
			Vector3 forward = new Vector3(1.0f, 0.0f, 0.0f);
			
			float inputAngle = GetInputAngle(forward, inputDirection);
			
			if (inputAngle >= 0 && inputAngle < one16thCircle * 1 ||
				inputAngle >= one16thCircle *15 && inputAngle < one16thCircle *16)
			{
				return "forward";
			}
			else if (inputAngle >= one16thCircle *1 && inputAngle < one16thCircle *3)
			{
				return"forwardUp";
			}
			else if (inputAngle >= one16thCircle *3 && inputAngle < one16thCircle *5)
			{
				return "up";
			}
			else if (inputAngle >= one16thCircle *5 && inputAngle < one16thCircle *7)
			{
				return "backUp";
			}
			else if (inputAngle >= one16thCircle *7 && inputAngle < one16thCircle *9)
			{
				return "back";
			}
			else if (inputAngle >= one16thCircle *9 && inputAngle < one16thCircle *11)
			{
				return "backDown";
			}
			else if (inputAngle >= one16thCircle *11 && inputAngle < one16thCircle *13)
			{
				return "down";
			}
			else if (inputAngle >= one16thCircle *13 && inputAngle < one16thCircle *15)
			{
				return "forwardDown";
			}
			else
			{
				return "invalid";
			}
		}

	}
	*/
}
