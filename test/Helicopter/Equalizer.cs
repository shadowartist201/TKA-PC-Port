using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace Helicopter
{
	internal class Equalizer
	{
		private bool visible_;

		//private VisualizationData visualizationData_;

		private Vector2 position_ = new Vector2(120f, 600f);

		private float dWidth_;

		private float dHeight_;

		private float equalizerHeight_;

		private float[] freqHeights_ = new float[16];

		private float startingHue_;

		private float startingHueRate_ = 1080f;

		private float bpmTimer_;

		private bool rainbowed_ = false;

		public Equalizer()
		{
			this.dWidth_ = Global.equalizerBar.Width + 2;
			this.dHeight_ = Global.equalizerBar.Height + 2;
			this.equalizerHeight_ = (int)(576f / this.dHeight_);
			//this.visualizationData_ = new VisualizationData();
		}

		public void Update(float dt)
		{
			if (!this.visible_)
			{
				return;
			}
			//MediaPlayer.GetVisualizationData(this.visualizationData_);
			for (int i = 0; i < 16; i++)
			{
				this.freqHeights_[i] = 0f;
				for (int j = 0; j < 16; j++)
				{
					//this.freqHeights_[i] += this.visualizationData_.Frequencies[i * 16 + j];
				}
				this.freqHeights_[i] *= 0.0625f;
				this.freqHeights_[i] *= this.equalizerHeight_;
				this.freqHeights_[i] -= 10f;
				this.freqHeights_[i] *= 1.15f;
			}
			if (this.rainbowed_)
			{
				this.startingHue_ += this.startingHueRate_ * dt;
				if (this.startingHue_ < 0f)
				{
					this.startingHue_ = 0f;
					this.startingHueRate_ = 0f - this.startingHueRate_;
				}
				if (this.startingHue_ > 360f)
				{
					this.startingHue_ = 360f;
					this.startingHueRate_ = 0f - this.startingHueRate_;
				}
			}
			else
			{
				this.bpmTimer_ += dt;
				if (this.bpmTimer_ > 0.333333343f)
				{
					this.bpmTimer_ -= 0.333333343f;
					this.startingHue_ += 90f;
					this.startingHue_ %= 360f;
				}
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			if (!this.visible_)
			{
				return;
			}
			for (int i = 0; i < this.freqHeights_.Length; i++)
			{
				int num = (int)this.freqHeights_[i];
				for (int j = 0; j < num; j++)
				{
					if (this.rainbowed_)
					{
						spriteBatch.Draw(Global.equalizerBar, this.position_ + new Vector2(this.dWidth_ * (float)i, (0f - this.dHeight_) * (float)j), Equalizer.GetColor((this.startingHue_ + 22.5f * (float)i + (float)j / this.equalizerHeight_ * 360f) % 360f));
					}
					else
					{
						spriteBatch.Draw(Global.equalizerBar, this.position_ + new Vector2(this.dWidth_ * (float)i, (0f - this.dHeight_) * (float)j), Equalizer.GetColor((this.startingHue_ + 11.25f * (float)i + (float)j / this.equalizerHeight_ * 180f) % 360f));
					}
				}
			}
		}

		public void TurnOn(bool rainbowed)
		{
			this.visible_ = true;
			this.rainbowed_ = rainbowed;
			this.startingHue_ = 0f;
			this.bpmTimer_ = 0f;
		}

		public void TurnOff()
		{
			this.visible_ = false;
		}

		private static Color GetColor(float hue)
		{
			Vector3 one = Vector3.One;
			if (hue < 0f)
			{
				return new Color(new Vector4(Color.White.ToVector3(), 0f));
			}
			if (hue <= 60f)
			{
				one.X = 1f;
				one.Y = hue / 60f;
				one.Z = 0f;
			}
			else if (hue <= 120f)
			{
				one.Y = 1f;
				one.X = 2f - hue / 60f;
				one.Z = 0f;
			}
			else if (hue <= 180f)
			{
				one.Y = 1f;
				one.Z = hue / 60f - 2f;
				one.X = 0f;
			}
			else if (hue <= 240f)
			{
				one.Z = 1f;
				one.Y = 4f - hue / 60f;
				one.X = 0f;
			}
			else if (hue <= 300f)
			{
				one.Z = 1f;
				one.X = hue / 60f - 4f;
				one.Y = 0f;
			}
			else
			{
				one.X = 1f;
				one.Z = 6f - hue / 60f;
				one.Y = 0f;
			}
			return new Color(one);
		}
	}
}
