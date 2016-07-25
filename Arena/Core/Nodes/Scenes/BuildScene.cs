using Arena;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ArenaMonoGame;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using FarseerPhysics.Collision;
using FarseerPhysics.Common;
namespace Arena{
	public enum BuildState{
		DRAW_REFERENCES=0,
		DRAW_SHIP=1,
		INSERT_MODULES=2,
		INSERT_CIRCUIT=3
	}
	public class BuildNode : Node{
		public ButtonState previousRight;
		public ButtonState previousLeft;
		public MouseState  mouse;
		public KeyboardState keyboard;
		public BuildState state;
		public Player player;
		public View view;
		public DrawBuilder drawBuilder;
		public PlayerBuilder playerBuilder;
		public ModuleBuilder moduleBuilder;
		public CircuitBuilder circuitBuilder;
		int level;
		Texture2D background;
		public CircuitBar circuitBar;
		public ModuleBar modulesBar;
		public BuildNode (int level=1){
			MainGame game = Program.mainGame;
			this.level = level;
			view = new View(this,50,50,game.WIDTH-100,game.HEIGHT-100);
			state = BuildState.DRAW_SHIP;
			previousLeft = previousRight = ButtonState.Released;
			background=Program.mainGame.Content.Load<Texture2D> ("blueprint.png");
			circuitBar = new CircuitBar (this, 0, 0, 500 , 50);
			modulesBar=new ModuleBar(this, 0, 0, 500, 50);
			drawBuilder= new DrawBuilder (this);
			playerBuilder= new PlayerBuilder (this);
			moduleBuilder= new ModuleBuilder (this);
			circuitBuilder = new CircuitBuilder (this);
		}
		void DrawFinish (){
			state = BuildState.DRAW_SHIP;
		}
		public bool ReadyToGo(){
			return (state == BuildState.INSERT_MODULES)/* && (modulesBar.selected == ModuleSelection.NONE)*/;
		}
		public void CraftFinish(){
			Program.mainGame.playNode = new PlayNode ();
			player = new Player (Program.mainGame.playNode,playerBuilder.playerVertex);
			state = BuildState.INSERT_MODULES;
			playerBuilder.playerVertex.Add (playerBuilder.playerVertex[0]);
		}
		void MountFinish(){
			MainGame game = Program.mainGame;
			Vector2 playerScreenPosition = player.position;
			game.playNode.map=new Map(game.Content.Load<Texture2D> ("levels/lv"+level));
			for(int t=0;t<moduleBuilder.modules.Count;t++){
				Vector2 pos = moduleBuilder.modules [t].pos1;
				Vector2 delta=moduleBuilder.modules[t].pos2-moduleBuilder.modules[t].pos1;
				Vector2 relativePos = (pos - playerScreenPosition) / Program.mainGame.PIXELS_PER_METER;
				ModuleBase module = (ModuleBase)Activator.CreateInstance(
					ModuleData.fromSelect(moduleBuilder.modules[t].kind).type,
					player,
					relativePos,
					moduleBuilder.modules [t].comp,
					delta.Length (),
					(float)Math.Atan2 (delta.Y, delta.X)

				);
				player.Fix (module, relativePos);
			}
			Console.WriteLine (player.position);
			BuildFinish ();
		}
		public void BuildFinish (){
			MainGame game = Program.mainGame;
			game.playNode.Add (player);
			game.playNode.player = player;
			Program.mainGame.state = GameStates.PLAY;
		}
		void updateReferences(){
			if (keyboard.IsKeyDown (Keys.Enter))
				DrawFinish ();
			if (view.hasMouse (mouse)) {
				drawBuilder.MakeLines ();
				drawBuilder.MakeCircles ();
				drawBuilder.EraseLinesAndCircles ();
			}
		}
		void updateModuleInsertion(){
			if (modulesBar.hasMouse (mouse))
				modulesBar.Select (mouse);
			if (view.hasMouse (mouse))
				moduleBuilder.MountModules (modulesBar.selected);
			moduleBuilder.DirectModules ();
			if (keyboard.IsKeyDown (Keys.Enter) && ReadyToGo ())
				MountFinish ();
		}
		void updateCircuitInsertion(){
			circuitBar.key = keyboard.GetPressedKeys ().Length > 0;
			if (circuitBar.hasMouse (mouse))
				circuitBar.Select (mouse, keyboard);
		}
		public override void Update(GameTime gameTime){	
			UpdateChildrens (gameTime);
			keyboard = Program.mainGame.keyState;
			mouse = Program.mainGame.mouseState;
			if (state == BuildState.DRAW_REFERENCES) {
				updateReferences ();
			} else if (state == BuildState.DRAW_SHIP) {
				if (view.hasMouse (mouse))
					playerBuilder.MakePlayerVertices ();
			} else if (state == BuildState.INSERT_MODULES) {
				updateModuleInsertion ();
			} else if (state == BuildState.INSERT_CIRCUIT) {
				updateCircuitInsertion ();
			}
			previousLeft = mouse.LeftButton;
			previousRight = mouse.RightButton;
		}
		public override void Draw(SpriteBatch batch){
			batch.Begin ();
			MainGame game = Program.mainGame;
			batch.Draw (background,Vector2.Zero,scale: new Vector2(1f*game.WIDTH/background.Width,1f*game.HEIGHT/background.Height));
			view.Draw (batch);
			if(state==BuildState.INSERT_MODULES )
				modulesBar.Draw (batch);
			circuitBar.key = keyboard.GetPressedKeys ().Length > 0;
			if(state==BuildState.INSERT_CIRCUIT)
				circuitBar.Draw (batch);
			batch.End ();
		}
	}
}