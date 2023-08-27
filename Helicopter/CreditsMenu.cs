using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Helicopter
{
	internal class CreditsMenu : Menu
	{
		private float scrollRate_;

		private float scrollProgress_;

		private Rectangle scrollRectangle_;

		private Vector2 scrollPosition_;

		public CreditsMenu()
			: base(horizontal: false)
		{
			base.AddMenuItem(new MenuItem(Global.creditsTex, new Rectangle(0, 1450, 259, 60), new Vector2(1039.5f, 630f)));
			this.scrollRate_ = 0.05f;
			this.scrollProgress_ = 0f;
			this.scrollRectangle_ = new Rectangle(1282, 0, 774, 0);
			this.scrollPosition_ = new Vector2(253f, 720f);
		}

		public void Update(float dt, InputState currInput, ref GameState gameState)
		{
			Rectangle credits_back = new(910, 600 ,259 ,60);
			this.scrollProgress_ += this.scrollRate_ * dt;
			float num = this.scrollProgress_ * 1510f;
			if (num > 720f)
			{
				if (num > 1510f)
				{
					float num2 = 2230f - num;
					if (num2 > 0f)
					{
						this.scrollRectangle_ = new Rectangle(1282, (int)(1510f - num2), 774, (int)num2);
						this.scrollPosition_ = new Vector2(this.scrollPosition_.X, 0f);
					}
					else
					{
						this.scrollProgress_ = 0f;
						this.scrollPosition_ = new Vector2(this.scrollPosition_.X, 720f);
						this.scrollRectangle_ = new Rectangle(1282, 0, 774, 0);
					}
				}
				else
				{
					this.scrollRectangle_ = new Rectangle(1282, (int)(num - 720f), 774, 720);
				}
			}
			else
			{
				this.scrollRectangle_ = new Rectangle(1282, 0, 774, (int)num);
				this.scrollPosition_ = new Vector2(this.scrollPosition_.X, 720f - num);
			}
			base.Update(dt, currInput);
			if (currInput.IsButtonPressed(Buttons.A) || currInput.IsButtonPressed(Buttons.B) || credits_back.Contains(Game1.touchLocations[0].Position * Game1.resolutionDifference))
			{
				Global.PlayCatSound();
				gameState = GameState.OPTIONS;
			}
		}

		public new void Draw(SpriteBatch spriteBatch)
		{
			this.DrawBackground(spriteBatch);
			base.Draw(spriteBatch);
		}

		private void DrawBackground(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(Global.creditsTex, Vector2.Zero, (Rectangle?)new Rectangle(0, 0, 1280, 720), Color.White);
			spriteBatch.Draw(Global.creditsTex, this.scrollPosition_, (Rectangle?)this.scrollRectangle_, Color.White);
			spriteBatch.Draw(Global.creditsTex, Vector2.Zero, (Rectangle?)new Rectangle(0, 722, 1280, 720), Color.White);
		}
	}
}
