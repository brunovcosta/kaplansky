using System;
using Microsoft.Xna.Framework.Graphics;
using Arena;
using ArenaMonoGame;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Collision.Shapes;
using Microsoft.Xna.Framework;
using FarseerPhysics.Factories;

namespace Arena {
	public class WheelAntiClockWise : Wheel{
		public static ModuleData moduleData = new ModuleData (typeof(WheelAntiClockWise), "btnRodaAnti", "roda", ModuleSelection.WHEEL_ANTI_CLOCK_WISE);
		public WheelAntiClockWise(PhysicalNode parent,Vector2 relativePos,Component component,float length,float deltaAngle) : base(parent,relativePos,component,length,deltaAngle) {
			torque*=-1;
		}
	}
}

