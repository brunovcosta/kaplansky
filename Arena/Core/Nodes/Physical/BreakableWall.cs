using System;
using Arena;
using Microsoft.Xna.Framework.Graphics;
using ArenaMonoGame;
using FarseerPhysics.Factories;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;

namespace Arena{
	public class BreakableWall :PhysicalNode{
		Texture2D tex;
		float width;
		public BreakableWall (float x,float y,float w){
			tex = Program.mainGame.Content.Load<Texture2D> ("ground_block_full");
			this.width = w;
			body = BodyFactory.CreateRectangle (Program.mainGame.playNode.world,width,width, 1);
			body.BodyType = BodyType.Static;
			body.Position = new Vector2  (x,y);
		}
		public override void Draw (Microsoft.Xna.Framework.Graphics.SpriteBatch batch){
			float ppm = Program.mainGame.PIXELS_PER_METER;
			batch.Draw (tex, position: body.Position * ppm, rotation: body.Rotation, scale:Vector2.One* ppm * width / tex.Width,origin: (tex.Width*Vector2.One)/2);
		}
	}
}

