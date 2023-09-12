using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Helicopter
{
	internal class HeartsManager
	{
		private bool active_;

		private Vector2[] heartPositions_ = new Vector2[20];

		private float[] heartRotations_ = new float[20];

		private Vector2[] heartVelocities_ = new Vector2[20];

		private float[] heartRotationRates_ = new float[20]
		{
			2f, 3f, 4f, 5f, 6f, 7f, 2f, 3f, 4f, 5f,
			6f, 7f, 2f, 3f, 4f, 5f, 6f, 7f, 4f, 6f
		};

		private int[] heartIndexes_ = new int[20]
		{
			0, 1, 2, 3, 4, 5, 0, 1, 2, 3,
			4, 5, 0, 1, 2, 3, 4, 5, 0, 1
		};

		private Rectangle[] heartRects_ = new Rectangle[6]
		{
			new Rectangle(0, 0, 48, 48),
			new Rectangle(48, 0, 48, 48),
			new Rectangle(96, 0, 48, 48),
			new Rectangle(144, 0, 48, 48),
			new Rectangle(192, 0, 48, 48),
			new Rectangle(240, 0, 48, 48)
		};

		public void Update(float dt)
		{
			if (!this.active_)
			{
				return;
			}
			for (int i = 0; i < this.heartPositions_.Length; i++)
			{
				this.heartPositions_[i] += this.heartVelocities_[i] * dt;
				this.heartRotations_[i] += this.heartRotationRates_[i] * dt;
				if (this.heartPositions_[i].X > 1280f)
				{
					this.ResetHeart(i);
				}
			}
		}

		private void ResetHeart(int index)
		{
			float num = Global.RandomBetween(500f, 1000f);
			float y = Global.RandomBetween(-9f / 32f * num, 9f / 32f * num);
			ref Vector2 reference = ref this.heartVelocities_[index];
			reference = new Vector2(num, y);
			this.heartPositions_[index].X = 0f;
			this.heartPositions_[index].Y = 360f;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			if (this.active_)
			{
				for (int i = 0; i < this.heartPositions_.Length; i++)
				{
					spriteBatch.Draw(Global.heartsTex, this.heartPositions_[i], (Rectangle?)this.heartRects_[this.heartIndexes_[i]], Color.White, this.heartRotations_[i], new Vector2(this.heartRects_[this.heartIndexes_[i]].Width / 2, this.heartRects_[this.heartIndexes_[i]].Height / 2), 1f, SpriteEffects.None, 0f);
				}
			}
		}

		public void TurnOn()
		{
			this.active_ = true;
			for (int i = 0; i < this.heartPositions_.Length; i++)
			{
				this.ResetHeart(i);
			}
		}

		public void TurnOff()
		{
			this.active_ = false;
		}
	}
}
