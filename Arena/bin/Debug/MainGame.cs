#region Using Statements
using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using FarseerPhysics.Dynamics;
using FarseerPhysics;
using FarseerPhysics.DebugView;
using FarseerPhysics.Factories;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;
using Arena;


#endregion

namespace Arena{
	public enum GameStates{
		BUILD=0,
		PLAY=1
	}
	public class MainGame : Game{
		public GameStates state;
		public float PIXELS_PER_METER=30;
		public int WIDTH=1600,HEIGHT=1600	*9/16;
		public SpriteBatch spriteBatch;
		public PlayNode playNode;
		public BuildNode buildNode;
		public MouseState mouseState;
		public KeyboardState keyState;
		public double time = 0;
		public TextFont font;
		public MainGame() {
			state = GameStates.BUILD;
			GraphicsDeviceManager graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";	            
			TargetElapsedTime=TimeSpan.FromTicks (333333 / 2);
			//graphics.IsFullScreen = true;
			IsMouseVisible = true;
			graphics.PreferredBackBufferWidth = WIDTH;
			graphics.PreferredBackBufferHeight = HEIGHT;
			graphics.ApplyChanges();
		}
		protected override void Initialize(){
			base.Initialize();
			buildNode = new BuildNode ();
		}
		protected override void LoadContent(){
			spriteBatch = new SpriteBatch(GraphicsDevice);
			font = new TextFont (Content.Load<Texture2D> ("font_bitmap"));
		}
		protected override void Update(GameTime gameTime){
			GetInputStates ();
			Debug (gameTime);

			if (state == GameStates.BUILD)
				buildNode.Update (gameTime);
			if (state == GameStates.PLAY)
				playNode.Update (gameTime);
		}
		protected override void Draw(GameTime gameTime){

			if (state == GameStates.BUILD) {
				buildNode.Draw (spriteBatch);
			}
			if (state == GameStates.PLAY) 
				playNode.Draw (spriteBatch);

			time = gameTime.TotalGameTime.TotalMilliseconds;
		}
		protected void GetInputStates(){
			mouseState = Mouse.GetState();
			keyState = Keyboard.GetState ();
		}
		protected void Debug(GameTime gametime){
			for(int t=0;t<100;t++){
				//Console.WriteLine ();
			}
			//Console.WriteLine ("FPS: " + (1000 /( gametime.TotalGameTime.TotalMilliseconds-time)));
			//Console.WriteLine ("Total de nós: " + playNode.Total () + buildNode.Total ());
			//Console.WriteLine ("mouse: ("+mouseState.X+','+mouseState.Y+')');
			//Console.WriteLine ("world: "+world.BodyList.Count);
		}
	}
}

