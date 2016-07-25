using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Collision.Shapes;
using ArenaMonoGame;
using FarseerPhysics.Common;
using System.Collections.Generic;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework.Content;
using FarseerPhysics.Common.Decomposition;
using System.Collections.Generic;
using Arena;

namespace Arena{
	public class Player : PhysicalNode{
		public Texture2D texture,texFuel;
		public float direction;
		public int width,height;
		public float density = .1f;
		public float energy;
		public float energyDensity=100;
		public Player (Node parent,List<Vertices> verticesList) {
			energy = 100;
			float ppm = Program.mainGame.PIXELS_PER_METER;
			foreach (Vertices vertices in verticesList) {
				Vertices vertex = new Vertices ();
				vertex.AddRange (vertices);
				for (int t=0;t<vertex.Count;t++) {
					vertex [t] -= position;
					vertex[t]/= ppm;
				}
				vertex.Scale (ppm*Vector2.One);
				texture = PlayerBuilder.TextureFromPolygon (
					Program.mainGame.Content.Load<Texture2D> ("player_base.png"),
					vertex
				);
				List<Fixture> compund = FixtureFactory.AttachCompoundPolygon (Triangulate.ConvexPartition (vertex, TriangulationAlgorithm.Earclip), density, body);
				compund[0].Body.BodyType = BodyType.Dynamic;
			}
			this.parent = parent;
			world = Program.mainGame.playNode.world;
			width = 40;
			direction = 0;
			body = new Body (world, position/ppm, direction);
			texFuel = Program.mainGame.Content.Load<Texture2D> ("combustivel.jpg");
			parent.Add (this);

		}
		public override void Update (GameTime gameTime){
			float ppm = Program.mainGame.PIXELS_PER_METER;
			position = ppm*body.Position;
			direction = (float)(body.Rotation);
			if (inLava ()) {
				body.ApplyForce (new Vector2 (0, -1.5f * body.Mass * Program.mainGame.playNode.GRAVITY.Length ()) - body.LinearVelocity * body.LinearVelocity.Length ());
				body.AngularVelocity /= 1.1f;
			}
			if ((position - Program.mainGame.playNode.map.arrival).Length () < 100)
				Program.mainGame.next();
		}
		public override void Draw (SpriteBatch batch){
			Vector2 origin = new Vector2(texture.Width/2, texture.Height/2);
			batch.Draw(texture,position: position,color: Color.White,rotation: direction,origin: origin,scale: Vector2.One,layerDepth: .5f);
		}
		public bool inLava(){
			return Program.mainGame.playNode.map.isInLava (body.Position);
		}
	}
}

