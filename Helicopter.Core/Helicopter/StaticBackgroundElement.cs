using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Helicopter.Core
{
	internal class StaticBackgroundElement
	{
		private Texture2D texture_;

		private Rectangle texRect_;

		private Vector2 position_;

		public void Draw(SpriteBatch spriteBatch)
		{
            spriteBatch.Draw(this.texture_, this.position_, (Rectangle?)this.texRect_, Color.White);
		}

		public void Reset(Texture2D texture, Rectangle texRect, Vector2 position)
		{
			this.texture_ = texture;
			this.texRect_ = texRect;
			this.position_ = position;
		}
	}
}
