using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Helicopter
{
	internal class BigRainbow
	{
		private bool active = false;

		private Vector2 position = new Vector2(0f, 0f);

		private float progress;

		private float progressRate = 0.5f;

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
			if (this.active)
			{
				if (this.progress < 1f)
				{
					spriteBatch.Draw(Global.rainbow3, this.position, (Rectangle?)new Rectangle(0, 0, (int)(this.progress * 1280f), 489), Color.White);
				}
				else
				{
					spriteBatch.Draw(Global.rainbow3, this.position + new Vector2((this.progress - 1f) * 1280f, 0f), (Rectangle?)new Rectangle((int)((this.progress - 1f) * 1280f), 0, (int)(1280f - (this.progress - 1f) * 1280f), 489), Color.White);
				}
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
