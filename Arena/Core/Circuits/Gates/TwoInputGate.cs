using System;
namespace Arena
{
	public abstract class TwoInputGate : Gate{
		public TwoInputGate(Component input1, Component input2) : base(2){
			this.inputs[0] = input1;
			this.inputs[1] = input2;
		}
		public override bool Filled (){
			return inputs [0] != null && inputs [1] != null;
		}
	}
}