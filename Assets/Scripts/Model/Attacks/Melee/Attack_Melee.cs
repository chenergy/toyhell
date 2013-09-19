using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ToyHell;

namespace ToyHell
{
	public class Attack_Melee : A_Attack
	{
		//protected List<MeleeHitBoxInstruction> instructions;
		
		public Attack_Melee (float length, float animationSpeed, Fighter attackOwner) : base (length, animationSpeed, attackOwner)
		{
			//this.instructions = new List<MeleeHitBoxInstruction>();
		}
		/*
		public override void Init(){
			foreach (MeleeHitBoxInstruction hbi in this.instructions){
				hbi.Init();
			}
		}
		
		public override void Execute(){
			foreach (MeleeHitBoxInstruction hbi in this.instructions){
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
			foreach (MeleeHitBoxInstruction hbi in this.instructions){
				hbi.Reset();
			}
			this.timer = 0.0f;
		}
		*/
		public void AddInstruction ( MeleeHitBoxInstruction hbi ){
			this.instructions.Add( hbi );
		}
	}
}

