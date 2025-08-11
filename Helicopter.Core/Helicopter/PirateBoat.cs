using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Helicopter.Core
{
	public class PirateBoat
	{
		private bool active = false;

		private Vector2 position;

		private Vector2 velocity;

		private int currFrame;

		private int numFrames;

		private float frameTimer;

		private float frameTime;

		public void Update(float dt)
		{
			if (this.active)
			{
				this.position += this.velocity * dt;
				if (this.position.X > 1280f || this.position.X < -279f)
				{
					this.active = false;
				}
				this.frameTimer += dt;
				if (this.frameTimer > this.frameTime)
				{
					this.currFrame = (this.currFrame + 1) % this.numFrames;
					this.frameTimer = 0f;
				}
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			if (this.active)
			{
				spriteBatch.Draw(Global.backgroundSpritesTexture, this.position, (Rectangle?)new Rectangle(this.currFrame * 279, 2558, 279, 211), Color.White);
			}
		}

		public void Reset()
		{
			this.active = true;
			this.position = new Vector2(-279f, Global.RandomBetween(0f, 509f));
			this.velocity = new Vector2(Global.RandomBetween(500f, 600f), 0f);
			this.currFrame = 0;
			this.numFrames = 11;
			this.frameTimer = 0f;
			this.frameTime = 0.05f;
		}

		public void TurnOff()
		{
			this.active = false;
		}
	}
}
