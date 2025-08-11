using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Helicopter.Core
{
	public class Laser
	{
		private bool active;

		private Vector2 position;

		private Vector2 dimensions = new Vector2(Vector2.Distance(Vector2.Zero, new Vector2(1280f, 720f)), 2f);

		private float[] rotations = new float[5];

		private float[] rotationPrimer = new float[5] { -2f, -1f, 0f, 1f, 2f };

		private float[] rotationSpeeds = new float[5];

		private float RDt;

		private Color color;

		public Laser(Vector2 _position, Color _color)
		{
			this.position = _position;
			this.color = _color;
			this.Reset();
		}

		public void Update(float dt)
		{
			if (!this.active)
			{
				return;
			}
			this.RDt += dt;
			if (this.RDt > Global.BPM)
			{
				this.Reset();
				return;
			}
			for (int i = 0; i < this.rotations.Length; i++)
			{
				this.rotations[i] += this.rotationSpeeds[i] * dt;
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			if (this.active)
			{
				float[] array = this.rotations;
				foreach (float rotation in array)
				{
					this.DrawLine(spriteBatch, rotation);
				}
			}
		}

		private void DrawLine(SpriteBatch spriteBatch, float rotation)
		{
			spriteBatch.Draw(Global.pixel, new Rectangle((int)this.position.X, (int)this.position.Y, (int)this.dimensions.X, (int)this.dimensions.Y), (Rectangle?)null, this.color, rotation, Vector2.Zero, SpriteEffects.None, 0f);
		}

		public void Set(Vector2 _position, Color _color)
		{
			this.position = _position;
			this.color = _color;
		}

		public void TurnOn()
		{
			this.active = true;
		}

		public void TurnOff()
		{
			this.active = false;
		}

		private void Reset()
		{
			float num = Global.RandomBetween(0.1f, 0.9f) * (float)Math.PI;
			float num2 = Global.RandomBetween(0.1f, 0.3f) * (float)(Global.Random.Next(2) * 2 - 1);
			for (int i = 0; i < this.rotations.Length; i++)
			{
				this.rotations[i] = num;
				this.rotationSpeeds[i] = num2 * this.rotationPrimer[i];
			}
			this.RDt = 0f;
		}
	}
}
