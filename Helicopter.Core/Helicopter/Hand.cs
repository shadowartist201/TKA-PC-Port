using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Helicopter.Core
{
	public class Hand
	{
		private bool active;

		private bool directedDownward;

		private Vector2 position;

		private Vector2 velocity;

		private float minPositionY;

		private float maxPositionY;

		private FireworkEffect fireworkEffect;

		public Hand()
		{
			this.fireworkEffect = new FireworkEffect(1);
			this.fireworkEffect.Initialize();
		}

		public void Update(float dt)
		{
			if (this.active)
			{
				this.position += this.velocity * dt;
				if (this.position.Y > this.maxPositionY)
				{
					this.position.Y = this.maxPositionY;
					this.velocity = -this.velocity;
					if (!this.directedDownward)
					{
						this.TurnOff();
					}
					else
					{
						this.fireworkEffect.AddParticles(this.position + new Vector2(70f, 169f));
					}
				}
				if (this.position.Y < this.minPositionY)
				{
					this.position.Y = this.minPositionY;
					this.velocity = -this.velocity;
					if (this.directedDownward)
					{
						this.TurnOff();
					}
				}
			}
			if (!this.fireworkEffect.Free)
			{
				this.fireworkEffect.Update(dt);
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			if (this.active)
			{
				if (this.directedDownward)
				{
					spriteBatch.Draw(Global.hand, this.position, Color.White);
				}
				else
				{
					spriteBatch.Draw(Global.reachingHand, this.position, Color.White);
				}
			}
			if (!this.fireworkEffect.Free)
			{
				this.fireworkEffect.Draw(spriteBatch);
			}
		}

		public void TurnOn(bool directedDownward_)
		{
			this.directedDownward = directedDownward_;
			if (this.directedDownward)
			{
				this.minPositionY = -169f;
				this.maxPositionY = 0f;
				this.velocity = new Vector2(0f, 200f);
				this.position = new Vector2(600f, -169f);
			}
			else
			{
				this.minPositionY = 414f;
				this.maxPositionY = 720f;
				this.velocity = new Vector2(0f, -500f);
				this.position = new Vector2(564f, 720f);
			}
			this.active = true;
		}

		public void TurnOff()
		{
			this.active = false;
		}

		public void Reset()
		{
			this.active = false;
			this.position = new Vector2(600f, -169f);
			this.velocity = new Vector2(0f, 200f);
		}
	}
}
