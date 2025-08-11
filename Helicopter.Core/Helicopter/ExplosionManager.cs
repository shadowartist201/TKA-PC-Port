using Microsoft.Xna.Framework.Graphics;

namespace Helicopter.Core
{
	internal class ExplosionManager
	{
		private bool on_ = false;

		private float explosionTimer_ = 0f;

		private int explosionCounter_ = 0;

		private Explosion[] explosions_ = new Explosion[20];

		public ExplosionManager()
		{
			for (int i = 0; i < this.explosions_.Length; i++)
			{
				this.explosions_[i] = new Explosion();
			}
		}

		public void Update(float dt)
		{
			Explosion[] array;
			if (this.on_)
			{
				this.explosionTimer_ += dt;
				if (this.explosionTimer_ > Global.BPM / 4f)
				{
					this.explosionCounter_++;
					this.explosionTimer_ -= Global.BPM / 4f;
					if (this.explosionCounter_ < 4)
					{
						int num = 0;
						array = this.explosions_;
						foreach (Explosion explosion in array)
						{
							if (!explosion.Visible)
							{
								explosion.Explode();
								num++;
								if (num > 6)
								{
									break;
								}
							}
						}
					}
					else if (this.explosionCounter_ == 8)
					{
						this.explosionCounter_ = 0;
						int num = 0;
						array = this.explosions_;
						foreach (Explosion explosion in array)
						{
							if (!explosion.Visible)
							{
								explosion.Explode();
								num++;
								if (num > 6)
								{
									break;
								}
							}
						}
					}
				}
			}
			array = this.explosions_;
			foreach (Explosion explosion in array)
			{
				explosion.Update(dt);
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			Explosion[] array = this.explosions_;
			foreach (Explosion explosion in array)
			{
				explosion.Draw(spriteBatch);
			}
		}

		public void TurnOn()
		{
			this.on_ = true;
			this.explosionTimer_ = 0f;
			this.explosionCounter_ = 0;
			int num = 0;
			Explosion[] array = this.explosions_;
			foreach (Explosion explosion in array)
			{
				if (!explosion.Visible)
				{
					explosion.Explode();
					num++;
					if (num > 6)
					{
						break;
					}
				}
			}
		}

		public void TurnOff()
		{
			this.on_ = false;
		}
	}
}
