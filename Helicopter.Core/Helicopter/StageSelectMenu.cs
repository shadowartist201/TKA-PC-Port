using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Helicopter.Core
{
	internal class StageSelectMenu : Menu
	{
		private GameState lastGameState;

		private int currentLevel;

		private int startingIndex = 0;

		private int maxIndex = 5;

		private float arrowOffsetX = 0f;

		private float arrowOffsetRateX = 100f;

		private Rectangle[] itemRects = new Rectangle[6]
		{
			new Rectangle(0, 722, 328, 327),
			new Rectangle(1280, 1095, 328, 327),
			new Rectangle(1280, 722, 328, 327),
			new Rectangle(660, 722, 328, 327),
			new Rectangle(330, 722, 328, 327),
			new Rectangle(486, 1115, 328, 327)
		};

		private Vector2[] itemPositions = new Vector2[3]
		{
			new Vector2(302f, 280f),
			new Vector2(638f, 280f),
			new Vector2(970f, 280f)
		};

		private Rectangle[] nameRects = new Rectangle[6]
		{
			new Rectangle(297, 1051, 197, 19),
			new Rectangle(0, 0, 0, 0),
			new Rectangle(297, 1080, 177, 19),
			new Rectangle(695, 1051, 177, 19),
			new Rectangle(496, 1051, 197, 19),
			new Rectangle(515, 1080, 179, 19)
		};

		private string[] stageNames = new string[6] { "Dream Pack", "Popaganda Pack", "Meat Pack", "Lava Pack", "Cloud Pack", "Nyan Pack" };

		private Vector2[] namePositions = new Vector2[3]
		{
			new Vector2(300f, 503f),
			new Vector2(640f, 503f),
			new Vector2(972f, 503f)
		};

		private Vector2[] namePositionsTrial = new Vector2[3]
		{
			new Vector2(300f, 476f),
			new Vector2(640f, 476f),
			new Vector2(972f, 476f)
		};

		private int ActualIndex => this.startingIndex + base.index_;

		public StageSelectMenu()
			: base(horizontal: true)
		{
			base.AddMenuItem(new MenuItem(Global.selectStageTex, this.itemRects[0], this.itemPositions[0]));
			base.AddMenuItem(new MenuItem(Global.selectStageTex, this.itemRects[1], this.itemPositions[1]));
			base.AddMenuItem(new MenuItem(Global.selectStageTex, this.itemRects[2], this.itemPositions[2]));
		}

		public void Update(float dt, InputState currInput, ref GameState gameState)
		{
            Rectangle arrow_left = new(60, 230, 83, 126);
            Rectangle arrow_right = new(1200, 230, 83, 126);
            Rectangle stage1 = new(138, 117, 328, 327);
            Rectangle stage2 = new(474, 117, 328, 327);
            Rectangle stage3 = new(806, 117, 328, 327);
            Rectangle back = new(155, 607, 153, 36);

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

            if (arrow_right.Contains((Game1.touchLocations[0].Position - Game1.touchOffset) * Game1.resolutionDifference) && currInput.IsThingTouched() && startingIndex != 3)
            {
                base.index_ = 2;
                this.startingIndex++;
                for (int i = 0; i < 3; i++)
                {
                    base.menuItems_[i] = new MenuItem(Global.selectStageTex, this.itemRects[i + this.startingIndex], this.itemPositions[i]);
                }
            }
            if (arrow_left.Contains((Game1.touchLocations[0].Position - Game1.touchOffset) * Game1.resolutionDifference) && currInput.IsThingTouched() && startingIndex != 0)
            {
                base.index_ = 0;
                this.startingIndex--;
                for (int i = 0; i < 3; i++)
                {
                    base.menuItems_[i] = new MenuItem(Global.selectStageTex, this.itemRects[i + this.startingIndex], this.itemPositions[i]);
                }

            }

            if (stage1.Contains((Game1.touchLocations[0].Position - Game1.touchOffset) * Game1.resolutionDifference) && currInput.IsThingTouched())
            {
                switch (this.startingIndex)
                {
                    case 0: this.currentLevel = 0; break;
                    case 1: this.currentLevel = 4; break;
                    case 2: this.currentLevel = 3; break;
                    case 3: this.currentLevel = 2; break;
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
            else if (stage2.Contains((Game1.touchLocations[0].Position - Game1.touchOffset) * Game1.resolutionDifference) && currInput.IsThingTouched())
            {
                switch (this.startingIndex)
                {
                    case 0: this.currentLevel = 4; break;
                    case 1: this.currentLevel = 3; break;
                    case 2: this.currentLevel = 2; break;
                    case 3: this.currentLevel = 1; break;
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
            else if (stage3.Contains((Game1.touchLocations[0].Position - Game1.touchOffset) * Game1.resolutionDifference) && currInput.IsThingTouched())
            {
                switch (this.startingIndex)
                {
                    case 0: this.currentLevel = 3; break;
                    case 1: this.currentLevel = 2; break;
                    case 2: this.currentLevel = 1; break;
                    case 3: this.currentLevel = 5; break;
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
            if (back.Contains((Game1.touchLocations[0].Position - Game1.touchOffset) * Game1.resolutionDifference) && currInput.IsThingTouched())
            {
                Global.PlayCatSound();
                this.ResetMenu();
                gameState = this.lastGameState;
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

			base.Update(dt, currInput);
		}

		private void ResetMenu()
		{
			base.index_ = 0;
			this.startingIndex = 0;
			for (int i = 0; i < 3; i++)
			{
				base.menuItems_[i] = new MenuItem(Global.selectStageTex, this.itemRects[i], this.itemPositions[i]);
			}
		}

		public new void Draw(SpriteBatch spriteBatch)
		{
			this.DrawBackground(spriteBatch);
			base.Draw(spriteBatch);
			this.DrawForeground(spriteBatch);
		}

		private void DrawBackground(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(Global.selectStageTex, Vector2.Zero, (Rectangle?)new Rectangle(0, 0, 1280, 720), Color.White);
			if (this.startingIndex < this.maxIndex - 2)
			{
				spriteBatch.Draw(Global.selectStageTex, new Vector2(1210f + this.arrowOffsetX, 280f), (Rectangle?)new Rectangle(1282, 0, 73, 106), Color.White, 0f, new Vector2(36.5f, 53f), 1f, SpriteEffects.None, 0f);
			}
			if (this.startingIndex > 0)
			{
				spriteBatch.Draw(Global.selectStageTex, new Vector2(70f + this.arrowOffsetX, 280f), (Rectangle?)new Rectangle(1360, 0, 73, 106), Color.White, 0f, new Vector2(36.5f, 53f), 1f, SpriteEffects.None, 0f);
			}
			if (Global.IsTrialMode)
			{
				if (this.startingIndex == 0)
				{
					spriteBatch.Draw(Global.selectStageTex, new Vector2(300f, 503f), (Rectangle?)new Rectangle(0, 1051, 224, 48), Color.White, 0f, new Vector2(112f, 24f), 1f, SpriteEffects.None, 0f);
					spriteBatch.DrawString(Global.menuFont, this.stageNames[this.startingIndex + 1], this.namePositions[1], Color.Black, 0f, Global.menuFont.MeasureString(this.stageNames[this.startingIndex + 1]) / 2f, 1f, SpriteEffects.None, 0f);
					for (int i = 2; i < 3; i++)
					{
						spriteBatch.DrawString(Global.menuFont, this.stageNames[this.startingIndex + i], this.namePositionsTrial[i], Color.Black, 0f, Global.menuFont.MeasureString(this.stageNames[this.startingIndex + i]) / 2f, 1f, SpriteEffects.None, 0f);
						spriteBatch.Draw(Global.selectStageTex, this.namePositionsTrial[i] + new Vector2(0f, 38f), (Rectangle?)new Rectangle(874, 1051, 288, 50), Color.White, 0f, new Vector2(144f, 25f), 1f, SpriteEffects.None, 0f);
					}
				}
				else if (this.startingIndex == 1)
				{
					spriteBatch.DrawString(Global.menuFont, this.stageNames[this.startingIndex], this.namePositions[0], Color.Black, 0f, Global.menuFont.MeasureString(this.stageNames[this.startingIndex]) / 2f, 1f, SpriteEffects.None, 0f);
					for (int i = 1; i < 3; i++)
					{
						spriteBatch.DrawString(Global.menuFont, this.stageNames[this.startingIndex + i], this.namePositionsTrial[i], Color.Black, 0f, Global.menuFont.MeasureString(this.stageNames[this.startingIndex + i]) / 2f, 1f, SpriteEffects.None, 0f);
						spriteBatch.Draw(Global.selectStageTex, this.namePositionsTrial[i] + new Vector2(0f, 38f), (Rectangle?)new Rectangle(874, 1051, 288, 50), Color.White, 0f, new Vector2(144f, 25f), 1f, SpriteEffects.None, 0f);
					}
				}
				else
				{
					for (int i = 0; i < 3; i++)
					{
						spriteBatch.DrawString(Global.menuFont, this.stageNames[this.startingIndex + i], this.namePositionsTrial[i], Color.Black, 0f, Global.menuFont.MeasureString(this.stageNames[this.startingIndex + i]) / 2f, 1f, SpriteEffects.None, 0f);
						spriteBatch.Draw(Global.selectStageTex, this.namePositionsTrial[i] + new Vector2(0f, 38f), (Rectangle?)new Rectangle(874, 1051, 288, 50), Color.White, 0f, new Vector2(144f, 25f), 1f, SpriteEffects.None, 0f);
					}
				}
			}
			else
			{
				for (int i = 0; i < 3; i++)
				{
					spriteBatch.DrawString(Global.menuFont, this.stageNames[this.startingIndex + i], this.namePositions[i], Color.Black, 0f, Global.menuFont.MeasureString(this.stageNames[this.startingIndex + i]) / 2f, 1f, SpriteEffects.None, 0f);
				}
			}
		}

		private void DrawForeground(SpriteBatch spriteBatch)
		{
			if (!Global.IsTrialMode)
			{
				return;
			}
			if (this.startingIndex == 0)
			{
				spriteBatch.Draw(Global.selectStageTex, this.itemPositions[0], (Rectangle?)new Rectangle(1000, 722, 269, 92), Color.White, 0f, new Vector2(134.5f, 46f), 1f, SpriteEffects.None, 0f);
				for (int i = 2; i < 3; i++)
				{
					spriteBatch.Draw(Global.selectStageTex, this.itemPositions[i], (Rectangle?)new Rectangle(0, 1103, 319, 319), Color.White, 0f, new Vector2(159.5f, 159.5f), 1f, SpriteEffects.None, 0f);
				}
			}
			else if (this.startingIndex == 1)
			{
				for (int i = 1; i < 3; i++)
				{
					spriteBatch.Draw(Global.selectStageTex, this.itemPositions[i], (Rectangle?)new Rectangle(0, 1103, 319, 319), Color.White, 0f, new Vector2(159.5f, 159.5f), 1f, SpriteEffects.None, 0f);
				}
			}
			else
			{
				for (int i = 0; i < 3; i++)
				{
					spriteBatch.Draw(Global.selectStageTex, this.itemPositions[i], (Rectangle?)new Rectangle(0, 1103, 319, 319), Color.White, 0f, new Vector2(159.5f, 159.5f), 1f, SpriteEffects.None, 0f);
				}
			}
		}

		public int getCurrentLevel()
		{
			return this.currentLevel;
		}

		public void SetLastGameState(GameState gameState)
		{
			this.lastGameState = gameState;
		}
	}
}
