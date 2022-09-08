using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Helicopter
{
	public class AnimatedSprite
	{
		private Texture2D texture;

		private Dictionary<int, Animation> Animations = new Dictionary<int, Animation>();

		private int currentAnimation = -1;

		private int lastAnimation;

		public AnimatedSprite(Texture2D _texture)
		{
			this.texture = _texture;
		}

		public void AddAnimation(Animation animation)
		{
			this.currentAnimation++;
			this.Animations.Add(this.currentAnimation, animation);
		}

		public void SetAnimation(int animationIndex)
		{
			this.currentAnimation = animationIndex;
		}

		public void Update(float dt)
		{
			if (this.currentAnimation != -1)
			{
				this.Animations[this.currentAnimation].Update(dt);
			}
			if (this.currentAnimation != this.lastAnimation)
			{
				this.lastAnimation = this.currentAnimation;
			}
		}

		public void Draw(SpriteBatch spriteBatch, Vector2 position, float rotation, SpriteEffects spriteEffects)
		{
			spriteBatch.Draw(this.texture, position, (Rectangle?)this.Animations[this.currentAnimation].CurrentFrame, Color.White, rotation, new Vector2(35f, 35f), 1f, spriteEffects, 1f);
		}
	}
}
