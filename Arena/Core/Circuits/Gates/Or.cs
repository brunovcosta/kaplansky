using System;

namespace Arena{
	public class Or : TwoInputGate{
		public Or(Component input1,Component input2) : base(input1,input2){}
		public override bool State (){
			return inputs[0].State () || inputs[1].State ();
		}
	}
}

