using UnityEngine;
using System.Collections.Generic;
using System;
using ToyHell;
using FSM;

namespace ToyHell{
	public class GameManager {
		private static GameManager instance = new GameManager();
		private GameModel gModel;
		
	    private GameManager() {
			this.gModel = new GameModel();
	    }
		/*
	    public static GameManager Instance {
	        get { return instance; }
	    }
		
		public static Player P1{
			get { return instance.gModel.p1; }
		}
		
		public static Player P2{
			get { return instance.gModel.p2; }
		}
		*/
		public static FightCamera Camera{
			get { return instance.gModel.camera; }
		}
		
		public static UI_Script UI{
			get { return instance.gModel.ui; }
		}
		
		public static Player[] Players{
			get { return instance.gModel.players; }
		}
		
		/*
		public static float LeftBoundary {
			get { return instance.gModel.leftBoundary; }
		}
		
		public static float RightBoundary {
			get { return instance.gModel.rightBoundary; }
		}
		*/
		
		public static Player GetPlayer( int playerNum ){
			return instance.gModel.players[playerNum - 1];
		}
		
		public static void Restart( int playerNum ){
			instance.gModel.players[playerNum - 1].RestartFighter();
		}
		
		public static void ProcessInput(){
			foreach (Player p in instance.gModel.players){
				//Debug.Log("Player " + p.PlayerNumber);
				if (p.Fighter != null){
					MoveCommand moveCommand = p.controls.GetMoveCommand();
					ActionCommand actionCommand = p.controls.GetActionCommand();
					
					p.DoMoveCommand(moveCommand);
					p.DoActionCommand(actionCommand);
				}
			}
		}
		
		public static void Update(){
			foreach (Player p in instance.gModel.players){
				p.Update();
			}
			
			if (instance.gModel.camera != null){
				instance.gModel.camera.Update();
			}
		}
		
		public static void CreateFighter(string fighter, int playerNum)
		{
			Player player = instance.gModel.players[playerNum - 1];
			GameObject locator = GameObject.Find("LocatorP" + playerNum.ToString());
			
			player.InstantiateFighter(fighter, locator.transform.position, locator.transform.rotation);
		}
		
		
		/*
		private static float GetPlayersDistance(){
			return Mathf.Abs(RightScreenBoundary() - LeftScreenBoundary());
		}
		
		private static float LeftScreenBoundary(){
			float maxLeft = 0.0f;
			if (instance.gModel.players.Length > 0){
				maxLeft = instance.gModel.players[0].Fighter.gobj.transform.position.x;
				
				foreach (Player p in instance.gModel.players){
					if (p.Fighter.gobj.transform.position.x < maxLeft){
						maxLeft = p.Fighter.gobj.transform.position.x;
					}
				}
			}
			return maxLeft;
		}
		
		private static float RightScreenBoundary(){
			float maxRight = 0.0f;
			if (instance.gModel.players.Length > 0){
				maxRight = instance.gModel.players[0].Fighter.gobj.transform.position.x;
				
				foreach (Player p in instance.gModel.players){
					if (p.Fighter.gobj.transform.position.x > maxRight){
						maxRight = p.Fighter.gobj.transform.position.x;
					}
				}
			}
			return maxRight;
		}
		*/
		/*
		public static bool CheckCanMoveForward(Fighter fighter){
			if ( GameManager.P1.Fighter != null && GameManager.P2.Fighter != null ){
				if (GameManager.GetPlayersDistance() > GameManager.P1.Fighter.radius + GameManager.P2.Fighter.radius){
					if (fighter.GlobalForwardVector.x == 1){
						if (GameManager.CheckWithinRightBoundary(fighter)){
							return true;
						}
					}
					else if (fighter.GlobalForwardVector.x == -1){
						if (GameManager.CheckWithinLeftBoundary(fighter)){
							return true;
						}
					}
				}
			}
			return false;
		}
		
		public static bool CheckCanMoveBackward(Fighter fighter){
			if ( GameManager.P1.Fighter != null && GameManager.P2.Fighter != null ){
				if (GameManager.GetPlayersDistance() < GameManager.Camera.maxDistance){
					if (fighter.GlobalForwardVector.x == 1){
						if (GameManager.CheckWithinLeftBoundary(fighter)){
							return true;
						}
					}
					else if (fighter.GlobalForwardVector.x == -1){
						if (GameManager.CheckWithinRightBoundary(fighter)){
							return true;
						}
					}
				}
			}
			return false;
		}
		
		private static bool CheckWithinLeftBoundary(Fighter fighter){
			return fighter.gobj.transform.position.x > GameManager.LeftBoundary;
		}
		
		private static bool CheckWithinRightBoundary(Fighter fighter){
			return fighter.gobj.transform.position.x < GameManager.RightBoundary;
		}
		*/
	}
}