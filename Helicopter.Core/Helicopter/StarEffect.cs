namespace Helicopter.Core
{
	public class StarEffect : ParticleSystem
	{
		public StarEffect(int effects)
			: base(effects)
		{
		}

		protected override void InitializeConstants()
		{
			base.minInitialSpeed = 20f;
			base.maxInitialSpeed = 40f;
			base.minAcceleration = 0f;
			base.maxAcceleration = 10f;
			base.minLifetime = 0.25f;
			base.maxLifetime = 0.75f;
			base.minScale = 1f;
			base.maxScale = 1f;
			base.minNumParticles = 3;
			base.maxNumParticles = 5;
			base.minRotationSpeed = 0f;
			base.maxRotationSpeed = 0f;
		}
	}
}
