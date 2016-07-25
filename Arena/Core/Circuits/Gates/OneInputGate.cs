using System;

namespace Arena{
	public class OneInputGate : Gate{
		public OneInputGate (Component input) :base(1){
			this.inputs[0] = input;
		}
		public override bool Filled (){
			return inputs [0] != null;
		}
		public override bool State (){
			throw new NotImplementedException ();
		}
	}
}

