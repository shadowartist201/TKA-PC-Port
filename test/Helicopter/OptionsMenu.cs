using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Diagnostics;
using System.Security.Policy;

namespace Helicopter
{
	internal class OptionsMenu : Menu
	{
		private GameState lastGameState;

		private bool musicOn = true;

		private bool sfxOn = true;

		private bool vibrationOn = true;

		private bool fullscreenOn = false;

		private bool resIs1080 = false;

		private bool resIs480 = false;

		public static bool displayMenu = false;

		private int startingResIndex = 1;

		public OptionsMenu()
			: base(horizontal: false)
		{
			base.AddMenuItem(new MenuItem(Global.optionsTex, new Rectangle(0, 722, 190, 41), new Vector2(223f, 191.5f)));
			base.AddMenuItem(new MenuItem(Global.optionsTex, new Rectangle(0, 765, 125, 41), new Vector2(190.5f, 254.5f)));
			base.AddMenuItem(new MenuItem(Global.optionsTex, new Rectangle(0, 808, 412, 41), new Vector2(334f, 318.5f)));
			base.AddMenuItem(new MenuItem(Global.optionsTex, new Rectangle(0, 851, 364, 54), new Vector2(640f, 490f)));
			base.AddMenuItem(new MenuItem(Global.optionsTex, new Rectangle(0, 907, 236, 54), new Vector2(640f, 587f)));
        }

		public void Update(float dt, InputState currInput, ref GameState gameState)
		{
			Debug.WriteLine(this.altIndex_);
			if (displayMenu)
			{
				if (currInput.IsButtonPressed(Buttons.DPadDown) && this.altIndex_ < 2)
				{
					this.altIndex_++;
				}
				if (currInput.IsButtonPressed(Buttons.DPadUp) && this.altIndex_ > 0)
				{
					this.altIndex_--;
				}
				if (currInput.IsButtonPressed(Buttons.DPadLeft))
				{
					switch (base.altIndex_)
					{
						case 0:
							this.fullscreenOn = !this.fullscreenOn;
							break;
						case 1:
							this.startingResIndex--;
							if (this.startingResIndex == -1)
								this.startingResIndex = 2;
							break;
					}
				}
				if (currInput.IsButtonPressed(Buttons.DPadRight))
				{
					switch (base.altIndex_)
					{
						case 0:
							this.fullscreenOn = !this.fullscreenOn;
							break;
						case 1:
							this.startingResIndex++;
							if (this.startingResIndex == 3)
								this.startingResIndex = 0;
							break;
					}
				}
				if (currInput.IsButtonPressed(Buttons.A))
				{
					if (base.altIndex_ == 2)
					{
						this.ChangeSettings();
						displayMenu = false;
						altIndex_ = 0;
					}
				}
				if (currInput.IsButtonPressed(Buttons.B) || currInput.IsButtonPressed(Buttons.Start))
				{
					displayMenu = false;
					altIndex_ = 0;
				}
                base.Update(dt, currInput);
            }
			else
			{
				if (this.index_ == 3 || this.index_ == 4)
				{
					if (currInput.IsButtonPressed(Buttons.DPadLeft))
					{
						displayMenu = true;
					}
				}
				base.Update(dt, currInput);
				if (currInput.IsButtonPressed(Buttons.A))
				{
					Global.PlayCatSound();
					switch (base.index_)
					{
						case 0:
							this.musicOn = !this.musicOn;
							break;
						case 1:
							this.sfxOn = !this.sfxOn;
							break;
						case 2:
							this.vibrationOn = !this.vibrationOn;
							break;
						case 3:
							base.index_ = 0;
							gameState = GameState.CREDITS;
							break;
						case 4:
							base.index_ = 0;
							gameState = this.lastGameState;
							break;
					}
					if (base.index_ < 3)
					{
						this.ChangeSettings();
					}
				}
				if (currInput.IsButtonPressed(Buttons.DPadLeft))
				{
					Global.PlayCatSound();
					switch (base.index_)
					{
						case 0:
							this.musicOn = true;
							break;
						case 1:
							this.sfxOn = true;
							break;
						case 2:
							this.vibrationOn = true;
							break;
					}
					if (base.index_ < 3)
					{
						this.ChangeSettings();
					}
				}
				if (currInput.IsButtonPressed(Buttons.DPadRight))
				{
					Global.PlayCatSound();
					switch (base.index_)
					{
						case 0:
							this.musicOn = false;
							break;
						case 1:
							this.sfxOn = false;
							break;
						case 2:
							this.vibrationOn = false;
							break;
					}
					if (base.index_ < 3)
					{
						this.ChangeSettings();
					}
				}
				if (currInput.IsButtonPressed(Buttons.B))
				{
					Global.PlayCatSound();
					base.index_ = 0;
					gameState = this.lastGameState;
				}
			}
		}

		private void ChangeSettings()
		{
			if (this.musicOn)
			{
				MediaPlayer.Volume = 0.1f;
			}
			else
			{
				MediaPlayer.Volume = 0f;
			}
			if (this.sfxOn)
			{
				Global.audioEngine.GetCategory("Default").SetVolume(1f);
			}
			else
			{
				Global.audioEngine.GetCategory("Default").SetVolume(0f);
			}
			if (this.vibrationOn)
			{
				Global.SetVibrationOn(on: true);
			}
			else
			{
				Global.SetVibrationOn(on: false);
			}
			if (this.fullscreenOn)
			{
				Global.SetFullscreenOn(on: true);
			}
			else if (!this.fullscreenOn)
			{
				Global.SetFullscreenOn(on: false);
			}
			if (startingResIndex == 2)
			{
				Global.SetResolution(3);
			}
			else if (startingResIndex == 0)
			{
				Global.SetResolution(1);
			}
			else if (startingResIndex == 1)
			{
				Global.SetResolution(2);
			}
            Resolution.SetResolution((int)Global.resolution.X, (int)Global.resolution.Y, Global.fullscreenOn); //outer resolution
        }

		public new void Draw(SpriteBatch spriteBatch)
		{
			this.DrawBackground(spriteBatch);
			base.Draw(spriteBatch);
		}

		private void DrawBackground(SpriteBatch spriteBatch)
		{
            spriteBatch.Draw(Global.optionsTex, Vector2.Zero, (Rectangle?)new Rectangle(0, 0, 1280, 720), Color.White);
            if (this.fullscreenOn)
            {
                    if (this.altIndex_ == 0 && displayMenu)
                        spriteBatch.Draw(Global.option_fullOn, new Vector2(21f, 524f), Color.Cyan);
                    else
                        spriteBatch.Draw(Global.option_fullOn, new Vector2(21f, 524f), Color.Black);
            }
            else
            {
                if (this.altIndex_ == 0 && displayMenu)
                    spriteBatch.Draw(Global.option_fullOff, new Vector2(21f, 524f), Color.Cyan);
                else
                    spriteBatch.Draw(Global.option_fullOff, new Vector2(21f, 524f), Color.Black);
            }
            if (startingResIndex == 2)
            {
                if (this.altIndex_ == 1 && displayMenu)
                    spriteBatch.Draw(Global.option_res1080, new Vector2(22f, 576f), Color.Cyan);
                else
                    spriteBatch.Draw(Global.option_res1080, new Vector2(22f, 576f), Color.Black);
            }
            else if (startingResIndex == 0)
            {
                if (this.altIndex_ == 1 && displayMenu)
                    spriteBatch.Draw(Global.option_res480, new Vector2(22f, 576f), Color.Cyan);
                else
                    spriteBatch.Draw(Global.option_res480, new Vector2(22f, 576f), Color.Black);
            }
            else if (startingResIndex == 1)
            {
                if (this.altIndex_ == 1 && displayMenu)
                    spriteBatch.Draw(Global.option_res720, new Vector2(22f, 577f), Color.Cyan);
                else
                    spriteBatch.Draw(Global.option_res720, new Vector2(22f, 577f), Color.Black);
            }
            if (altIndex_ == 2 && displayMenu)
                spriteBatch.Draw(Global.option_apply, new Vector2(150f, 634f), Color.Cyan);
            else
                spriteBatch.Draw(Global.option_apply, new Vector2(150f, 634f), Color.Black);
            spriteBatch.Draw(Global.option_display, new Vector2(59f, 465f), Color.Black);
			if (this.musicOn)
			{
				this.DrawOnOff(spriteBatch, new Vector2(900f, 171f));
			}
			else
			{
				this.DrawOffOn(spriteBatch, new Vector2(900f, 171f));
			}
			if (this.sfxOn)
			{
				this.DrawOnOff(spriteBatch, new Vector2(900f, 234f));
			}
			else
			{
				this.DrawOffOn(spriteBatch, new Vector2(900f, 234f));
			}
			if (this.vibrationOn)
			{
				this.DrawOnOff(spriteBatch, new Vector2(900f, 298f));
			}
			else
			{
				this.DrawOffOn(spriteBatch, new Vector2(900f, 298f));
			}
		}

		private void DrawOnOff(SpriteBatch spriteBatch, Vector2 position)
		{
			spriteBatch.Draw(Global.optionsTex, position, (Rectangle?)new Rectangle(747, 722, 89, 41), Color.White);
			spriteBatch.Draw(Global.optionsTex, position + new Vector2(160f, 0f), (Rectangle?)new Rectangle(414, 722, 119, 41), Color.White);
		}

		private void DrawOffOn(SpriteBatch spriteBatch, Vector2 position)
		{
			spriteBatch.Draw(Global.optionsTex, position, (Rectangle?)new Rectangle(653, 722, 92, 41), Color.White);
			spriteBatch.Draw(Global.optionsTex, position + new Vector2(160f, 0f), (Rectangle?)new Rectangle(535, 722, 116, 41), Color.White);
		}

		public void SetLastGameState(GameState gameState)
		{
			this.lastGameState = gameState;
		}
	}
}
