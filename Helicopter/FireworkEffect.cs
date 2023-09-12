using System;

namespace Helicopter
{
	public class FireworkEffect : ParticleSystem
	{
		public FireworkEffect(int effects)
			: base(effects)
		{
		}

		protected override void InitializeConstants()
		{
			base.minInitialSpeed = 0f;
			base.maxInitialSpeed = 120f;
			base.minAcceleration = 0f;
			base.maxAcceleration = 50f;
			base.minLifetime = 0.68182f;
			base.maxLifetime = 0.68182f;
			base.minScale = 1f;
			base.maxScale = 1.3f;
			base.minNumParticles = 8;
			base.maxNumParticles = 10;
			base.minRotationSpeed = 0f;
			base.maxRotationSpeed = (float)Math.PI;
		}
	}
}
