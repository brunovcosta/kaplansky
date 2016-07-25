using System;
using Microsoft.Xna.Framework.Graphics;
using ArenaMonoGame;
using Arena;
using Microsoft.Xna.Framework;

namespace Arena{
    public class BadBiter : EnemyBase{
		protected Texture2D tex;
		protected float rot;
		int frame;
		public BadBiter (Node parent,float x,float y):base(parent){
			frame = 0;
			tex = Program.mainGame.Content.Load<Texture2D> ("biter.png");
			position = new Vector2 (x, y);
			rot = 0;
		}
        public override void Update (GameTime gameTime){
            base.Update(gameTime);
			Player player = Program.mainGame.playNode.player;
			Vector2 playerPos = player.position;
			float dist  = (playerPos - position).Length ();
			rot = (float)(Math.Atan2 (playerPos.Y - position.Y, playerPos.X - position.X) + Math.Sin (Program.mainGame.time * (dist / 1000000)) / 4);
			if(dist < 2000)
				position += 1 * (playerPos - position) / dist;
			if (dist < 1000) 
				position += 5 * (playerPos - position) / dist;
			if (dist < 500) 
				position += 10 * (playerPos - position) / dist;
			if (dist < 200) {
				player.energy -= 20;
				player.body.ApplyLinearImpulse (500000 * (playerPos - position) / dist);
				player.body.ApplyAngularImpulse ((float)(10 * Math.Sin (Program.mainGame.time / 50)));
				Die ();
			}
			frame = (gameTime.TotalGameTime.Milliseconds/100) % 4;
        }
		public override void Draw (SpriteBatch batch){
			Rectangle ret = new Rectangle ();
			ret.Width = tex.Width/4;
			ret.Height = tex.Height;
			ret.X = frame*ret.Width;
			ret.Y = 0;
			batch.Draw (tex, position: position, rotation: rot,
				origin: new Vector2 (ret.Width/2,ret.Height/2),
				effects: ((Math.Cos (rot) < 0) ? SpriteEffects.FlipVertically : SpriteEffects.None),
				sourceRectangle: ret
			);
		}
	}
}

