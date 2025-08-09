using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Helicopter
{
	public class TunnelParticle
	{
		public bool visible_;

		public Vector2 position_;

		private Vector2 velocity_;

		private Vector2 acceleration_;

		private Vector2 origin_;

		private float scale_;

		private float scaleRate_;

		private float lifetime_;

		private float timeSinceStart_;

		public Color color_;

		public TunnelParticle()
		{
			this.origin_ = new Vector2(16f, 15f);
			this.visible_ = false;
		}

		public void Update(float dt, float tunnelVelocity)
		{
			this.velocity_ += this.acceleration_ * dt;
			this.position_ += this.velocity_ * dt;
			this.position_.X -= tunnelVelocity * dt;
			this.scale_ -= this.scaleRate_ * dt;
			this.timeSinceStart_ += dt;
			if (this.timeSinceStart_ > this.lifetime_ || this.position_.X < 0f)
			{
				this.visible_ = false;
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
            spriteBatch.Draw(Global.tunnelStar, this.position_, (Rectangle?)null, Color.FromNonPremultiplied(new Vector4(Global.tunnelColor.ToVector3(), 0.75f)), 0f, this.origin_, this.scale_, SpriteEffects.None, 0f);
		}

		public void DrawOverride(SpriteBatch spriteBatch)
		{
            spriteBatch.Draw(Global.tunnelStar, this.position_, (Rectangle?)null, Color.FromNonPremultiplied(new Vector4(this.color_.ToVector3(), 0.75f)), 0f, this.origin_, this.scale_, SpriteEffects.None, 0f);
        }

		public void Reset(Vector2 position, Vector2 velocity, Vector2 acceleration, float startScale, float scaleRate, float lifetime)
		{
			this.position_ = position;
			this.velocity_ = velocity;
			this.acceleration_ = acceleration;
			this.scale_ = startScale;
			this.scaleRate_ = scaleRate;
			this.lifetime_ = lifetime;
			this.timeSinceStart_ = 0f;
			this.visible_ = true;
			this.color_ = Color.White;
		}
	}
}
