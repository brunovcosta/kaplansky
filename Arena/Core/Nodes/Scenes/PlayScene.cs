using System;
using Arena;
using Microsoft.Xna.Framework;
using FarseerPhysics.Dynamics;
using ArenaMonoGame;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;
using FarseerPhysics.DebugView;
using FarseerPhysics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Arena {
	public class PlayNode : Node {
		public Vector2 GRAVITY;
		public World world;
		public Player player;
		public DebugViewXNA physicsView;
		public Camera cam;
		public Map map;
		float previousScroll;
		public float deltaScroll;
		Texture2D background,flag;
		public EnemyNode enemies;

		public PlayNode() {
			MainGame game = Program.mainGame;
			GRAVITY = new Vector2(0f, 9.82f);
			world = new World(GRAVITY);
			physicsView = new DebugViewXNA(world);
			physicsView.LoadContent(game.GraphicsDevice, game.Content);
			physicsView.AppendFlags(DebugViewFlags.Shape);
			physicsView.AppendFlags(DebugViewFlags.PolygonPoints);
			cam = new Camera();
			cam.Pos = new Vector2(0f, 0f);
			background = game.Content.Load<Texture2D>("background.png");
			flag = game.Content.Load<Texture2D> ("arrival");
			enemies = new EnemyNode(this);
		}

		public override void Update(GameTime gameTime) {
			MainGame game = Program.mainGame;
			KeyboardState key =	game.keyState;
			MouseState mouse = game.mouseState;
			deltaScroll = mouse.ScrollWheelValue - previousScroll;
			map.Look(player.body.Position);
			if (key.IsKeyDown(Keys.OemMinus) || deltaScroll < 0)
				cam.Zoom /= (float)Math.Exp(.1);
			if (key.IsKeyDown(Keys.OemPlus) || deltaScroll > 0)
				cam.Zoom *= (float)Math.Exp(.1);
			if (key.IsKeyDown(Keys.Back)) {
				game.goTo ();
			}
			world.Step(0.033333f);
			UpdateChildrens(gameTime);
			cam.Pos = player.position + 0.5f * (mouse.Position.ToVector2() - new Vector2(
				game.WIDTH / 2,
				game.HEIGHT / 2
			)) / cam.Zoom;
			previousScroll = mouse.ScrollWheelValue;
		}

		void DrawPhysics() {
			MainGame game = Program.mainGame;
			Matrix transform = Matrix.CreateOrthographicOffCenter(
                -game.GraphicsDevice.Viewport.Width / (game.PIXELS_PER_METER * 2 * cam.Zoom),
                game.GraphicsDevice.Viewport.Width / (game.PIXELS_PER_METER * 2 * cam.Zoom),
                game.GraphicsDevice.Viewport.Height / (game.PIXELS_PER_METER * 2 * cam.Zoom),
                -game.GraphicsDevice.Viewport.Height / (game.PIXELS_PER_METER * 2 * cam.Zoom),
                0f,
                1f);
			Matrix view = Matrix.Identity;
			view.Translation = new Vector3(-cam.Pos / (game.PIXELS_PER_METER), 0);
			physicsView.RenderDebugData(ref transform, ref view);
		}

		public override void Draw(SpriteBatch batch) {
			MainGame game = Program.mainGame;
			batch.Begin();
			batch.Draw(background, position: Vector2.Zero, layerDepth: 1,scale:  new Vector2(1f*game.WIDTH/background.Width,1f*game.HEIGHT/background.Height));
			batch.End();
			batch.Begin(
				sortMode: SpriteSortMode.BackToFront,
				blendState: BlendState.AlphaBlend,
				transformMatrix: cam.getTransformation(game.GraphicsDevice)
			);
			if (player != null) {
				map.Draw (batch, player.body.Position);
			}
			Rectangle arrivalRect = new Rectangle ();
			arrivalRect.Size = (game.PIXELS_PER_METER*map.SIZE_IN_METERS * Vector2.One).ToPoint ();
			arrivalRect.Location = map.arrival.ToPoint()-(arrivalRect.Size.ToVector2()/2).ToPoint();
			batch.Draw (flag, destinationRectangle: arrivalRect);

			DrawChildrens(batch);
			batch.End();
			batch.Begin();
			if(player!=null)
				batch.Draw(
					player.texFuel,
					position: new Vector2(10, 10),
					scale: new Vector2(
						player.energy / player.texFuel.Width,
						1
					)
				);
			//DrawPhysics();
			batch.End();
		}
	}
}

