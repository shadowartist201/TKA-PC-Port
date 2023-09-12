using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Helicopter
{
	public class FluctuationManager
	{
		private bool active_;

		private Vector2 position_;

		private Vector2 origin_;

		private Color color_;

		private float scale_;

		private float scaleRate_;

		private float startScale_;

		private float endScale_;

		private float startScaleRate_;

		private float endScaleRate_;

		private float BPMTimer;

		private float BPMTime;

		public FluctuationManager()
		{
			this.active_ = false;
		}

		public void Update(float dt, Vector2 catPosition)
		{
			if (!this.active_)
			{
				return;
			}
			this.position_ = catPosition;
			this.BPMTimer += dt;
			if (this.BPMTimer > this.BPMTime)
			{
				if (this.color_ == Color.White)
				{
					this.color_ = Color.Black;
				}
				else
				{
					this.color_ = Color.White;
				}
				this.BPMTimer -= this.BPMTime;
				this.scale_ = this.startScale_;
				this.scaleRate_ = (this.endScale_ - this.startScale_) / this.BPMTime;
			}
			this.scale_ += this.scaleRate_ * dt;
			this.startScale_ += this.startScaleRate_ * dt;
			this.endScale_ += this.endScaleRate_ * dt;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			if (this.active_)
			{
				spriteBatch.Draw(Global.fluctuationShape, this.position_, (Rectangle?)null, this.color_, 0f, this.origin_, this.scale_, SpriteEffects.None, 0f);
			}
		}

		public void TurnOn()
		{
			this.Reset();
			this.active_ = true;
		}

		public void TurnOff()
		{
			this.active_ = false;
		}

		public void Reset()
		{
			this.position_ = new Vector2(640f, 360f);
			this.origin_ = new Vector2(250f, 250f);
			this.color_ = Color.White;
			this.startScale_ = 0f;
			this.endScale_ = 0f;
			this.startScaleRate_ = 0f;
			this.endScaleRate_ = 2f * (1f - this.endScale_) / 11.352f;
			this.BPMTimer = 0f;
			this.BPMTime = Global.BPM / 2f;
			this.scale_ = this.startScale_;
			this.scaleRate_ = (this.endScale_ - this.startScale_) / this.BPMTime;
			this.active_ = false;
		}
	}
}
