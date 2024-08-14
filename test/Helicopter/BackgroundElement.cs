using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Helicopter
{
	internal class BackgroundElement
	{
		private Vector2 position_;

		private Texture2D texture_;

		private Vector2 texStartPosition_;

		private Vector2 texDimensions_;

		public Vector2 texPosition_;

		private Vector2 texVelocity_;

		private Vector2 texAcceleration_;

		private TexDirection texDirection_;

		private Vector2 winSize_ = new Vector2(1280f, 720f);

		public float TexVelocity => this.texVelocity_.Length();

		public void Update(float dt)
		{
			this.texVelocity_ += this.texAcceleration_ * dt;
			this.texPosition_ += this.texVelocity_ * dt;
			switch (this.texDirection_)
			{
			case TexDirection.RIGHT:
				if (this.texPosition_.X > this.texStartPosition_.X + this.texDimensions_.X)
				{
					this.texPosition_ = this.texStartPosition_;
				}
				break;
			case TexDirection.DOWN:
				if (this.texPosition_.Y < this.texStartPosition_.Y)
				{
					this.texPosition_ = this.texStartPosition_ + new Vector2(0f, this.texDimensions_.Y);
				}
				break;
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			switch (this.texDirection_)
			{
			case TexDirection.RIGHT:
				if (this.texPosition_.X > this.texStartPosition_.X + this.texDimensions_.X - this.winSize_.X)
				{
					int num2 = (int)(this.texStartPosition_.X + this.texDimensions_.X - this.texPosition_.X);
					spriteBatch.Draw(this.texture_, this.position_ + new Vector2(num2, 0f), (Rectangle?)new Rectangle((int)this.texStartPosition_.X, (int)this.texStartPosition_.Y, (int)this.winSize_.X - num2, (int)this.texDimensions_.Y), Color.White);
					spriteBatch.Draw(this.texture_, this.position_, (Rectangle?)new Rectangle((int)this.texPosition_.X, (int)this.texPosition_.Y, num2, (int)this.texDimensions_.Y), Color.White);
				}
				else
				{
					spriteBatch.Draw(this.texture_, this.position_, (Rectangle?)new Rectangle((int)this.texPosition_.X, (int)this.texPosition_.Y, (int)this.winSize_.X, (int)this.texDimensions_.Y), Color.White);
				}
				break;
			case TexDirection.DOWN:
				if (this.texPosition_.Y > this.texStartPosition_.Y + this.texDimensions_.Y - this.winSize_.Y)
				{
					int num = (int)(this.texStartPosition_.Y + this.texDimensions_.Y - this.texPosition_.Y);
					spriteBatch.Draw(this.texture_, this.position_ + new Vector2(0f, num), (Rectangle?)new Rectangle((int)this.texStartPosition_.X, (int)this.texStartPosition_.Y, (int)this.texDimensions_.X, (int)this.winSize_.Y - num), Color.White);
					spriteBatch.Draw(this.texture_, this.position_, (Rectangle?)new Rectangle((int)this.texPosition_.X, (int)this.texPosition_.Y, (int)this.texDimensions_.X, num), Color.White);
				}
				else
				{
					spriteBatch.Draw(this.texture_, this.position_, (Rectangle?)new Rectangle((int)this.texPosition_.X, (int)this.texPosition_.Y, (int)this.texDimensions_.X, (int)this.winSize_.Y), Color.White);
				}
				break;
			}
		}

		public void Reset(Texture2D texture, Vector2 texStartPosition, Vector2 texDimensions, Vector2 position, TexDirection texDirection, float texVelocity)
		{
			this.position_ = position;
			this.texture_ = texture;
			this.texStartPosition_ = texStartPosition;
			this.texDimensions_ = texDimensions;
			this.texPosition_ = texStartPosition;
			switch (texDirection)
			{
			case TexDirection.RIGHT:
				this.texVelocity_ = new Vector2(texVelocity, 0f);
				break;
			case TexDirection.DOWN:
				this.texVelocity_ = new Vector2(0f, texVelocity);
				break;
			}
			this.texAcceleration_ = Vector2.Zero;
			this.texDirection_ = texDirection;
		}

		public void SetAcceleration(float acceleration)
		{
			switch (this.texDirection_)
			{
			case TexDirection.RIGHT:
				this.texAcceleration_ = new Vector2(acceleration, 0f);
				break;
			case TexDirection.DOWN:
				this.texAcceleration_ = new Vector2(0f, acceleration);
				break;
			}
		}

		public void SetVelocity(float velocity)
		{
			switch (this.texDirection_)
			{
			case TexDirection.RIGHT:
				this.texVelocity_ = new Vector2(velocity, 0f);
				break;
			case TexDirection.DOWN:
				this.texVelocity_ = new Vector2(0f, velocity);
				break;
			}
		}
	}
}
