using UnityEngine;
using System.Collections.Generic;
using System;
using ToyHell;

namespace ToyHell
{
	public class FightCamera
	{
		public Camera camera;
		public CameraInput input;
		/*
		public Player p1;
		public Player p2;
		public Vector3 p1Position;
		public Vector3 p2Position;
		public float maxDistance;
		
		private const float minZDistance = -15.0f;
		private const float maxZDistance = -30.0f;
		private float cameraZ;
		
		public FightCamera ( Player p1, Player p2 )
		{
			this.camera = Camera.main;
			this.input = this.camera.GetComponent<CameraInput>();
			this.maxDistance = this.input.maxDistance;
			this.p1 = p1;
			this.p2 = p2;
		}
		*/
		public void Update(){
			/*
			float leftBoundary = 0.0f;
			float rightBoundary
			
			foreach (Player p in GameManager.
			
			this.cameraZ = -(p1Position - p2Position).magnitude - 5.0f;
			
			Vector3 cameraPosition = 
				new Vector3( (this.p1Position.x + this.p2Position.x)/2.0f, 
				camera.transform.position.y, 
				Mathf.Clamp(this.cameraZ, maxZDistance, minZDistance) );

			if (cameraPosition.x > GameManager.LeftBoundary && cameraPosition.x < GameManager.RightBoundary){
				this.camera.transform.position = cameraPosition;
			}
			*/
		}
	}
}

