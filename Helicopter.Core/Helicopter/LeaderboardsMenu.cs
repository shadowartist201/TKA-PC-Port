using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Helicopter.Core
{
	internal class LeaderboardsMenu : Menu
	{
		private GameState lastGameState = GameState.MAIN_MENU;

		public LeaderboardsMenu()
			: base(horizontal: false)
		{
			base.AddMenuItem(new MenuItem(Global.leaderboardTex, new Rectangle(0, 722, 173, 40), new Vector2(1086.5f, 654f)));
		}

		public void Update(float dt, InputState currInput, ref GameState gameState)
		{
            Rectangle stats_back = new(1000, 634, 173, 40);
            base.Update(dt, currInput);
            if (currInput.IsButtonPressed(Buttons.A) || currInput.IsButtonPressed(Buttons.B) || (stats_back.Contains((Game1.touchLocations[0].Position - Game1.touchOffset) * Game1.resolutionDifference) && currInput.IsThingTouched()))
            {
                Global.PlayCatSound();
				gameState = this.lastGameState;
			}
		}

		public void Draw(SpriteBatch spriteBatch, ScoreSystem scoreSystem)
		{
			this.DrawBackground(spriteBatch);
			base.Draw(spriteBatch);
			scoreSystem.DrawAllScores(spriteBatch);
		}

		private void DrawBackground(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(Global.leaderboardTex, Vector2.Zero, (Rectangle?)new Rectangle(0, 0, 1280, 720), Color.White);
		}

		public void SetLastGameState(GameState gameState)
		{
			this.lastGameState = gameState;
		}
	}
}
