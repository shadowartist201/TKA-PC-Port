using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Helicopter;

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

	private Vector2[] starPositions = (Vector2[])(object)new Vector2[13];

	private float[] starRotations = new float[13];

	private float[] starScales = new float[13];

	private Vector2[] starScaleInfos = (Vector2[])(object)new Vector2[13];

	private float[] starScaleRates = new float[13];

	public MainMenu()
		: base(horizontal: true)
	{
																																																																																																										shineRotation = 2.3177972f;
		titleAnimFrame = 0;
		titleAnimTime = 1f / 30f;
		titleAnimTimer = 0f;
		titleAnimNumFrames = 20;
		catAnimFrame = 0;
		catAnimTime = 1f / 30f;
		catAnimTimer = 0f;
		catAnimNumFrames = 9;
	Vector2 reference =starPositions[0];
		reference = new Vector2(90f, 148f);
	Vector2 reference2 =starPositions[1];
		reference2 = new Vector2(224f, 58f);
	Vector2 reference3 =starPositions[2];
		reference3 = new Vector2(420f, -6f);
	Vector2 reference4 =starPositions[3];
		reference4 = new Vector2(370f, 74f);
	Vector2 reference5 =starPositions[4];
		reference5 = new Vector2(506f, 40f);
	Vector2 reference6 =starPositions[5];
		reference6 = new Vector2(666f, 80f);
	Vector2 reference7 =starPositions[6];
		reference7 = new Vector2(766f, 28f);
	Vector2 reference8 =starPositions[7];
		reference8 = new Vector2(1002f, -4f);
	Vector2 reference9 =starPositions[8];
		reference9 = new Vector2(1144f, 50f);
	Vector2 reference10 =starPositions[9];
		reference10 = new Vector2(1020f, 94f);
	Vector2 reference11 =starPositions[10];
		reference11 = new Vector2(1156f, 202f);
	Vector2 reference12 =starPositions[11];
		reference12 = new Vector2(1068f, 216f);
	Vector2 reference13 =starPositions[12];
		reference13 = new Vector2(1202f, 286f);
		starRotations[0] = 0.523599f;
		starRotations[1] = 0.349066f;
		starRotations[2] = 0.087266f;
		starRotations[3] = 0f;
		starRotations[4] = 0.314159f;
		starRotations[5] = 0.575959f;
		starRotations[6] = 0.523599f;
		starRotations[7] = 0.5236f;
		starRotations[8] = -0.2618f;
		starRotations[9] = 0.36652f;
		starRotations[10] = 0.31416f;
		starRotations[11] = 0.5236f;
		starRotations[12] = 0.13963f;
		starScales[0] = 0.6f;
		starScales[1] = 0.766f;
		starScales[2] = 0.68f;
		starScales[3] = 1f;
		starScales[4] = 0.933f;
		starScales[5] = 0.77f;
		starScales[6] = 0.47f;
		starScales[7] = 0.572f;
		starScales[8] = 0.755f;
		starScales[9] = 0.852f;
		starScales[10] = 0.441f;
		starScales[11] = 0.608f;
		starScales[12] = 0.458f;
	Vector2 reference14 =starScaleInfos[0];
		reference14 = new Vector2(Global.RandomBetween(0f, 0.4f), Global.RandomBetween(0.6f, 1f));
	Vector2 reference15 =starScaleInfos[1];
		reference15 = new Vector2(Global.RandomBetween(0f, 0.4f), Global.RandomBetween(0.6f, 1f));
	Vector2 reference16 =starScaleInfos[2];
		reference16 = new Vector2(Global.RandomBetween(0f, 0.4f), Global.RandomBetween(0.6f, 1f));
	Vector2 reference17 =starScaleInfos[3];
		reference17 = new Vector2(Global.RandomBetween(0f, 0.4f), Global.RandomBetween(0.6f, 1f));
	Vector2 reference18 =starScaleInfos[4];
		reference18 = new Vector2(Global.RandomBetween(0f, 0.4f), Global.RandomBetween(0.6f, 1f));
	Vector2 reference19 =starScaleInfos[5];
		reference19 = new Vector2(Global.RandomBetween(0f, 0.4f), Global.RandomBetween(0.6f, 1f));
	Vector2 reference20 =starScaleInfos[6];
		reference20 = new Vector2(Global.RandomBetween(0f, 0.4f), Global.RandomBetween(0.6f, 1f));
	Vector2 reference21 =starScaleInfos[7];
		reference21 = new Vector2(Global.RandomBetween(0f, 0.4f), Global.RandomBetween(0.6f, 1f));
	Vector2 reference22 =starScaleInfos[8];
		reference22 = new Vector2(Global.RandomBetween(0f, 0.4f), Global.RandomBetween(0.6f, 1f));
	Vector2 reference23 =starScaleInfos[9];
		reference23 = new Vector2(Global.RandomBetween(0f, 0.4f), Global.RandomBetween(0.6f, 1f));
	Vector2 reference24 =starScaleInfos[10];
		reference24 = new Vector2(Global.RandomBetween(0f, 0.4f), Global.RandomBetween(0.6f, 1f));
	Vector2 reference25 =starScaleInfos[11];
		reference25 = new Vector2(Global.RandomBetween(0f, 0.4f), Global.RandomBetween(0.6f, 1f));
	Vector2 reference26 =starScaleInfos[12];
		reference26 = new Vector2(Global.RandomBetween(0f, 0.4f), Global.RandomBetween(0.6f, 1f));
		starScaleRates[0] = Global.RandomBetween(1.25f, 2.5f);
		starScaleRates[1] = Global.RandomBetween(1.25f, 2.5f);
		starScaleRates[2] = Global.RandomBetween(1.25f, 2.5f);
		starScaleRates[3] = Global.RandomBetween(1.25f, 2.5f);
		starScaleRates[4] = Global.RandomBetween(1.25f, 2.5f);
		starScaleRates[5] = Global.RandomBetween(1.25f, 2.5f);
		starScaleRates[6] = Global.RandomBetween(1.25f, 2.5f);
		starScaleRates[7] = Global.RandomBetween(1.25f, 2.5f);
		starScaleRates[8] = Global.RandomBetween(1.25f, 2.5f);
		starScaleRates[9] = Global.RandomBetween(1.25f, 2.5f);
		starScaleRates[10] = Global.RandomBetween(1.25f, 2.5f);
		starScaleRates[11] = Global.RandomBetween(1.25f, 2.5f);
		starScaleRates[12] = Global.RandomBetween(1.25f, 2.5f);
	}

    public void Update(float dt, InputState currInput, ref GameState gameState)
    {
        this.UpdateBackground(dt);
        base.Update(dt, currInput);
        if (!currInput.IsButtonPressed(Buttons.A))
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
        }
    }

    public void UpdateBackground(float dt)
	{
		shineRotation += 0.464f * dt;
		titleAnimTimer += dt;
		if (titleAnimTimer > titleAnimTime)
		{
			titleAnimFrame = (titleAnimFrame + 1) % titleAnimNumFrames;
			titleAnimTimer = 0f;
		}
		catAnimTimer += dt;
		if (catAnimTimer > catAnimTime)
		{
			catAnimFrame = (catAnimFrame + 1) % catAnimNumFrames;
			catAnimTimer = 0f;
		}
		for (int i = 0; i < starPositions.Length; i++)
		{
			starScales[i] += starScaleRates[i] * dt;
			if (starScales[i] < starScaleInfos[i].X)
			{
				starScales[i] = starScaleInfos[i].X;
				starScaleRates[i] = 0f - starScaleRates[i];
			}
			else if (starScales[i] > starScaleInfos[i].Y)
			{
				starScales[i] = starScaleInfos[i].Y;
				starScaleRates[i] = 0f - starScaleRates[i];
			}
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
		if (canBuyGame && (menuItems_.Count == 4 || menuItems_.Count == 0))
		{
			menuItems_.Clear();
			index_ = 0;
			AddMenuItem(new MenuItem(Global.mainTex, new Rectangle(1134, 722, 169, 42), new Vector2(204f, 600f)));
			AddMenuItem(new MenuItem(Global.mainTex, new Rectangle(1134, 766, 284, 42), new Vector2(450f, 600f)));
			AddMenuItem(new MenuItem(Global.mainTex, new Rectangle(1132, 810, 211, 42), new Vector2(718f, 600f)));
			AddMenuItem(new MenuItem(Global.mainTex, new Rectangle(1132, 854, 126, 43), new Vector2(907f, 600f)));
			AddMenuItem(new MenuItem(Global.mainTex, new Rectangle(1132, 899, 156, 42), new Vector2(1068f, 600f)));
			SetItemVertices();
		}
		if (!canBuyGame && (menuItems_.Count == 5 || menuItems_.Count == 0))
		{
			menuItems_.Clear();
			index_ = 0;
			AddMenuItem(new MenuItem(Global.mainTex, new Rectangle(1134, 722, 169, 42), new Vector2(207f, 600f)));
			AddMenuItem(new MenuItem(Global.mainTex, new Rectangle(1134, 766, 284, 42), new Vector2(505f, 600f)));
			AddMenuItem(new MenuItem(Global.mainTex, new Rectangle(1132, 810, 211, 42), new Vector2(824f, 600f)));
			AddMenuItem(new MenuItem(Global.mainTex, new Rectangle(1132, 899, 156, 42), new Vector2(1080f, 600f)));
			SetItemVertices();
		}
		DrawBackground(spriteBatch);
		base.Draw(spriteBatch);
	}

	public void DrawBackground(SpriteBatch spriteBatch)
	{
		spriteBatch.Draw(Global.mainTex, Vector2.Zero, (Rectangle?)new Rectangle(0, 0, 1280, 720), Color.White);
		spriteBatch.Draw(Global.mainCircleTex, new Vector2(820f, 288f), (Rectangle?)null, Color.White, shineRotation, new Vector2(927f, 927f), 1f, (SpriteEffects)0, 0f);
		spriteBatch.Draw(Global.mainTex, new Vector2(74f, 0f), (Rectangle?)new Rectangle(0, 720, 1132, 489), Color.White);
		spriteBatch.Draw(Global.mainCatTex, new Vector2(820f, 288f), (Rectangle?)new Rectangle(catAnimFrame % 3 * 445, catAnimFrame / 3 * 319 + 1, 445, 318), Color.White, 0f, new Vector2(222.5f, 159.5f), 1f, (SpriteEffects)0, 0f);
		spriteBatch.Draw(Global.mainTitleTex, new Vector2(200f, 222f), (Rectangle?)new Rectangle(titleAnimFrame % 4 * 816, titleAnimFrame / 4 * 320, 816, 320), Color.White);
		for (int i = 0; i < starPositions.Length; i++)
		{
			spriteBatch.Draw(Global.mainStarTex, starPositions[i], (Rectangle?)null, Color.White, starRotations[i], new Vector2(20.5f, 20f), starScales[i], (SpriteEffects)0, 0f);
		}
	}
}
