using System;
using Microsoft.Xna.Framework;
using FarseerPhysics.Common;
using System.Collections.Generic;

namespace Arena
{
	public class Geometry
	{
		public static float Distance(Vector2 v1,Vector2 v2){
			return (v1 - v2).Length ();
		}
		public static float Distance(float x1,float y1,float x2,float y2){
			return (float)Math.Sqrt ((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
		}
		public static bool IsSimpleCurve(Vertices vertex){
			if (vertex.Count < 3)
				return true;

			for (int i = 0; i < vertex.Count-1; ++i)
			{
				Vector2 a1 = vertex[i];
				Vector2 a2 = vertex.NextVertex(i);
				for (int j = i + 1; j < vertex.Count-1; ++j)
				{
					Vector2 b1 = vertex[j];
					Vector2 b2 = vertex.NextVertex(j);

					Vector2 temp;

					if (LineTools.LineIntersect2(ref a1, ref a2, ref b1, ref b2, out temp))
						return false;
				}
			}
			return true;
		}
		public static bool PointInPolygon(Vector2 point,Vertices vertex){
			int count=0;
			Vector2 horizontalLine = new Vector2 (8000, 0);
			for (int t = 0; t < vertex.Count; t++) {
				Vector2 p1 = vertex [t];
				Vector2 p2 = vertex.NextVertex (t);
				if (Geometry.LineIntersect (point, point + horizontalLine, p1, p2))
					count++;
			}
			return count % 2 == 1;
		}
		public static bool PointInPolygons(Vector2 point, List<Vertices> vertexes){
			foreach (Vertices v in vertexes) {
				if(PointInPolygon(point,v)){
					return true;
				}
			}
					return false;
		}
		public static bool LineIntersect(Vector2 p1,Vector2 p2,Vector2 p3,Vector2 p4){
			return MathUtils.Cross ((p2 - p1), (p3 - p1)) * MathUtils.Cross ((p2 - p1), (p4 - p1)) <= 0 &&
			MathUtils.Cross ((p4 - p3), (p1 - p3)) * MathUtils.Cross ((p4 - p3), (p2 - p3)) <= 0;
		}
        public static bool IsInCone(Vector2 pos1, float dir, float angle, Vector2 pos2){
            float len = 5000;
            Vector2 posA = pos1 + len * (new Vector2((float)Math.Cos(dir-angle/2), (float)Math.Sin(dir-angle/2)));
            Vector2 posB = pos1 + len * (new Vector2((float)Math.Cos(dir+angle/2), (float)Math.Sin(dir+angle/2)));
            Vertices v = new Vertices();
            v.Add(pos1);
            v.Add(posA);
            v.Add(posB);
            return PointInPolygon(pos2, v);
        }
	}
}

