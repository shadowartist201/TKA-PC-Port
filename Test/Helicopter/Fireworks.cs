using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Helicopter
{
	public class Fireworks
	{
		private bool showActive;

		private FireworkEffect[] fireworkEffects;

		public Fireworks(int _num)
		{
			this.fireworkEffects = new FireworkEffect[_num];
			for (int i = 0; i < _num; i++)
			{
				this.fireworkEffects[i] = new FireworkEffect(1);
				this.fireworkEffects[i].Initialize();
			}
		}

		public void Update(float dt)
		{
			if (this.showActive)
			{
				for (int i = 0; i < this.fireworkEffects.Length; i++)
				{
					if (this.fireworkEffects[i].Free)
					{
						this.fireworkEffects[i].AddParticles(new Vector2(Global.RandomBetween(0f, 1280f), Global.RandomBetween(0f, 300f)));
					}
				}
			}
			FireworkEffect[] array = this.fireworkEffects;
			foreach (FireworkEffect fireworkEffect in array)
			{
				if (!fireworkEffect.Free)
				{
					fireworkEffect.Update(dt);
				}
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			FireworkEffect[] array = this.fireworkEffects;
			foreach (FireworkEffect fireworkEffect in array)
			{
				if (!fireworkEffect.Free)
				{
					fireworkEffect.Draw(spriteBatch);
				}
			}
		}

		public void SetOff(Vector2 _position)
		{
			for (int i = 0; i < this.fireworkEffects.Length; i++)
			{
				if (this.fireworkEffects[i].Free)
				{
					this.fireworkEffects[i].AddParticles(_position);
					break;
				}
			}
		}

		public void TurnOn()
		{
			this.showActive = true;
		}

		public void TurnOff()
		{
			this.showActive = false;
		}
	}
}
