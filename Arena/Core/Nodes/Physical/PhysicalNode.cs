using System;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Arena;
using FarseerPhysics.Factories;
using FarseerPhysics.Dynamics.Joints;

namespace Arena{
	public class PhysicalNode : Node{
		public Body body;
		public World world;
		public void Fix(PhysicalNode module,Vector2 relativePos){
			JointFactory.CreateRevoluteJoint (world, module.body, body, Vector2.Zero,relativePos);
		}
	}
}

