using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Helicopter
{
	public class MenuItem
	{
		private Texture2D texture_;

		public Rectangle texRect_;

		private Vector2 position_;

		private Vector2 origin_;

		private float rotation_;

		private float minRotation_ = -(float)Math.PI / 32f;

		private float maxRotation_ = (float)Math.PI / 32f;

		private float rotationRate_ = (float)Math.PI;

		public Rectangle CollisionRect => new Rectangle((int)(this.position_.X - this.origin_.X), (int)(this.position_.Y - this.origin_.Y), this.texRect_.Width, this.texRect_.Height);

		public MenuItem(Texture2D texture, Rectangle texRect, Vector2 position)
		{
			this.texture_ = texture;
			this.texRect_ = texRect;
			this.position_ = position;
			this.origin_ = new Vector2(texRect.Width / 2, texRect.Height / 2);
		}

		public void Update(float dt, bool selected)
		{
			if (selected)
			{
				this.rotation_ += this.rotationRate_ * dt;
				if (this.rotation_ > this.maxRotation_)
				{
					this.rotation_ = this.maxRotation_;
					this.rotationRate_ = 0f - this.rotationRate_;
				}
				if (this.rotation_ < this.minRotation_)
				{
					this.rotation_ = this.minRotation_;
					this.rotationRate_ = 0f - this.rotationRate_;
				}
			}
			else
			{
				this.rotation_ = 0f;
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(this.texture_, this.position_, (Rectangle?)this.texRect_, Color.White, this.rotation_, this.origin_, 1f, SpriteEffects.None, 0f);
		}

		public void SetTexRect(Rectangle texRect)
		{
			this.texRect_ = texRect;
			this.origin_ = new Vector2(texRect.Width / 2, texRect.Height / 2);
		}
	}
}
