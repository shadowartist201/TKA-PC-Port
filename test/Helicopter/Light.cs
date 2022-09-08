using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Helicopter
{
	public class Light
	{
		private bool active;

		private Vector2 position;

		private Vector2 velocity;

		private bool smoothRotation = true;

		private float rotation;

		private float rotationVelocity;

		private float rotationUpper;

		private float rotationLower;

		private float[] rotationList;

		private int rotationIndex;

		private float eventTimer_;

		private int currEvent_;

		private float[] eventTimes_;

		private bool smoothHue = true;

		private float hue;

		private float hueChange;

		private float hueUpper;

		private float hueLower;

		private float[] hueList;

		private int hueIndex;

		private float hueDt;

		private float HDt;

		public Light(Vector2 _position, Vector2 _velocity)
		{
			this.position = _position;
			this.velocity = _velocity;
		}

		public void Update(float dt)
		{
			if (!this.active)
			{
				return;
			}
			this.HDt += dt;
			this.eventTimer_ += dt;
			this.position += this.velocity * dt;
			if (this.position.X < 150f || this.position.X > 1130f)
			{
				this.velocity.X = 0f - this.velocity.X;
				this.position.X += this.velocity.X * dt;
			}
			if (this.position.Y < 150f || this.position.Y > 570f)
			{
				this.velocity.Y = 0f - this.velocity.Y;
				this.position.Y += this.velocity.Y * dt;
			}
			if (this.smoothRotation)
			{
				this.rotation += this.rotationVelocity * dt;
				if (this.rotation > this.rotationUpper)
				{
					this.rotation = this.rotationUpper;
					this.rotationVelocity = 0f - this.rotationVelocity;
				}
				if (this.rotation < this.rotationLower)
				{
					this.rotation = this.rotationLower;
					this.rotationVelocity = 0f - this.rotationVelocity;
				}
			}
			else if (this.eventTimes_.Length == 1 && this.eventTimer_ > this.eventTimes_[this.currEvent_])
			{
				this.rotationIndex = (this.rotationIndex + 1) % this.rotationList.Length;
				this.rotation = this.rotationList[this.rotationIndex];
				this.eventTimer_ = 0f;
			}
			else if (this.currEvent_ < this.eventTimes_.Length && this.eventTimer_ > this.eventTimes_[this.currEvent_])
			{
				this.rotationIndex = (this.rotationIndex + 1) % this.rotationList.Length;
				this.rotation = this.rotationList[this.rotationIndex];
				this.hueIndex = (this.hueIndex + 1) % this.hueList.Length;
				this.hue = this.hueList[this.hueIndex];
				this.currEvent_++;
			}
			if (this.smoothHue)
			{
				this.hue += this.hueChange * dt;
				if (this.hue > this.hueUpper)
				{
					this.hue = this.hueUpper;
					this.hueChange = 0f - this.hueChange;
				}
				if (this.hue < this.hueLower)
				{
					this.hue = this.hueLower;
					this.hueChange = 0f - this.hueChange;
				}
			}
			else if (this.HDt > this.hueDt)
			{
				this.HDt = 0f;
				this.hueIndex = (this.hueIndex + 1) % this.hueList.Length;
				this.hue = this.hueList[this.hueIndex];
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			if (this.active)
			{
				spriteBatch.Draw(Global.lightEffect, this.position, (Rectangle?)null, Light.GetColor(this.hue), this.rotation, new Vector2(187f, 66f), 1f, SpriteEffects.None, 0f);
			}
		}

		public void ResetPosition(Vector2 _position, Vector2 _velocity)
		{
			this.position = _position;
			this.velocity = _velocity;
		}

		public void ResetVelocity(Vector2 _velocity)
		{
			this.velocity = _velocity;
		}

		private void Reset(float _rotation, float _hue)
		{
			this.rotation = _rotation;
			this.hue = _hue;
			this.rotationIndex = 0;
			this.currEvent_ = 0;
			this.hueIndex = 0;
			this.eventTimer_ = 0f;
			this.HDt = 0f;
		}

		public void TurnOn()
		{
			this.active = true;
		}

		public void TurnOff()
		{
			this.active = false;
		}

		public void SetRotationBehavior(bool _smoothRotation, float[] _rotationList, float[] _eventTimes, float _rotationVelocity, float _rotationLower, float _rotationUpper)
		{
			this.smoothRotation = _smoothRotation;
			if (_smoothRotation)
			{
				this.Reset(_rotationLower, this.hue);
				this.rotationVelocity = _rotationVelocity;
				this.rotationUpper = _rotationUpper;
				this.rotationLower = _rotationLower;
			}
			else
			{
				this.Reset(_rotationList[0], this.hue);
				this.rotationList = new float[_rotationList.Length];
				this.rotationList = _rotationList;
				this.eventTimes_ = _eventTimes;
			}
		}

		public void SetHueBehavior(bool _smoothHue, float[] _hueList, float _hueDt, float _hueChange, float _hueLower, float _hueUpper)
		{
			this.smoothHue = _smoothHue;
			if (_smoothHue)
			{
				this.Reset(this.rotation, _hueLower);
				this.hueChange = _hueChange;
				this.hueUpper = _hueUpper;
				this.hueLower = _hueLower;
			}
			else
			{
				this.Reset(this.rotation, _hueList[0]);
				this.hueList = new float[_hueList.Length];
				this.hueList = _hueList;
				this.hueDt = _hueDt;
			}
		}

		public void ContinueLights(float[] _eventTimes)
		{
			this.eventTimes_ = _eventTimes;
			this.currEvent_ = 0;
			this.eventTimer_ = 0f;
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
