using System;
using Microsoft.Xna.Framework.Graphics;
using ArenaMonoGame;
using Microsoft.Xna.Framework;

namespace Arena{
	public class GuiButton : Panel{
		Texture2D texture;
		public GuiButton (String texPath,int x,int y,int w,int h) : base(x,y,w,h){
			texture = Program.mainGame.Content.Load<Texture2D> (texPath);
		}
		public void Draw(SpriteBatch batch,bool selected){
			batch.Draw (texture,new Vector2(x,y),scale: selected? 1.1f*Vector2.One :Vector2.One);
		}
	}
}

