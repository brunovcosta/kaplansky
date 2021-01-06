using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace Arena{
	public class CircuitBuilder{
		Stack<Component> unprocessedCircuitStack;
		BuildNode node;
		public CircuitBuilder(BuildNode node){
			unprocessedCircuitStack = new Stack<Component> ();
			this.node = node;
		}
		public void ProcessStack(){
			while (unprocessedCircuitStack.Peek ().Filled () && !Filled ()) {
				Component lastComponent = unprocessedCircuitStack.Pop ();
				unprocessedCircuitStack.Peek ().Add (lastComponent);
			}
		}
		public bool Filled(){
			return unprocessedCircuitStack.Count == 1 && unprocessedCircuitStack.Peek ().Filled ();
		}
		public void Add(Component comp){
			if (comp != null) {
				unprocessedCircuitStack.Push (comp);
				ProcessStack ();
				node.circuitBar.selected=null;
			}
		}
		public void CircuitModules(Component comp){
			Add (comp);
			if (Filled ()) {
				node.moduleBuilder.modules [node.moduleBuilder.modules.Count - 1].comp = unprocessedCircuitStack.Pop ();
				node.state = BuildState.INSERT_MODULES;
			}
		}
	}
}

