using System;
using Microsoft.Xna.Framework.Input;

namespace Arena
{
	public class Panel{
		public int x,y,w,h;
		public Panel (int x,int y,int w,int h){
			this.x = x;
			this.y = y;
			this.w = w;
			this.h = h;
		}
		public bool hasMouse(MouseState mouse){
			return mouse.Position.X > x && mouse.Position.X < x + w &&
			mouse.Position.Y > y && mouse.Position.Y < y + h;
		}
	}
}

