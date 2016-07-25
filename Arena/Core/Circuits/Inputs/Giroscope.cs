using System;
using FarseerPhysics.Dynamics;

namespace Arena{
	public class Gyroscope : Input{
		Body body;
		public Gyroscope (Body body){
			this.body = body;
		}
		public override bool State (){
			return body.AngularVelocity < 0;
		}
	}
}

