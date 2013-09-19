using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using ToyHell;

namespace ToyHell{
	public class ProjectileHitBoxInstruction : A_HitBoxInstruction
	{
		private string 		projectileName;
		private Transform	startJoint;
		private Vector3		increment;
		private float		speed;
		private float		duration;
		
		public ProjectileHitBoxInstruction( string projectileName, Vector3 direction, float speed, Fighter fighter, int damage, float startTime, float endTime, Vector3 movement = default(Vector3), Vector3 knockback = default(Vector3)) : base(fighter, damage, startTime, endTime, movement, knockback){
			this.projectileName = projectileName;
			this.startJoint		= fighter.socketJoint;
			this.increment		= direction.normalized * speed * (Time.deltaTime);
			this.speed			= speed;
			this.duration		= (endTime - startTime);
		}
		
		//public override void Init(){}
		
		public override void Start(){
			GameObject projectile 	= (GameObject)GameObject.Instantiate( Resources.Load("Projectiles/" + this.projectileName, typeof(GameObject)),
				this.startJoint.position, Quaternion.identity );
			ProjectileInput pInput 	= projectile.GetComponent<ProjectileInput>();
			pInput.increment 		= new Vector3(this.increment.x * fighter.globalForwardVector.x, this.increment.y, this.increment.z);
			pInput.speed 			= this.speed;
			
			HitBox newHitbox		= new HitBox( this.fighter, pInput.hitboxObject, true );
			//pInput.hitbox 			= newHitbox;
			
			HitBoxInput hInput 		= pInput.hitboxObject.GetComponent<HitBoxInput>();
			hInput.hitbox 			= newHitbox;
			
			base.Start();
			
			GameObject.Destroy(projectile, this.duration);
		}
		
		public override void Disable(){ }
	}
}

