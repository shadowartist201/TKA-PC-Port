using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Helicopter.Core
{
	internal class Dancer : AnimatedSpriteA
	{
		private bool visible_ = false;

		private Vector2 position_ = Vector2.Zero;

		private Vector2 velocity_ = new Vector2(400f, 0f);

		private Color color_;

		private SpriteEffects spriteEffects_ = SpriteEffects.None;

		public Dancer()
			: base(Global.pelvicTex)
		{
			base.SetAnimation(new Rectangle(0, 0, 178, 454), 2, 0.1724138f);
		}

		public new void Update(float dt)
		{
			if (this.visible_)
			{
				this.position_ += this.velocity_ * dt;
				if (Math.Sign(this.velocity_.X) == 1 && this.position_.X > (float)(1280 + base.frameInfo.Width / 2))
				{
					this.position_.X -= 1280 + base.frameInfo.Width;
				}
				if (Math.Sign(this.velocity_.X) == -1 && this.position_.X < (float)(-base.frameInfo.Width / 2))
				{
					this.position_.X += 1280 + base.frameInfo.Width;
				}
				base.Update(dt);
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
			this.velocity_ = new Vector2(400f, 0f);
			this.spriteEffects_ = SpriteEffects.None;
		}

		public void ReverseDirection()
		{
			this.velocity_ = new Vector2(-400f, 0f);
			this.spriteEffects_ = SpriteEffects.FlipHorizontally;
		}

		public void TurnOff()
		{
			this.visible_ = false;
		}
	}
}
