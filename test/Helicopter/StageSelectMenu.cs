using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Helicopter;

internal class StageSelectMenu : Menu
{
	private GameState lastGameState;

	private int currentLevel;

	private int startingIndex = 0;

	private int maxIndex = 4;

	private float arrowOffsetX = 0f;

	private float arrowOffsetRateX = 100f;

	private Rectangle[] itemRects = (Rectangle[])(object)new Rectangle[5]
	{
		new Rectangle(0, 722, 328, 327),
		new Rectangle(1280, 1095, 328, 327),
		new Rectangle(1280, 722, 328, 327),
		new Rectangle(660, 722, 328, 327),
		new Rectangle(330, 722, 328, 327)
	};

	private Vector2[] itemPositions = (Vector2[])(object)new Vector2[3]
	{
		new Vector2(302f, 280f),
		new Vector2(638f, 280f),
		new Vector2(970f, 280f)
	};

	private Rectangle[] nameRects = (Rectangle[])(object)new Rectangle[5]
	{
		new Rectangle(297, 1051, 197, 19),
		new Rectangle(0, 0, 0, 0),
		new Rectangle(297, 1080, 177, 19),
		new Rectangle(695, 1051, 177, 19),
		new Rectangle(496, 1051, 197, 19)
	};

	private string[] stageNames = new string[5] { "Dream Pack", "Popaganda Pack", "Meat Pack", "Lava Pack", "Cloud Pack" };

	private Vector2[] namePositions = (Vector2[])(object)new Vector2[3]
	{
		new Vector2(300f, 503f),
		new Vector2(640f, 503f),
		new Vector2(972f, 503f)
	};

	private Vector2[] namePositionsTrial = (Vector2[])(object)new Vector2[3]
	{
		new Vector2(300f, 476f),
		new Vector2(640f, 476f),
		new Vector2(972f, 476f)
	};

	private int ActualIndex => startingIndex + index_;

	public StageSelectMenu()
		: base(horizontal: true)
	{
																																																																																										AddMenuItem(new MenuItem(Global.selectStageTex, itemRects[0], itemPositions[0]));
		AddMenuItem(new MenuItem(Global.selectStageTex, itemRects[1], itemPositions[1]));
		AddMenuItem(new MenuItem(Global.selectStageTex, itemRects[2], itemPositions[2]));
	}

    public void Update(float dt, InputState currInput, ref GameState gameState)
    {
        this.arrowOffsetX += this.arrowOffsetRateX * dt;
        if (this.arrowOffsetX > 5f)
        {
            this.arrowOffsetX = 5f;
            this.arrowOffsetRateX = 0f - this.arrowOffsetRateX;
        }
        if (this.arrowOffsetX < -5f)
        {
            this.arrowOffsetX = -5f;
            this.arrowOffsetRateX = 0f - this.arrowOffsetRateX;
        }
        if (currInput.IsButtonPressed(Buttons.DPadRight) && base.index_ == 2 && this.startingIndex + 2 < this.maxIndex)
        {
            this.startingIndex++;
            for (int i = 0; i < 3; i++)
            {
                base.menuItems_[i] = new MenuItem(Global.selectStageTex, this.itemRects[i + this.startingIndex], this.itemPositions[i]);
            }
        }
        if (currInput.IsButtonPressed(Buttons.DPadLeft) && base.index_ == 0 && this.startingIndex > 0)
        {
            this.startingIndex--;
            for (int i = 0; i < 3; i++)
            {
                base.menuItems_[i] = new MenuItem(Global.selectStageTex, this.itemRects[i + this.startingIndex], this.itemPositions[i]);
            }
        }
        base.Update(dt, currInput);
        if (currInput.IsButtonPressed(Buttons.A) && (!Global.IsTrialMode || this.ActualIndex == 0 || this.ActualIndex == 1))
        {
            this.currentLevel = this.ActualIndex;
            switch (this.currentLevel)
            {
                case 1:
                    this.currentLevel = 4;
                    break;
                case 2:
                    this.currentLevel = 3;
                    break;
                case 3:
                    this.currentLevel = 2;
                    break;
                case 4:
                    this.currentLevel = 1;
                    break;
                case 5:
                    this.currentLevel = 5;
                    break;
            }
            this.ResetMenu();
            Global.PlayCatSound();
            if (this.lastGameState == GameState.MAIN_MENU)
            {
                gameState = GameState.CAT_SELECT;
            }
            else
            {
                gameState = GameState.PLAY;
            }
        }
        if (currInput.IsButtonPressed(Buttons.B))
        {
            Global.PlayCatSound();
            this.ResetMenu();
            gameState = this.lastGameState;
        }
    }

    private void ResetMenu()
	{
						index_ = 0;
		startingIndex = 0;
		for (int i = 0; i < 3; i++)
		{
			menuItems_[i] = new MenuItem(Global.selectStageTex, itemRects[i], itemPositions[i]);
		}
	}

	public new void Draw(SpriteBatch spriteBatch)
	{
		DrawBackground(spriteBatch);
		base.Draw(spriteBatch);
		DrawForeground(spriteBatch);
	}

	private void DrawBackground(SpriteBatch spriteBatch)
	{
																																																																																																																				spriteBatch.Draw(Global.selectStageTex, Vector2.Zero, (Rectangle?)new Rectangle(0, 0, 1280, 720), Color.White);
		if (startingIndex < maxIndex - 2)
		{
			spriteBatch.Draw(Global.selectStageTex, new Vector2(1210f + arrowOffsetX, 280f), (Rectangle?)new Rectangle(1282, 0, 73, 106), Color.White, 0f, new Vector2(36.5f, 53f), 1f, (SpriteEffects)0, 0f);
		}
		if (startingIndex > 0)
		{
			spriteBatch.Draw(Global.selectStageTex, new Vector2(70f + arrowOffsetX, 280f), (Rectangle?)new Rectangle(1360, 0, 73, 106), Color.White, 0f, new Vector2(36.5f, 53f), 1f, (SpriteEffects)0, 0f);
		}
		if (Global.IsTrialMode)
		{
			if (startingIndex == 0)
			{
				spriteBatch.Draw(Global.selectStageTex, new Vector2(300f, 503f), (Rectangle?)new Rectangle(0, 1051, 224, 48), Color.White, 0f, new Vector2(112f, 24f), 1f, (SpriteEffects)0, 0f);
				spriteBatch.DrawString(Global.menuFont, stageNames[startingIndex + 1], namePositions[1], Color.Black, 0f, Global.menuFont.MeasureString(stageNames[startingIndex + 1]) / 2f, 1f, (SpriteEffects)0, 0f);
				for (int i = 2; i < 3; i++)
				{
					spriteBatch.DrawString(Global.menuFont, stageNames[startingIndex + i], namePositionsTrial[i], Color.Black, 0f, Global.menuFont.MeasureString(stageNames[startingIndex + i]) / 2f, 1f, (SpriteEffects)0, 0f);
					spriteBatch.Draw(Global.selectStageTex, namePositionsTrial[i] + new Vector2(0f, 38f), (Rectangle?)new Rectangle(874, 1051, 288, 50), Color.White, 0f, new Vector2(144f, 25f), 1f, (SpriteEffects)0, 0f);
				}
			}
			else if (startingIndex == 1)
			{
				spriteBatch.DrawString(Global.menuFont, stageNames[startingIndex], namePositions[0], Color.Black, 0f, Global.menuFont.MeasureString(stageNames[startingIndex]) / 2f, 1f, (SpriteEffects)0, 0f);
				for (int i = 1; i < 3; i++)
				{
					spriteBatch.DrawString(Global.menuFont, stageNames[startingIndex + i], namePositionsTrial[i], Color.Black, 0f, Global.menuFont.MeasureString(stageNames[startingIndex + i]) / 2f, 1f, (SpriteEffects)0, 0f);
					spriteBatch.Draw(Global.selectStageTex, namePositionsTrial[i] + new Vector2(0f, 38f), (Rectangle?)new Rectangle(874, 1051, 288, 50), Color.White, 0f, new Vector2(144f, 25f), 1f, (SpriteEffects)0, 0f);
				}
			}
			else
			{
				for (int i = 0; i < 3; i++)
				{
					spriteBatch.DrawString(Global.menuFont, stageNames[startingIndex + i], namePositionsTrial[i], Color.Black, 0f, Global.menuFont.MeasureString(stageNames[startingIndex + i]) / 2f, 1f, (SpriteEffects)0, 0f);
					spriteBatch.Draw(Global.selectStageTex, namePositionsTrial[i] + new Vector2(0f, 38f), (Rectangle?)new Rectangle(874, 1051, 288, 50), Color.White, 0f, new Vector2(144f, 25f), 1f, (SpriteEffects)0, 0f);
				}
			}
		}
		else
		{
			for (int i = 0; i < 3; i++)
			{
				spriteBatch.DrawString(Global.menuFont, stageNames[startingIndex + i], namePositions[i], Color.Black, 0f, Global.menuFont.MeasureString(stageNames[startingIndex + i]) / 2f, 1f, (SpriteEffects)0, 0f);
			}
		}
	}

	private void DrawForeground(SpriteBatch spriteBatch)
	{
																																		if (!Global.IsTrialMode)
		{
			return;
		}
		if (startingIndex == 0)
		{
			spriteBatch.Draw(Global.selectStageTex, itemPositions[0], (Rectangle?)new Rectangle(1000, 722, 269, 92), Color.White, 0f, new Vector2(134.5f, 46f), 1f, (SpriteEffects)0, 0f);
			for (int i = 2; i < 3; i++)
			{
				spriteBatch.Draw(Global.selectStageTex, itemPositions[i], (Rectangle?)new Rectangle(0, 1103, 319, 319), Color.White, 0f, new Vector2(159.5f, 159.5f), 1f, (SpriteEffects)0, 0f);
			}
		}
		else if (startingIndex == 1)
		{
			for (int i = 1; i < 3; i++)
			{
				spriteBatch.Draw(Global.selectStageTex, itemPositions[i], (Rectangle?)new Rectangle(0, 1103, 319, 319), Color.White, 0f, new Vector2(159.5f, 159.5f), 1f, (SpriteEffects)0, 0f);
			}
		}
		else
		{
			for (int i = 0; i < 3; i++)
			{
				spriteBatch.Draw(Global.selectStageTex, itemPositions[i], (Rectangle?)new Rectangle(0, 1103, 319, 319), Color.White, 0f, new Vector2(159.5f, 159.5f), 1f, (SpriteEffects)0, 0f);
			}
		}
	}

	public int getCurrentLevel()
	{
		return currentLevel;
	}

	public void SetLastGameState(GameState gameState)
	{
		lastGameState = gameState;
	}
}
