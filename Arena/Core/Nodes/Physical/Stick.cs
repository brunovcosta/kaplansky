using System;
using Arena;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;
using ArenaMonoGame;
using Microsoft.Xna.Framework;
using FarseerPhysics.Factories;

namespace Arena{
	public class Stick : PhysicalNode{
		float density=70;
		float width = 10;
		public Stick (PhysicalNode parent,Vector2 relativePos){
			this.world = parent.world;
			body = new Body (world, parent.body.Position+relativePos);
			PolygonShape shape = new PolygonShape (density);
			Vertices vertices = new Vertices (4);
			float ppm = Program.mainGame.PIXELS_PER_METER;
			float l = width / (2 * ppm);
			vertices.Add (new Vector2(l,l*30));
			vertices.Add (new Vector2(-l,0));
			vertices.Add (new Vector2(l,0));
			vertices.Add (new Vector2(-l,l*30));
			shape.Vertices = vertices;
			body.BodyType = BodyType.Dynamic;
			body.CreateFixture (shape);
			parent.Fix (this,relativePos);
		}
	}
}

