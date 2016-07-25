using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Arena;

namespace Arena{
	public class Paint{
		public static void DrawSquare(SpriteBatch batch,int x,int y,Color color,int size){
			batch.Draw (Shapes.Rectangle (size, size), (x - size / 2) * Vector2.UnitX + (y - size / 2)*Vector2.UnitY, color);
		}
		public static void DrawLineSegment(SpriteBatch spriteBatch,int x1,int y1, int x2,int y2, Color color, int lineWidth,float layerDepth=0){

			float angle = (float)Math.Atan2(y2 -y1, x2 -x1);
			float length = Geometry.Distance(x1,y1, x2,y2);

			spriteBatch.Draw(Shapes.Dot(), x1*Vector2.UnitX+y1*Vector2.UnitY, null, color,
				angle, Vector2.Zero, length*Vector2.UnitX+ lineWidth*Vector2.UnitY,
				SpriteEffects.None,layerDepth);
		}
		public static void DrawCircle(SpriteBatch batch,Vector2 center,float radius,Color color, int lineWidth){
			int step = 6;
			for(int t=0;t<360;t+=step){
				//DrawSquare (batch, center + radius * (new Vector2 ((float)Math.Cos (t * Math.PI / 180), (float)Math.Sin (t * Math.PI / 180))), color, 6);
				DrawLineSegment (batch,
					(int)(center.X + radius * Math.Cos (t*Math.PI/180)),
					(int)(center.Y + radius * Math.Sin (t*Math.PI/180)),
					(int)(center.X + radius * Math.Cos ((t + step) * Math.PI / 180)),
					(int)(center.Y + radius * Math.Sin ((t + step)*Math.PI/180)),
					color,lineWidth);
			}
		}
	}
}

