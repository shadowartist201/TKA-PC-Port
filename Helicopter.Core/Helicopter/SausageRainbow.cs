using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Helicopter.Core
{
	internal class SausageRainbow
	{
		private bool largeSausage;

		private bool active = false;

		private Vector2 position;

		private float progress;

		private float progressRate = 0.5f;

		public SausageRainbow(bool isLargeSausage)
		{
			this.largeSausage = isLargeSausage;
			if (this.largeSausage)
			{
				this.position = new Vector2(96.5f, 50f);
			}
			else
			{
				this.position = new Vector2(473.5f, 210f);
			}
		}

		public void Update(float dt)
		{
			if (this.active)
			{
				this.progress += this.progressRate * dt;
				if (this.progress > 2f)
				{
					this.TurnOff();
				}
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			if (!this.active)
			{
				return;
			}
			if (this.progress < 1f)
			{
				if (this.largeSausage)
				{
					spriteBatch.Draw(Global.backgroundSpritesTexture, this.position, (Rectangle?)new Rectangle(0, 330, (int)(this.progress * 1087f), 500), Color.White);
				}
				else
				{
					spriteBatch.Draw(Global.backgroundSpritesTexture, this.position, (Rectangle?)new Rectangle(1100, 330, (int)(this.progress * 333f), 210), Color.White);
				}
			}
			else if (this.largeSausage)
			{
				spriteBatch.Draw(Global.backgroundSpritesTexture, this.position + new Vector2((this.progress - 1f) * 1087f, 0f), (Rectangle?)new Rectangle((int)((this.progress - 1f) * 1087f), 330, (int)(1087f - (this.progress - 1f) * 1087f), 500), Color.White);
			}
			else
			{
				spriteBatch.Draw(Global.backgroundSpritesTexture, this.position + new Vector2((this.progress - 1f) * 333f, 0f), (Rectangle?)new Rectangle(1100 + (int)((this.progress - 1f) * 333f), 330, (int)(333f - (this.progress - 1f) * 333f), 210), Color.White);
			}
		}

		public void Reset()
		{
			this.progress = 0f;
			this.TurnOn();
		}

		public void TurnOn()
		{
			this.active = true;
		}

		public void TurnOff()
		{
			this.active = false;
		}
	}
}
