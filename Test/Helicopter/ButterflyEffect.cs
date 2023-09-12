using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Helicopter
{
	public class ButterflyEffect
	{
		private bool active1_;

		private bool active2_;

		private ButterflyParticle[] butterflyParticles_ = new ButterflyParticle[160];

		private float eventTimer_;

		private int currEvent_;

		private float[] eventTimes_ = new float[13];

		private float emmitTimer_;

		private float emmitTime_;

		private float spamTimer_;

		private float spamDuration_ = 1.07657f;

		private float restDuration_ = 0.28f;

		private float emmitterRotation_;

		private float emmitterRotationRate_;

		public ButterflyEffect()
		{
			for (int i = 0; i < this.butterflyParticles_.Length; i++)
			{
				this.butterflyParticles_[i] = new ButterflyParticle();
			}
			this.eventTimes_[0] = 0f;
			this.eventTimes_[1] = 1.356f;
			this.eventTimes_[2] = 2.713f;
			this.eventTimes_[3] = 5.458f;
			this.eventTimes_[4] = 6.822f;
			this.eventTimes_[5] = 8.195f;
			this.eventTimes_[6] = 10.253f;
			this.eventTimes_[7] = 10.948f;
			this.eventTimes_[8] = 12.32f;
			this.eventTimes_[9] = 13.693f;
			this.eventTimes_[10] = 16.43f;
			this.eventTimes_[11] = 17.786f;
			this.eventTimes_[12] = 19.175f;
		}

		public void Update(float dt, Vector2 catPosition)
		{
			if (this.active1_)
			{
				this.eventTimer_ += dt;
				if (this.eventTimer_ > this.eventTimes_[this.currEvent_])
				{
					this.CreateButterflies();
					this.currEvent_++;
					if (this.currEvent_ >= this.eventTimes_.Length)
					{
						this.active1_ = false;
					}
				}
			}
			if (this.active2_)
			{
				this.spamTimer_ += dt;
				this.emmitterRotation_ += this.emmitterRotationRate_ * dt;
				if (this.spamTimer_ < this.spamDuration_)
				{
					this.emmitTimer_ += dt;
					if (this.emmitTimer_ > this.emmitTime_)
					{
						this.EmmitButterflyNotAttracted(catPosition, 400f * new Vector2((float)Math.Cos(this.emmitterRotation_), (float)Math.Sin(this.emmitterRotation_)));
						this.emmitTimer_ = 0f;
					}
				}
				else if (this.spamTimer_ > this.spamDuration_ + this.restDuration_)
				{
					this.spamTimer_ = 0f;
				}
			}
			for (int i = 0; i < this.butterflyParticles_.Length; i++)
			{
				this.butterflyParticles_[i].Update(dt, catPosition);
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			for (int i = 0; i < this.butterflyParticles_.Length; i++)
			{
				this.butterflyParticles_[i].Draw(spriteBatch);
			}
		}

		public void TurnOn1()
		{
			this.active1_ = true;
			this.eventTimer_ = 0f;
			this.currEvent_ = 0;
		}

		public void TurnOn2()
		{
			this.active2_ = true;
			this.spamTimer_ = 0f;
			this.emmitterRotation_ = 0f;
			this.emmitterRotationRate_ = (float)Math.PI * 2f;
			this.emmitTimer_ = 0f;
			this.emmitTime_ = 0.05f;
		}

		public void TurnOff2()
		{
			this.active2_ = false;
		}

		private void CreateButterflies()
		{
			for (int i = 0; i < 4; i++)
			{
				Vector2 vector;
				Vector2 vector2;
				int num;
				switch (i)
				{
				case 0:
					vector = Vector2.Zero;
					vector2 = new Vector2(1280f, 0f);
					num = 13;
					break;
				case 1:
					vector = new Vector2(1280f, 0f);
					vector2 = new Vector2(1280f, 720f);
					num = 7;
					break;
				case 2:
					vector = new Vector2(1280f, 720f);
					vector2 = new Vector2(0f, 720f);
					num = 13;
					break;
				default:
					vector = new Vector2(0f, 720f);
					vector2 = Vector2.Zero;
					num = 7;
					break;
				}
				float num2 = Vector2.Distance(vector, vector2) / (float)num;
				Vector2 vector3 = Vector2.Normalize(vector2 - vector);
				vector += vector3 * (num2 / 2f);
				for (int j = 0; j < num; j++)
				{
					Vector2 position = vector + vector3 * num2 * j;
					this.EmmitButterflyAttracted(position);
				}
			}
		}

		private void EmmitButterflyAttracted(Vector2 position)
		{
			for (int i = 0; i < this.butterflyParticles_.Length; i++)
			{
				if (!this.butterflyParticles_[i].active_)
				{
					this.butterflyParticles_[i].Reset(position, Vector2.Zero, 1f, 0f, attracted: true);
					break;
				}
			}
		}

		private void EmmitButterflyNotAttracted(Vector2 position, Vector2 velocity)
		{
			for (int i = 0; i < this.butterflyParticles_.Length; i++)
			{
				if (!this.butterflyParticles_[i].active_)
				{
					this.butterflyParticles_[i].Reset(position, velocity, 1f, 0f, attracted: false);
					break;
				}
			}
		}

		public void Reset()
		{
			for (int i = 0; i < this.butterflyParticles_.Length; i++)
			{
				this.butterflyParticles_[i].TurnOff();
			}
			this.eventTimer_ = 0f;
			this.currEvent_ = 0;
			this.spamTimer_ = 0f;
			this.active1_ = false;
			this.active2_ = false;
		}
	}
}
