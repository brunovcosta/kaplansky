using System;

namespace Arena{
	public abstract class Gate : Component{
		public Component[] inputs;
		public int size;
		public Gate(int size){
			this.size = size;
			inputs = new Component[size];
		}
		public override bool Add(Component comp){
			if (Filled ()) {
				return false;
			} else {
				for(int t=0;t<size;t++){
					if(inputs[t]==null){
						inputs [t] = comp;
						break;
					}else{
						continue;
					}
				}
				return true;
			}
		}
	}
}

