using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Helicopter.Core
{
	public class LyricEffect
	{
		private bool[] actives_ = new bool[3];

		private Vector2[] positions_ = new Vector2[3];

		private float[] lyricTimes_ = new float[3];

		private float[] lyricTimers_ = new float[3];

		private float[] rotations_ = new float[3];

		private float rotationRate_;

		private Rectangle[] lyricRectangles_ = new Rectangle[3]
		{
			new Rectangle(0, 0, 179, 153),
			new Rectangle(179, 0, 164, 153),
			new Rectangle(343, 0, 179, 153)
		};

		public void Update(float dt)
		{
			for (int i = 0; i < 3; i++)
			{
				this.rotations_[i] += this.rotationRate_ * dt;
				this.lyricTimers_[i] += dt;
				if (this.lyricTimers_[i] > this.lyricTimes_[i])
				{
					this.actives_[i] = false;
				}
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			for (int i = 0; i < 3; i++)
			{
				if (this.actives_[i])
				{
					spriteBatch.Draw(Global.feelWantTouch, this.positions_[i], (Rectangle?)this.lyricRectangles_[i], Color.White, this.rotations_[i], new Vector2(this.lyricRectangles_[i].Width / 2, this.lyricRectangles_[i].Height / 2), (this.lyricTimes_[i] - this.lyricTimers_[i]) / this.lyricTimes_[i], SpriteEffects.None, 0f);
				}
			}
		}

		public void TurnOn(int lyricIndex)
		{
			this.actives_[lyricIndex] = true;
			this.lyricTimers_[lyricIndex] = 0f;
			this.lyricTimes_[lyricIndex] = 2f;
			ref Vector2 reference = ref this.positions_[lyricIndex];
			reference = new Vector2(213 + lyricIndex * 426, 360f);
			this.rotations_[lyricIndex] = 0f;
			this.rotationRate_ = (float)Math.PI * 4f / this.lyricTimes_[lyricIndex];
		}

		public void TurnOff()
		{
			for (int i = 0; i < 3; i++)
			{
				this.actives_[i] = false;
			}
		}
	}
}
