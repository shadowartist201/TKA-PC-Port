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
				//Global.audioEngine.GetCategory("Default").SetVolume(1f);
			}
			else
			{
				//Global.audioEngine.GetCategory("Default").SetVolume(0f);
			}
			if (this.vibrationOn)
			{
				Global.SetVibrationOn(on: true);
			}
			else
			{
				Global.SetVibrationOn(on: false);
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
