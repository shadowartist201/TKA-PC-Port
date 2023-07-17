using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Helicopter
{
	public class ItemSelectedEffect
	{
		private ItemSelectedParticle[] particles_ = new ItemSelectedParticle[300];

		private Vector2[] itemVertices_ = new Vector2[4];

		private int activeParticles_;

		private float particleDensity_;

		private float minStartScale_;

		private float maxStartScale_;

		private float minLifetime_;

		private float maxLifetime_;

		private float scaleRate_;

		private float speed_;

		private float acceleration_;

		public ItemSelectedEffect()
		{
			this.minStartScale_ = 0.4f;
			this.maxStartScale_ = 0.8f;
			this.minLifetime_ = 0.2f;
			this.maxLifetime_ = 0.5f;
			this.scaleRate_ = 1.5f;
			this.speed_ = 22f;
			this.acceleration_ = 32f;
			this.particleDensity_ = 10f / 153f;
			for (int i = 0; i < this.particles_.Length; i++)
			{
				switch (i % 6)
				{
				case 0:
					this.particles_[i] = new ItemSelectedParticle(new Color(255, 255, 128));
					break;
				case 1:
					this.particles_[i] = new ItemSelectedParticle(new Color(255, 255, 0));
					break;
				case 2:
					this.particles_[i] = new ItemSelectedParticle(new Color(255, 128, 255));
					break;
				case 3:
					this.particles_[i] = new ItemSelectedParticle(new Color(255, 0, 255));
					break;
				case 4:
					this.particles_[i] = new ItemSelectedParticle(new Color(128, 255, 255));
					break;
				case 5:
					this.particles_[i] = new ItemSelectedParticle(new Color(0, 255, 255));
					break;
				}
			}
		}

		public void Update(float dt)
		{
			for (int i = 0; i < this.particles_.Length; i++)
			{
				this.particles_[i].Update(dt);
			}
			this.activeParticles_ = 0;
			for (int i = 0; i < 4; i++)
			{
				int num = (int)(Vector2.Distance(this.itemVertices_[i], this.itemVertices_[(i + 1) % 4]) * this.particleDensity_);
				for (int j = 0; j < num; j++)
				{
					if (!this.particles_[this.activeParticles_].visible_)
					{
						Vector2 position = Vector2.Lerp(this.itemVertices_[i], this.itemVertices_[(i + 1) % 4], (float)j / (float)num);
						float num2 = Global.RandomBetween(0f, (float)Math.PI * 2f);
						Vector2 vector = new Vector2((float)Math.Cos(num2), (float)Math.Sin(num2));
						float lifetime = Global.RandomBetween(this.minLifetime_, this.maxLifetime_);
						float startScale = Global.RandomBetween(this.minStartScale_, this.maxStartScale_);
						this.particles_[this.activeParticles_].Reset(position, this.speed_ * vector, this.acceleration_ * vector, startScale, this.scaleRate_, lifetime);
					}
					this.activeParticles_++;
					if (this.activeParticles_ >= this.particles_.Length)
					{
						return;
					}
				}
			}
		}

		public void SetItemVertices(Rectangle itemRect)
		{
			ref Vector2 reference = ref this.itemVertices_[0];
			reference = new Vector2(itemRect.X, itemRect.Y);
			ref Vector2 reference2 = ref this.itemVertices_[1];
			reference2 = new Vector2(itemRect.X + itemRect.Width, itemRect.Y);
			ref Vector2 reference3 = ref this.itemVertices_[2];
			reference3 = new Vector2(itemRect.X + itemRect.Width, itemRect.Y + itemRect.Height);
			ref Vector2 reference4 = ref this.itemVertices_[3];
			reference4 = new Vector2(itemRect.X, itemRect.Y + itemRect.Height);
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			for (int i = 0; i < this.activeParticles_; i++)
			{
				this.particles_[i].Draw(spriteBatch);
			}
		}
	}
}
