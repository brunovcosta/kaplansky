using System;
using Arena;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ArenaMonoGame;
using FarseerPhysics.Dynamics;

namespace Arena{
	public class Gun : ModuleBase{
		public Texture2D texture,textureOn,textureOff;
		int width=20,height=20;		
        public Vector2 position;
		public static ModuleData moduleData = new ModuleData (typeof(Gun), "botLaser", "laser", ModuleSelection.GUN, 20);
		public Gun (PhysicalNode parent,Vector2 relativePos,Component component,float length,float deltaAngle) : base(parent,relativePos,component,length,deltaAngle) {
			world = parent.world;
			texture = Program.mainGame.Content.Load<Texture2D> ("laser.png");
			this.deltaAngle = deltaAngle;
			float ppm = Program.mainGame.PIXELS_PER_METER;
			direction = 0;
			body = new Body (world, position/ppm, direction);
			body.BodyType = BodyType.Dynamic;
		}
		public override void Update (GameTime gameTime){
			float ppm = Program.mainGame.PIXELS_PER_METER;
			position = ppm*body.Position;
			direction = ((PhysicalNode) parent).body.Rotation+deltaAngle;
            if(component.State()){
                foreach(Node enemy in Program.mainGame.playNode.enemies.childrens){
                    if(Geometry.IsInCone(
                        parent.position,
                        ((Player)parent).direction,(float)Math.PI/4,
                        enemy.position)
                      ){
                        ((EnemyBase)enemy).Hurt(.01f);
                    }
                }
            }
		}
		public override void Draw (SpriteBatch batch){
			if (component.State ())
				Paint.DrawLineSegment (
					batch,
					(int)position.X,
					(int)position.Y,
					(int)(position.X + 5000 * Math.Cos (direction)),
					(int)(position.Y + 5000 * Math.Sin (direction)),
					Color.Yellow,
					2,
					.1f

				);
			batch.Draw (
				texture: texture,
				position: position,
				origin: new Vector2 (texture.Width/2,texture.Height/2),
				rotation: direction,
				scale: Vector2.One*width/texture.Height,
				layerDepth: .1f
			);
		}
	}
}

