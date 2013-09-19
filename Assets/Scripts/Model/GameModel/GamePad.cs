using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using ToyHell;

namespace ToyHell{
	public enum MoveCommand { NONE, LEFT, RIGHT, UP, DOWN };
	public enum ActionCommand {  NONE, ATTACK, ACTIVATE, JUMP, SWITCH_LEFT, SWITCH_RIGHT, BLOCK, };
	
	public class GamePad{
		private Dictionary<string, KeyCode> keys;
		private MoveCommand[]				moveCommands;
		private ActionCommand[]				actionCommands;
		private string 						XAxis;
		private string						YAxis;
		
		public GamePad(Player player){
			int playerNumber 	= player.PlayerNumber;
			this.keys 			= new Dictionary<string, KeyCode>();
			this.XAxis 			= "HorizontalP" + playerNumber;
			this.YAxis 			= "VerticalP" + playerNumber;
			this.AssignKeysByPlayerNumber(playerNumber);
			//Debug.Log(player.PlayerNumber.ToString() + " - XAxis: " + XAxis + " YAxis: " + YAxis);
			
			if (Application.platform.ToString().Substring(0, 3) == "OSX"){
				this.keys["AttackJoystick"] = (KeyCode) Enum.Parse(typeof(KeyCode), "Joystick" + playerNumber + "Button18");
				this.keys["ActivateJoystick"] = (KeyCode) Enum.Parse(typeof(KeyCode), "Joystick" + playerNumber + "Button19");
				this.keys["BlockJoystick"] = (KeyCode) Enum.Parse(typeof(KeyCode), "Joystick" + playerNumber + "Button17");
				this.keys["JumpJoystick"] = (KeyCode) Enum.Parse(typeof(KeyCode), "Joystick" + playerNumber + "Button16");
				this.keys["SwitchLeftJoystick"] = (KeyCode) Enum.Parse(typeof(KeyCode), "Joystick" + playerNumber + "Button13");
				this.keys["SwitchRightJoystick"] = (KeyCode) Enum.Parse(typeof(KeyCode), "Joystick" + playerNumber + "Button14");
			}
			
			this.moveCommands = new MoveCommand[] { MoveCommand.LEFT, MoveCommand.RIGHT, MoveCommand.UP, MoveCommand.DOWN, MoveCommand.NONE };
			this.actionCommands = new ActionCommand[] { ActionCommand.ATTACK, ActionCommand.ACTIVATE, ActionCommand.JUMP, ActionCommand.SWITCH_LEFT, ActionCommand.SWITCH_RIGHT, ActionCommand.BLOCK, ActionCommand.NONE };
		}
		
		
		// Get a movement command when there has been an input
		public MoveCommand GetMoveCommand(){
			foreach (MoveCommand mc in this.moveCommands){
				if ( this.IsCorrectMoveCommand(mc) ){
					return mc;
				}
			}
			return MoveCommand.NONE;
		}
		
		// Get single action commands when there has been an input
		public ActionCommand GetActionCommand(){
			foreach (ActionCommand ac in this.actionCommands){
				if ( this.IsCorrectActionCommand(ac) ){
					return ac;
				}
			}
			return ActionCommand.NONE;
		}
		
		// Private Helper Functions
		private void AssignKeysByPlayerNumber(int number){
			PlayerControls controls 			= GameObject.Find("GlobalInputListener").GetComponent<GlobalInputListener>().GetControls(number);
			
			this.keys["AttackKey"] 				= controls.Attack;
			this.keys["ActivateKey"]		 	= controls.Activate;
			this.keys["BlockKey"]				= controls.Block;
			this.keys["JumpKey"]			  	= controls.Jump;
			this.keys["SwitchLeftKey"]	  		= controls.SwitchLeft;
			this.keys["SwitchRightKey"]	 		= controls.SwitchRight;
			
			this.keys["AttackJoystick"] 		= controls.AttackJoystick;
			this.keys["ActivateJoystick"] 		= controls.ActivateJoystick;
			this.keys["BlockJoystick"]			= controls.BlockJoystick;
			this.keys["JumpJoystick"]	  		= controls.JumpJoystick;
			this.keys["SwitchLeftJoystick"]	  	= controls.SwitchLeftJoystick;
			this.keys["SwitchRightJoystick"]	= controls.SwitchRightJoystick;
			
			this.keys["LeftKey"]				= controls.Left;
			this.keys["RightKey"]				= controls.Right;
			this.keys["UpKey"]					= controls.Up;
			this.keys["DownKey"]				= controls.Down;
		}
		
		private bool GetKey( string key ){
			if (this.keys.ContainsKey(key)){
				if (Input.GetKey(this.keys[key])){
					return true;
				}
			}
			return false;
		}
		
		private bool GetKeyDown( string key ){
			if (this.keys.ContainsKey(key)){
				if ( Input.GetKeyDown(this.keys[key]) ){
					return true;
				}
			}
			return false;
		}
		
		private float GetAxisRaw( string axis ){
			return ( Input.GetAxisRaw(axis) );
		}
		
		private bool IsCorrectActionCommand( ActionCommand command ){
			switch (command){
			case ActionCommand.BLOCK:
				return (this.GetKey("BlockJoystick") || this.GetKey("BlockKey"));
				break;
			case ActionCommand.ATTACK:
				return (this.GetKeyDown("AttackJoystick") || this.GetKeyDown("AttackKey"));
				break;
			case ActionCommand.ACTIVATE:
				return (this.GetKeyDown("ActivateJoystick") || this.GetKeyDown("ActivateKey"));
				break;
			case ActionCommand.JUMP:
				return (this.GetKeyDown("JumpJoystick") || this.GetKeyDown("JumpKey"));
				break;
			case ActionCommand.SWITCH_LEFT:
				return (this.GetKeyDown("SwitchLeftJoystick") || this.GetKeyDown("SwitchLeftKey"));
				break;
			case ActionCommand.SWITCH_RIGHT:
				return (this.GetKeyDown("SwitchRightJoystick") || this.GetKeyDown("SwitchRightKey"));
				break;
			default:
				break;
			}
			return false;
		}
		
		private bool IsCorrectMoveCommand( MoveCommand command ){
			switch (command){
			case MoveCommand.LEFT:
				return (this.GetKey("LeftJoystick") || this.GetKey("LeftKey"));
				break;
			case MoveCommand.RIGHT:
				return (this.GetKey("RightJoystick") || this.GetKey("RightKey"));
				break;
			case MoveCommand.UP:
				return (this.GetKey("UpJoystick") || this.GetKey("UpKey"));
				break;
			case MoveCommand.DOWN:
				return (this.GetKey("DownJoystick") || this.GetKey("DownKey"));
				break;
			default:
				break;
			}
			return false;
		}
	}
}