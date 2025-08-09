using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Helicopter
{
	public class Eyes
	{
		private bool active;

		private Vector2 position;

		private float alpha;

		private float alphaRate;

		private int currFrame;

		private int numFrames;

		private float frameTimer;

		private float frameTime;

		public void Update(float dt)
		{
			if (this.active)
			{
				this.alpha += this.alphaRate * dt;
				if (this.alpha > 1f)
				{
					this.alpha = 1f;
					this.alphaRate = 0f - this.alphaRate;
				}
				if (this.alpha < 0f)
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
				spriteBatch.Draw(Global.searchingEyes, this.position, (Rectangle?)new Rectangle(281 * this.currFrame, 0, 281, 79), Color.FromNonPremultiplied(new Vector4(Color.White.ToVector3(), this.alpha)));
			}
		}

		public void TurnOn()
		{
			this.active = true;
			this.position = new Vector2(499.5f, 320.5f);
			this.alpha = 0f;
			this.alphaRate = 0.6f;
			this.frameTime = 0.075f;
			this.frameTimer = 0f;
			this.currFrame = 0;
			this.numFrames = 8;
		}

		public void TurnOff()
		{
			this.active = false;
		}
	}
}
