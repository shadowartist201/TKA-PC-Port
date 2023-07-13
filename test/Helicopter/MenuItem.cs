using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Helicopter;

public class MenuItem
{
	private Texture2D texture_;

	public Rectangle texRect_;

	private Vector2 position_;

	private Vector2 origin_;

	private float rotation_;

	private float minRotation_ = -(float)Math.PI / 32f;

	private float maxRotation_ = (float)Math.PI / 32f;

	private float rotationRate_ = (float)Math.PI;

	public Rectangle CollisionRect => new Rectangle((int)(position_.X - origin_.X), (int)(position_.Y - origin_.Y), texRect_.Width, texRect_.Height);

	public MenuItem(Texture2D texture, Rectangle texRect, Vector2 position)
	{
														texture_ = texture;
		texRect_ = texRect;
		position_ = position;
		origin_ = new Vector2((float)(texRect.Width / 2), (float)(texRect.Height / 2));
	}

	public void Update(float dt, bool selected)
	{
		if (selected)
		{
			rotation_ += rotationRate_ * dt;
			if (rotation_ > maxRotation_)
			{
				rotation_ = maxRotation_;
				rotationRate_ = 0f - rotationRate_;
			}
			if (rotation_ < minRotation_)
			{
				rotation_ = minRotation_;
				rotationRate_ = 0f - rotationRate_;
			}
		}
		else
		{
			rotation_ = 0f;
		}
	}

	public void Draw(SpriteBatch spriteBatch)
	{
										spriteBatch.Draw(texture_, position_, (Rectangle?)texRect_, Color.White, rotation_, origin_, 1f, (SpriteEffects)0, 0f);
	}

	public void SetTexRect(Rectangle texRect)
	{
										texRect_ = texRect;
		origin_ = new Vector2((float)(texRect.Width / 2), (float)(texRect.Height / 2));
	}
}
