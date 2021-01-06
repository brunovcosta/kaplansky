using System;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Common;
using ArenaMonoGame;
using Microsoft.Xna.Framework;
using FarseerPhysics.Collision;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Arena{
	public class PlayerBuilder{
		public float minDist=5;
		public List<Vertices> playerVertex;
		BuildNode node;
		public PlayerBuilder(BuildNode node){
			playerVertex = new List<Vertices> ();
			this.node = node;
		}
		public static Texture2D TextureFromPolygon(Texture2D tex,Vertices poly){
			AABB aabb = poly.GetAABB ();
			int width = (int)aabb.Width+1, height = (int)aabb.Height+1;
			Color[] pixels = new Color[width * height];
			Color[] texData=new Color[tex.Width*tex.Height];
			tex.GetData<Color> (texData);
			for (int i=0; i<height; i++) {
				for (int j=0; j<width; j++) {
					int pos = width * i + j;
					pixels [pos] = Geometry.PointInPolygon (new Vector2(j-width/2,i-height/2),poly)?
						texData[(i%tex.Width)*tex.Width+(j%tex.Height)]:
						Color.Transparent;
				}
			}
			Texture2D result = new Texture2D (Program.mainGame.GraphicsDevice,width, height);
			result.SetData (pixels);
			return result;
		}
		private float mouseVertexDistance(){
			Vertices lastVertex = playerVertex[playerVertex.Count-1];
			return (MouseInterface.MouseOnGrid (node.mouse, node.view.GRID) - lastVertex [lastVertex.Count - 1]).Length ();
		}
        public void MakePlayerVertices() {
            if (node.keyboard.IsKeyDown(Keys.Back) && playerVertex.Count > 0)
                playerVertex.RemoveAt(playerVertex.Count - 1);

            if (playerVertex.Count > 0) {
                Vertices lastVertex = playerVertex[playerVertex.Count-1];
                if (node.mouse.LeftButton == ButtonState.Pressed) {
                    if (lastVertex.Count == 0 || mouseVertexDistance()> minDist)
                        lastVertex.Add (MouseInterface.MouseOnGrid (node.mouse, node.view.GRID));
                    if (lastVertex.Count >= 3 && !Geometry.IsSimpleCurve (lastVertex))
                        lastVertex.RemoveAt (lastVertex.Count - 1);
                } else {
                    if (playerVertex.Count >= 3)
                        node.CraftFinish ();
                }
            }
		}
	}
}

