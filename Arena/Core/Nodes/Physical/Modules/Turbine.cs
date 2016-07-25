using System;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using FarseerPhysics.Collision.Shapes;
using ArenaMonoGame;
using Microsoft.Xna.Framework.Input;
using Arena;

namespace Arena{
	public class Turbine : ModuleBase{
		public Texture2D texture,textureOn,textureOff;
		int width = 20;
		public float force;
		public float FORCE_TAX=5;
		float consume = .05f;
		public static ModuleData moduleData = new ModuleData (typeof(Turbine), "botTurb", "turbine_off", ModuleSelection.TURBINE);
		public Turbine (PhysicalNode parent,Vector2 relativePos,Component component,float length,float deltaAngle) : base(parent,relativePos,component,length,deltaAngle){
			world = parent.world;
			textureOn =  Program.mainGame.Content.Load<Texture2D> ("turbine_on.png");
			textureOff =  Program.mainGame.Content.Load<Texture2D> ("turbine_off.png");
			texture = textureOff;
			direction = 0;
			force = length * length / FORCE_TAX;;
			width = (int)length;
			float ppm = Program.mainGame.PIXELS_PER_METER;
			body = new Body (world, parent.body.Position, direction);
			body.BodyType = BodyType.Dynamic;
		}		
		public override void Update (GameTime gameTime){
			float ppm = Program.mainGame.PIXELS_PER_METER;
			position = ppm*body.Position;
			Player player = Program.mainGame.playNode.player;
			direction = ((PhysicalNode) parent).body.Rotation+deltaAngle;
			if (component.State () && player.energy>0) {
				Random rand = new Random ();
				float randomDeltaAngle = component.State() ? (float)rand.NextDouble ()-.5f : 0;
				direction = ((PhysicalNode) parent).body.Rotation+deltaAngle+.5f*randomDeltaAngle;
				body.ApplyForce (new Vector2 (force * (float)Math.Cos (direction), force * (float)Math.Sin (direction)));
				texture = textureOn;
				player.energy -= consume;
			} else {
				texture = textureOff;
			}
		}
		public override void Draw (SpriteBatch batch){
			Vector2 origin = new Vector2(texture.Width/2, texture.Height/2);
			batch.Draw(
				texture, 
				position, 
				color: Color.White,
				rotation: direction,
				origin: origin,
				scale: Vector2.One*((float)width)/texture.Height,
				layerDepth: .1f
			);
		}
	}
}

