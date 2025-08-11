using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Helicopter.Core
{
	public class ParticleEmitter
	{
		private bool active;

		public Vector2 currPosition;

		private Vector2 currVelocity;

		private float rotation;

		private float rotationVelocity = 3f;

		public ParticleSystem particleSystem;

		public ParticleEmitter(Vector2 _currPosition, Vector2 _currVelocity, int effects)
		{
			this.particleSystem = new StarEffect(effects);
			this.particleSystem.Initialize();
			this.currPosition = _currPosition;
			this.currVelocity = _currVelocity;
		}

		public void Update(float dt)
		{
			if (this.active)
			{
				this.currPosition += this.currVelocity * dt;
				this.rotation += this.rotationVelocity * dt;
				this.particleSystem.AddParticles(this.currPosition);
				this.particleSystem.Update(dt);
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			if (this.active)
			{
				this.particleSystem.Draw(spriteBatch);
				spriteBatch.Draw(Global.bigStar, this.currPosition, (Rectangle?)null, Color.White, this.rotation, new Vector2(25f, 25f), 0.5f, SpriteEffects.None, 0f);
			}
		}

		public void Reset(Vector2 _currPosition, Vector2 _currVelocity)
		{
			this.particleSystem.Reset();
			this.currPosition = _currPosition;
			this.currVelocity = _currVelocity;
		}

		public void TurnOn()
		{
			this.active = true;
		}

		public void TurnOff()
		{
			this.active = false;
		}
	}
}
