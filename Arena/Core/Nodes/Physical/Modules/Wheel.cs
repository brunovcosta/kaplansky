using System;
using Microsoft.Xna.Framework.Graphics;
using Arena;
using ArenaMonoGame;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Collision.Shapes;
using Microsoft.Xna.Framework;
using FarseerPhysics.Factories;

namespace Arena {
	public class Wheel : ModuleBase{
		public Texture2D tex;
		public float torque;
		public float TORQUE_TAX=10;
		float consume = .05f;
		public Wheel(PhysicalNode parent,Vector2 relativePos,Component component,float length,float deltaAngle) : base(parent,relativePos,component,length,deltaAngle) {
			Console.WriteLine (parent.position);
			world = Program.mainGame.playNode.world;
			tex = Program.mainGame.Content.Load<Texture2D>("roda.png");
			body=BodyFactory.CreateCircle(world: world, radius: (length/2)/Program.mainGame.PIXELS_PER_METER, density: 1,position:relativePos+parent.position/Program.mainGame.PIXELS_PER_METER);
			body.BodyType = BodyType.Dynamic;
			body.Friction = 1000f;
			body.AngularDamping = 1;
			torque = length * length/TORQUE_TAX;
		}
		public override void Update (GameTime gameTime){
			float ppm = Program.mainGame.PIXELS_PER_METER;
			position = ppm*body.Position;
			Player player = Program.mainGame.playNode.player;
			direction = ((PhysicalNode) parent).body.Rotation;
			if (component.State () && player.energy>0) {
				body.ApplyTorque (torque);
				player.energy -= consume;
			}
		}
		public override void Draw (SpriteBatch batch){
			Vector2 origin = Vector2.One*tex.Width/2;
			batch.Draw(
				tex,
				position, 
				color: Color.White,
				rotation: body.Rotation,
				origin: origin,
				scale: Vector2.One*length/tex.Width,
				layerDepth: .1f
			);
		}
	}
}

