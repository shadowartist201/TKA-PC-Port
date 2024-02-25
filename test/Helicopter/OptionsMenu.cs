using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Helicopter
{
	internal class OptionsMenu : Menu
	{
		private GameState lastGameState;

		private bool musicOn = true;

		private bool sfxOn = true;

		private bool vibrationOn = true;

		private bool fullscreenOn = false;

		private int resIndex = 1;

		private MenuItem[] resOptions = new MenuItem[3];

		private int resValue = 1;

		public OptionsMenu()
			: base(horizontal: false)
		{
			base.AddMenuItem(new MenuItem(Global.optionsTex, new Rectangle(0, 722, 190, 41), new Vector2(130f+190/2, 174f+41/2))); //musik
			base.AddMenuItem(new MenuItem(Global.optionsTex, new Rectangle(0, 765, 125, 41), new Vector2(130f+125/2, 237f+41/2))); //sfx
			base.AddMenuItem(new MenuItem(Global.optionsTex, new Rectangle(0, 808, 412, 41), new Vector2(130f+412/2, 300f+41/2))); //vibrayshun
            base.AddMenuItem(new MenuItem(Global.optionsTex, new Rectangle(417, 808, 393, 40), new Vector2(130f+393/2, 363f+40/2))); //fullscreen
            base.AddMenuItem(new MenuItem(Global.optionsTex, new Rectangle(417, 765, 413, 40), new Vector2(130f+413/2, 426f+40/2))); //resolushun
            base.AddMenuItem(new MenuItem(Global.optionsTex, new Rectangle(0, 851, 364, 54), new Vector2(456f+364/2, 514f+54/2))); //kredits
			base.AddMenuItem(new MenuItem(Global.optionsTex, new Rectangle(0, 907, 236, 54), new Vector2(520f+236/2, 612f+54/2))); //bakc

			resOptions[0] = new MenuItem(Global.optionsTex, new Rectangle(416, 850, 342, 40), new Vector2(854f+342/2, 425f+20)); //1080
			resOptions[1] = new MenuItem(Global.optionsTex, new Rectangle(416, 892, 320, 40), new Vector2(854f+320/2, 425f+20)); //720
			resOptions[2] = new MenuItem(Global.optionsTex, new Rectangle(837, 723, 298, 40), new Vector2(876f+298/2, 425f+20)); //480
        }

		public void Update(float dt, InputState currInput, ref GameState gameState)
		{
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
						this.fullscreenOn = !this.fullscreenOn;
						break;
					case 4:
						ResSubMenu(currInput);
						break;
					case 5:
						base.index_ = 0;
						gameState = GameState.CREDITS;
						break;
					case 6:
						base.index_ = 0;
						gameState = this.lastGameState;
						break;
				}
				if (base.index_ < 5)
				{
					this.ChangeSettings();
				}
			}
			if (currInput.IsButtonPressed(Buttons.DPadLeft))
			{
				if (base.index_ != 4)
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
					case 3:
						this.fullscreenOn = true;
						break;
					case 4:
						ResSubMenu(currInput);
						break;
				}
				if (base.index_ < 4)
				{
					this.ChangeSettings();
				}
			}
			if (currInput.IsButtonPressed(Buttons.DPadRight))
			{
				if (base.index_ != 4)
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
                    case 3:
                        this.fullscreenOn = false;
                        break;
					case 4:
						ResSubMenu(currInput);
						break;
                }
                if (base.index_ < 4)
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
			else
			{
				Global.SetFullscreenOn(on: false);
			}
			switch(this.resValue)
			{
				case 0:
					Global.SetResolution(3);
					break;
				case 1:
					Global.SetResolution(2);
					break;
				case 2:
					Global.SetResolution(1);
					break;
			}
            Resolution.SetResolution((int)Global.resolution.X, (int)Global.resolution.Y, Global.fullscreenOn);
        }

		private void ResSubMenu(InputState currInput)
		{
            if (currInput.IsButtonPressed(Buttons.A))
			{
				this.resValue = this.resIndex;
			}
            if (currInput.IsButtonPressed(Buttons.DPadLeft))
			{
                this.resIndex--;
                if (this.resIndex == -1)
                    this.resIndex = 2;
            }
			if (currInput.IsButtonPressed(Buttons.DPadRight))
			{
				this.resIndex++;
				if (this.resIndex == 3)
					this.resIndex = 0;
			}
		}

		public new void Draw(SpriteBatch spriteBatch)
		{
			this.DrawBackground(spriteBatch);
			base.Draw(spriteBatch);
		}

		private void DrawBackground(SpriteBatch spriteBatch)
		{
            spriteBatch.Draw(Global.optionsTex, Vector2.Zero, (Rectangle?)new Rectangle(0, 0, 1280, 720), Color.White);
			this.resOptions[this.resIndex].Draw(spriteBatch);
			if (this.musicOn)
			{
				this.DrawOnOff(spriteBatch, new Vector2(901f, 175f));
			}
			else
			{
				this.DrawOffOn(spriteBatch, new Vector2(901f, 175f));
			}
			if (this.sfxOn)
			{
				this.DrawOnOff(spriteBatch, new Vector2(901f, 237f));
			}
			else
			{
				this.DrawOffOn(spriteBatch, new Vector2(901f, 237f));
			}
			if (this.vibrationOn)
			{
				this.DrawOnOff(spriteBatch, new Vector2(901f, 300f));
			}
			else
			{
				this.DrawOffOn(spriteBatch, new Vector2(901f, 300f));
			}
            if (this.fullscreenOn)
            {
                this.DrawOnOff(spriteBatch, new Vector2(901f, 363f));
            }
            else
            {
                this.DrawOffOn(spriteBatch, new Vector2(901f, 363f));
            }
        }

		private void DrawOnOff(SpriteBatch spriteBatch, Vector2 position)
		{
			spriteBatch.Draw(Global.optionsTex, position, (Rectangle?)new Rectangle(747, 722, 89, 41), Color.White); //blue on
			spriteBatch.Draw(Global.optionsTex, position + new Vector2(160f, 0f), (Rectangle?)new Rectangle(414, 722, 119, 41), Color.White); //white off
		}

		private void DrawOffOn(SpriteBatch spriteBatch, Vector2 position)
		{
			spriteBatch.Draw(Global.optionsTex, position, (Rectangle?)new Rectangle(653, 722, 92, 41), Color.White); //white on
			spriteBatch.Draw(Global.optionsTex, position + new Vector2(160f, 0f), (Rectangle?)new Rectangle(535, 722, 116, 41), Color.White); //blue off
		}

		public void SetLastGameState(GameState gameState)
		{
			this.lastGameState = gameState;
		}
	}
}
