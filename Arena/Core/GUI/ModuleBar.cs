using System;
using Microsoft.Xna.Framework.Input;
using Arena;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Arena{
	public enum ModuleSelection{
		NONE=0,
		TURBINE=1,
		GUN=2,
		WHEEL_CLOCK_WISE=3,
		WHEEL_ANTI_CLOCK_WISE=4
	}
	public class ModuleBar : Panel{
		BuildNode node;
		public ModuleSelection selected;
		List<GuiButton> buttons;
		public int BUTTON_SIZE=50;
		public ModuleBar (BuildNode node,int x,int y,int w,int h):base(x,y,w,h){
			this.node = node;
			buttons = new List<GuiButton>();
			for(int t=0;t<ModuleData.all.Length-1;t++){
				buttons.Add(new GuiButton (ModuleData.fromSelect((ModuleSelection)(t+1)).buttonPath,x+BUTTON_SIZE*t,y,BUTTON_SIZE,BUTTON_SIZE));
			}
		}
		public void Select(MouseState mouse){
			if (node.mouse.LeftButton == ButtonState.Pressed && node.previousLeft == ButtonState.Released && hasMouse (mouse)) {
				selected = ModuleSelection.NONE;
				for(int t=0;t<buttons.Count;t++){
					if (buttons[t].hasMouse(mouse))
						selected = ModuleData.all[t+1];
				}
			}
		}
		public void Draw(SpriteBatch batch){
			for (int t = 0; t < buttons.Count; t++) {
				buttons[t].Draw(batch, selected == (ModuleSelection)t+1);
			}
		}
	}
}

