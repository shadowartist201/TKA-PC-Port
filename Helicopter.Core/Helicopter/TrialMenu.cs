using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Helicopter.Core
{
	internal class TrialMenu : Menu
	{
		private float startTimer;

		private bool canBuyGame;

		public TrialMenu()
			: base(horizontal: true)
		{
		}

		public void Update(float dt, InputState currInput, ref GameState gameState)
		{
			this.startTimer += dt;
			if (!(this.startTimer > 1f))
			{
				return;
			}
			base.Update(dt, currInput);
			if (!currInput.IsButtonPressed(Buttons.A))
			{
				return;
			}
			Global.PlayCatSound();
			switch (base.index_)
			{
			case 0:
				if (this.canBuyGame)
				{
					if (Global.IsTrialMode && Global.CanBuyGame())
					{
						//Guide.ShowMarketplace(Global.playerIndex.Value);
					}
				}
				else
				{
					base.index_ = 0;
					gameState = GameState.MAIN_MENU;
				}
				break;
			case 1:
				base.index_ = 0;
				gameState = GameState.MAIN_MENU;
				break;
			}
		}

		public new void Draw(SpriteBatch spriteBatch)
		{
			if (Global.IsTrialMode)
			{
				this.canBuyGame = Global.CanBuyGame();
			}
			else
			{
				this.canBuyGame = false;
			}
			if (this.canBuyGame && (base.menuItems_.Count == 1 || base.menuItems_.Count == 0))
			{
				base.menuItems_.Clear();
				base.index_ = 0;
				base.AddMenuItem(new MenuItem(Global.trialTex, new Rectangle(0, 558, 379, 106), new Vector2(438.5f, 557f)));
				base.AddMenuItem(new MenuItem(Global.trialTex, new Rectangle(380, 558, 379, 106), new Vector2(841.5f, 557f)));
				base.SetItemVertices();
			}
			if (!this.canBuyGame && (base.menuItems_.Count == 2 || base.menuItems_.Count == 0))
			{
				base.menuItems_.Clear();
				base.index_ = 0;
				base.AddMenuItem(new MenuItem(Global.trialTex, new Rectangle(380, 558, 379, 106), new Vector2(841.5f, 557f)));
				base.SetItemVertices();
			}
			this.DrawBackground(spriteBatch);
			base.Draw(spriteBatch);
		}

		private void DrawBackground(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(Global.trialTex, new Vector2(222f, 82f), (Rectangle?)new Rectangle(0, 0, 836, 556), Color.White);
		}

		public void ResetStartTimer()
		{
			this.startTimer = 0f;
		}
	}
}
