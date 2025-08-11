using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Helicopter.Core
{
	internal class ShineEffectManager
	{
		private bool active1_;

		private bool active2_;

		private bool active3_;

		private bool active4_;

		private ShineEffect[] shineEffects = new ShineEffect[20];

		private float eventTimer_;

		private int currEvent_;

		private float[] eventTimes_ = new float[17];

		private float[] eventTimes2_ = new float[5];

		private Vector2 emitterPosition_;

		private Vector2 emitterPositionOffset_;

		private float emitter2Angle_;

		private float emitter2Radius_;

		private float angleOffset_;

		private float radiusOffset_;

		private float minRadius_;

		private float maxRadius_;

		private float emitter3Timer_;

		private bool directUpdwards_;

		private float minStartScale_;

		private float maxStartScale_;

		private float minScaleRate_;

		private float maxScaleRate_;

		private float minLifetime_;

		private float maxLifetime_;

		public ShineEffectManager()
		{
			for (int i = 0; i < this.shineEffects.Length; i++)
			{
				this.shineEffects[i] = new ShineEffect();
			}
			this.emitterPositionOffset_ = new Vector2(106f, 106f);
			this.emitter2Angle_ = 0f;
			this.emitter2Radius_ = 100f;
			this.angleOffset_ = (float)Math.PI / 4f;
			this.radiusOffset_ = 30f;
			this.minRadius_ = 100f;
			this.maxRadius_ = 288f;
			this.minStartScale_ = 0f;
			this.maxStartScale_ = 0f;
			this.minScaleRate_ = 1f;
			this.maxScaleRate_ = 1f;
			this.minLifetime_ = 1f;
			this.maxLifetime_ = 1f;
			this.eventTimes_[0] = 0f;
			this.eventTimes_[1] = 0.481f;
			this.eventTimes_[2] = 0.481f;
			this.eventTimes_[3] = 1.166f;
			this.eventTimes_[4] = 1.383f;
			this.eventTimes_[5] = 1.808f;
			this.eventTimes_[6] = 2.148f;
			this.eventTimes_[7] = 2.148f;
			this.eventTimes_[8] = 2.611f;
			this.eventTimes_[9] = 2.97f;
			this.eventTimes_[10] = 3.461f;
			this.eventTimes_[11] = 3.461f;
			this.eventTimes_[12] = 3.876f;
			this.eventTimes_[13] = 4.216f;
			this.eventTimes_[14] = 4.585f;
			this.eventTimes_[15] = 5.033f;
			this.eventTimes_[16] = 5.033f;
			this.eventTimes2_[0] = 0f;
			this.eventTimes2_[1] = 0.335f;
			this.eventTimes2_[2] = 0.531f;
			this.eventTimes2_[3] = 0.939f;
			this.eventTimes2_[4] = 1.245f;
		}

		public void Update(float dt, Vector2 catPosition)
		{
			if (this.active1_ || this.active2_)
			{
				this.eventTimer_ += dt;
				if (this.eventTimer_ > this.eventTimes_[this.currEvent_])
				{
					if (this.active1_)
					{
						this.emitterPosition_ += this.emitterPositionOffset_;
						if (this.emitterPosition_.X < 128f || this.emitterPosition_.X > 1152f)
						{
							this.emitterPositionOffset_.X = 0f - this.emitterPositionOffset_.X;
							this.emitterPosition_.X += 2f * this.emitterPositionOffset_.X;
						}
						if (this.emitterPosition_.Y < 72f || this.emitterPosition_.Y > 648f)
						{
							this.emitterPositionOffset_.Y = 0f - this.emitterPositionOffset_.Y;
							this.emitterPosition_.Y += 2f * this.emitterPositionOffset_.Y;
						}
						this.EmmitShine(this.emitterPosition_, 1, Global.RandomColor());
					}
					if (this.active2_)
					{
						this.emitter2Angle_ += this.angleOffset_;
						this.emitter2Radius_ += this.radiusOffset_;
						if (this.emitter2Radius_ < this.minRadius_)
						{
							this.emitter2Radius_ = this.minRadius_;
							this.radiusOffset_ = 0f - this.radiusOffset_;
						}
						if (this.emitter2Radius_ > this.maxRadius_)
						{
							this.emitter2Radius_ = this.maxRadius_;
							this.radiusOffset_ = 0f - this.radiusOffset_;
						}
						Vector2 position = new Vector2(640f + this.emitter2Radius_ * (float)Math.Cos(this.emitter2Angle_), 360f + this.emitter2Radius_ * (float)Math.Sin(this.emitter2Angle_));
						this.EmmitShine(position, 2, Global.RandomColor());
					}
					this.currEvent_++;
					if (this.currEvent_ >= this.eventTimes_.Length)
					{
						this.active1_ = false;
						this.active2_ = false;
					}
				}
			}
			if (this.active3_)
			{
				this.emitter3Timer_ += dt;
				if (this.emitter3Timer_ > Global.BPM)
				{
					this.emitter2Angle_ += this.angleOffset_ / 2f;
					Color color = Global.RandomColor();
					this.EmmitShine(new Vector2(640f + this.emitter2Radius_ * (float)Math.Cos(this.emitter2Angle_), 360f + this.emitter2Radius_ * (float)Math.Sin(this.emitter2Angle_)), 1, color);
					this.EmmitShine(new Vector2(640f + this.emitter2Radius_ * (float)Math.Cos(this.emitter2Angle_ + (float)Math.PI), 360f + this.emitter2Radius_ * (float)Math.Sin(this.emitter2Angle_ + (float)Math.PI)), 1, color);
					this.emitter3Timer_ = 0f;
				}
			}
			if (this.active4_)
			{
				this.eventTimer_ += dt;
				if (this.eventTimer_ > this.eventTimes2_[this.currEvent_])
				{
					if (this.directUpdwards_)
					{
						this.emitterPosition_ += new Vector2(32f, -28.8f);
					}
					else
					{
						this.emitterPosition_ += new Vector2(32f, 28.8f);
					}
					Color color = Global.RandomColor();
					this.EmmitShine(this.emitterPosition_, 0, color);
					this.EmmitShine(new Vector2(1280f - this.emitterPosition_.X, this.emitterPosition_.Y), 0, color);
					this.EmmitShine(new Vector2(this.emitterPosition_.X, 720f - this.emitterPosition_.Y), 0, color);
					this.EmmitShine(new Vector2(1280f - this.emitterPosition_.X, 720f - this.emitterPosition_.Y), 0, color);
					this.currEvent_++;
					if (this.currEvent_ >= this.eventTimes2_.Length)
					{
						this.active4_ = false;
					}
				}
			}
			ShineEffect[] array = this.shineEffects;
			foreach (ShineEffect shineEffect in array)
			{
				shineEffect.Update(dt);
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			ShineEffect[] array = this.shineEffects;
			foreach (ShineEffect shineEffect in array)
			{
				shineEffect.Draw(spriteBatch);
			}
		}

		public void TurnOn1()
		{
			this.active1_ = true;
			this.currEvent_ = 0;
			this.eventTimer_ = 0f;
			this.emitterPosition_ = new Vector2(640f, 360f);
		}

		public void Continue1()
		{
			this.active1_ = true;
			this.currEvent_ = 0;
			this.eventTimer_ = 0f;
		}

		public void TurnOn2()
		{
			this.active2_ = true;
			this.currEvent_ = 0;
			this.eventTimer_ = 0f;
			this.emitter2Angle_ = 0f;
			this.emitter2Radius_ = this.minRadius_;
			this.radiusOffset_ = Math.Abs(this.radiusOffset_);
		}

		public void Continue2()
		{
			this.active2_ = true;
			this.currEvent_ = 0;
			this.eventTimer_ = 0f;
		}

		public void TurnOn3()
		{
			this.active3_ = true;
			this.emitter3Timer_ = 0f;
			this.emitter2Radius_ = 230f;
			this.emitter2Angle_ = 0f;
		}

		public void TurnOff3()
		{
			this.active3_ = false;
		}

		public void TurnOn4()
		{
			this.active4_ = true;
			this.currEvent_ = 0;
			this.eventTimer_ = 0f;
			this.directUpdwards_ = true;
			this.emitterPosition_ = new Vector2(0f, 720f);
		}

		public void Continue4(bool directUpwards)
		{
			this.active4_ = true;
			this.currEvent_ = 0;
			this.eventTimer_ = 0f;
			this.directUpdwards_ = directUpwards;
		}

		public void TurnOff4()
		{
			this.active4_ = false;
		}

		public void TurnOff()
		{
			this.active1_ = false;
			this.active2_ = false;
			this.active3_ = false;
			this.active4_ = false;
		}

		public void Reset()
		{
			this.active1_ = false;
			this.active2_ = false;
			this.active3_ = false;
			this.active4_ = false;
			ShineEffect[] array = this.shineEffects;
			foreach (ShineEffect shineEffect in array)
			{
				shineEffect.Reset();
			}
		}

		public void SetCircle(float circleRadius)
		{
			int num = 0;
			for (int i = 0; i < this.shineEffects.Length; i++)
			{
				if (!this.shineEffects[i].visible_)
				{
					Vector2 position = new Vector2(640f + circleRadius * (float)Math.Cos((float)num * (float)Math.PI / 4f), 360f + circleRadius * (float)Math.Sin((float)num * (float)Math.PI / 4f));
					this.shineEffects[i].TurnOn(position, 0f, 0.351741135f, 2.843f, Global.rainbowColors8[num], 0);
					num++;
				}
				if (num == 8)
				{
					break;
				}
			}
		}

		private void EmmitShine(Vector2 position, int textureIndex, Color color)
		{
			float startScale = Global.RandomBetween(this.minStartScale_, this.maxStartScale_);
			float scaleRate = Global.RandomBetween(this.minScaleRate_, this.maxScaleRate_);
			float lifetime = Global.RandomBetween(this.minLifetime_, this.maxLifetime_);
			for (int i = 0; i < this.shineEffects.Length; i++)
			{
				if (!this.shineEffects[i].visible_)
				{
					this.shineEffects[i].TurnOn(position, startScale, scaleRate, lifetime, color, textureIndex);
					break;
				}
			}
		}
	}
}
