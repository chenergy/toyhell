using UnityEngine;
using System.Collections.Generic;
using System;
using ToyHell;
using FSM;

public enum GameState { START_MENU, PAUSE_MENU, LOADING, PLAYING, DEATH_SCREEN };

namespace ToyHell{
	public class GameModel {
		public FightCamera 	camera;
		public UI_Script 	ui;
		public Player[] 	players;
		public string		lastLevel;
		public GameState	gameState;

	    // make sure the constructor is private, so it can only be instantiated here
	    public GameModel() {
			this.players	= new Player[4];
			this.gameState	= GameState.PLAYING;
			//this.camera 	= new FightCamera( p1, p2 );
			this.ui 		= GameObject.Find("UI").GetComponent<UI_Script>();
			this.lastLevel  = "";
			
			for (int i = 0; i < 4; i++){
				this.players[i] = new Player(i+1);
			}
	    }
	}
}