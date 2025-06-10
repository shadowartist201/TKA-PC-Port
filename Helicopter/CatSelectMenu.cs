using Kotlin.Ranges;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.ComponentModel.Design;

namespace Helicopter
{
	internal class CatSelectMenu : Menu
	{
		private GameState lastGameState;

		private int currentCat;

		private Rectangle[] fiveCatBox = [new Rectangle(8, 121, 240, 238), new Rectangle(264, 121, 240, 238), new Rectangle(520, 121, 240, 238), new Rectangle(776, 121, 240, 238), new Rectangle(1032, 121, 240, 238)];

		private Rectangle[] fourCatBox = [new Rectangle(40, 121, 240, 238), new Rectangle(360, 121, 240, 238), new Rectangle(680, 121, 240, 238), new Rectangle(1000, 121, 240, 238)];

		private Rectangle[] threeCatBox = [new Rectangle(93, 121, 240, 238), new Rectangle(520, 121, 240, 238), new Rectangle(946, 121, 240, 238)];

		private Rectangle back = new (168, 608, 136, 32);

        private int[] numCats_ = new int[6] { 5, 4, 4, 4, 3, 3 };

		private int[] startCatIndex_ = new int[6] { 0, 5, 9, 13, 17, 21 };

		private string[] catNames_ = new string[24]
		{
			"JetPack Kitteh", "Byarf Kitteh", "Butterfly Kitteh", "Dream Kitteh", "Mermaid Kitteh", 
			"Baby Kitteh", "Love Kitteh", "Angel Kitteh", "Death Kitteh", 
			"Bat Kitteh", "Fire Kitteh", "Rock Kitteh", "Dragon Kitteh", 
			"Steak Kitteh", "Bacon Kitteh", "HotDog Kitteh", "Burger Kitteh", 
			"Alien Kitteh", "Grin Kitteh", "MC Kitteh",
			"", "Nyan Cat", "Tac Nyan", "Gameboy Cat"
		};

		private string[] lockedNames_ = new string[4] { "Unlocked in\n Full Mode", "Unlocked at\n  40,000 P", "Unlocked at\n  60,000 P", "Unlocked at\n  80,000 P" };

		public GameState LastGameState => this.lastGameState;

		private Vector2 touch;

		public CatSelectMenu()
			: base(horizontal: true)
		{
			this.SetCats(0);
		}

		public void Update(float dt, InputState currInput, ref GameState gameState, int currentLevel, ScoreSystem scoreSystem)
		{
			this.SetCats(currentLevel);
			base.Update(dt, currInput);
			touch = (Game1.touchLocations[0].Position - Game1.touchOffset) * Game1.resolutionDifference;

			if (currentLevel == 0)
			{
				for (int i = 0; i < fiveCatBox.Length; i++)
				{
					if (fiveCatBox[i].Contains(touch) && currInput.IsThingTouched())
					{
						switch (i)
						{
							case 0:
                                base.index_ = i + 1;
                                Global.PlayCatSound();
                                this.currentCat = this.startCatIndex_[currentLevel] + base.index_ - 1;
                                gameState = GameState.PLAY;
								break;
							case 1:
                                base.index_ = i + 1;
                                Global.PlayCatSound();
                                this.currentCat = this.startCatIndex_[currentLevel] + base.index_ - 1;
                                gameState = GameState.PLAY;
                                break;
                            case 2:
								if (scoreSystem.seaFortyUnlocked)
								{
                                    base.index_ = i + 1;
                                    Global.PlayCatSound();
                                    this.currentCat = this.startCatIndex_[currentLevel] + base.index_ - 1;
                                    gameState = GameState.PLAY;
                                }
								break;
							case 3:
								if (scoreSystem.seaSixtyUnlocked)
								{
                                    base.index_ = i + 1;
                                    Global.PlayCatSound();
                                    this.currentCat = this.startCatIndex_[currentLevel] + base.index_ - 1;
                                    gameState = GameState.PLAY;
                                }
								break;
							case 4:
								if (scoreSystem.seaEightyUnlocked)
								{
                                    base.index_ = i + 1;
                                    Global.PlayCatSound();
                                    this.currentCat = this.startCatIndex_[currentLevel] + base.index_ - 1;
                                    gameState = GameState.PLAY;
                                }
								break;
							default:
								break;
                        }
					}
				}
			}
			else if (currentLevel == 1 || currentLevel == 2 || currentLevel == 3)
			{
				for (int i = 0; i < fourCatBox.Length; i++)
				{
					if (fourCatBox[i].Contains(touch) && currInput.IsThingTouched())
					{
						switch(i)
						{
							case 0:
                                base.index_ = i + 1;
                                Global.PlayCatSound();
                                this.currentCat = this.startCatIndex_[currentLevel] + base.index_ - 1;
                                gameState = GameState.PLAY;
                                break;
							case 1:
								if ((scoreSystem.cloudFortyUnlocked && currentLevel == 1) || (scoreSystem.lavaFortyUnlocked && currentLevel == 2) || (scoreSystem.meatFortyUnlocked && currentLevel == 3))
                                {
                                    base.index_ = i + 1;
                                    Global.PlayCatSound();
                                    this.currentCat = this.startCatIndex_[currentLevel] + base.index_ - 1;
                                    gameState = GameState.PLAY;
                                }
                                break;
							case 2:
								if ((scoreSystem.cloudSixtyUnlocked && currentLevel == 1) || (scoreSystem.lavaSixtyUnlocked && currentLevel == 2) || (scoreSystem.meatSixtyUnlocked && currentLevel == 3))
                                {
                                    base.index_ = i + 1;
                                    Global.PlayCatSound();
                                    this.currentCat = this.startCatIndex_[currentLevel] + base.index_ - 1;
                                    gameState = GameState.PLAY;
                                }
                                break;
							case 3:
								if ((scoreSystem.cloudEightyUnlocked && currentLevel == 1) || (scoreSystem.lavaEightyUnlocked && currentLevel == 2) || (scoreSystem.meatEightyUnlocked && currentLevel == 3))
                                {
                                    base.index_ = i + 1;
                                    Global.PlayCatSound();
                                    this.currentCat = this.startCatIndex_[currentLevel] + base.index_ - 1;
                                    gameState = GameState.PLAY;
                                }
                                break;
							default:
								break;
						}
					}
				}
			}
			else if (currentLevel == 4 || currentLevel == 5)
			{
				for (int i = 0; i < threeCatBox.Length; i++)
				{
					if (threeCatBox[i].Contains(touch) && currInput.IsThingTouched())
					{
						switch (i)
						{
							case 0:
                                base.index_ = i + 1;
                                Global.PlayCatSound();
                                this.currentCat = this.startCatIndex_[currentLevel] + base.index_ - 1;
                                gameState = GameState.PLAY;
                                break;
							case 1:
								if ((scoreSystem.ronFortyUnlocked && currentLevel == 4) || (scoreSystem.nyanFortyUnlocked && currentLevel == 5))
                                {
                                    base.index_ = i + 1;
                                    Global.PlayCatSound();
                                    this.currentCat = this.startCatIndex_[currentLevel] + base.index_ - 1;
                                    gameState = GameState.PLAY;
                                }
                                break;
							case 2:
                                if ((scoreSystem.ronSixtyUnlocked && currentLevel == 4) || (scoreSystem.nyanSixtyUnlocked && currentLevel == 5))
                                {
                                    base.index_ = i + 1;
                                    Global.PlayCatSound();
                                    this.currentCat = this.startCatIndex_[currentLevel] + base.index_ - 1;
                                    gameState = GameState.PLAY;
                                }
                                break;
                            default:
								break;
                        }
					}
				}
			}
			if (back.Contains(touch) && currInput.IsThingTouched())
			{
				Global.PlayCatSound();
				base.index_ = 0;
				gameState = this.lastGameState;
			}
			base.index_ = 0;
		}

		private void SetCats(int currentLevel)
		{
			if (base.menuItems_.Count != this.numCats_[currentLevel])
			{
				base.menuItems_.Clear();
				for (int i = 0; i < this.numCats_[currentLevel]; i++)
				{
					base.menuItems_.Add(new MenuItem(Global.selectCatTex, new Rectangle(0, 0, 0, 0), new Vector2(((float)i + 0.5f) * (1280f / (float)this.numCats_[currentLevel]), 240f)));
				}
			}
			for (int i = 0; i < this.numCats_[currentLevel]; i++)
			{
				base.menuItems_[i].SetTexRect(new Rectangle((this.startCatIndex_[currentLevel] + i) % 5 * 242, 722 + (this.startCatIndex_[currentLevel] + i) / 5 * 240, 240, 238));
			}
		}

		public void Draw(SpriteBatch spriteBatch, int currentLevel, ScoreSystem scoreSystem)
		{
			this.DrawBackground(spriteBatch, currentLevel);
			base.Draw(spriteBatch);
			this.DrawForeground(spriteBatch, currentLevel, scoreSystem);
		}

		private void DrawBackground(SpriteBatch spriteBatch, int currentLevel)
		{
			spriteBatch.Draw(Global.selectCatTex, Vector2.Zero, (Rectangle?)new Rectangle(0, 0, 1280, 118), Color.White);
			spriteBatch.Draw(Global.selectCatTex, new Vector2(0f, 428f), (Rectangle?)new Rectangle(0, 428, 1280, 292), Color.White);
		}

		private void DrawForeground(SpriteBatch spriteBatch, int currentLevel, ScoreSystem scoreSystem)
		{
			float scale = 1f;
			if (currentLevel == 0)
			{
				scale = 0.85f;
			}
			for (int i = 0; i < this.numCats_[currentLevel]; i++)
			{
				spriteBatch.DrawString(Global.menuFont, this.catNames_[this.startCatIndex_[currentLevel] + i], new Vector2(((float)i + 0.5f) * (1280f / (float)this.numCats_[currentLevel]) - 1.5f, 389.5f), Color.White, 0f, Global.menuFont.MeasureString(this.catNames_[this.startCatIndex_[currentLevel] + i]) / 2f, scale, SpriteEffects.None, 0f);
				spriteBatch.DrawString(Global.menuFont, this.catNames_[this.startCatIndex_[currentLevel] + i], new Vector2(((float)i + 0.5f) * (1280f / (float)this.numCats_[currentLevel]), 388f), Color.Black, 0f, Global.menuFont.MeasureString(this.catNames_[this.startCatIndex_[currentLevel] + i]) / 2f, scale, SpriteEffects.None, 0f);
				bool flag = false;
				if (i == 1 && this.numCats_[currentLevel] == 5)
				{
					flag = Global.IsTrialMode;
				}
				if ((i == 2 && this.numCats_[currentLevel] == 5) || (i == 1 && this.numCats_[currentLevel] == 4) || (i == 1 && this.numCats_[currentLevel] == 3))
				{
					switch (currentLevel)
					{
					case 0:
						flag = !scoreSystem.seaFortyUnlocked;
						break;
					case 1:
						flag = !scoreSystem.cloudFortyUnlocked;
						break;
					case 2:
						flag = !scoreSystem.lavaFortyUnlocked;
						break;
					case 3:
						flag = !scoreSystem.meatFortyUnlocked;
						break;
					case 4:
						flag = !scoreSystem.ronFortyUnlocked;
						break;
					case 5:
						flag = !scoreSystem.nyanFortyUnlocked;
						break;
					}
				}
				if ((i == 3 && this.numCats_[currentLevel] == 5) || (i == 2 && this.numCats_[currentLevel] == 4) || (i == 2 && this.numCats_[currentLevel] == 3))
				{
					switch (currentLevel)
					{
					case 0:
						flag = !scoreSystem.seaSixtyUnlocked;
						break;
					case 1:
						flag = !scoreSystem.cloudSixtyUnlocked;
						break;
					case 2:
						flag = !scoreSystem.lavaSixtyUnlocked;
						break;
					case 3:
						flag = !scoreSystem.meatSixtyUnlocked;
						break;
					case 4:
						flag = !scoreSystem.ronSixtyUnlocked;
						break;
                    case 5:
                        flag = !scoreSystem.nyanSixtyUnlocked;
                        break;
                    }
                }
				if ((i == 4 && this.numCats_[currentLevel] == 5) || (i == 3 && this.numCats_[currentLevel] == 4) || (i == 3 && this.numCats_[currentLevel] == 3))
				{
					switch (currentLevel)
					{
					case 0:
						flag = !scoreSystem.seaEightyUnlocked;
						break;
					case 1:
						flag = !scoreSystem.cloudEightyUnlocked;
						break;
					case 2:
						flag = !scoreSystem.lavaEightyUnlocked;
						break;
					case 3:
						flag = !scoreSystem.meatEightyUnlocked;
						break;
					case 4:
						flag = !scoreSystem.ronEightyUnlocked;
						break;
                    case 5:
                        flag = !scoreSystem.nyanEightyUnlocked;
                        break;
                    }
                }
				if (flag)
				{
					spriteBatch.Draw(Global.selectCatTex, new Vector2(((float)i + 0.5f) * (1280f / (float)this.numCats_[currentLevel]), 240f), (Rectangle?)new Rectangle(0, 1682, 240, 240), Color.White, 0f, new Vector2(120f, 120f), 1f, SpriteEffects.None, 0f);
					if (i == 1 && this.numCats_[currentLevel] == 5)
					{
						spriteBatch.DrawString(Global.menuFont, this.lockedNames_[0], new Vector2(((float)i + 0.5f) * (1280f / (float)this.numCats_[currentLevel]) - 1.5f, 433.5f), Color.White, 0f, Global.menuFont.MeasureString(this.lockedNames_[0]) / 2f, scale, SpriteEffects.None, 0f);
						spriteBatch.DrawString(Global.menuFont, this.lockedNames_[0], new Vector2(((float)i + 0.5f) * (1280f / (float)this.numCats_[currentLevel]), 432f), Color.Black, 0f, Global.menuFont.MeasureString(this.lockedNames_[0]) / 2f, scale, SpriteEffects.None, 0f);
					}
					if ((i == 2 && this.numCats_[currentLevel] == 5) || (i == 1 && this.numCats_[currentLevel] == 4) || (i == 1 && this.numCats_[currentLevel] == 3))
					{
						spriteBatch.DrawString(Global.menuFont, this.lockedNames_[1], new Vector2(((float)i + 0.5f) * (1280f / (float)this.numCats_[currentLevel]) - 1.5f, 433.5f), Color.White, 0f, Global.menuFont.MeasureString(this.lockedNames_[1]) / 2f, scale, SpriteEffects.None, 0f);
						spriteBatch.DrawString(Global.menuFont, this.lockedNames_[1], new Vector2(((float)i + 0.5f) * (1280f / (float)this.numCats_[currentLevel]), 432f), Color.Black, 0f, Global.menuFont.MeasureString(this.lockedNames_[1]) / 2f, scale, SpriteEffects.None, 0f);
					}
					if ((i == 3 && this.numCats_[currentLevel] == 5) || (i == 2 && this.numCats_[currentLevel] == 4) || (i == 2 && this.numCats_[currentLevel] == 3))
					{
						spriteBatch.DrawString(Global.menuFont, this.lockedNames_[2], new Vector2(((float)i + 0.5f) * (1280f / (float)this.numCats_[currentLevel]) - 1.5f, 433.5f), Color.White, 0f, Global.menuFont.MeasureString(this.lockedNames_[2]) / 2f, scale, SpriteEffects.None, 0f);
						spriteBatch.DrawString(Global.menuFont, this.lockedNames_[2], new Vector2(((float)i + 0.5f) * (1280f / (float)this.numCats_[currentLevel]), 432f), Color.Black, 0f, Global.menuFont.MeasureString(this.lockedNames_[2]) / 2f, scale, SpriteEffects.None, 0f);
					}
					if ((i == 4 && this.numCats_[currentLevel] == 5) || (i == 3 && this.numCats_[currentLevel] == 4) || (i == 3 && this.numCats_[currentLevel] == 3))
					{
						spriteBatch.DrawString(Global.menuFont, this.lockedNames_[3], new Vector2(((float)i + 0.5f) * (1280f / (float)this.numCats_[currentLevel]) - 1.5f, 433.5f), Color.White, 0f, Global.menuFont.MeasureString(this.lockedNames_[3]) / 2f, scale, SpriteEffects.None, 0f);
						spriteBatch.DrawString(Global.menuFont, this.lockedNames_[3], new Vector2(((float)i + 0.5f) * (1280f / (float)this.numCats_[currentLevel]), 432f), Color.Black, 0f, Global.menuFont.MeasureString(this.lockedNames_[3]) / 2f, scale, SpriteEffects.None, 0f);
					}
				}
			}
		}

		public int getCurrentCat()
		{
			return this.currentCat;
		}

		public void SetLastGameState(GameState gameState)
		{
			this.lastGameState = gameState;
		}
	}
}
