using System;
using Arena;

namespace Arena {
    public class EnemyBase : Node{
        float hp;
        public EnemyBase(Node parent):base(parent) {
        }
        public void Hurt(float demage){
            hp -= demage;
        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime) {
            base.Update(gameTime);
            if (hp < 0)
                Die();
        }
    }
}

