using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Arena{
	public class DrawBuilder{
		public List<Vector2> lines;
		public List<Vector2> circles;
		BuildNode node;
		public DrawBuilder (BuildNode node){
			lines = new List<Vector2> ();
			circles = new List<Vector2> ();
			this.node = node;
		}
		public void MakeLines(){
			if (node.mouse.LeftButton == ButtonState.Pressed) {
				if (node.previousLeft == ButtonState.Released) {
					lines.Add (MouseInterface.MouseOnGrid (node.mouse, node.view.GRID));
					lines.Add (MouseInterface.MouseOnGrid (node.mouse, node.view.GRID));
				} else {
					lines [lines.Count - 1] = MouseInterface.MouseOnGrid (node.mouse, node.view.GRID);
				}
			}
		}
		public void MakeCircles(){
			if (node.mouse.RightButton == ButtonState.Pressed) {
				if (node.previousRight == ButtonState.Released) {
					circles.Add (MouseInterface.MouseOnGrid (node.mouse, node.view.GRID));
					circles.Add (MouseInterface.MouseOnGrid (node.mouse, node.view.GRID));
				} else {
					circles [circles.Count - 1] = MouseInterface.MouseOnGrid (node.mouse, node.view.GRID);
				}
			}
		}
		public void EraseLinesAndCircles(){
			if (node.keyboard.IsKeyDown (Keys.Back)){
				lines = new List<Vector2> ();
				circles = new List<Vector2> ();
			}
		}
	}
}

