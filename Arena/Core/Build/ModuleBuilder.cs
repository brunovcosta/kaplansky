using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ArenaMonoGame;
using Microsoft.Xna.Framework.Input;
using Arena;

namespace Arena{
	public class ModuleEntry{
		public Vector2 pos1;
		public Vector2 pos2{ get; set;}
		public Component comp;
		public ModuleSelection kind;
	}
	public class ModuleBuilder{
		public List<ModuleEntry> modules;
		BuildNode node;
		public ModuleBuilder (BuildNode node){
			modules = new List<ModuleEntry> ();
			this.node = node;
		}
		public void FinishModulesInsertion(){
			node.state = BuildState.INSERT_CIRCUIT;
			node.circuitBar.selected = null;
		}
		public void DirectModules(){
			if (
				node.mouse.LeftButton == ButtonState.Released &&
				modules.Count>0
			) {
				if (modules [modules.Count - 1].pos2 == Vector2.Zero) {
					modules [modules.Count - 1].pos2 = MouseInterface.MouseOnGrid (node.mouse, node.view.GRID);
					FinishModulesInsertion ();
				}
			}
		}
		public void MountModules(ModuleSelection selection){
			/*Console.WriteLine (
				Geometry.PointInPolygon (
					node.mouse.Position.ToVector2 (),
					node.playerBuilder.playerVertex
				)
			);
*/
			if (selection != ModuleSelection.NONE) {
				if (
					node.mouse.LeftButton == ButtonState.Pressed &&
					node.previousLeft == ButtonState.Released &&
				    Geometry.PointInPolygons (
						node.mouse.Position.ToVector2 (),
						node.playerBuilder.playerVertex
					)
				) {
					ModuleEntry entry = new ModuleEntry ();
					entry.pos1 = MouseInterface.MouseOnGrid (node.mouse, node.view.GRID);
					entry.kind = selection;
					modules.Add (entry);
				}
			}
		}
	}
}

