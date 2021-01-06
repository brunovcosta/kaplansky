using System;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ArenaMonoGame;
using System.Collections.Generic;
using FarseerPhysics.Factories;
using Arena;


namespace Arena{
	public class Map{
		public List<Body> blocks{ set; get;}
		int height	,width,UPDATE_RANGE = 20,DRAW_RANGE_X=130,DRAW_RANGE_Y=130*9/16;
		public float SIZE_IN_METERS=4;
		public Vector2 arrival;
		PolygonShape shape;
		Color[] data;
		World world;
		Texture2D ground_full,ground_upleft,ground_upright,ground_downleft,ground_downright,lava;
		Vector2 upLeft,upRight,downLeft,downRight;
		Vertices vertQuad,vertUpLeft,vertUpRight,vertDownLeft,vertDownRight;
		public Map (Texture2D mapImg){
			MainGame game = Program.mainGame;
			ground_full = game.Content.Load<Texture2D> ("ground_block_full");
			ground_upleft = game.Content.Load<Texture2D> ("ground_block_upleft");
			ground_upright = game.Content.Load<Texture2D> ("ground_block_upright");
			ground_downleft = game.Content.Load<Texture2D> ("ground_block_downleft");
			ground_downright = game.Content.Load<Texture2D> ("ground_block_downright");
			lava = game.Content.Load<Texture2D> ("lava");
			this.height = mapImg.Height;
			this.width = mapImg.Width;
			world = game.playNode.world;
			data=new Color[mapImg.Width*mapImg.Height];
			mapImg.GetData<Color> (data);
			blocks = new List<Body>();
			shape = new PolygonShape (1);
			shape.Vertices=PolygonTools.CreateRectangle (SIZE_IN_METERS,SIZE_IN_METERS);
			float ppm = game.PIXELS_PER_METER;
			BuildNode build = game.buildNode;
			PlayNode play = game.playNode;
			upLeft = new Vector2(-SIZE_IN_METERS/2,-SIZE_IN_METERS/2);
			upRight = new Vector2(SIZE_IN_METERS/2,-SIZE_IN_METERS/2);
			downLeft = new Vector2(-SIZE_IN_METERS/2,SIZE_IN_METERS/2);
			downRight = new Vector2(SIZE_IN_METERS/2,SIZE_IN_METERS/2);

			vertQuad = new Vertices();
			vertUpLeft = new Vertices();
			vertUpRight = new Vertices();
			vertDownLeft = new Vertices();
			vertDownRight = new Vertices();

			vertQuad.Add(upLeft);
			vertQuad.Add(upRight);
			vertQuad.Add(downLeft);
			vertQuad.Add(downRight);

			vertUpLeft.Add(upRight);
			vertUpLeft.Add(downLeft);
			vertUpLeft.Add(downRight);

			vertUpRight.Add(upLeft);
			vertUpRight.Add(downLeft);
			vertUpRight.Add(downRight);

			vertDownLeft.Add(upLeft);
			vertDownLeft.Add(upRight);
			vertDownLeft.Add(downRight);

			vertDownRight.Add(upLeft);
			vertDownRight.Add(upRight);
			vertDownRight.Add(downLeft);
			for (int i = 0; i < mapImg.Height; i++) {
				for (int j = 0; j < mapImg.Width; j++) {
					int pos = mapImg.Width * i + j;
					if(data[pos] == Color.Blue){
						build.player.body.Position = new Vector2 (j * SIZE_IN_METERS, i * SIZE_IN_METERS);
						build.player.position = new Vector2 (j * SIZE_IN_METERS, i * SIZE_IN_METERS) * Program.mainGame.PIXELS_PER_METER;
					}
					if(data[pos] == Color.Yellow){
                        BadBiter bad = new BadBiter(play.enemies,ppm*j * SIZE_IN_METERS,ppm*i * SIZE_IN_METERS);
					}
					if(data[pos]==Color.Green){
						arrival = new Vector2 (j * SIZE_IN_METERS * ppm, i * SIZE_IN_METERS * ppm);
					}
					if (data [pos] == Color.Brown) {
						int n = 1;
						for (int ki = 0; ki < n; ki++) {
							for (int kj = 0; kj < n; kj++) {
								BreakableWall wall = new BreakableWall (j * SIZE_IN_METERS +kj*SIZE_IN_METERS/n, i * SIZE_IN_METERS +ki*SIZE_IN_METERS/n,SIZE_IN_METERS / n);
								play.Add (wall);
							}
						}
					}
				}
			}

		}
		public void Look(Vector2 pos){
			int x0 = (int)((pos.X-UPDATE_RANGE*SIZE_IN_METERS/2) / SIZE_IN_METERS);
			int y0 = (int)((pos.Y-UPDATE_RANGE*SIZE_IN_METERS/2)/ SIZE_IN_METERS);
			x0 = x0 > 0 ? x0 : 0;
			y0 = y0 > 0 ? y0 : 0;
			int xmax = x0 + UPDATE_RANGE < width ? x0 + UPDATE_RANGE : width;
			int ymax = y0 + UPDATE_RANGE < height ? y0 + UPDATE_RANGE : height;
			foreach (Body block in blocks) {
				while (block.FixtureList.Count > 0) {
					block.DestroyFixture (block.FixtureList [0]);
					world.RemoveBody (block);
				}
			}
			blocks.RemoveRange (0,blocks.Count);
			for (int i = y0; i < ymax ; i++) {
				for (int j = x0; j < xmax; j++) {
					int index = width * i + j;
					if (data [index] == Color.Black ) {
						Vertices vertices;
						bool up,down,left,right;
						if (index > 0)
							left = (data [index - 1] == Color.Black);
						else
							left = true;
						if (index < data.Length-1)
							right = (data [index + 1] == Color.Black);
						else
							right = true;
						if (index >= width)
							up = (data [index - width] == Color.Black);
						else
							up = true;
						if (index < data.Length - width)
							down = (data [index + width] == Color.Black);
						else
							down = true;
						vertices = vertQuad;
						if (!up && !left && (down || right))
							vertices = vertUpLeft;
						if (!up && !right && (down || left))
							vertices = vertUpRight;
						if (!down && !left && (up || right))
							vertices = vertDownLeft;
						if (!down && !right && (up || left))
							vertices = vertDownRight;

						Body b = BodyFactory.CreatePolygon(world, vertices, 1);//BodyFactory.CreateRectangle(world, SIZE,SIZE,1);
						b.Position = new Vector2(j * SIZE_IN_METERS, i * SIZE_IN_METERS);
						b.Friction = 1f;
						blocks.Add (b);
					}
				}
			}


		}
		public bool isInLava(Vector2 pos){
			int range = 4;
			int x0 = (int)((pos.X-range*SIZE_IN_METERS/2) / SIZE_IN_METERS);
			int y0 = (int)((pos.Y-range*SIZE_IN_METERS/2)/ SIZE_IN_METERS);
			x0 = x0 > 0 ? x0 : 0;
			y0 = y0 > 0 ? y0 : 0;
			int xmax = x0 + range < width ? x0 + range : width;
			int ymax = y0 + range < height ? y0 + range : height;
			for (int i = y0; i < ymax ; i++) {
				for (int j = x0; j < xmax; j++) {
					int index = (int)(i*width + j);
					if (data [index] == Color.Red &&
					   pos.X > j * SIZE_IN_METERS - SIZE_IN_METERS / 2 && pos.X < j * SIZE_IN_METERS + SIZE_IN_METERS / 2 &&
					   pos.Y > i * SIZE_IN_METERS - SIZE_IN_METERS / 2 && pos.Y < i * SIZE_IN_METERS + SIZE_IN_METERS / 2)
						return true;

				}
			}
			return false;
		}
		public void Draw(SpriteBatch batch,Vector2 pos){
			int x0 = (int)((pos.X-DRAW_RANGE_X*SIZE_IN_METERS/2) / SIZE_IN_METERS);
			int y0 = (int)((pos.Y-DRAW_RANGE_Y*SIZE_IN_METERS/2)/ SIZE_IN_METERS);
			x0 = x0 > 0 ? x0 : 0;
			y0 = y0 > 0 ? y0 : 0;
			int xmax = x0 + DRAW_RANGE_X < width ? x0 + DRAW_RANGE_X : width;
			int ymax = y0 + DRAW_RANGE_Y < height ? y0 + DRAW_RANGE_Y : height;
			for (int i = y0; i < ymax ; i++) {
				for (int j = x0; j < xmax; j++) {
					int index = (int)(i*width + j);
					if (data [index] == Color.Black ) {
						bool up,down,left,right;
						if (index > 0)
							left = (data [index - 1] == Color.Black);
						else
							left = true;
						if (index < data.Length-1)
							right = (data [index + 1] == Color.Black);
						else
							right = true;
						if (index >= width)
							up = (data [index - width] == Color.Black);
						else
							up = true;
						if (index < data.Length - width)
							down = (data [index + width] == Color.Black);
						else
							down = true;
						Texture2D tex=ground_full;
						if (!up && !left && (down || right))
							tex = ground_upleft;
						if (!up && !right && (down || left))
							tex = ground_upright;
						if (!down && !left && (up || right))
							tex = ground_downleft;
						if (!down && !right && (up || left))
							tex = ground_downright;
						batch.Draw (
							texture: tex,
							position: SIZE_IN_METERS*(j*Vector2.UnitX+i*Vector2.UnitY)*Program.mainGame.PIXELS_PER_METER,
							origin: ground_full.Width*Vector2.One/2,
							scale: Vector2.One*SIZE_IN_METERS*Program.mainGame.PIXELS_PER_METER/(ground_full.Width)
						);
					}else if(data[index] == Color.Red){
						batch.Draw (
							texture: lava,
							position: SIZE_IN_METERS*(j*Vector2.UnitX+i*Vector2.UnitY)*Program.mainGame.PIXELS_PER_METER,
							origin: ground_full.Width*Vector2.One/2,
							scale: Vector2.One*SIZE_IN_METERS*Program.mainGame.PIXELS_PER_METER/(ground_full.Width),
							color: Color.Lerp(Color.Transparent,Color.White,.5f)
						);
					}
				}
			}


		}
	}
}

