using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Helicopter.Core
{
	public class ButterflyParticle
	{
		public bool active_;

		private bool attracted_;

		private Vector2 velocity_;

		private Vector2 position_;

		private float scale_;

		private float rotation_;

		private float rotationRate_;

		private int texIndex_;

		public void Reset(Vector2 position, Vector2 velocity, float scale, float rotationRate, bool attracted)
		{
			this.active_ = true;
			this.position_ = position;
			this.velocity_ = velocity;
			this.scale_ = scale;
			this.rotationRate_ = rotationRate;
			this.rotation_ = Global.RandomBetween(0f, (float)Math.PI * 2f);
			this.texIndex_ = Global.Random.Next(0, 6);
			this.attracted_ = attracted;
		}

		public void Update(float dt, Vector2 catPosition)
		{
			if (!this.active_)
			{
				return;
			}
			if (this.attracted_)
			{
				this.velocity_ = Vector2.Normalize(catPosition - this.position_) * 400f;
				this.position_ += this.velocity_ * dt;
				this.rotation_ += this.rotationRate_ * dt;
				if (Vector2.Distance(this.position_, catPosition) < 5f)
				{
					this.active_ = false;
				}
			}
			else
			{
				this.position_ += this.velocity_ * dt;
				this.rotation_ += this.rotationRate_ * dt;
				if (this.position_.X < 0f || this.position_.X > 1280f || this.position_.Y < 0f || this.position_.Y > 720f)
				{
					this.active_ = false;
				}
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			if (this.active_)
			{
				spriteBatch.Draw(Global.butterflyParticles, this.position_, (Rectangle?)new Rectangle(15 * this.texIndex_, 0, 15, 17), Color.White, this.rotation_, new Vector2(7.5f, 8.5f), this.scale_, SpriteEffects.None, 0f);
			}
		}

		public void TurnOff()
		{
			this.active_ = false;
		}
	}
}
