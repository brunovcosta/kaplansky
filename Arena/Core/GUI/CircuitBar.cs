using System;
using Microsoft.Xna.Framework.Input;
using ArenaMonoGame;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Arena{
	public class CircuitBar : Panel{
		BuildNode node;
		GuiButton button,clock,and,gyroscope;
		public Component selected;
		public bool key;
		public CircuitBar (BuildNode node,int x,int y,int w,int h):base(x,y,w,h){
			this.node = node;
			int t = 0;
			and = new GuiButton ("botAnd.png",x+50*t++,y,50,50);
			clock = new GuiButton ("botClock.png",x+50*t++,y,50,50);
			gyroscope = new GuiButton ("botGyroscope.png",x+50*t++,y,50,50);
			button = new GuiButton ("botBot.png",x+50*t++,y,50,50);
		}
		public void Select(MouseState mouse,KeyboardState keyboard){
			if (node.mouse.LeftButton == ButtonState.Pressed && node.previousLeft == ButtonState.Released && hasMouse (mouse)) {
				if (button.hasMouse (mouse) && key) 
					selected = new Button (keyboard.GetPressedKeys () [0]);
				else if (and.hasMouse (mouse)) 
					selected = new And (null, null);
				 else if (clock.hasMouse (mouse)) 
					selected = new Clock ();
				 else if (gyroscope.hasMouse (mouse)) 
					selected = new Gyroscope (node.player.body);
				 else 
					selected = null;

				node.circuitBuilder.CircuitModules (selected);
			}
		}
		public void Draw(SpriteBatch batch){
			and.Draw (batch,selected is And);
			clock.Draw (batch,selected is Clock);
			gyroscope.Draw (batch,selected is Gyroscope);
			if (key) {
				button.Draw (batch, selected is Button);
				Program.mainGame.font.Draw (
					batch,
					Program.mainGame.keyState.GetPressedKeys ()[0] .ToString (),
					new Vector2(150,3),
					16
				);
			}
		}
	}
}

