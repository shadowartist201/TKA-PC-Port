using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Helicopter
{
	public class BRainbow
	{
		private bool active = false;

		private Vector2 position = new Vector2(0f, 403f);

		private float progress;

		private float progressRate = 1.7f;

		public void Update(float dt)
		{
			if (!this.active)
			{
				return;
			}
			this.progress += this.progressRate * dt;
			this.position -= new Vector2(Global.mountainVelocity * dt, 0f);
			if (this.progress > 1f)
			{
				this.position += new Vector2(200f, 0f);
				this.progress = 0f;
				if (this.position.X > 1580f)
				{
					this.position = new Vector2(0f, 403f);
					this.TurnOff();
				}
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			if (this.active)
			{
				spriteBatch.Draw(Global.rainbow2, this.position - new Vector2((1f - this.progress) * 200f, 0f), (Rectangle?)new Rectangle((int)(this.progress * 200f), 0, 200, 100), Color.White);
				spriteBatch.Draw(Global.rainbow2, this.position, (Rectangle?)new Rectangle(0, 0, (int)(this.progress * 200f), 100), Color.White);
			}
		}

		public void Reset()
		{
			this.position = new Vector2(0f, 403f);
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
