using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Helicopter
{
	public class Vine
	{
		private bool active;

		private Vector2 position = new Vector2(0f, -90f);

		private float progress;

		private float progressRate = 0.5f;

		private float animTime;

		private float animTimer;

		private int animFrame;

		public void Update(float dt)
		{
			if (this.active)
			{
				this.progress += this.progressRate * dt;
				if (this.progress > 2f)
				{
					this.TurnOff();
				}
				this.animTimer += dt;
				if (this.animTimer > this.animTime)
				{
					this.animTimer = 0f;
					this.animFrame = (this.animFrame + 1) % 2;
				}
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			if (this.active)
			{
				if (this.progress < 1f)
				{
					spriteBatch.Draw(Global.backgroundSpritesTexture, this.position + new Vector2(0f, 810f - this.progress * 810f), (Rectangle?)new Rectangle(this.animFrame * 295, 1761 - (int)(this.progress * 810f), 295, (int)(this.progress * 810f)), Color.White);
				}
				else
				{
					spriteBatch.Draw(Global.backgroundSpritesTexture, this.position + new Vector2(0f, 810f - (2f - this.progress) * 810f), (Rectangle?)new Rectangle(this.animFrame * 295, 1761 - (int)((2f - this.progress) * 810f), 295, (int)((2f - this.progress) * 810f)), Color.White);
				}
			}
		}

		public void Reset()
		{
			this.progress = 0f;
			this.animTime = 0.1f;
			this.animTimer = 0f;
			this.animFrame = 0;
			this.active = true;
		}

		public void TurnOff()
		{
			this.active = false;
		}
	}
}
