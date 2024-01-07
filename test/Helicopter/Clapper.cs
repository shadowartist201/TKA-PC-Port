using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Helicopter
{
    internal class Clapper : AnimatedSpriteA
    {
        private bool visible_ = false;

        private Vector2 position_ = Vector2.Zero;

        private Color color_;

        private float[] frameTimes = { 0.545882f, 0.08f, 0.08f, 0.08f, 0.08f, 0.545882f };

        private int numFrames = 6;

        private SpriteEffects spriteEffects_ = SpriteEffects.None;

        public Clapper() : base(Global.nyanHands)
        {
            base.Clapper_SetAnimation(new Rectangle(0, 0, 170, 247), numFrames, frameTimes);
        }

        public new void Update(float dt)
        {
            if (this.visible_)
            {
                base.Clapper_Update(dt);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (this.visible_)
            {
                base.Draw(spriteBatch, this.position_, 0f, 1f, this.color_, this.spriteEffects_);
            }
        }

        public void TurnOn(Vector2 position, int state, Color color)
        {
            this.visible_ = true;
            this.position_ = position;
            base.currentFrame = state;
            this.color_ = color;
            this.spriteEffects_ = SpriteEffects.None;
        }

        public void TurnOff()
        {
            this.visible_ = false;
        }
    }
}
