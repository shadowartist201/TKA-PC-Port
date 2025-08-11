using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Helicopter.Core
{
	internal class TunnelEffects
	{
		private TunnelParticle[] particles_ = new TunnelParticle[246];

		private TunnelParticle[] particles2_ = new TunnelParticle[100];

		private int activeParticles2_;

		private int particleDensity_;

		private float minStartScale_;

		private float maxStartScale_;

		private float minLifetime_;

		private float maxLifetime_;

		private float scaleRate_;

		private float speed_;

		private float acceleration_;

		public TunnelEffects()
		{
			this.particleDensity_ = 3;
			this.minStartScale_ = 0.5f;
			this.maxStartScale_ = 0.8f;
			this.minLifetime_ = 0.3f;
			this.maxLifetime_ = 0.5f;
			this.scaleRate_ = 1.5f;
			this.speed_ = 21f;
			this.acceleration_ = 32f;
			for (int i = 0; i < this.particles_.Length; i++)
			{
				this.particles_[i] = new TunnelParticle();
			}
			for (int i = 0; i < this.particles2_.Length; i++)
			{
				this.particles2_[i] = new TunnelParticle();
			}
		}

		public void UpdateTunnel(float dt, Vector2[] vertices, float width, float height, float tunnelVelocity)
		{
			for (int i = 0; i < this.particles_.Length; i++)
			{
				this.particles_[i].Update(dt, tunnelVelocity);
			}
			for (int i = 0; i < 41; i++)
			{
				for (int j = i * 2 * this.particleDensity_; j < this.particleDensity_ + i * 2 * this.particleDensity_; j++)
				{
					if (!this.particles_[j].visible_)
					{
						float x = vertices[i].X + Global.RandomBetween(0f, width);
						float y = vertices[i].Y;
						float num = Global.RandomBetween(0f, (float)Math.PI * 2f);
						float num2 = (float)Math.Cos(num);
						float num3 = (float)Math.Sin(num);
						float lifetime = Global.RandomBetween(this.minLifetime_, this.maxLifetime_);
						float startScale = Global.RandomBetween(this.minStartScale_, this.maxStartScale_);
						this.particles_[j].Reset(new Vector2(x, y), new Vector2(this.speed_ * num2, this.speed_ * num3), new Vector2(this.acceleration_ * num2, this.acceleration_ * num3), startScale, this.scaleRate_, lifetime);
					}
				}
				for (int j = this.particleDensity_ + i * 2 * this.particleDensity_; j < 2 * this.particleDensity_ + i * 2 * this.particleDensity_; j++)
				{
					if (!this.particles_[j].visible_)
					{
						float x = vertices[i].X + Global.RandomBetween(0f, width);
						float y = vertices[i].Y + height;
						float num = Global.RandomBetween(0f, (float)Math.PI * 2f);
						float num2 = (float)Math.Cos(num);
						float num3 = (float)Math.Sin(num);
						float lifetime = Global.RandomBetween(this.minLifetime_, this.maxLifetime_);
						float startScale = Global.RandomBetween(this.minStartScale_, this.maxStartScale_);
						this.particles_[j].Reset(new Vector2(x, y), new Vector2(this.speed_ * num2, this.speed_ * num3), new Vector2(this.acceleration_ * num2, this.acceleration_ * num3), startScale, this.scaleRate_, lifetime);
					}
				}
			}
		}

		public void UpdateSymbols(float dt, Vector2[] symbolPositions, Vector2[] symbolVertices1, Vector2[] symbolVertices2, Vector2[] symbolVertices3, float tunnelVelocity)
		{
			this.activeParticles2_ = 0;
			for (int i = 0; i < this.particles2_.Length; i++)
			{
				this.particles2_[i].Update(dt, tunnelVelocity);
			}
			this.UpdateSymbolParticles(symbolPositions[0], symbolVertices1);
			this.UpdateSymbolParticles(symbolPositions[1], symbolVertices2);
			this.UpdateSymbolParticles(symbolPositions[2], symbolVertices3);
		}

		private void UpdateSymbolParticles(Vector2 symbolPosition, Vector2[] symbolVertices)
		{
			for (int i = 0; i < symbolVertices.Length; i++)
			{
				int num = (int)(Vector2.Distance(symbolVertices[i], symbolVertices[(i + 1) % symbolVertices.Length]) * (float)this.particleDensity_ / 32f * 3f / 4f);
				if (num == 0)
				{
					num = 1;
				}
				for (int j = 0; j < num; j++)
				{
					if (!this.particles2_[this.activeParticles2_].visible_)
					{
						Vector2 position = symbolPosition + Vector2.Lerp(symbolVertices[i], symbolVertices[(i + 1) % symbolVertices.Length], (float)j / (float)num);
						Vector2 velocity = Vector2.Normalize(symbolVertices[(i + 1) % symbolVertices.Length] - symbolVertices[i]) * this.speed_ * 1.5f;
						float lifetime = Global.RandomBetween(this.minLifetime_, this.maxLifetime_);
						float startScale = this.minStartScale_ + Global.RandomBetween(0f, (this.maxStartScale_ - this.minStartScale_) / 2f);
						this.particles2_[this.activeParticles2_].Reset(position, velocity, Vector2.Zero, startScale, this.scaleRate_ * 3f / 4f, lifetime);
					}
					this.activeParticles2_++;
				}
			}
		}

		public void Draw(SpriteBatch spriteBatch, Tunnel tunnel)
		{
			TunnelParticle[] array = this.particles_;
			foreach (TunnelParticle tunnelParticle in array)
			{
				if (tunnel.isNyanRainbow)
				{
                    tunnelParticle.color_ = ColorFromHSV(((tunnelParticle.position_.X + tunnel.rainbowOffset) % 1280) * tunnel.rainbowFactor, 1, 1);
                    tunnelParticle.DrawOverride(spriteBatch);
                }
				else
				{
                    tunnelParticle.color_ = Global.tunnelColor;
                    tunnelParticle.Draw(spriteBatch);
                }
			}
			for (int j = 0; j < this.activeParticles2_; j++)
			{
				if (tunnel.isNyanRainbow)
				{
                    this.particles2_[j].color_ = ColorFromHSV(((particles2_[j].position_.X + tunnel.rainbowOffset) % 1280) * tunnel.rainbowFactor, 1, 1);
					this.particles2_[j].DrawOverride(spriteBatch);
                }
				else
				{
					this.particles2_[j].color_ = Global.tunnelColor;
                    this.particles2_[j].Draw(spriteBatch);
                }
			}
		}

        public static Color ColorFromHSV(double hue, double saturation, double value)
        {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            value = value * 255;
            int v = Convert.ToInt32(value);
            int p = Convert.ToInt32(value * (1 - saturation));
            int q = Convert.ToInt32(value * (1 - f * saturation));
            int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

            if (hi == 0)
                return new Color(v, t, p, 255);
            else if (hi == 1)
                return new Color(q, v, p, 255);
            else if (hi == 2)
                return new Color(p, v, t, 255);
            else if (hi == 3)
                return new Color( p, q, v, 255);
            else if (hi == 4)
                return new Color(t, p, v, 255);
            else
                return new Color(v, p, q, 255);
        }
    }
}
