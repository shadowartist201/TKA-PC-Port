using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Helicopter;

internal class TrialMenu : Menu
{
	private float startTimer;

	private bool canBuyGame;

	public TrialMenu()
		: base(horizontal: true)
	{
	}

	public void Update(float dt, InputState currInput,GameState gameState)
	{
				startTimer += dt;
		if (!(startTimer > 1f))
		{
			return;
		}
		Update(dt, currInput);
		if (!currInput.IsButtonPressed((Buttons)4096))
		{
			return;
		}
		Global.PlayCatSound();
		switch (index_)
		{
		case 0:
			if (canBuyGame)
			{
				if (Global.IsTrialMode && Global.CanBuyGame())
				{
					//Guide.ShowMarketplace(Global.playerIndex.Value);
				}
			}
			else
			{
				index_ = 0;
				gameState = GameState.MAIN_MENU;
			}
			break;
		case 1:
			index_ = 0;
			gameState = GameState.MAIN_MENU;
			break;
		}
	}

	public new void Draw(SpriteBatch spriteBatch)
	{
														if (Global.IsTrialMode)
		{
			canBuyGame = Global.CanBuyGame();
		}
		else
		{
			canBuyGame = false;
		}
		if (canBuyGame && (menuItems_.Count == 1 || menuItems_.Count == 0))
		{
			menuItems_.Clear();
			index_ = 0;
			AddMenuItem(new MenuItem(Global.trialTex, new Rectangle(0, 558, 379, 106), new Vector2(438.5f, 557f)));
			AddMenuItem(new MenuItem(Global.trialTex, new Rectangle(380, 558, 379, 106), new Vector2(841.5f, 557f)));
			SetItemVertices();
		}
		if (!canBuyGame && (menuItems_.Count == 2 || menuItems_.Count == 0))
		{
			menuItems_.Clear();
			index_ = 0;
			AddMenuItem(new MenuItem(Global.trialTex, new Rectangle(380, 558, 379, 106), new Vector2(841.5f, 557f)));
			SetItemVertices();
		}
		DrawBackground(spriteBatch);
		base.Draw(spriteBatch);
	}

	private void DrawBackground(SpriteBatch spriteBatch)
	{
								spriteBatch.Draw(Global.trialTex, new Vector2(222f, 82f), (Rectangle?)new Rectangle(0, 0, 836, 556), Color.White);
	}

	public void ResetStartTimer()
	{
		startTimer = 0f;
	}
}
