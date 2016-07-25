using System;
using Arena;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using FarseerPhysics.Common;
using Microsoft.Xna.Framework.Input;
using ArenaMonoGame;

namespace Arena{
	public class View : Panel{
		public int GRID=1;
		public BuildScene node;
		public View(BuildScene node,int x,int y,int w,int h):base(x,y,w,h){
			this.node = node;
		}
		public void Draw (SpriteBatch batch){
			DrawGrid (batch);
			DrawLines (batch);
			DrawCircles (batch);
			//DrawPoints (batch);
			DrawModules (batch);
		}
		public void DrawGrid(SpriteBatch batch){
			if (GRID > 10) {
				for (int t = x; t<= x+w;t += GRID) {
					Paint.DrawLineSegment (batch,t,y,t,y+h,Color.Gray,1);
				}
				for (int t = y; t<= y+h; t += GRID) {
					Paint.DrawLineSegment (batch,x,t,x+w,t,Color.Gray,1);
				}
			}
		}
		public void DrawLines(SpriteBatch batch){
			int l = Program.mainGame.WIDTH;
			List<Vector2> lines = node.drawBuilder.lines;
			for (int t = 0; t < lines.Count-1; t+=2) 
				Paint.DrawLineSegment (batch,
					(int)(lines[t]+l*(lines[t]-lines[t+1])/Geometry.Distance (lines[t],lines[t+1])).X,
					(int)(lines[t]+l*(lines[t]-lines[t+1])/Geometry.Distance (lines[t],lines[t+1])).Y,
					(int)(lines[t+1]+l*(lines[t+1]-lines[t])/Geometry.Distance (lines[t],lines[t+1])).X,
					(int)(lines[t+1]+l*(lines[t+1]-lines[t])/Geometry.Distance (lines[t],lines[t+1])).Y,
					Color.White,1); 
			foreach (var playerVertex in node.playerBuilder.playerVertex) {
				for (int t = 0; t < playerVertex.Count - 1; t += 1) {
					Paint.DrawLineSegment (batch,
						(int)playerVertex [t].X,
						(int)playerVertex [t].Y,
						(int)playerVertex [t + 1].X,
						(int)playerVertex [t + 1].Y,
						Color.White, 4
					);
				}
			}
		}
		public void DrawPoints(SpriteBatch batch){
			foreach (var playerVertex in node.playerBuilder.playerVertex) {
				for (int t = 0; t < playerVertex.Count; t++){
					Paint.DrawSquare (batch, (int)playerVertex [t].X, (int)playerVertex [t].Y, Color.Yellow, 7);
				}
			}
		}
		public void DrawCircles(SpriteBatch batch){
			List<Vector2> circles = node.drawBuilder.circles;
			for (int t = 0; t < circles.Count-1; t+=2) 
				Paint.DrawCircle (batch, circles [t], Geometry.Distance (circles [t], circles [t + 1]), Color.White, 1);
		}
		public void DrawModules(SpriteBatch batch){
			List<ModuleEntry> modules = node.moduleBuilder.modules;
			for(int t=0;t<modules.Count;t++){
				Vector2 pos, delta;
				Texture2D tex;
				ModuleData data = ModuleData.fromSelect(node.moduleBuilder.modules[t].kind);
				tex= Program.mainGame.Content.Load<Texture2D>(data.texturePath);
				pos = modules [t].pos1;
				delta = (modules [t].pos2==Vector2.Zero ? MouseInterface.MouseOnGrid(node.mouse,node.view.GRID) : modules [t].pos2) - modules [t].pos1;
				float size = 0;
				if (data.size == 0)
					size = (int)delta.Length ();
				else
					size = data.size;
				//Console.WriteLine ("data.size: "+data.size);
				batch.Draw (
					texture: tex,
					position: pos,
					color: Color.White,
					rotation: (float)Math.Atan2 (delta.Y,delta.X),
					origin: tex.Width*Vector2.UnitX/2+tex.Height*Vector2.UnitY/2,
					scale: Vector2.One*((float)size)/tex.Height
				);
			}
		}
	}
}