using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ToyHell;

namespace ToyHell
{
	public abstract class A_Attack
	{
		protected	float		speed;
		protected 	Fighter	owner;
		protected	float		timer;
		protected	float		length;
		protected	List<A_HitBoxInstruction> instructions;
		
		protected A_Attack( float length, float speed, Fighter owner )
		{
			this.speed			= speed;
			this.owner 			= owner;
			this.timer 			= 0.0f;
			this.length			= length;
			this.instructions 	= new List<A_HitBoxInstruction>();
			//this.animationName 		= animationName;
			//this.animationSpeed		= animationSpeed;
			
			//if (attackOwner.gobj.animation.GetClip(animationName) != null){
			//	this.attackLength = attackOwner.gobj.animation[animationName].clip.length;
			//}
		}
		/*
		public virtual void Init(){
			foreach (A_HitBoxInstruction hbi in this.instructions){
				hbi.Init();
			}
		}
		*/
		
		public virtual void Init(){
			this.timer = 0.0f;
		}
		
		public virtual void Execute(){
			foreach (A_HitBoxInstruction hbi in this.instructions){
				if (this.timer < hbi.StartTime / this.speed){
					hbi.Disable();
				}
				else if ((this.timer >= hbi.StartTime / this.speed) && (this.timer <= hbi.EndTime / this.speed)){
					hbi.Execute();
				}
				else if (this.timer >= hbi.EndTime / this.speed){
					hbi.Reset();
				}
			}
			this.timer += Time.deltaTime;
		}
		
		public virtual void Reset(){
			foreach (A_HitBoxInstruction hbi in this.instructions){
				hbi.Reset();
			}
			this.timer = 0.0f;
		}
		
		public void SetSpeed( float speed ){
			this.speed = speed;
		}
		
		public bool CheckComplete(){
			return (this.timer >= (this.length / this.speed));
		}
	}
}

