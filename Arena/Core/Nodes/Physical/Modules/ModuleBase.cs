using System;
using Arena;
using Microsoft.Xna.Framework;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
namespace Arena{
	public class ModuleBase : PhysicalNode{
		public Component component;
		public float deltaAngle;
		public float length;
		public float direction;
		public static ModuleData moduleData;
		public ModuleBase(PhysicalNode parent,Vector2 relativePos,Component component,float length,float deltaAngle) {
			this.parent = parent;
			this.component = component;
			this.length = length;
			this.deltaAngle=deltaAngle;

			parent.Add (this);
		}
	}
}

