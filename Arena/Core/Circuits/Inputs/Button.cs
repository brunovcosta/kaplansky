using System;
using Microsoft.Xna.Framework.Input;
using ArenaMonoGame;
namespace Arena{
	public class Button : Input{
		public Keys key;
		public Button(Keys key){
			this.key = key;
		}
		public override bool State (){
			return Program.mainGame.keyState.IsKeyDown (key);
		}
	}
}

