using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ArenaMonoGame;


namespace Arena{
	public class Shapes{
		public static int DOT_SIZE=1;
		public static Texture2D Rectangle(int width,int height){
			Color[] pixels = new Color[width * height];
			for (int t=0; t<width*height; t++) {
				pixels [t] = Color.White;
			}
			Texture2D tex = new Texture2D (Program.mainGame.GraphicsDevice,width, height);
			tex.SetData (pixels);
			return tex;
		}
		public static Texture2D Dot(){
			return Rectangle (DOT_SIZE, DOT_SIZE);
		}
	}
}

