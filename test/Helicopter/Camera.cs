using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Helicopter;

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

	public static Effect[] effects = (Effect[])(object)new Effect[5];

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
																																																										timer += dt;
		alpha += alphaRate * dt;
		if (alpha < alphaMin)
		{
			alpha = alphaMin;
			alphaRate = 0f - alphaRate;
		}
		if (alpha > alphaMax)
		{
			alpha = alphaMax;
			alphaRate = 0f - alphaRate;
		}
		theta += thetaRate * dt;
		theta %= (float)Math.PI * 2f;
		effectOffset += effectOffsetRate * dt;
		effectOffset.X %= effectOffsetMax.X;
		effectOffset.Y %= effectOffsetMax.Y;
		if (numShakes > 0)
		{
			timeBetweenTimer += dt;
			if (timeBetweenTimer > timeBetweenShakes)
			{
				timeBetweenTimer -= timeBetweenShakes;
				DoShake(shakeMagnitude, shakeDuration);
				numShakes--;
			}
		}
		if (shaking)
		{
			shakeTimer += dt;
			if (shakeTimer >= shakeDuration)
			{
				shaking = false;
				shakeTimer = shakeDuration;
				position_ = new Vector2(640f, 360f);
			}
			float num = shakeTimer / shakeDuration;
			float num2 = shakeMagnitude * (1f - num * num);
			shakeOffset = new Vector2(Global.RandomBetween(-1f, 1f), Global.RandomBetween(-1f, 1f)) * num2;
			position_ += shakeOffset;
			if (!scaling_)
			{
				scale_ = 1f + num * 0.1f;
			}
		}
		if (moving_)
		{
			position_ += movingVelocity_ * dt;
			if (position_.X > 640f + movingBound_)
			{
				position_.X = 640f + movingBound_;
				movingVelocity_.X = 0f - movingVelocity_.X;
			}
			if (position_.X < 640f - movingBound_)
			{
				position_.X = 640f - movingBound_;
				movingVelocity_.X = 0f - movingVelocity_.X;
			}
		}
		if (rotating_)
		{
			rotation_ += rotationRate_ * dt;
			if (rotation_ > rotationMax_)
			{
				rotation_ = rotationMax_;
				rotationRate_ = 0f - rotationRate_;
			}
			if (rotation_ < rotationMin_)
			{
				rotation_ = rotationMin_;
				rotationRate_ = 0f - rotationRate_;
			}
		}
		if (scaling_)
		{
			scale_ += scaleRate_ * dt;
			if (scale_ > scaleMax_)
			{
				scale_ = scaleMax_;
				scaleRate_ = 0f - scaleRate_;
			}
			if (scale_ < scaleMin_)
			{
				scale_ = scaleMin_;
				scaleRate_ = 0f - scaleRate_;
			}
		}
		if (coloring_)
		{
			colorHue_ += colorRate_ * dt;
			if (colorHue_ < colorMin_)
			{
				colorHue_ = colorMin_;
				colorRate_ = 0f - colorRate_;
			}
			if (colorHue_ > colorMax_)
			{
				colorHue_ = colorMax_;
				colorRate_ = 0f - colorRate_;
			}
			color_ = GetColor(colorHue_);
		}
		if (!flipping_)
		{
			return;
		}
		flipTimer_ += dt;
		if (flipTimer_ > flipDuration_)
		{
			flipTimer_ -= flipDuration_;
			SpriteEffects val = spriteEffect_;
			switch ((int)val)
			{
			case 0:
				spriteEffect_ = (SpriteEffects)1;
				break;
			case 1:
				spriteEffect_ = (SpriteEffects)0;
				break;
			case 2:
				spriteEffect_ = (SpriteEffects)0;
				break;
			}
		}
	}

	public static void Draw(SpriteBatch spriteBatch, RenderTarget2D renderTarget, GraphicsDeviceManager graphics, GraphicsDevice graphicsDevice)
	{
																						switch (effectIndex)
		{
		case 0:
			//effects[effectIndex].Parameters["Offset"].SetValue(new Vector2((float)Math.Cos(theta), (float)Math.Sin(theta)));
			break;
		case 2:
			//effects[effectIndex].Parameters["Offset"].SetValue(effectOffset.X);
			break;
		case 3:
			//effects[effectIndex].Parameters["WaveDimensions"].SetValue(new Vector2(10f, 0.03f));
			//effects[effectIndex].Parameters["Timer"].SetValue(timer);
			break;
		case 4:
			//effects[effectIndex].Parameters["Timer"].SetValue(timer);
			//effects[effectIndex].Parameters["Strength"].SetValue(strength);
			break;
		}
		graphicsDevice.SetRenderTarget((RenderTarget2D)null);
		graphicsDevice.Clear(Color.White);
		if (effectIndex == -1)
		{
			spriteBatch.Begin((SpriteSortMode)1, BlendState.NonPremultiplied);
			spriteBatch.Draw((Texture2D)(object)renderTarget, position_, (Rectangle?)null, color_, rotation_, new Vector2(640f, 360f), scale_, spriteEffect_, 0f);
			spriteBatch.End();
		}
		else
		{
			spriteBatch.Begin((SpriteSortMode)0, (BlendState)null, (SamplerState)null, (DepthStencilState)null, (RasterizerState)null, effects[effectIndex]);
			spriteBatch.Draw((Texture2D)(object)renderTarget, Vector2.Zero, Color.White * alpha);
			spriteBatch.End();
		}
	}

	public static void Reset()
	{
																alpha = 1f;
		timer = 0f;
		strength = 0.5f;
		theta = 0f;
		effectOffset = Vector2.Zero;
		effectIndex = -1;
		shaking = false;
		numShakes = 0;
		moving_ = false;
		rotating_ = false;
		scaling_ = false;
		coloring_ = false;
		flipping_ = false;
		position_ = new Vector2(640f, 360f);
		rotation_ = 0f;
		scale_ = 1.1f;
		color_ = Color.White;
		spriteEffect_ = (SpriteEffects)0;
	}

	public static void SetEffect(int newEffectIndex)
	{
														scale_ = 1f;
		switch (newEffectIndex)
		{
		case -1:
			effectIndex = -1;
			break;
		case 0:
			alpha = 0f;
			alphaMin = 0f;
			alphaMax = 0.15f;
			alphaRate = 1.6551723f;
			thetaRate = 0f;
			effectIndex = 0;
			break;
		case 1:
			effectIndex = 1;
			break;
		case 2:
			effectIndex = 4;
			strength = 0.01f;
			alpha = 1f;
			alphaMin = 1f;
			alphaMax = 1f;
			alphaRate = 0f;
			break;
		case 3:
			alpha = 0.6f;
			alphaMin = 0.6f;
			alphaMax = 0.6f;
			alphaRate = 0f;
			thetaRate = 18.221237f;
			effectIndex = 0;
			break;
		case 4:
			effectIndex = 3;
			alpha = 0.5f;
			alphaMin = 0.5f;
			alphaMax = 0.5f;
			alphaRate = 0f;
			break;
		case 5:
			effectIndex = 2;
			effectOffset = Vector2.Zero;
			effectOffsetMax = Vector2.One;
			effectOffsetRate = new Vector2(0.3f, 0f);
			break;
		}
	}

	public static void GoCrazy(float duration)
	{
		DoRotating(duration);
		DoScaling(duration);
		DoColoring(duration);
		DoFlipping(duration);
	}

	public static void DoShake(float magnitude, float duration)
	{
		shaking = true;
		shakeMagnitude = magnitude;
		shakeDuration = duration;
		shakeTimer = 0f;
	}

	public static void DoShakes(int num, float timeBetween, float magnitude, float duration)
	{
		numShakes = num;
		timeBetweenShakes = timeBetween;
		timeBetweenTimer = 0f;
		DoShake(magnitude, duration);
		numShakes--;
	}

	public static void DoScaling(float duration)
	{
		scaling_ = true;
		scaleRate_ = 2f * (scaleMax_ - scaleMin_) / duration;
	}

	public static void StopScaling()
	{
		scaling_ = false;
		scale_ = 1.1f;
	}

	public static void DoRotating(float duration)
	{
		rotating_ = true;
		rotationRate_ = 2f * (rotationMax_ - rotationMin_) / duration;
		scale_ = 1.1f;
	}

	public static void StopRotating()
	{
		rotating_ = false;
		rotation_ = 0f;
	}

	public static void DoColoring(float duration)
	{
		coloring_ = true;
		colorRate_ = 360f / duration;
	}

	public static void DoFlipping(float duration)
	{
		flipping_ = true;
		flipDuration_ = duration * 8f;
		flipTimer_ = 0f;
	}

	public static void StopFlipping()
	{
				flipping_ = false;
		spriteEffect_ = (SpriteEffects)0;
	}

	private static Color GetColor(float hue)
	{
																										Vector3 one = Vector3.One;
		if (hue < 0f)
		{
			Color white = Color.White;
			return new Color(new Vector4(((Color)(white)).ToVector3(), 0f));
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
