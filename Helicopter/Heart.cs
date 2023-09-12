using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Helicopter
{
	public class Heart
	{
		private bool active;

		private float scale = 0f;

		private float scaleRate = 3.3f;

		private float rotation = 0f;

		private float rotationRate = 1f;

		private int index = 0;

		private int totalIndexes = 7;

		private bool alternating;

		public void Update(float dt)
		{
			if (!this.active)
			{
				return;
			}
			this.scale += this.scaleRate * dt;
			if (this.scale > 1.125f)
			{
				this.scale = 0f;
				if (this.alternating)
				{
					this.index = (this.index + 1) % this.totalIndexes;
				}
			}
			this.rotation += this.rotationRate * dt;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			if (this.active)
			{
				switch (this.index)
				{
				case 0:
					this.DrawSymbol(spriteBatch, Global.hugeHeart);
					break;
				case 1:
					this.DrawSymbol(spriteBatch, Global.hugestar);
					break;
				case 2:
					this.DrawSymbol(spriteBatch, Global.hugeButterfly);
					break;
				case 3:
					this.DrawSymbol(spriteBatch, Global.hugeCat);
					break;
				case 4:
					this.DrawSymbol(spriteBatch, Global.hugeCrown);
					break;
				case 5:
					this.DrawSymbol(spriteBatch, Global.hugeMoon);
					break;
				case 6:
					this.DrawSymbol(spriteBatch, Global.hugeRabbit);
					break;
				}
			}
		}

		private void DrawSymbol(SpriteBatch spriteBatch, Texture2D texture)
		{
			spriteBatch.Draw(texture, new Vector2((float)(texture.Width / 2) - (float)(texture.Width / 2) * this.scale + 400f, (float)(texture.Height / 2) - (float)(texture.Height / 2) * this.scale + 300f), (Rectangle?)null, Global.tunnelColor, this.rotation, new Vector2(texture.Width / 2, texture.Height / 2), this.scale, SpriteEffects.None, 0f);
		}

		public void Reset(int _index, bool _alternating)
		{
			this.TurnOff();
			this.scale = 0f;
			this.rotation = 0f;
			this.index = _index;
			this.alternating = _alternating;
		}

		public void TurnOn()
		{
			this.active = true;
		}

		public void TurnOff()
		{
			this.active = false;
		}
	}
}
