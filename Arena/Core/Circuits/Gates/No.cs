using System;

namespace Arena{
	public class No : OneInputGate{
		public No (Component input):base(input){}
		public override bool State(){
			return !inputs[0].State();
		}
	}
}

