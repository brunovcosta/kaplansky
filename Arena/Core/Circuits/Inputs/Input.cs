using System;

namespace Arena{
	public abstract class Input : Component{
		public override bool Filled (){
			return true;
		}
		public override bool Add (Component comp){
			return false;
		}
	}
}

