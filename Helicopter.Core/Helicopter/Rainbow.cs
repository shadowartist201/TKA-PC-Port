using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Helicopter.Core
{
	public class Rainbow
	{
		private bool active;

		private float progress;

		private float progressRate = 1f;

		public void Update(float dt)
		{
			if (this.active)
			{
				this.progress += this.progressRate * dt;
				if (this.progress > 1.2f)
				{
					this.progress = 0f;
					this.TurnOff();
				}
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			if (this.active)
			{
				spriteBatch.Draw(Global.rainbow, Vector2.Zero, (Rectangle?)new Rectangle(0, 0, (int)(this.progress * 1280f), 639), Color.FromNonPremultiplied(new Vector4(Color.White.ToVector3(), 1.2f - this.progress)));
			}
		}

		public void TurnOn()
		{
			this.active = true;
		}

		public void TurnOff()
		{
			this.active = false;
		}

		public void Reset()
		{
			this.active = false;
			this.progress = 0f;
		}
	}
}
