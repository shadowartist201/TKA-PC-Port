using System;
using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Helicopter
{
	internal class MainMenu : Menu
	{
		private bool canBuyGame;

		private float shineRotation;

		private int titleAnimFrame;

		private float titleAnimTime;

		private float titleAnimTimer;

		private int titleAnimNumFrames;

		private int catAnimFrame;

		private float catAnimTime;

		private float catAnimTimer;

		private int catAnimNumFrames;

		private Vector2[] starPositions = new Vector2[13];

		private float[] starRotations = new float[13];

		private float[] starScales = new float[13];

		private Vector2[] starScaleInfos = new Vector2[13];

		private float[] starScaleRates = new float[13];

		public MainMenu()
			: base(horizontal: true)
		{
			this.shineRotation = 2.31779718f;
			this.titleAnimFrame = 0;
			this.titleAnimTime = 71f / (678f * (float)Math.PI);
			this.titleAnimTimer = 0f;
			this.titleAnimNumFrames = 20;
			this.catAnimFrame = 0;
			this.catAnimTime = 71f / (678f * (float)Math.PI);
			this.catAnimTimer = 0f;
			this.catAnimNumFrames = 9;
			ref Vector2 reference = ref this.starPositions[0];
			reference = new Vector2(90f, 148f);
			ref Vector2 reference2 = ref this.starPositions[1];
			reference2 = new Vector2(224f, 58f);
			ref Vector2 reference3 = ref this.starPositions[2];
			reference3 = new Vector2(420f, -6f);
			ref Vector2 reference4 = ref this.starPositions[3];
			reference4 = new Vector2(370f, 74f);
			ref Vector2 reference5 = ref this.starPositions[4];
			reference5 = new Vector2(506f, 40f);
			ref Vector2 reference6 = ref this.starPositions[5];
			reference6 = new Vector2(666f, 80f);
			ref Vector2 reference7 = ref this.starPositions[6];
			reference7 = new Vector2(766f, 28f);
			ref Vector2 reference8 = ref this.starPositions[7];
			reference8 = new Vector2(1002f, -4f);
			ref Vector2 reference9 = ref this.starPositions[8];
			reference9 = new Vector2(1144f, 50f);
			ref Vector2 reference10 = ref this.starPositions[9];
			reference10 = new Vector2(1020f, 94f);
			ref Vector2 reference11 = ref this.starPositions[10];
			reference11 = new Vector2(1156f, 202f);
			ref Vector2 reference12 = ref this.starPositions[11];
			reference12 = new Vector2(1068f, 216f);
			ref Vector2 reference13 = ref this.starPositions[12];
			reference13 = new Vector2(1202f, 286f);
			this.starRotations[0] = 0.523599f;
			this.starRotations[1] = 0.349066f;
			this.starRotations[2] = 0.087266f;
			this.starRotations[3] = 0f;
			this.starRotations[4] = 0.314159f;
			this.starRotations[5] = 0.575959f;
			this.starRotations[6] = 0.523599f;
			this.starRotations[7] = 0.5236f;
			this.starRotations[8] = -0.2618f;
			this.starRotations[9] = 0.36652f;
			this.starRotations[10] = 0.31416f;
			this.starRotations[11] = 0.5236f;
			this.starRotations[12] = 0.13963f;
			this.starScales[0] = 0.6f;
			this.starScales[1] = 0.766f;
			this.starScales[2] = 0.68f;
			this.starScales[3] = 1f;
			this.starScales[4] = 0.933f;
			this.starScales[5] = 0.77f;
			this.starScales[6] = 0.47f;
			this.starScales[7] = 0.572f;
			this.starScales[8] = 0.755f;
			this.starScales[9] = 0.852f;
			this.starScales[10] = 0.441f;
			this.starScales[11] = 0.608f;
			this.starScales[12] = 0.458f;
			ref Vector2 reference14 = ref this.starScaleInfos[0];
			reference14 = new Vector2(Global.RandomBetween(0f, 0.4f), Global.RandomBetween(0.6f, 1f));
			ref Vector2 reference15 = ref this.starScaleInfos[1];
			reference15 = new Vector2(Global.RandomBetween(0f, 0.4f), Global.RandomBetween(0.6f, 1f));
			ref Vector2 reference16 = ref this.starScaleInfos[2];
			reference16 = new Vector2(Global.RandomBetween(0f, 0.4f), Global.RandomBetween(0.6f, 1f));
			ref Vector2 reference17 = ref this.starScaleInfos[3];
			reference17 = new Vector2(Global.RandomBetween(0f, 0.4f), Global.RandomBetween(0.6f, 1f));
			ref Vector2 reference18 = ref this.starScaleInfos[4];
			reference18 = new Vector2(Global.RandomBetween(0f, 0.4f), Global.RandomBetween(0.6f, 1f));
			ref Vector2 reference19 = ref this.starScaleInfos[5];
			reference19 = new Vector2(Global.RandomBetween(0f, 0.4f), Global.RandomBetween(0.6f, 1f));
			ref Vector2 reference20 = ref this.starScaleInfos[6];
			reference20 = new Vector2(Global.RandomBetween(0f, 0.4f), Global.RandomBetween(0.6f, 1f));
			ref Vector2 reference21 = ref this.starScaleInfos[7];
			reference21 = new Vector2(Global.RandomBetween(0f, 0.4f), Global.RandomBetween(0.6f, 1f));
			ref Vector2 reference22 = ref this.starScaleInfos[8];
			reference22 = new Vector2(Global.RandomBetween(0f, 0.4f), Global.RandomBetween(0.6f, 1f));
			ref Vector2 reference23 = ref this.starScaleInfos[9];
			reference23 = new Vector2(Global.RandomBetween(0f, 0.4f), Global.RandomBetween(0.6f, 1f));
			ref Vector2 reference24 = ref this.starScaleInfos[10];
			reference24 = new Vector2(Global.RandomBetween(0f, 0.4f), Global.RandomBetween(0.6f, 1f));
			ref Vector2 reference25 = ref this.starScaleInfos[11];
			reference25 = new Vector2(Global.RandomBetween(0f, 0.4f), Global.RandomBetween(0.6f, 1f));
			ref Vector2 reference26 = ref this.starScaleInfos[12];
			reference26 = new Vector2(Global.RandomBetween(0f, 0.4f), Global.RandomBetween(0.6f, 1f));
			this.starScaleRates[0] = Global.RandomBetween(1.25f, 2.5f);
			this.starScaleRates[1] = Global.RandomBetween(1.25f, 2.5f);
			this.starScaleRates[2] = Global.RandomBetween(1.25f, 2.5f);
			this.starScaleRates[3] = Global.RandomBetween(1.25f, 2.5f);
			this.starScaleRates[4] = Global.RandomBetween(1.25f, 2.5f);
			this.starScaleRates[5] = Global.RandomBetween(1.25f, 2.5f);
			this.starScaleRates[6] = Global.RandomBetween(1.25f, 2.5f);
			this.starScaleRates[7] = Global.RandomBetween(1.25f, 2.5f);
			this.starScaleRates[8] = Global.RandomBetween(1.25f, 2.5f);
			this.starScaleRates[9] = Global.RandomBetween(1.25f, 2.5f);
			this.starScaleRates[10] = Global.RandomBetween(1.25f, 2.5f);
			this.starScaleRates[11] = Global.RandomBetween(1.25f, 2.5f);
			this.starScaleRates[12] = Global.RandomBetween(1.25f, 2.5f);
		}

		public void Update(float dt, InputState currInput, ref GameState gameState)
		{
			this.UpdateBackground(dt);
			base.Update(dt, currInput);
			Rectangle playButton = new(123, 579, 169, 42);
			Rectangle optionsButton = new(363, 579, 284, 42);
			Rectangle statsButton = new(719, 579, 211, 42);
			Rectangle exitButton = new(1002, 579, 156, 42);
            if (playButton.Contains((Game1.touchLocations[0].Position - Game1.touchOffset) * Game1.resolutionDifference) && currInput.IsThingTouched())
            {
                Global.PlayCatSound();
                gameState = GameState.STAGE_SELECT;
            }
            else if (optionsButton.Contains((Game1.touchLocations[0].Position - Game1.touchOffset) * Game1.resolutionDifference) && currInput.IsThingTouched())
            {
                Global.PlayCatSound();
                gameState = GameState.OPTIONS;
            }
            else if (statsButton.Contains((Game1.touchLocations[0].Position - Game1.touchOffset) * Game1.resolutionDifference) && currInput.IsThingTouched())
            {
                Global.PlayCatSound();
                gameState = GameState.LEADERBOARDS;
            }
            else if (exitButton.Contains((Game1.touchLocations[0].Position - Game1.touchOffset) * Game1.resolutionDifference) && currInput.IsThingTouched())
            {
                Global.PlayCatSound();
                gameState = GameState.EXIT;
            }
            /*else if (!currInput.IsButtonPressed(Buttons.A))
			{
				return;
			}
			Global.PlayCatSound();
			switch (base.index_)
			{
			case 0:
				base.index_ = 0;
				gameState = GameState.STAGE_SELECT;
				break;
			case 1:
				base.index_ = 0;
				gameState = GameState.OPTIONS;
				break;
			case 2:
				base.index_ = 0;
				gameState = GameState.LEADERBOARDS;
				break;
			case 3:
				if (this.canBuyGame)
				{
					//if (Global.IsTrialMode && Global.CanBuyGame())
					//{
						//Guide.ShowMarketplace(Global.playerIndex.Value);
					//}
				}
				else
				{
					gameState = GameState.EXIT;
				}
				break;
			case 4:
				gameState = GameState.EXIT;
				break;
			}*/
		}

		public void UpdateBackground(float dt)
		{
			this.shineRotation += 0.464f * dt;
			this.titleAnimTimer += dt;
			if (this.titleAnimTimer > this.titleAnimTime)
			{
				this.titleAnimFrame = (this.titleAnimFrame + 1) % this.titleAnimNumFrames;
				this.titleAnimTimer = 0f;
			}
			this.catAnimTimer += dt;
			if (this.catAnimTimer > this.catAnimTime)
			{
				this.catAnimFrame = (this.catAnimFrame + 1) % this.catAnimNumFrames;
				this.catAnimTimer = 0f;
			}
			for (int i = 0; i < this.starPositions.Length; i++)
			{
				this.starScales[i] += this.starScaleRates[i] * dt;
				if (this.starScales[i] < this.starScaleInfos[i].X)
				{
					this.starScales[i] = this.starScaleInfos[i].X;
					this.starScaleRates[i] = 0f - this.starScaleRates[i];
				}
				else if (this.starScales[i] > this.starScaleInfos[i].Y)
				{
					this.starScales[i] = this.starScaleInfos[i].Y;
					this.starScaleRates[i] = 0f - this.starScaleRates[i];
				}
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
			if (this.canBuyGame && (base.menuItems_.Count == 4 || base.menuItems_.Count == 0))
			{
				base.menuItems_.Clear();
				base.index_ = 0;
				base.AddMenuItem(new MenuItem(Global.mainTex, new Rectangle(1134, 722, 169, 42), new Vector2(204f, 600f)));
				base.AddMenuItem(new MenuItem(Global.mainTex, new Rectangle(1134, 766, 284, 42), new Vector2(450f, 600f)));
				base.AddMenuItem(new MenuItem(Global.mainTex, new Rectangle(1132, 810, 211, 42), new Vector2(718f, 600f)));
				base.AddMenuItem(new MenuItem(Global.mainTex, new Rectangle(1132, 854, 126, 43), new Vector2(907f, 600f)));
				base.AddMenuItem(new MenuItem(Global.mainTex, new Rectangle(1132, 899, 156, 42), new Vector2(1068f, 600f)));
				base.SetItemVertices();
			}
			if (!this.canBuyGame && (base.menuItems_.Count == 5 || base.menuItems_.Count == 0))
			{
				base.menuItems_.Clear();
				base.index_ = 0;
				base.AddMenuItem(new MenuItem(Global.mainTex, new Rectangle(1134, 722, 169, 42), new Vector2(207f, 600f)));
				base.AddMenuItem(new MenuItem(Global.mainTex, new Rectangle(1134, 766, 284, 42), new Vector2(505f, 600f)));
				base.AddMenuItem(new MenuItem(Global.mainTex, new Rectangle(1132, 810, 211, 42), new Vector2(824f, 600f)));
				base.AddMenuItem(new MenuItem(Global.mainTex, new Rectangle(1132, 899, 156, 42), new Vector2(1080f, 600f)));
				base.SetItemVertices();
			}
			this.DrawBackground(spriteBatch);
			base.Draw(spriteBatch);
		}

		public void DrawBackground(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(Global.mainTex, Vector2.Zero, (Rectangle?)new Rectangle(0, 0, 1280, 720), Color.White);
			spriteBatch.Draw(Global.mainCircleTex, new Vector2(820f, 288f), (Rectangle?)null, Color.White, this.shineRotation, new Vector2(927f, 927f), 1f, SpriteEffects.None, 0f);
			spriteBatch.Draw(Global.mainTex, new Vector2(74f, 0f), (Rectangle?)new Rectangle(0, 720, 1132, 489), Color.White);
			spriteBatch.Draw(Global.mainCatTex, new Vector2(820f, 288f), (Rectangle?)new Rectangle(this.catAnimFrame % 3 * 445, this.catAnimFrame / 3 * 319 + 1, 445, 318), Color.White, 0f, new Vector2(222.5f, 159.5f), 1f, SpriteEffects.None, 0f);
			spriteBatch.Draw(Global.mainTitleTex, new Vector2(200f, 222f), (Rectangle?)new Rectangle(this.titleAnimFrame % 4 * 816, this.titleAnimFrame / 4 * 320, 816, 320), Color.White);
			for (int i = 0; i < this.starPositions.Length; i++)
			{
				spriteBatch.Draw(Global.mainStarTex, this.starPositions[i], (Rectangle?)null, Color.White, this.starRotations[i], new Vector2(20.5f, 20f), this.starScales[i], SpriteEffects.None, 0f);
			}
		}
	}
}
