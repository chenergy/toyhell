using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ToyHell;

namespace ToyHell
{
	public class Attack_Projectile : A_Attack
	{
		//protected List<ProjectileHitBoxInstruction> instructions;
		
		public Attack_Projectile (float length, float animationSpeed, Fighter attackOwner) : base(length, animationSpeed, attackOwner)
		{
			//this.instructions = new List<ProjectileHitBoxInstruction>();
		}
		/*
		public override void Init(){
			foreach (ProjectileHitBoxInstruction hbi in this.instructions){
				hbi.Init();
			}
		}
		
		public override void Execute(){
			foreach (ProjectileHitBoxInstruction hbi in this.instructions){
				if (this.timer < hbi.startTime / this.animationSpeed){
					hbi.Disable();
				}
				else if ((this.timer >= hbi.startTime / this.animationSpeed) && (this.timer <= hbi.endTime / this.animationSpeed)){
					hbi.Execute();
				}
				else if (this.timer >= hbi.endTime / this.animationSpeed){
					hbi.Reset();
				}
			}
			this.timer += Time.deltaTime;
		}
		
		public override void Reset(){
			foreach (ProjectileHitBoxInstruction hbi in this.instructions){
				hbi.Reset();
			}
			this.timer = 0.0f;
		}
		*/
		public void AddInstruction ( ProjectileHitBoxInstruction hbi ){
			this.instructions.Add( hbi );
		}
	}
}

