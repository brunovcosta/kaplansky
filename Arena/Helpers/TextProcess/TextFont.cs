using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Arena{
	public class TextFont{
		Texture2D font;
		public TextFont (Texture2D font){
			this.font = font;
		}
		public void Draw(SpriteBatch batch,string text,Vector2 position,int size){
			char[] array = text.ToCharArray ();
			Rectangle ret;
			ret.Width = font.Width / 16;
			ret.Height = font.Height / 14;

			int h = size;
			int w = 14 * h / 16;
		
			for (int t = 0; t < array.Length; t++) {
				ret.X = ret.Width * ((array [t] - 32) % 16);
				ret.Y = ret.Height * ((array [t] - 32) / 16);
				batch.Draw (texture: font,
					position: position + w * t * Vector2.UnitX,
					sourceRectangle: ret,
					origin: Vector2.Zero,
					scale: Vector2.One * size / ret.Height
				);
			}
		}
	}
}

