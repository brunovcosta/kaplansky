using System;
using ArenaMonoGame;
namespace Arena{
	public class Clock : Input{
		public override bool State (){
			return (int)(Program.mainGame.time / 1000) % 2 == 0;
		}
	}
}

