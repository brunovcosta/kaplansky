using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Arena
{
	public class MouseInterface
	{
		public static Vector2 MouseOnGrid(MouseState state,int grid){
			return new Vector2 (((state.X + grid / 2) / grid) * grid, ((state.Y + grid / 2) / grid) * grid);
		}
	}
}

