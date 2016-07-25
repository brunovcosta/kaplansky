using System;

namespace Arena{
	public abstract class Component{
		public abstract bool State();
		public abstract bool Filled();
		public abstract bool Add(Component comp);
	}
}

