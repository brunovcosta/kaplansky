using System;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using FarseerPhysics.Collision.Shapes;
using ArenaMonoGame;
using Microsoft.Xna.Framework.Input;
using Arena;

namespace Arena{
	public class TestingBlock : ModuleBase{
		public Texture2D texture;
		public float direction;
		int width,height;
		public Vector2 position;
		public TestingBlock (PhysicalNode parent,Vector2 relativePos,Component component,float length,float deltaAngle) : base(parent,relativePos,component,length,deltaAngle){
			world = Program.mainGame.playNode.world;
			width = height = 20;
			this.parent = parent;
			texture = Shapes.Rectangle (width,height);
			position = parent.position+new Vector2(1,-100);
			direction = 0;
			float ppm = Program.mainGame.PIXELS_PER_METER;
			body = new Body (world, position/ppm, direction);
			CircleShape shape = new CircleShape ((width/2)/ppm,1);
			body.BodyType = BodyType.Dynamic;
			body.CreateFixture(shape);
		}		
		public override void Update (GameTime gameTime){
			float ppm = Program.mainGame.PIXELS_PER_METER;
			position = ppm*body.Position;
			direction = body.Rotation;
			body.ApplyForce (new Vector2(0,-10));
		}
		public override void Draw (SpriteBatch batch){
			Vector2 delta = new Vector2 (width / 2, height / 2);
			Vector2 newPos = position - delta;
			Rectangle sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
			Vector2 origin = new Vector2(texture.Width/2, texture.Height/2);
			batch.Draw(texture, position, sourceRectangle, Color.White, direction, origin, ((float)width)/texture.Width, SpriteEffects.None, 1);

			/*		batch.Draw(texture, position-delta,Color.White);
				Paint.DrawDot (batch,new Vector2 (newPos.X, newPos.Y), Color.White);
			Paint.DrawDot (batch,new Vector2(newPos.X+width,newPos.Y+height),Color.White);
			Paint.DrawDot (batch,new Vector2(newPos.X+width,newPos.Y),Color.White);
			Paint.DrawDot (batch,new Vector2(newPos.X,newPos.Y+height),Color.White);*/

		}
	}
}

