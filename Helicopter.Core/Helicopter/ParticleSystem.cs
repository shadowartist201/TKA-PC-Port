using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Helicopter.Core
{
	public abstract class ParticleSystem
	{
		public const int AlphaBlendDrawOrder = 100;

		public const int AdditiveDrawOrder = 200;

		private Vector2 origin;

		private int howManyEffects;

		private Particle[] particles;

		private Queue<Particle> freeParticles;

		public Vector2 worldVelocity;

		protected int minNumParticles;

		protected int maxNumParticles;

		protected float minInitialSpeed;

		protected float maxInitialSpeed;

		protected float minAcceleration;

		protected float maxAcceleration;

		protected float minRotationSpeed;

		protected float maxRotationSpeed;

		protected float minLifetime;

		protected float maxLifetime;

		protected float minScale;

		protected float maxScale;

		public bool Free => this.freeParticles.Count == this.particles.Length;

		protected ParticleSystem(int howManyEffects)
		{
			this.howManyEffects = howManyEffects;
		}

		public void Initialize()
		{
			this.InitializeConstants();
			this.origin.X = Global.stars[0].Width / 2;
			this.origin.Y = Global.stars[0].Height / 2;
			this.particles = new Particle[this.howManyEffects * this.maxNumParticles];
			this.freeParticles = new Queue<Particle>(this.howManyEffects * this.maxNumParticles);
			for (int i = 0; i < this.particles.Length; i++)
			{
				this.particles[i] = new Particle();
				this.freeParticles.Enqueue(this.particles[i]);
			}
		}

		protected abstract void InitializeConstants();

		public void AddParticles(Vector2 where)
		{
			int num = Global.Random.Next(this.minNumParticles, this.maxNumParticles);
			for (int i = 0; i < num; i++)
			{
				if (this.freeParticles.Count <= 0)
				{
					break;
				}
				Particle p = this.freeParticles.Dequeue();
				this.InitializeParticle(p, where);
			}
		}

		protected virtual void InitializeParticle(Particle p, Vector2 where)
		{
			Vector2 vector = Global.PickDirection(0f, (float)Math.PI * 2f);
			float num = Global.RandomBetween(this.minInitialSpeed, this.maxInitialSpeed);
			float num2 = Global.RandomBetween(this.minAcceleration, this.maxAcceleration);
			float lifetime = Global.RandomBetween(this.minLifetime, this.maxLifetime);
			float scale = Global.RandomBetween(this.minScale, this.maxScale);
			float rotationSpeed = Global.RandomBetween(this.minRotationSpeed, this.maxRotationSpeed);
			p.Initialize(this.worldVelocity, where, num * vector, num2 * vector, lifetime, scale, rotationSpeed, Global.stars.Length);
		}

		public void Update(float dt)
		{
			Particle[] array = this.particles;
			foreach (Particle particle in array)
			{
				if (particle.Active)
				{
					particle.Update(dt);
					if (!particle.Active)
					{
						this.freeParticles.Enqueue(particle);
					}
				}
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			Particle[] array = this.particles;
			foreach (Particle particle in array)
			{
				if (particle.Active)
				{
					float num = particle.TimeSinceStart / particle.Lifetime;
					float w = 4f * num * (1f - num);
					Color color = Color.FromNonPremultiplied(new Vector4(1f, 1f, 1f, w));
					float num2 = particle.Scale * (0.75f + 0.25f * num);
					spriteBatch.Draw(Global.stars[particle.texIndex], particle.Position, (Rectangle?)null, color, particle.Rotation, this.origin, num2, SpriteEffects.None, 0f);
				}
			}
		}

		public void Reset()
		{
			this.freeParticles.Clear();
			Particle[] array = this.particles;
			foreach (Particle item in array)
			{
				this.freeParticles.Enqueue(item);
			}
		}
	}
}
