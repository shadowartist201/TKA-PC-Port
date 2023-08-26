using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Helicopter
{
	public static class Camera
	{
		private static int effectIndex = 0;

		private static float alpha = 1f;

		private static float alphaMin = 0f;

		private static float alphaMax = 1f;

		private static float alphaRate = 1f;

		private static float timer;

		private static float strength = 0.5f;

		private static float theta;

		private static float thetaRate;

		private static Vector2 effectOffset;

		private static Vector2 effectOffsetRate;

		private static Vector2 effectOffsetMax;

		public static Effect[] effects = new Effect[5];

		private static bool flipping_;

		private static float flipDuration_;

		private static float flipTimer_;

		private static int numShakes;

		private static float timeBetweenShakes;

		private static float timeBetweenTimer;

		private static bool shaking;

		private static float shakeMagnitude;

		private static float shakeDuration;

		private static float shakeTimer;

		private static Vector2 shakeOffset;

		private static bool moving_ = false;

		private static Vector2 movingVelocity_ = new Vector2(0f, 0f);

		private static float movingBound_ = 10f;

		private static bool rotating_ = false;

		private static float rotationRate_ = 0f;

		private static float rotationMin_ = -(float)Math.PI / 60f;

		private static float rotationMax_ = (float)Math.PI / 60f;

		private static bool scaling_ = false;

		private static float scaleRate_ = 0f;

		private static float scaleMin_ = 1.1f;

		private static float scaleMax_ = 1.2f;

		private static bool coloring_ = false;

		private static float colorHue_ = 0f;

		private static float colorRate_ = 0f;

		private static float colorMin_ = 0f;

		private static float colorMax_ = 360f;

		private static Vector2 position_ = new Vector2(640f, 360f);

		private static float rotation_ = 0f;

		private static float scale_ = 1.1f;

		private static Color color_ = Color.White;

		private static SpriteEffects spriteEffect_;

		public static void Update(float dt)
		{
			Camera.timer += dt;
			Camera.alpha += Camera.alphaRate * dt;
			if (Camera.alpha < Camera.alphaMin)
			{
				Camera.alpha = Camera.alphaMin;
				Camera.alphaRate = 0f - Camera.alphaRate;
			}
			if (Camera.alpha > Camera.alphaMax)
			{
				Camera.alpha = Camera.alphaMax;
				Camera.alphaRate = 0f - Camera.alphaRate;
			}
			Camera.theta += Camera.thetaRate * dt;
			Camera.theta %= (float)Math.PI * 2f;
			Camera.effectOffset += Camera.effectOffsetRate * dt;
			Camera.effectOffset.X %= Camera.effectOffsetMax.X;
			Camera.effectOffset.Y %= Camera.effectOffsetMax.Y;
			if (Camera.numShakes > 0)
			{
				Camera.timeBetweenTimer += dt;
				if (Camera.timeBetweenTimer > Camera.timeBetweenShakes)
				{
					Camera.timeBetweenTimer -= Camera.timeBetweenShakes;
					Camera.DoShake(Camera.shakeMagnitude, Camera.shakeDuration);
					Camera.numShakes--;
				}
			}
			if (Camera.shaking)
			{
				Camera.shakeTimer += dt;
				if (Camera.shakeTimer >= Camera.shakeDuration)
				{
					Camera.shaking = false;
					Camera.shakeTimer = Camera.shakeDuration;
					Camera.position_ = new Vector2(640f, 360f);
				}
				float num = Camera.shakeTimer / Camera.shakeDuration;
				float num2 = Camera.shakeMagnitude * (1f - num * num);
				Camera.shakeOffset = new Vector2(Global.RandomBetween(-1f, 1f), Global.RandomBetween(-1f, 1f)) * num2;
				Camera.position_ += Camera.shakeOffset;
				if (!Camera.scaling_)
				{
					Camera.scale_ = 1f + num * 0.1f;
				}
			}
			if (Camera.moving_)
			{
				Camera.position_ += Camera.movingVelocity_ * dt;
				if (Camera.position_.X > 640f + Camera.movingBound_)
				{
					Camera.position_.X = 640f + Camera.movingBound_;
					Camera.movingVelocity_.X = 0f - Camera.movingVelocity_.X;
				}
				if (Camera.position_.X < 640f - Camera.movingBound_)
				{
					Camera.position_.X = 640f - Camera.movingBound_;
					Camera.movingVelocity_.X = 0f - Camera.movingVelocity_.X;
				}
			}
			if (Camera.rotating_)
			{
				Camera.rotation_ += Camera.rotationRate_ * dt;
				if (Camera.rotation_ > Camera.rotationMax_)
				{
					Camera.rotation_ = Camera.rotationMax_;
					Camera.rotationRate_ = 0f - Camera.rotationRate_;
				}
				if (Camera.rotation_ < Camera.rotationMin_)
				{
					Camera.rotation_ = Camera.rotationMin_;
					Camera.rotationRate_ = 0f - Camera.rotationRate_;
				}
			}
			if (Camera.scaling_)
			{
				Camera.scale_ += Camera.scaleRate_ * dt;
				if (Camera.scale_ > Camera.scaleMax_)
				{
					Camera.scale_ = Camera.scaleMax_;
					Camera.scaleRate_ = 0f - Camera.scaleRate_;
				}
				if (Camera.scale_ < Camera.scaleMin_)
				{
					Camera.scale_ = Camera.scaleMin_;
					Camera.scaleRate_ = 0f - Camera.scaleRate_;
				}
			}
			if (Camera.coloring_)
			{
				Camera.colorHue_ += Camera.colorRate_ * dt;
				if (Camera.colorHue_ < Camera.colorMin_)
				{
					Camera.colorHue_ = Camera.colorMin_;
					Camera.colorRate_ = 0f - Camera.colorRate_;
				}
				if (Camera.colorHue_ > Camera.colorMax_)
				{
					Camera.colorHue_ = Camera.colorMax_;
					Camera.colorRate_ = 0f - Camera.colorRate_;
				}
				Camera.color_ = Camera.GetColor(Camera.colorHue_);
			}
			if (!Camera.flipping_)
			{
				return;
			}
			Camera.flipTimer_ += dt;
			if (Camera.flipTimer_ > Camera.flipDuration_)
			{
				Camera.flipTimer_ -= Camera.flipDuration_;
				switch (Camera.spriteEffect_)
				{
				case SpriteEffects.None:
					Camera.spriteEffect_ = SpriteEffects.FlipHorizontally;
					break;
				case SpriteEffects.FlipHorizontally:
					Camera.spriteEffect_ = SpriteEffects.None;
					break;
				case SpriteEffects.FlipVertically:
					Camera.spriteEffect_ = SpriteEffects.None;
					break;
				}
			}
		}

		public static void Draw(SpriteBatch spriteBatch, RenderTarget2D renderTarget, GraphicsDeviceManager graphics, GraphicsDevice graphicsDevice)
		{
			switch (Camera.effectIndex)
			{
			case 0:
				//Camera.effects[Camera.effectIndex].Parameters["Offset"].SetValue(new Vector2((float)Math.Cos(Camera.theta), (float)Math.Sin(Camera.theta)));
				break;
			case 2:
				//Camera.effects[Camera.effectIndex].Parameters["Offset"].SetValue(Camera.effectOffset.X);
				break;
			case 3:
				//Camera.effects[Camera.effectIndex].Parameters["WaveDimensions"].SetValue(new Vector2(10f, 0.03f));
				//Camera.effects[Camera.effectIndex].Parameters["Timer"].SetValue(Camera.timer);
				break;
			case 4:
				//Camera.effects[Camera.effectIndex].Parameters["Timer"].SetValue(Camera.timer);
				//Camera.effects[Camera.effectIndex].Parameters["Strength"].SetValue(Camera.strength);
				break;
			}
			graphicsDevice.SetRenderTarget(null);
			graphicsDevice.Clear(Color.White);
			if (Camera.effectIndex == -1)
			{
				spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied);
				spriteBatch.Draw((Texture2D)renderTarget, Camera.position_, (Rectangle?)null, Camera.color_, Camera.rotation_, new Vector2(640f, 360f), Camera.scale_, Camera.spriteEffect_, 0f);
				spriteBatch.End();
			}
			else
			{
				spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, Camera.effects[Camera.effectIndex]);
				spriteBatch.Draw(renderTarget, Vector2.Zero, Color.White * Camera.alpha);
				spriteBatch.End();
			}
		}

		public static void Reset()
		{
			Camera.alpha = 1f;
			Camera.timer = 0f;
			Camera.strength = 0.5f;
			Camera.theta = 0f;
			Camera.effectOffset = Vector2.Zero;
			Camera.effectIndex = -1;
			Camera.shaking = false;
			Camera.numShakes = 0;
			Camera.moving_ = false;
			Camera.rotating_ = false;
			Camera.scaling_ = false;
			Camera.coloring_ = false;
			Camera.flipping_ = false;
			Camera.position_ = new Vector2(640f, 360f);
			Camera.rotation_ = 0f;
			Camera.scale_ = 1.1f;
			Camera.color_ = Color.White;
			Camera.spriteEffect_ = SpriteEffects.None;
		}

		public static void SetEffect(int newEffectIndex)
		{
			Camera.scale_ = 1f;
			switch (newEffectIndex)
			{
			case -1:
				Camera.effectIndex = -1;
				break;
			case 0:
				Camera.alpha = 0f;
				Camera.alphaMin = 0f;
				Camera.alphaMax = 0.15f;
				Camera.alphaRate = 1.65517235f;
				Camera.thetaRate = 0f;
				Camera.effectIndex = 0;
				break;
			case 1:
				Camera.effectIndex = 1;
				break;
			case 2:
				Camera.effectIndex = 4;
				Camera.strength = 0.01f;
				Camera.alpha = 1f;
				Camera.alphaMin = 1f;
				Camera.alphaMax = 1f;
				Camera.alphaRate = 0f;
				break;
			case 3:
				Camera.alpha = 0.6f;
				Camera.alphaMin = 0.6f;
				Camera.alphaMax = 0.6f;
				Camera.alphaRate = 0f;
				Camera.thetaRate = 18.2212372f;
				Camera.effectIndex = 0;
				break;
			case 4:
				Camera.effectIndex = 3;
				Camera.alpha = 0.5f;
				Camera.alphaMin = 0.5f;
				Camera.alphaMax = 0.5f;
				Camera.alphaRate = 0f;
				break;
			case 5:
				Camera.effectIndex = 2;
				Camera.effectOffset = Vector2.Zero;
				Camera.effectOffsetMax = Vector2.One;
				Camera.effectOffsetRate = new Vector2(0.3f, 0f);
				break;
			}
		}

		public static void GoCrazy(float duration)
		{
			Camera.DoRotating(duration);
			Camera.DoScaling(duration);
			Camera.DoColoring(duration);
			Camera.DoFlipping(duration);
		}

		public static void DoShake(float magnitude, float duration)
		{
			Camera.shaking = true;
			Camera.shakeMagnitude = magnitude;
			Camera.shakeDuration = duration;
			Camera.shakeTimer = 0f;
		}

		public static void DoShakes(int num, float timeBetween, float magnitude, float duration)
		{
			Camera.numShakes = num;
			Camera.timeBetweenShakes = timeBetween;
			Camera.timeBetweenTimer = 0f;
			Camera.DoShake(magnitude, duration);
			Camera.numShakes--;
		}

		public static void DoScaling(float duration)
		{
			Camera.scaling_ = true;
			Camera.scaleRate_ = 2f * (Camera.scaleMax_ - Camera.scaleMin_) / duration;
		}

		public static void StopScaling()
		{
			Camera.scaling_ = false;
			Camera.scale_ = 1.1f;
		}

		public static void DoRotating(float duration)
		{
			Camera.rotating_ = true;
			Camera.rotationRate_ = 2f * (Camera.rotationMax_ - Camera.rotationMin_) / duration;
			Camera.scale_ = 1.1f;
		}

		public static void StopRotating()
		{
			Camera.rotating_ = false;
			Camera.rotation_ = 0f;
		}

		public static void DoColoring(float duration)
		{
			Camera.coloring_ = true;
			Camera.colorRate_ = 360f / duration;
		}

		public static void DoFlipping(float duration)
		{
			Camera.flipping_ = true;
			Camera.flipDuration_ = duration * 8f;
			Camera.flipTimer_ = 0f;
		}

		public static void StopFlipping()
		{
			Camera.flipping_ = false;
			Camera.spriteEffect_ = SpriteEffects.None;
		}

		public static Color GetColor(float hue)
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
