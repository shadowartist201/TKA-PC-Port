using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Helicopter
{
	public class Foot
	{
		private bool active;

		private Vector2 position = new Vector2(600f, -169f);

		private Vector2 velocity = new Vector2(0f, 200f);

		public void Update(float dt)
		{
			if (this.active)
			{
				this.position += this.velocity * dt;
				if (this.position.Y > 0f)
				{
					this.position.Y = 0f;
					this.velocity = -this.velocity;
				}
				if (this.position.Y < -302f)
				{
					this.position.Y = -302f;
					this.velocity = -this.velocity;
					this.TurnOff();
				}
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			if (this.active)
			{
				spriteBatch.Draw(Global.backgroundSpritesTexture, this.position, (Rectangle?)new Rectangle(592, 1000, 442, 302), Color.White);
			}
		}

		public void Reset()
		{
			this.active = true;
			this.position = new Vector2(419f, -302f);
			this.velocity = new Vector2(0f, 200f);
		}

		public void TurnOff()
		{
			this.active = false;
		}
	}
}
