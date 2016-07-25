using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Arena {
    public class Node {
        public List<Node> childrens= new List<Node>();
        protected Node parent;

        public float imageAngle{ get; set; }

		public Vector2 position{ get; set; }

        public Texture2D imageIndex{ get; set; }

        public Node(Node parent) {
            this.parent = parent;
            parent.Add(this);
        }

        public void Add(Node newChild) {
            childrens.Remove(newChild);
            childrens.Add(newChild);

        }

        public Node() {
        }

        public virtual void Update(GameTime gameTime){}

        public void UpdateChildrens(GameTime gameTime) {
            for (int t = 0; t < childrens.Count; t++) {
                childrens[t].UpdateChildrens(gameTime);
                childrens[t].Update(gameTime);
            }
        }

        public virtual void Draw(SpriteBatch batch) {
        }

        public void DrawChildrens(SpriteBatch batch) {
            for (int t = 0; t < childrens.Count; t++) {
                childrens[t].Draw(batch);
                childrens[t].DrawChildrens(batch);
            }
        }

        public int Total() {
            int total = 1;
            for (int t = 0; t < childrens.Count; t++) {
                total += childrens[t].Total();
            }
            return  total;
        }

        public void Die() {
            parent.childrens.Remove(this);
        }
    }
}

