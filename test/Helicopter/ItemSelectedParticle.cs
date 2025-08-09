using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Helicopter
{
	internal class ItemSelectedParticle
	{
		public bool visible_;

		private Vector2 position_;

		private Vector2 velocity_;

		private Vector2 acceleration_;

		private Vector2 origin_;

		private float scale_;

		private float scaleRate_;

		private float lifetime_;

		private float timeSinceStart_;

		private float rotation_;

		private float rotationRate_;

		private Color color_;

		private int colorIndex_;

		public ItemSelectedParticle(Color color)
		{
			this.origin_ = new Vector2(16f, 15f);
			this.color_ = color;
			this.visible_ = false;
			this.rotationRate_ = (float)Math.PI * 2f;
		}

		public void Update(float dt)
		{
			if (this.visible_)
			{
				this.velocity_ += this.acceleration_ * dt;
				this.position_ += this.velocity_ * dt;
				this.scale_ -= this.scaleRate_ * dt;
				this.rotation_ = (this.rotation_ + this.rotationRate_ * dt) % ((float)Math.PI * 2f);
				this.timeSinceStart_ += dt;
				if (this.timeSinceStart_ > this.lifetime_ || this.position_.X < 0f)
				{
					this.visible_ = false;
				}
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			if (this.visible_)
			{
				this.colorIndex_ = (this.colorIndex_ + 1) % 6;
				spriteBatch.Draw(Global.tunnelStar, this.position_, (Rectangle?)null, Color.FromNonPremultiplied(new Vector4(this.color_.ToVector3(), 0.9f)), this.rotation_, this.origin_, this.scale_, SpriteEffects.None, 0f);
			}
		}

		public void Reset(Vector2 position, Vector2 velocity, Vector2 acceleration, float startScale, float scaleRate, float lifetime)
		{
			this.position_ = position;
			this.velocity_ = velocity;
			this.acceleration_ = acceleration;
			this.scale_ = startScale;
			this.scaleRate_ = scaleRate;
			this.lifetime_ = startScale / scaleRate;
			this.timeSinceStart_ = 0f;
			this.visible_ = true;
		}
	}
}
