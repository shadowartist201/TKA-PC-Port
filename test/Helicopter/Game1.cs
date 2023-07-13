using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
//using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Helicopter;

public class Game1 : Game
{
	private RenderTarget2D renderTarget;

	private bool splashScreen = true;

	private int splashScreenIndex = 0;

	private float splashScreenTimer = 0f;

	private float splashScreenTime = 1f;

	private GameState gameState = GameState.OPENING;

	private OpeningMenu openingMenu;

	private PauseMenu pauseMenu;

	private TrialMenu trialMenu;

	private MainMenu mainMenu;

	private OptionsMenu optionsMenu;

	private CreditsMenu creditsMenu;

	private StageSelectMenu stageSelectMenu;

	private CatSelectMenu catSelectMenu;

	private LeaderboardsMenu leaderboardsMenu;

	private GraphicsDeviceManager graphics;

	private SpriteBatch spriteBatch;

	private InputState currInput = new InputState();

	private Helicopter helicopter;

	private Background background;

	private Tunnel tunnel;

	private BSpriteManager bSpriteManager;

	private Light[] lights = new Light[6];

	private Laser[] lasers = new Laser[5];

	private SpreadLaser spreadLaser;

	private ParticleEmitter[] shootingStars = new ParticleEmitter[20];

	private LyricEffect lyricEffect;

	private Fireworks fireworks;

	private Heart heart;

	private Hand hand;

	private Eyes eyes;

	private Rainbow rainbow;

	private FlashManager flashManager;

	private ShineEffectManager shineManager;

	private FluctuationManager fluctuationManager;

	private ButterflyEffect butterflyEffect;

	private KaraokeLyrics karaokeLyrics;

	private Equalizer equalizer;

	private MeatToMouth meatToMouth;

	private ExplosionManager explosionManager;

	private DancerManager dancerManager;

	private HeartsManager heartsManager;

	private SongManager songManager;

	private ScoreSystem scoreSystem;

	private int currEvent;

	private float[] eventTimes = new float[100];

	private bool justStarted = false;

	private float frameTime;

	private float fps;

	private float total;

	private bool starShowerActive;

	private int starCount = 0;

	private int starsInitiated;

	private float dx = 100f;

	private float dy = 300f;

	private Vector2[] _heartPositions = (Vector2[])new Vector2[16]
	{
		new Vector2(150f, 45f),
		new Vector2(180f, 13f),
		new Vector2(225f, 0f),
		new Vector2(270f, 25f),
		new Vector2(300f, 90f),
		new Vector2(275f, 160f),
		new Vector2(225f, 220f),
		new Vector2(190f, 260f),
		new Vector2(150f, 300f),
		new Vector2(120f, 13f),
		new Vector2(75f, 0f),
		new Vector2(30f, 25f),
		new Vector2(0f, 90f),
		new Vector2(25f, 160f),
		new Vector2(75f, 220f),
		new Vector2(110f, 260f)
	};

	private Vector2[] _starPositions = (Vector2[])new Vector2[20]
	{
		new Vector2(150f, 0f),
		new Vector2(167.5f, 57.5f),
		new Vector2(185f, 115f),
		new Vector2(242.5f, 115f),
		new Vector2(300f, 115f),
		new Vector2(252.5f, 150f),
		new Vector2(205f, 185f),
		new Vector2(222.5f, 242.5f),
		new Vector2(240f, 300f),
		new Vector2(195f, 265f),
		new Vector2(150f, 230f),
		new Vector2(105f, 265f),
		new Vector2(60f, 300f),
		new Vector2(77.5f, 242.5f),
		new Vector2(95f, 185f),
		new Vector2(47.5f, 150f),
		new Vector2(0f, 115f),
		new Vector2(57.5f, 115f),
		new Vector2(115f, 115f),
		new Vector2(132.5f, 57.5f)
	};

	public Game1()
	{
																																																																																																																																																										Global.DeviceManager = new StorageDeviceManager(this);
		((Collection<IGameComponent>)Components).Add(Global.DeviceManager);
		//GamerServicesComponent item = new GamerServicesComponent(this);
		//Components.Add(item);
		Global.DeviceManager.DeviceSelectorCanceled += DeviceSelectorCanceled;
		Global.DeviceManager.DeviceDisconnected += DeviceDisconnected;
		Global.DeviceManager.PromptForDevice();
		Exiting += OnExit;
		graphics = new GraphicsDeviceManager(this);
		Content.RootDirectory = "Content";
		graphics.PreferredBackBufferWidth = 1280;
		graphics.PreferredBackBufferHeight = 720;
		graphics.IsFullScreen = false;
		graphics.SynchronizeWithVerticalRetrace = false;
		IsFixedTimeStep = false;
		graphics.ApplyChanges();
	}

	private void DeviceDisconnected(object sender, StorageDeviceEventArgs e)
	{
		e.EventResponse = StorageDeviceSelectorEventResponse.Prompt;
	}

	private void DeviceSelectorCanceled(object sender, StorageDeviceEventArgs e)
	{
		e.EventResponse = StorageDeviceSelectorEventResponse.Prompt;
	}

	protected override void Initialize()
	{
		MediaPlayer.IsVisualizationEnabled = true;
		renderTarget = new RenderTarget2D(GraphicsDevice, 1280, 720, false, (SurfaceFormat)0, (DepthFormat)0);
		Global.audioEngine = new AudioEngine("Content/Music//newXactProject.xgs");
		Global.waveBank = new WaveBank(Global.audioEngine, "Content/Music//Wave Bank.xwb");
		Global.soundBank = new SoundBank(Global.audioEngine, "Content/Music//Sound Bank.xsb");
		Global.itemSelectedEffect = new ItemSelectedEffect();
		base.Initialize();
	}

	protected override void LoadContent()
	{
		spriteBatch = new SpriteBatch(GraphicsDevice);
		LoadAssets();
		LoadMenus();
		LoadBackground();
		LoadHelicopter();
		LoadForeground();
		LoadEventInfo(0);
		MediaPlayer.Stop();
		MediaPlayer.Play(songManager.CurrentSong);
        MediaPlayer.IsRepeating = true;
		MediaPlayer.Volume = 1f;
		base.LoadContent();
	}

	protected override void UnloadContent()
	{
	}

	private void OnExit(object o, EventArgs e)
	{
		scoreSystem.SaveInfo();
	}

    protected override void Update(GameTime gameTime)
    {
        float num = (float)gameTime.ElapsedGameTime.TotalSeconds;
        float elapsedMilliseconds = (float)MediaPlayer.PlayPosition.TotalMilliseconds;
		Debug.WriteLine(MediaPlayer.State);
        //Global.IsTrialMode = Guide.IsTrialMode;
        currInput.Update();
        if (currInput.IsButtonUp(Buttons.A))
        {
            justStarted = false;
        }
        Global.mountainVelocity = background.GetVelocity();
        switch (gameState)
        {
            case GameState.OPENING:
                {
                    if (splashScreen)
                    {
                        splashScreenTimer += num;
                        if (splashScreenTimer > splashScreenTime)
                        {
                            if (splashScreenIndex == 1)
                            {
                                splashScreen = false;
                            }
                            else
                            {
                                splashScreenIndex = 1;
                            }
                            splashScreenTimer = 0f;
                        }
                        break;
                    }
                    openingMenu.Update(num, currInput, gameState);
                    mainMenu.UpdateBackground(num);
                    for (PlayerIndex playerIndex = PlayerIndex.One; playerIndex <= PlayerIndex.Four; playerIndex++)
                    {
                        if (GamePad.GetState(playerIndex).IsButtonDown(Buttons.Start))
                        {
                            Global.PlayCatSound();
                            Global.playerIndex = playerIndex;
                            scoreSystem.LoadInfo();
                            gameState = GameState.MAIN_MENU;
                            break;
                        }
                    }
                    if (currInput.IsButtonDown(Buttons.Start))
                    {
                        Global.PlayCatSound();
                        if (!Global.playerIndex.HasValue)
                        {
                            Global.playerIndex = PlayerIndex.One;
                        }
                        scoreSystem.LoadInfo();
                        gameState = GameState.MAIN_MENU;
                    }
                    break;
                }
            case GameState.MAIN_MENU:
                mainMenu.Update(num, currInput, ref gameState);
                switch (gameState)
                {
                    case GameState.OPTIONS:
                        optionsMenu.SetLastGameState(GameState.MAIN_MENU);
                        break;
                    case GameState.STAGE_SELECT:
                        stageSelectMenu.SetLastGameState(GameState.MAIN_MENU);
                        break;
                    case GameState.LEADERBOARDS:
                        leaderboardsMenu.SetLastGameState(GameState.MAIN_MENU);
                        break;
                }
                break;
            case GameState.STAGE_SELECT:
                stageSelectMenu.Update(num, currInput, ref gameState);
                switch (gameState)
                {
                    case GameState.CAT_SELECT:
                        songManager.LoadNewSong(stageSelectMenu.getCurrentLevel());
                        LoadEventInfo(stageSelectMenu.getCurrentLevel());
                        background.LoadNewBackground(stageSelectMenu.getCurrentLevel());
                        bSpriteManager.LoadNewBackground(stageSelectMenu.getCurrentLevel());
                        ResetGame(stageSelectMenu.getCurrentLevel() == 3);
                        MediaPlayer.Stop();
                        MediaPlayer.Play(songManager.CurrentSong);
                        catSelectMenu.SetLastGameState(GameState.STAGE_SELECT);
                        break;
                    case GameState.PLAY:
                        songManager.LoadNewSong(stageSelectMenu.getCurrentLevel());
                        LoadEventInfo(stageSelectMenu.getCurrentLevel());
                        background.LoadNewBackground(stageSelectMenu.getCurrentLevel());
                        bSpriteManager.LoadNewBackground(stageSelectMenu.getCurrentLevel());
                        ResetGame(stageSelectMenu.getCurrentLevel() == 3);
                        if (stageSelectMenu.getCurrentLevel() == 4)
                        {
                            Camera.SetEffect(-1);
                        }
						MediaPlayer.Stop();
                        MediaPlayer.Play(songManager.CurrentSong);
                        justStarted = true;
                        Global.ResetVibration();
                        break;
                }
                break;
            case GameState.CAT_SELECT:
                catSelectMenu.Update(num, currInput, ref gameState, stageSelectMenu.getCurrentLevel(), scoreSystem);
                switch (gameState)
                {
                    case GameState.PLAY:
                        helicopter.ChangeAnimation(catSelectMenu.getCurrentCat());
                        Global.SetVibrationResume();
                        MediaPlayer.Resume();
                        break;
                    case GameState.STAGE_SELECT:
                        Global.ResetVibration();
                        songManager.LoadNewSong(-1);
                        MediaPlayer.Play(songManager.CurrentSong);
                        break;
                }
                if (catSelectMenu.LastGameState != GameState.PAUSE)
                {
                    UpdateChoreography(num, elapsedMilliseconds, stageSelectMenu.getCurrentLevel());
                    UpdateBackground(num);
                    UpdateForeground(num);
                }
                break;
            case GameState.PLAY:
                UpdateChoreography(num, elapsedMilliseconds, stageSelectMenu.getCurrentLevel());
                UpdateBackground(num);
                UpdateHelicopter(num);
                UpdateForeground(num);
                if (currInput.IsButtonPressed(Buttons.Start))
                {
                    MediaPlayer.Pause();
                    Global.SetVibrationPause();
                    gameState = GameState.PAUSE;
                }
                break;
            case GameState.PAUSE:
                pauseMenu.Update(num, currInput, ref gameState);
                switch (gameState)
                {
                    case GameState.PLAY:
                        Global.SetVibrationResume();
                        MediaPlayer.Resume();
                        break;
                    case GameState.OPTIONS:
                        optionsMenu.SetLastGameState(GameState.PAUSE);
                        break;
                    case GameState.MAIN_MENU:
                        Global.ResetVibration();
                        MediaPlayer.Stop();
                        songManager.LoadNewSong(-1);
                        MediaPlayer.Play(songManager.CurrentSong);
                        break;
                    case GameState.STAGE_SELECT:
                        stageSelectMenu.SetLastGameState(GameState.PAUSE);
                        break;
                    case GameState.CAT_SELECT:
                        catSelectMenu.SetLastGameState(GameState.PAUSE);
                        break;
                    case GameState.LEADERBOARDS:
                        leaderboardsMenu.SetLastGameState(GameState.PAUSE);
                        break;
                }
                break;
            case GameState.TRIAL_PAUSE:
                trialMenu.Update(num, currInput, gameState);
                if (gameState == GameState.MAIN_MENU)
                {
                    Global.ResetVibration();
                    MediaPlayer.Stop();
                    songManager.LoadNewSong(-1);
                    MediaPlayer.Play(songManager.CurrentSong);
                }
                break;
            case GameState.OPTIONS:
                optionsMenu.Update(num, currInput, ref gameState);
                break;
            case GameState.LEADERBOARDS:
                leaderboardsMenu.Update(num, currInput, ref gameState);
                break;
            case GameState.CREDITS:
                creditsMenu.Update(num, currInput, ref gameState);
                break;
            case GameState.EXIT:
                base.Exit();
                break;
        }
        /*if (Global.IsTrialMode && scoreSystem.CurrScore > 30000 && !helicopter.IsDead())
        {
            scoreSystem.CurrScore = 30000;
            helicopter.Kill();
            tunnel.velocity = 0f;
            scoreSystem.End(stageSelectMenu.getCurrentLevel(), catSelectMenu.getCurrentCat());
            MediaPlayer.Pause();
            Global.SetVibrationPause();
            gameState = GameState.TRIAL_PAUSE;
            trialMenu.ResetStartTimer();
        }*/
        currInput.EndUpdate();
        Global.audioEngine.Update();
        Global.UpdateVibration(num);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
	{
		float num = (float)gameTime.ElapsedGameTime.TotalSeconds;
		total += num;
		if (total >= 1f)
		{
			frameTime = fps;
			fps = 0f;
			total = 0f;
		}
		fps += 1f;
		if (gameState == GameState.CAT_SELECT || gameState == GameState.PLAY || gameState == GameState.PAUSE)
		{
			GraphicsDevice.SetRenderTarget(renderTarget);
			spriteBatch.Begin((SpriteSortMode)1, BlendState.NonPremultiplied);
			DrawStage(num, gameTime);
			spriteBatch.End();
			Camera.Draw(spriteBatch, renderTarget, graphics, GraphicsDevice);
		}
		spriteBatch.Begin((SpriteSortMode)1, BlendState.NonPremultiplied);
		DrawMenu();
		spriteBatch.End();
	}

	private void LoadAssets()
	{
		Global.spriteFont = Content.Load<SpriteFont>("font");
		Global.menuFont = Content.Load<SpriteFont>("Fonts//MenuFont");
		Global.setPixel(GraphicsDevice);
		Global.tunnelStar = Content.Load<Texture2D>("Graphics//Tunnel//star");
		Global.scoreTexture = Content.Load<Texture2D>("Graphics//Score//score");
		Global.highScoreTexture = Content.Load<Texture2D>("Graphics//Score//high");
		Global.numbersTexture = Content.Load<Texture2D>("Graphics//Score//numbers");
		Global.creditsTex = Content.Load<Texture2D>("Graphics//Menu//Credits//credits_bgMenu");
		Global.splashTex = Content.Load<Texture2D>("Graphics//Menu//Splash//splash_bgMenu");
		Global.selectCatTex = Content.Load<Texture2D>("Graphics//Menu//KittenSelect//selectCat_bgMenu");
		Global.selectStageTex = Content.Load<Texture2D>("Graphics//Menu//StageSelect//selectStage_bgMenu");
		Global.pauseTex = Content.Load<Texture2D>("Graphics//Menu//Pause//pause_bgMenu");
		Global.optionsTex = Content.Load<Texture2D>("Graphics//Menu//Options//options_bgMenu");
		Global.leaderboardTex = Content.Load<Texture2D>("Graphics//Menu//Leaderboards//leaderboards_bgMenu");
		Global.trialTex = Content.Load<Texture2D>("Graphics//Menu//Trial//trial_bgMenu");
		Global.mainTex = Content.Load<Texture2D>("Graphics//Menu//Main//main_bgMenu");
		Global.mainCatTex = Content.Load<Texture2D>("Graphics//Menu//Main//main_cat");
		Global.mainCircleTex = Content.Load<Texture2D>("Graphics//Menu//Main//main_circle");
		Global.mainStarTex = Content.Load<Texture2D>("Graphics//Menu//Main//main_star");
		Global.mainTitleTex = Content.Load<Texture2D>("Graphics//Menu//Main//main_title");
		Global.mainPressStartTex = Content.Load<Texture2D>("Graphics//Menu//Main//main_pressStart");
		Global.bigStar = Content.Load<Texture2D>("Graphics//StarEffect//bigstar");
		Global.stars = (Texture2D[])new Texture2D[7]
		{
			Content.Load<Texture2D>("Graphics//StarEffect//star1"),
			Content.Load<Texture2D>("Graphics//StarEffect//star2"),
			Content.Load<Texture2D>("Graphics//StarEffect//star3"),
			Content.Load<Texture2D>("Graphics//StarEffect//star4"),
			Content.Load<Texture2D>("Graphics//StarEffect//star5"),
			Content.Load<Texture2D>("Graphics//StarEffect//star6"),
			Content.Load<Texture2D>("Graphics//StarEffect//star7")
		};
		Global.heartsTex = Content.Load<Texture2D>("Graphics//Effects//hearts");
		Global.butterflyParticles = Content.Load<Texture2D>("Graphics//Effects//Butterflies");
		Global.equalizerBar = Content.Load<Texture2D>("Graphics//Effects//EqualizerBar");
		Global.mouth = Content.Load<Texture2D>("Graphics//Effects//mouth");
		Global.meatsToMouth = Content.Load<Texture2D>("Graphics//Effects//meatsToMouth");
		Global.pelvicTex = Content.Load<Texture2D>("Graphics//Effects//danceFull");
		Global.explosionTex = Content.Load<Texture2D>("Graphics//Effects//explosion");
		Global.searchingEyes = Content.Load<Texture2D>("Graphics//Effects//SearchingEyes");
		Global.rainbow2 = Content.Load<Texture2D>("Graphics//BackgroundSprites//rainbow3");
		Global.rainbow3 = Content.Load<Texture2D>("Graphics//BackgroundSprites//hugeRainbow");
		Global.shineShapes = (Texture2D[])new Texture2D[3];
		Global.shineShapes[0] = Content.Load<Texture2D>("Graphics//Effects//lightShape1");
		Global.shineShapes[1] = Content.Load<Texture2D>("Graphics//Effects//lightShape2");
		Global.shineShapes[2] = Content.Load<Texture2D>("Graphics//Effects//lightShape3");
		Global.fluctuationShape = Content.Load<Texture2D>("Graphics//Effects//fluctuationShape");
		Global.feelWantTouch = Content.Load<Texture2D>("Graphics//Effects//FeelWantTouch");
		Global.lightEffect = Content.Load<Texture2D>("Graphics//light2");
		Global.hugestar = Content.Load<Texture2D>("Graphics//Symbols//hugestar");
		Global.hugeHeart = Content.Load<Texture2D>("Graphics//Symbols//hugeheart");
		Global.hand = Content.Load<Texture2D>("Graphics//hand");
		Global.reachingHand = Content.Load<Texture2D>("Graphics//Effects//reachingHand");
		Global.rainbow = Content.Load<Texture2D>("Graphics//rainbow2");
		Global.hugeAtom = Content.Load<Texture2D>("Graphics//Symbols//hugeAtom");
		Global.hugeButterfly = Content.Load<Texture2D>("Graphics//Symbols//hugeButterfly");
		Global.hugeCat = Content.Load<Texture2D>("Graphics//Symbols//hugeCat");
		Global.hugeCrown = Content.Load<Texture2D>("Graphics//Symbols//hugeCrown");
		Global.hugeMoon = Content.Load<Texture2D>("Graphics//Symbols//hugeMoon");
		Global.hugeRabbit = Content.Load<Texture2D>("Graphics//Symbols//hugeRabit");
		Global.cats = Content.Load<Texture2D>("Graphics//cats");
		Global.AButtonTexture = Content.Load<Texture2D>("Graphics//xboxControllerButtonA");
		Global.YButtonTexture = Content.Load<Texture2D>("Graphics//xboxControllerButtonY");
		for (int i = 0; i < Camera.effects.Length; i++)
		{
			//Camera.effects[i] = Content.Load<Effect>("Effects//effect" + i);
		}
		scoreSystem = new ScoreSystem();
		songManager = new SongManager(this);
	}

	private void LoadMenus()
	{
		openingMenu = new OpeningMenu();
		pauseMenu = new PauseMenu();
		trialMenu = new TrialMenu();
		mainMenu = new MainMenu();
		optionsMenu = new OptionsMenu();
		creditsMenu = new CreditsMenu();
		stageSelectMenu = new StageSelectMenu();
		catSelectMenu = new CatSelectMenu();
		leaderboardsMenu = new LeaderboardsMenu();
	}

	private void LoadBackground()
	{
		background = new Background(this);
		tunnel = new Tunnel(40, 3);
		bSpriteManager = new BSpriteManager(this);
	}

	private void LoadHelicopter()
	{
		helicopter = new Helicopter();
	}

	private void LoadForeground()
	{
		heart = new Heart();
		rainbow = new Rainbow();
		eyes = new Eyes();
		hand = new Hand();
		fireworks = new Fireworks(8);
		for (int i = 0; i < shootingStars.Length; i++)
		{
			shootingStars[i] = new ParticleEmitter(new Vector2((float)(i * 60), (float)(-i * 20)), new Vector2(100f, 300f), 2);
		}
		for (int i = 0; i < lights.Length; i++)
		{
			lights[i] = new Light(new Vector2((float)(i * 140), 0f), Vector2.Zero);
		}
		for (int i = 0; i < lasers.Length; i++)
		{
			lasers[i] = new Laser(Vector2.Zero, Color.LimeGreen);
		}
		spreadLaser = new SpreadLaser();
		lyricEffect = new LyricEffect();
		flashManager = new FlashManager();
		shineManager = new ShineEffectManager();
		fluctuationManager = new FluctuationManager();
		butterflyEffect = new ButterflyEffect();
		karaokeLyrics = new KaraokeLyrics();
		equalizer = new Equalizer();
		meatToMouth = new MeatToMouth();
		explosionManager = new ExplosionManager();
		dancerManager = new DancerManager();
		heartsManager = new HeartsManager();
	}

	private void LoadEventInfo(int currentLevel)
	{
		switch (currentLevel)
		{
		case 0:
			LoadEventInfoSeaOfLove();
			break;
		case 1:
			LoadEventInfoLikeARainbow();
			break;
		case 2:
			LoadEventInfoYoureShining();
			break;
		case 3:
			LoadEventInfoTasteOfHeaven();
			break;
		case 4:
			LoadEventInfoIntergalacticalHigh();
			break;
		}
	}

	private void LoadEventInfoSeaOfLove()
	{
		eventTimes[0] = 0f;
		eventTimes[1] = 1594.5f;
		eventTimes[2] = 6900f;
		eventTimes[3] = 12100f;
		eventTimes[4] = 12300f;
		eventTimes[5] = 17800f;
		eventTimes[6] = 23000f;
		eventTimes[7] = 25500f;
		eventTimes[8] = 28000f;
		eventTimes[9] = 28500f;
		eventTimes[10] = 31000f;
		eventTimes[11] = 32800f;
		eventTimes[12] = 34200f;
		eventTimes[13] = 45300f;
		eventTimes[14] = 56000f;
		eventTimes[15] = 57000f;
		eventTimes[16] = 59000f;
		eventTimes[17] = 70000f;
		eventTimes[18] = 74500f;
		eventTimes[19] = 79000f;
		eventTimes[20] = 80500f;
		eventTimes[21] = 102000f;
		eventTimes[22] = 102680f;
		eventTimes[23] = 124500f;
		eventTimes[24] = 129700f;
		eventTimes[25] = 135200f;
		eventTimes[26] = 140700f;
		eventTimes[27] = 146200f;
		eventTimes[28] = 151200f;
		eventTimes[29] = 157100f;
		eventTimes[30] = 157000f;
		eventTimes[31] = 159500f;
		eventTimes[32] = 162000f;
		eventTimes[33] = 162600f;
		eventTimes[34] = 165500f;
		eventTimes[35] = 167400f;
		eventTimes[36] = 168000f;
		eventTimes[37] = 180500f;
		eventTimes[38] = 189500f;
		eventTimes[39] = 192500f;
		eventTimes[40] = 235000f;
		eventTimes[41] = 235500f;
	}

	private void LoadEventInfoLikeARainbow()
	{
		eventTimes[0] = 0f;
		eventTimes[1] = 726f;
		eventTimes[2] = 766f;
		eventTimes[3] = 6192f;
		eventTimes[4] = 11650f;
		eventTimes[5] = 17148f;
		eventTimes[6] = 22742f;
		eventTimes[7] = 44662f;
		eventTimes[8] = 58873f;
		eventTimes[9] = 66526f;
		eventTimes[10] = 66598f;
		eventTimes[11] = 69111f;
		eventTimes[12] = 75168f;
		eventTimes[13] = 81073f;
		eventTimes[14] = 85837f;
		eventTimes[15] = 88510f;
		eventTimes[16] = 93960f;
		eventTimes[17] = 99418f;
		eventTimes[18] = 99594f;
		eventTimes[19] = 104916f;
		eventTimes[20] = 105004f;
		eventTimes[21] = 107502f;
		eventTimes[22] = 107645f;
		eventTimes[23] = 109085f;
		eventTimes[24] = 110542f;
		eventTimes[25] = 113015f;
		eventTimes[26] = 118865f;
		eventTimes[27] = 124809f;
		eventTimes[28] = 129685f;
		eventTimes[29] = 131065f;
		eventTimes[30] = 132510f;
		eventTimes[31] = 150065f;
		eventTimes[32] = 154358f;
		eventTimes[33] = 165354f;
		eventTimes[34] = 170764f;
		eventTimes[35] = 173517f;
		eventTimes[36] = 176318f;
		eventTimes[37] = 198206f;
		eventTimes[38] = 200760f;
		eventTimes[39] = 206729f;
		eventTimes[40] = 209234f;
		eventTimes[41] = 213323f;
		eventTimes[42] = 214748f;
		eventTimes[43] = 217445f;
		eventTimes[44] = 218802f;
		eventTimes[45] = 220190f;
		eventTimes[46] = 242142f;
		eventTimes[47] = 244640f;
		eventTimes[48] = 250609f;
		eventTimes[49] = 257098f;
		eventTimes[50] = 259905f;
		eventTimes[51] = 261334f;
		eventTimes[52] = 264015f;
	}

	private void LoadEventInfoYoureShining()
	{
		eventTimes[0] = 0f;
		eventTimes[1] = 2900f;
		eventTimes[2] = 5743f;
		eventTimes[3] = 8566f;
		eventTimes[4] = 11019f;
		eventTimes[5] = 11370f;
		eventTimes[6] = 16676f;
		eventTimes[7] = 22152f;
		eventTimes[8] = 27918f;
		eventTimes[9] = 33540f;
		eventTimes[10] = 34429f;
		eventTimes[11] = 35887f;
		eventTimes[12] = 37522f;
		eventTimes[13] = 39156f;
		eventTimes[14] = 41525f;
		eventTimes[15] = 45235f;
		eventTimes[16] = 45637f;
		eventTimes[17] = 47085f;
		eventTimes[18] = 48751f;
		eventTimes[19] = 52970f;
		eventTimes[20] = 56587f;
		eventTimes[21] = 68200f;
		eventTimes[22] = 69694f;
		eventTimes[23] = 71092f;
		eventTimes[24] = 72528f;
		eventTimes[25] = 73925f;
		eventTimes[26] = 75323f;
		eventTimes[27] = 76683f;
		eventTimes[28] = 78118f;
		eventTimes[29] = 79549f;
		eventTimes[30] = 80917f;
		eventTimes[31] = 82664f;
		eventTimes[32] = 86532f;
		eventTimes[33] = 90873f;
		eventTimes[34] = 92248f;
		eventTimes[35] = 93951f;
		eventTimes[36] = 97912f;
		eventTimes[37] = 101803f;
		eventTimes[38] = 101850f;
		eventTimes[39] = 107578f;
		eventTimes[40] = 110402f;
		eventTimes[41] = 113047f;
		eventTimes[42] = 113225f;
		eventTimes[43] = 113462f;
		eventTimes[44] = 114889f;
		eventTimes[45] = 116049f;
		eventTimes[46] = 116300f;
		eventTimes[47] = 117712f;
		eventTimes[48] = 118873f;
		eventTimes[49] = 119139f;
		eventTimes[50] = 120529f;
		eventTimes[51] = 121692f;
		eventTimes[52] = 121940f;
		eventTimes[53] = 122595f;
		eventTimes[54] = 124345f;
		eventTimes[55] = 146931f;
		eventTimes[56] = 147331f;
		eventTimes[57] = 148772f;
		eventTimes[58] = 149775f;
		eventTimes[59] = 150468f;
		eventTimes[60] = 152571f;
		eventTimes[61] = 154514f;
		eventTimes[62] = 155409f;
		eventTimes[63] = 158226f;
		eventTimes[64] = 158582f;
		eventTimes[65] = 159980f;
		eventTimes[66] = 161042f;
		eventTimes[67] = 161690f;
		eventTimes[68] = 163851f;
		eventTimes[69] = 165816f;
		eventTimes[70] = 166697f;
		eventTimes[71] = 169520f;
		eventTimes[72] = 170008f;
		eventTimes[73] = 171398f;
		eventTimes[74] = 172810f;
		eventTimes[75] = 174222f;
		eventTimes[76] = 175029f;
		eventTimes[77] = 175633f;
		eventTimes[78] = 177038f;
		eventTimes[79] = 178457f;
		eventTimes[80] = 179528f;
		eventTimes[81] = 179876f;
		eventTimes[82] = 180808f;
		eventTimes[83] = 192619f;
		eventTimes[84] = 194002f;
		eventTimes[85] = 195428f;
		eventTimes[86] = 196825f;
		eventTimes[87] = 198223f;
		eventTimes[88] = 199620f;
		eventTimes[89] = 201032f;
		eventTimes[90] = 202473f;
		eventTimes[91] = 203972f;
		eventTimes[92] = 205325f;
		eventTimes[93] = 206999f;
		eventTimes[94] = 210842f;
		eventTimes[95] = 215150f;
		eventTimes[96] = 216518f;
		eventTimes[97] = 218352f;
		eventTimes[98] = 222267f;
		eventTimes[99] = 225993f;
	}

	private void LoadEventInfoTasteOfHeaven()
	{
		eventTimes[0] = 0f;
		eventTimes[1] = 7999f;
		eventTimes[2] = 8648f;
		eventTimes[3] = 9332f;
		eventTimes[4] = 10712f;
		eventTimes[5] = 21330f;
		eventTimes[6] = 32008f;
		eventTimes[7] = 37340f;
		eventTimes[8] = 39983f;
		eventTimes[9] = 41352f;
		eventTimes[10] = 42661f;
		eventTimes[11] = 63992f;
		eventTimes[12] = 83955f;
		eventTimes[13] = 85347f;
		eventTimes[14] = 90644f;
		eventTimes[15] = 112011f;
		eventTimes[16] = 133342f;
		eventTimes[17] = 143960f;
		eventTimes[18] = 154661f;
		eventTimes[19] = 157339f;
		eventTimes[20] = 159993f;
		eventTimes[21] = 160678f;
		eventTimes[22] = 161327f;
		eventTimes[23] = 165362f;
		eventTimes[24] = 207988f;
		eventTimes[25] = 210000f;
	}

	private void LoadEventInfoIntergalacticalHigh()
	{
		eventTimes[0] = 0.528f;
		eventTimes[1] = 5561f;
		eventTimes[2] = 11411f;
		eventTimes[3] = 20776f;
		eventTimes[4] = 22015f;
		eventTimes[5] = 44290f;
		eventTimes[6] = 66094f;
		eventTimes[7] = 75795f;
		eventTimes[8] = 77111f;
		eventTimes[9] = 99155f;
		eventTimes[10] = 110143f;
		eventTimes[11] = 121190f;
		eventTimes[12] = 143205f;
		eventTimes[13] = 154184f;
		eventTimes[14] = 176247f;
		eventTimes[15] = 198234f;
		eventTimes[16] = 220383f;
		eventTimes[17] = 231247f;
		eventTimes[18] = 232553f;
	}

	private void UpdateBackground(float dt)
	{
		background.Update(dt);
		bSpriteManager.Update(dt);
		tunnel.Update(dt, helicopter, scoreSystem, stageSelectMenu.getCurrentLevel(), catSelectMenu.getCurrentCat());
	}

	private void UpdateHelicopter(float dt)
	{
		if (!justStarted)
		{
			helicopter.HandleInput(currInput, tunnel, scoreSystem, dt);
		}
		helicopter.Update(dt, tunnel, scoreSystem);
	}

	private void UpdateForeground(float dt)
	{
		fireworks.Update(dt);
		ParticleEmitter[] array = shootingStars;
		foreach (ParticleEmitter particleEmitter in array)
		{
			particleEmitter.Update(dt);
		}
		UpdateStarShower();
		Light[] array2 = lights;
		foreach (Light light in array2)
		{
			light.Update(dt);
		}
		Laser[] array3 = lasers;
		foreach (Laser laser in array3)
		{
			laser.Update(dt);
		}
		spreadLaser.Update(dt);
		lyricEffect.Update(dt);
		heart.Update(dt);
		rainbow.Update(dt);
		eyes.Update(dt);
		hand.Update(dt);
		flashManager.Update(dt);
		shineManager.Update(dt, helicopter.position);
		fluctuationManager.Update(dt, helicopter.position);
		butterflyEffect.Update(dt, helicopter.position);
		equalizer.Update(dt);
		meatToMouth.Update(dt);
		explosionManager.Update(dt);
		dancerManager.Update(dt);
		heartsManager.Update(dt);
		scoreSystem.Update(dt);
	}

	private void UpdateStarShower()
	{
		if (!starShowerActive)
		{
			return;
		}
		if (starsInitiated == 13)
		{
			dy = 150f;
		}
		if (starsInitiated == 43)
		{
			dy = 75f;
		}
		if (shootingStars[starCount].currPosition.Y > 800f)
		{
			if (shootingStars[(starCount + 19) % 20].currPosition.X + dx < 130f || shootingStars[(starCount + 19) % 20].currPosition.X + dx > 1150f)
			{
				dx = 0f - dx;
			}
			shootingStars[starCount].currPosition.X = shootingStars[(starCount + 19) % 20].currPosition.X + dx;
			shootingStars[starCount].currPosition.Y = shootingStars[(starCount + 19) % 20].currPosition.Y - Global.BPM * 2f * dy;
			starCount = (starCount + 1) % 20;
			starsInitiated++;
		}
	}

	private void DrawStage(float elapsed, GameTime gameTime)
	{
		if (gameState == GameState.PLAY || gameState == GameState.PAUSE || gameState == GameState.TRIAL_PAUSE)
		{
			DrawGame(elapsed, gameTime);
		}
		if (gameState == GameState.CAT_SELECT)
		{
			DrawBackground(gameTime);
			DrawForeground();
		}
	}

    private void DrawMenu()
    {
        switch (gameState)
        {
            case GameState.OPENING:
                if (splashScreen)
                {
                    if (splashScreenIndex == 0)
                    {
                        spriteBatch.Draw(Global.splashTex, Vector2.Zero, (Rectangle?)new Rectangle(0, 0, 1280, 720), Color.White);
                    }
                    else
                    {
                        spriteBatch.Draw(Global.splashTex, Vector2.Zero, (Rectangle?)new Rectangle(0, 720, 1280, 720), Color.White);
                    }
                }
                else
                {
                    mainMenu.DrawBackground(spriteBatch);
                    openingMenu.Draw(spriteBatch);
                }
                break;
            case GameState.MAIN_MENU:
                mainMenu.Draw(spriteBatch);
                break;
            case GameState.STAGE_SELECT:
                stageSelectMenu.Draw(spriteBatch);
                break;
            case GameState.CAT_SELECT:
                catSelectMenu.Draw(spriteBatch, stageSelectMenu.getCurrentLevel(), scoreSystem);
                break;
            case GameState.PLAY:
                if (!tunnel.IsOn() && !helicopter.IsDead())
                {
                    DisplayInstructions();
                }
                break;
            case GameState.PAUSE:
                pauseMenu.Draw(spriteBatch);
                break;
            case GameState.TRIAL_PAUSE:
                trialMenu.Draw(spriteBatch);
                break;
            case GameState.OPTIONS:
                optionsMenu.Draw(spriteBatch);
                break;
            case GameState.LEADERBOARDS:
                leaderboardsMenu.Draw(spriteBatch, scoreSystem);
                break;
            case GameState.CREDITS:
                creditsMenu.Draw(spriteBatch);
                break;
            case GameState.EXIT:
                break;
        }
    }

    private void DrawGame(float elapsed, GameTime gameTime)
	{
		DrawBackground(gameTime);
		DrawHelicopter();
		DrawForeground();
		scoreSystem.Draw(spriteBatch);
	}

	private void DrawBackground(GameTime gameTime)
	{
		background.DrawBackBack(spriteBatch);
		bSpriteManager.DrawBack(spriteBatch);
		background.DrawMiddleBack(spriteBatch);
		bSpriteManager.DrawMiddleBack(spriteBatch);
		background.DrawBack(spriteBatch);
		bSpriteManager.DrawMiddle(spriteBatch);
		background.DrawMiddle(spriteBatch);
		bSpriteManager.DrawFore(spriteBatch);
		tunnel.Draw(spriteBatch);
	}

	private void DrawHelicopter()
	{
		helicopter.Draw(spriteBatch);
	}

	private void DrawForeground()
	{
		meatToMouth.Draw(spriteBatch);
		heartsManager.Draw(spriteBatch);
		explosionManager.Draw(spriteBatch);
		dancerManager.Draw(spriteBatch);
		karaokeLyrics.Draw(spriteBatch);
		equalizer.Draw(spriteBatch);
		shineManager.Draw(spriteBatch);
		fluctuationManager.Draw(spriteBatch);
		butterflyEffect.Draw(spriteBatch);
		ParticleEmitter[] array = shootingStars;
		foreach (ParticleEmitter particleEmitter in array)
		{
			particleEmitter.Draw(spriteBatch);
		}
		Light[] array2 = lights;
		foreach (Light light in array2)
		{
			light.Draw(spriteBatch);
		}
		Laser[] array3 = lasers;
		foreach (Laser laser in array3)
		{
			laser.Draw(spriteBatch);
		}
		spreadLaser.Draw(spriteBatch);
		lyricEffect.Draw(spriteBatch);
		heart.Draw(spriteBatch);
		rainbow.Draw(spriteBatch);
		eyes.Draw(spriteBatch);
		hand.Draw(spriteBatch);
		fireworks.Draw(spriteBatch);
		flashManager.Draw(spriteBatch);
	}

	private void DisplayInstructions()
	{
		spriteBatch.DrawString(Global.spriteFont, "Press       To Start", new Vector2(545f, 190f), Global.tunnelColor, 0f, Vector2.Zero, 0.75f, (SpriteEffects)0, 0f);
		spriteBatch.Draw(Global.AButtonTexture, new Rectangle(612, 192, 30, 30), Color.White);
		spriteBatch.DrawString(Global.spriteFont, "Hold       to go up\nRelease to go down", new Vector2(543f, 269f), Global.tunnelColor, 0f, Vector2.Zero, 0.75f, (SpriteEffects)0, 0f);
		spriteBatch.Draw(Global.AButtonTexture, new Rectangle(597, 272, 30, 30), Color.White);
	}

	private static int IsOdd(int i)
	{
		if (i % 2 == 1)
		{
			return 1;
		}
		return -1;
	}

	private void UpdateChoreography(float dt, float elapsedMilliseconds, int currentLevel)
	{
		switch (currentLevel)
		{
		case 0:
			UpdateChoreographySeaOfLove(dt, elapsedMilliseconds);
			break;
		case 1:
			UpdateChoreographyLikeARainbow(dt, elapsedMilliseconds);
			break;
		case 2:
			UpdateChoreographyYoureShining(dt, elapsedMilliseconds);
			break;
		case 3:
			UpdateChoreographyTasteOfHeaven(dt, elapsedMilliseconds);
			break;
		case 4:
			UpdateChoreographyRon(dt, elapsedMilliseconds);
			break;
		}
	}

	private void UpdateChoreographySeaOfLove(float dt, float elapsedMilliseconds)
	{
		if (elapsedMilliseconds > eventTimes[currEvent])
		{
			switch (currEvent)
			{
			case 0:
				DoOpeningStars();
				break;
			case 1:
				DoStationaryLights();
				flashManager.DoFlash();
				bSpriteManager.SetBRainbow();
				bSpriteManager.SetBigRainbow();
				break;
			case 2:
				ResetShootingStars();
				DoLeftStars();
				break;
			case 3:
				ResetShootingStars();
				DoRightStars();
				break;
			case 4:
				DoSwayingLights();
				break;
			case 5:
				ResetShootingStars();
				DoLeftStars();
				break;
			case 6:
				ResetShootingStars();
				DoRightStars();
				break;
			case 7:
				rainbow.TurnOn();
				break;
			case 8:
				hand.TurnOn(directedDownward_: true);
				break;
			case 9:
				ResetShootingStars();
				DoLeftStars();
				break;
			case 10:
				DoRightStars();
				break;
			case 11:
				ResetLights();
				break;
			case 12:
				ResetShootingStars();
				tunnel.Set(TunnelEffect.BW);
				scoreSystem.TurnOnBass();
				TurnOnLasers();
				break;
			case 13:
				TurnOffLasers();
				fireworks.TurnOn();
				break;
			case 14:
				tunnel.Set(TunnelEffect.Normal);
				tunnel.SetColor(Color.White, Color.Black, Color.White);
				scoreSystem.TurnOffBass();
				fireworks.TurnOff();
				break;
			case 16:
				DoStarShower();
				break;
			case 17:
				background.SetAcceleration(100f);
				break;
			case 19:
				starShowerActive = false;
				break;
			case 20:
				ResetHeart(1, alternating: false);
				heart.TurnOn();
				tunnel.Set(TunnelEffect.Rainbow);
				Global.SetVibrationEndless(on: true);
				scoreSystem.TurnOnBass();
				background.SetAcceleration(0f);
				fireworks.TurnOn();
				DoStarStars(new Vector2(490f, 0f), new Vector2(0f, 400f));
				break;
			case 21:
				ResetShootingStars();
				ResetHeart(0, alternating: false);
				tunnel.Set(TunnelEffect.Normal);
				tunnel.SetColor(Color.White, Color.Black, Color.White);
				Global.SetVibrationEndless(on: false);
				scoreSystem.TurnOffBass();
				fireworks.TurnOff();
				break;
			case 22:
				heart.TurnOn();
				tunnel.Set(TunnelEffect.Rainbow);
				Global.SetVibrationEndless(on: true);
				scoreSystem.TurnOnBass();
				DoHeartStars(new Vector2(490f, 0f), new Vector2(0f, 400f));
				DoPunctuatedLights();
				break;
			case 23:
				ResetHeart(0, alternating: true);
				Global.SetVibrationEndless(on: false);
				tunnel.Set(TunnelEffect.BW);
				break;
			case 24:
				DoHeartStars(new Vector2(-300f, 510f), new Vector2(500f, 0f));
				break;
			case 25:
				DoStarStars(new Vector2(1280f, 510f), new Vector2(-500f, 0f));
				break;
			case 26:
				DoHeartStars(new Vector2(-300f, 510f), new Vector2(500f, 0f));
				break;
			case 27:
				tunnel.Set(TunnelEffect.Normal);
				tunnel.SetColor(Color.White, Color.Black, Color.White);
				scoreSystem.TurnOffBass();
				background.SetVelocity(200f);
				bSpriteManager.SetBRainbow();
				bSpriteManager.SetBigRainbow();
				ResetLights();
				DoRightStars();
				break;
			case 28:
				DoLeftStars();
				break;
			case 29:
				DoRightStars();
				break;
			case 30:
				fireworks.TurnOn();
				break;
			case 31:
				rainbow.TurnOn();
				break;
			case 32:
				hand.TurnOn(directedDownward_: true);
				break;
			case 33:
				DoLeftStars();
				break;
			case 34:
				DoRightStars();
				break;
			case 35:
				fireworks.TurnOff();
				break;
			case 36:
				DoStarShower();
				break;
			case 37:
				background.SetAcceleration(100f);
				break;
			case 38:
				starShowerActive = false;
				break;
			case 39:
				background.SetAcceleration(0f);
				heart.TurnOn();
				fireworks.TurnOn();
				tunnel.Set(TunnelEffect.Rainbow);
				Global.SetVibrationEndless(on: true);
				scoreSystem.TurnOnBass();
				DoPunctuatedLights();
				TurnOnLasers();
				break;
			case 40:
				ResetChoreography(1, alternating: false, meat: false);
				Global.SetVibrationEndless(on: false);
				break;
			case 41:
				MediaPlayer.Stop();
				MediaPlayer.Play(songManager.CurrentSong);
				break;
			}
			currEvent++;
			if (currEvent == 42)
			{
				currEvent = 0;
			}
		}
	}

	private void UpdateChoreographyLikeARainbow(float dt, float elapsedMilliseconds)
	{
		if (elapsedMilliseconds > eventTimes[currEvent])
		{
			switch (currEvent)
			{
			case 0:
				tunnel.Set(TunnelEffect.Normal);
				tunnel.SetColor(Color.Black, Color.Red, Color.Blue);
				break;
			case 1:
				hand.TurnOn(directedDownward_: false);
				break;
			case 2:
				tunnel.Set(TunnelEffect.RainbowPunctuated);
				scoreSystem.TurnOnBass();
				break;
			case 3:
				hand.TurnOn(directedDownward_: false);
				break;
			case 4:
				hand.TurnOn(directedDownward_: false);
				break;
			case 5:
				hand.TurnOn(directedDownward_: false);
				break;
			case 6:
				tunnel.Set(TunnelEffect.Normal);
				tunnel.SetColor(Color.Black, Color.Red, Color.Blue);
				scoreSystem.TurnOffBass();
				butterflyEffect.TurnOn1();
				break;
			case 7:
				butterflyEffect.TurnOn1();
				break;
			case 9:
				hand.TurnOn(directedDownward_: false);
				break;
			case 10:
				butterflyEffect.TurnOn1();
				break;
			case 11:
				bSpriteManager.SetBigRainbow();
				rainbow.TurnOn();
				break;
			case 12:
				flashManager.DoFlash();
				break;
			case 13:
				bSpriteManager.setPirateBoat();
				break;
			case 14:
				eyes.TurnOn();
				break;
			case 15:
				hand.TurnOn(directedDownward_: false);
				butterflyEffect.TurnOn2();
				break;
			case 16:
				hand.TurnOn(directedDownward_: false);
				break;
			case 17:
				hand.TurnOn(directedDownward_: false);
				break;
			case 18:
				tunnel.Set(TunnelEffect.BW);
				break;
			case 19:
				hand.TurnOn(directedDownward_: false);
				break;
			case 20:
				tunnel.Set(TunnelEffect.BWDouble);
				break;
			case 21:
				butterflyEffect.TurnOff2();
				break;
			case 22:
				tunnel.Set(TunnelEffect.BWQuad);
				break;
			case 23:
				tunnel.Set(TunnelEffect.Normal);
				tunnel.SetColor(Color.Black, Color.Red, Color.Blue);
				break;
			case 24:
				hand.TurnOn(directedDownward_: false);
				scoreSystem.TurnOnMovement();
				scoreSystem.TurnOnBass();
				tunnel.Set(TunnelEffect.BWQuad);
				heart.Reset(0, _alternating: true);
				heart.TurnOn();
				DoFinaleLights();
				break;
			case 25:
				bSpriteManager.SetBigRainbow();
				rainbow.TurnOn();
				break;
			case 26:
				flashManager.DoFlash();
				break;
			case 27:
				bSpriteManager.setPirateBoat();
				break;
			case 28:
				eyes.TurnOn();
				break;
			case 29:
				heart.TurnOff();
				ResetLights();
				break;
			case 30:
				heart.TurnOn();
				DoFinaleLights();
				break;
			case 31:
				heart.TurnOff();
				ResetLights();
				tunnel.Set(TunnelEffect.BWDouble);
				break;
			case 32:
				tunnel.Set(TunnelEffect.RainbowPunctuated);
				scoreSystem.TurnOffMovement();
				hand.TurnOn(directedDownward_: false);
				break;
			case 33:
				hand.TurnOn(directedDownward_: false);
				break;
			case 34:
				hand.TurnOn(directedDownward_: false);
				break;
			case 35:
				tunnel.Set(TunnelEffect.Normal);
				scoreSystem.TurnOffBass();
				break;
			case 36:
				tunnel.Set(TunnelEffect.BW);
				scoreSystem.TurnOnBass();
				break;
			case 37:
				hand.TurnOn(directedDownward_: false);
				tunnel.Set(TunnelEffect.Normal);
				scoreSystem.TurnOffBass();
				butterflyEffect.TurnOn1();
				break;
			case 38:
				bSpriteManager.SetBigRainbow();
				rainbow.TurnOn();
				break;
			case 39:
				flashManager.DoFlash();
				break;
			case 40:
				tunnel.Set(TunnelEffect.BW);
				flashManager.DoStrobe(Global.BPM);
				break;
			case 41:
				bSpriteManager.setPirateBoat();
				break;
			case 42:
				tunnel.Set(TunnelEffect.BWDouble);
				flashManager.DoStrobe(Global.BPM / 2f);
				break;
			case 43:
				eyes.TurnOn();
				tunnel.Set(TunnelEffect.BWQuad);
				flashManager.DoStrobe(Global.BPM / 4f);
				break;
			case 44:
				tunnel.Set(TunnelEffect.Normal);
				tunnel.SetColor(Color.Black, Color.Red, Color.Blue);
				flashManager.Reset();
				break;
			case 45:
				scoreSystem.TurnOnMovement();
				scoreSystem.TurnOnBass();
				tunnel.Set(TunnelEffect.BWQuad);
				heart.Reset(0, _alternating: true);
				heart.TurnOn();
				DoFinaleLights();
				flashManager.DoStrobe(Global.BPM);
				break;
			case 46:
				hand.TurnOn(directedDownward_: false);
				break;
			case 47:
				bSpriteManager.SetBigRainbow();
				rainbow.TurnOn();
				break;
			case 48:
				flashManager.DoFlash();
				break;
			case 49:
				bSpriteManager.setPirateBoat();
				break;
			case 50:
				heart.TurnOff();
				ResetLights();
				flashManager.Reset();
				break;
			case 51:
				eyes.TurnOn();
				break;
			case 52:
				ResetChoreography(1, alternating: false, meat: false);
				Global.SetVibrationEndless(on: false);
				MediaPlayer.Stop();
				MediaPlayer.Play(songManager.CurrentSong);
				break;
			}
			currEvent++;
			if (currEvent == 53)
			{
				currEvent = 0;
			}
		}
	}

	private void UpdateChoreographyYoureShining(float dt, float elapsedMilliseconds)
	{
		if (elapsedMilliseconds > eventTimes[currEvent])
		{
			switch (currEvent)
			{
			case 0:
				flashManager.DoFade(2.9f);
				shineManager.SetCircle(333f);
				break;
			case 1:
				shineManager.SetCircle(250f);
				break;
			case 2:
				shineManager.SetCircle(167f);
				break;
			case 3:
				shineManager.SetCircle(83f);
				break;
			case 4:
				shineManager.TurnOn1();
				break;
			case 5:
				flashManager.DoFlash();
				break;
			case 6:
				shineManager.Continue1();
				break;
			case 7:
				shineManager.Continue1();
				break;
			case 8:
				shineManager.Continue1();
				break;
			case 9:
				shineManager.TurnOn2();
				break;
			case 10:
				lyricEffect.TurnOn(0);
				break;
			case 11:
				lyricEffect.TurnOn(1);
				break;
			case 12:
				lyricEffect.TurnOn(2);
				break;
			case 13:
				shineManager.Continue2();
				break;
			case 14:
				flashManager.DoFlash();
				break;
			case 15:
				fluctuationManager.TurnOn();
				tunnel.Set(TunnelEffect.BWDouble);
				break;
			case 16:
				lyricEffect.TurnOn(0);
				break;
			case 17:
				lyricEffect.TurnOn(1);
				break;
			case 18:
				lyricEffect.TurnOn(2);
				break;
			case 19:
				flashManager.DoFlash();
				break;
			case 20:
				fluctuationManager.TurnOff();
				tunnel.Set(TunnelEffect.BW);
				scoreSystem.TurnOnBass();
				TurnOnLasers();
				break;
			case 21:
				flashManager.DoFlash();
				break;
			case 22:
				flashManager.DoFlash();
				break;
			case 23:
				flashManager.DoFlash();
				break;
			case 24:
				flashManager.DoFlash();
				break;
			case 25:
				flashManager.DoFlash();
				break;
			case 26:
				flashManager.DoFlash();
				break;
			case 27:
				flashManager.DoFlash();
				break;
			case 28:
				flashManager.DoFlash();
				break;
			case 29:
				lyricEffect.TurnOn(0);
				break;
			case 30:
				lyricEffect.TurnOn(1);
				break;
			case 31:
				lyricEffect.TurnOn(2);
				break;
			case 32:
				flashManager.DoFlash();
				break;
			case 33:
				lyricEffect.TurnOn(0);
				break;
			case 34:
				lyricEffect.TurnOn(1);
				break;
			case 35:
				lyricEffect.TurnOn(2);
				break;
			case 36:
				flashManager.DoFlash();
				break;
			case 37:
				tunnel.Set(TunnelEffect.Normal);
				tunnel.SetColor(Color.White, Color.Black, Color.White);
				scoreSystem.TurnOffBass();
				TurnOffLasers();
				break;
			case 38:
				DoPianoLights();
				break;
			case 39:
				ContinuePianoLights();
				break;
			case 40:
				ContinuePianoLights();
				break;
			case 41:
				scoreSystem.TurnOnBass();
				break;
			case 42:
				ContinuePianoLights();
				break;
			case 43:
				flashManager.DoFlash();
				break;
			case 44:
				flashManager.DoFlash();
				break;
			case 45:
				ContinuePianoLights();
				break;
			case 46:
				flashManager.DoFlash();
				break;
			case 47:
				flashManager.DoFlash();
				break;
			case 48:
				ContinuePianoLights();
				break;
			case 49:
				flashManager.DoFlash();
				break;
			case 50:
				flashManager.DoFlash();
				break;
			case 51:
				ContinuePianoLights();
				break;
			case 52:
				flashManager.DoFlash();
				break;
			case 53:
				scoreSystem.TurnOffBass();
				break;
			case 54:
				ResetLights();
				shineManager.TurnOn3();
				break;
			case 55:
				shineManager.TurnOff3();
				shineManager.TurnOn4();
				break;
			case 56:
				lyricEffect.TurnOn(0);
				break;
			case 57:
				lyricEffect.TurnOn(1);
				break;
			case 58:
				shineManager.Continue4(directUpwards: true);
				break;
			case 59:
				lyricEffect.TurnOn(2);
				break;
			case 60:
				shineManager.Continue4(directUpwards: true);
				break;
			case 61:
				flashManager.DoFlash();
				break;
			case 62:
				shineManager.Continue4(directUpwards: false);
				break;
			case 63:
				shineManager.Continue4(directUpwards: true);
				break;
			case 64:
				lyricEffect.TurnOn(0);
				break;
			case 65:
				lyricEffect.TurnOn(1);
				break;
			case 66:
				shineManager.Continue4(directUpwards: true);
				break;
			case 67:
				lyricEffect.TurnOn(2);
				break;
			case 68:
				shineManager.Continue4(directUpwards: true);
				break;
			case 69:
				flashManager.DoFlash();
				break;
			case 70:
				shineManager.Continue4(directUpwards: false);
				break;
			case 71:
				fluctuationManager.TurnOn();
				tunnel.Set(TunnelEffect.BWDouble);
				break;
			case 72:
				flashManager.DoFlash();
				break;
			case 73:
				flashManager.DoFlash();
				break;
			case 74:
				flashManager.DoFlash();
				break;
			case 75:
				flashManager.DoFlash();
				break;
			case 77:
				flashManager.DoFlash();
				break;
			case 78:
				flashManager.DoFlash();
				break;
			case 79:
				flashManager.DoFlash();
				break;
			case 80:
				fluctuationManager.TurnOff();
				tunnel.Set(TunnelEffect.Normal);
				tunnel.SetColor(Color.White, Color.Black, Color.White);
				break;
			case 81:
				flashManager.DoFlash();
				break;
			case 82:
				tunnel.Set(TunnelEffect.BW);
				TurnOnLasers();
				DoFinaleLights();
				scoreSystem.TurnOnBass();
				shineManager.TurnOn3();
				break;
			case 83:
				flashManager.DoFlash();
				break;
			case 84:
				flashManager.DoFlash();
				break;
			case 85:
				flashManager.DoFlash();
				break;
			case 86:
				flashManager.DoFlash();
				break;
			case 87:
				flashManager.DoFlash();
				break;
			case 88:
				flashManager.DoFlash();
				break;
			case 89:
				flashManager.DoFlash();
				break;
			case 90:
				flashManager.DoFlash();
				break;
			case 91:
				lyricEffect.TurnOn(0);
				break;
			case 92:
				lyricEffect.TurnOn(1);
				break;
			case 93:
				lyricEffect.TurnOn(2);
				break;
			case 94:
				flashManager.DoFlash();
				break;
			case 95:
				lyricEffect.TurnOn(0);
				break;
			case 96:
				lyricEffect.TurnOn(1);
				break;
			case 97:
				lyricEffect.TurnOn(2);
				break;
			case 98:
				flashManager.DoFlash();
				break;
			case 99:
				ResetChoreography(1, alternating: false, meat: false);
				Global.SetVibrationEndless(on: false);
				MediaPlayer.Stop();
				MediaPlayer.Play(songManager.CurrentSong);
				break;
			}
			currEvent++;
			if (currEvent == 100)
			{
				currEvent = 0;
			}
		}
	}

	private void UpdateChoreographyTasteOfHeaven(float dt, float elapsedMilliseconds)
	{
		Camera.Update(dt);
		karaokeLyrics.Update(dt, elapsedMilliseconds);
		if (elapsedMilliseconds > eventTimes[currEvent])
		{
			switch (currEvent)
			{
			case 0:
				karaokeLyrics.TurnOn();
				break;
			case 1:
				bSpriteManager.SetSausageRainbowSmall();
				bSpriteManager.SetSausageRainbowLarge();
				Camera.DoShake(2f, 0.16666f);
				break;
			case 2:
				Camera.DoShake(2f, 0.16666f);
				break;
			case 3:
				Camera.DoShakes(4, 1f / 3f, 2f, 0.16666f);
				break;
			case 6:
				meatToMouth.TurnOn();
				break;
			case 7:
				bSpriteManager.SetSausageRainbowSmall();
				bSpriteManager.SetSausageRainbowLarge();
				Camera.DoShake(2f, 0.16666f);
				break;
			case 8:
				Camera.DoShake(2f, 0.16666f);
				break;
			case 9:
				Camera.DoShakes(4, 1f / 3f, 2f, 0.16666f);
				break;
			case 10:
				meatToMouth.TurnOff();
				equalizer.TurnOn(rainbowed: true, true);
				Camera.DoRotating(1f / 3f);
				Camera.DoFlipping(1f / 3f);
				break;
			case 12:
				equalizer.TurnOff();
				Camera.StopRotating();
				Camera.StopFlipping();
				break;
			case 14:
				Camera.DoScaling(1f / 3f);
				break;
			case 15:
				Camera.StopScaling();
				break;
			case 17:
				meatToMouth.TurnOn();
				break;
			case 18:
				equalizer.TurnOn(rainbowed: false, false);
				break;
			case 19:
				bSpriteManager.SetSausageRainbowSmall();
				bSpriteManager.SetSausageRainbowLarge();
				Camera.DoShake(2f, 0.16666f);
				break;
			case 20:
				Camera.DoShake(2f, 0.16666f);
				break;
			case 21:
				Camera.DoShake(2f, 0.16666f);
				break;
			case 22:
				Camera.DoShakes(4, 1f / 3f, 2f, 0.16666f);
				break;
			case 23:
				equalizer.TurnOn(rainbowed: true, true);
				Camera.GoCrazy(1f / 3f);
				break;
			case 24:
				ResetChoreography(1, alternating: false, meat: true);
				break;
			case 25:
				Global.SetVibrationEndless(on: false);
				MediaPlayer.Stop();
				MediaPlayer.Play(songManager.CurrentSong);
                    break;
			}
			currEvent++;
			if (currEvent == 26)
			{
				currEvent = 0;
			}
		}
	}

	public void UpdateChoreographyRon(float dt, float elapsedMilliseconds)
	{
		Camera.Update(dt);
		if (elapsedMilliseconds > eventTimes[currEvent])
		{
			switch (currEvent)
			{
			case 0:
				explosionManager.TurnOn();
				Camera.SetEffect(0);
				break;
			case 4:
				explosionManager.TurnOff();
				Camera.SetEffect(-1);
				dancerManager.TurnOn(0);
				break;
			case 5:
				dancerManager.TurnOn(1);
				break;
			case 6:
				dancerManager.TurnOff();
				Camera.SetEffect(1);
				heartsManager.TurnOn();
				break;
			case 8:
				Camera.SetEffect(2);
				break;
			case 9:
				heartsManager.TurnOff();
				Camera.SetEffect(-1);
				tunnel.Set(TunnelEffect.Disappear);
				break;
			case 10:
				Camera.SetEffect(5);
				tunnel.Set(TunnelEffect.Normal);
				break;
			case 11:
				Camera.SetEffect(4);
				break;
			case 12:
				Camera.SetEffect(1);
				heartsManager.TurnOn();
				break;
			case 13:
				heartsManager.TurnOff();
				tunnel.Set(TunnelEffect.Disappear);
				Camera.SetEffect(3);
				dancerManager.TurnOn(0);
				break;
			case 14:
				tunnel.Set(TunnelEffect.Normal);
				explosionManager.TurnOn();
				Camera.SetEffect(0);
				break;
			case 15:
				Camera.SetEffect(2);
				dancerManager.TurnOn(1);
				heartsManager.TurnOn();
				break;
			case 16:
				explosionManager.TurnOff();
				Camera.SetEffect(-1);
				break;
			case 17:
				ResetChoreography(1, alternating: false, meat: false);
				break;
			case 18:
				Global.SetVibrationEndless(on: false);
				MediaPlayer.Stop();
				MediaPlayer.Play(songManager.CurrentSong);
                    break;
			}
			currEvent++;
			if (currEvent == 19)
			{
				currEvent = 0;
			}
		}
	}

	private void DoStationaryLights()
	{
		int num = 0;
		for (int i = 0; i < 2; i++)
		{
			for (int j = 0; j < 2; j++)
			{
				lights[num].ResetPosition(new Vector2((float)(150 + 980 * i), (float)(150 + 420 * j)), Vector2.Zero);
				lights[num].SetRotationBehavior(_smoothRotation: false, new float[4]
				{
					0f,
					(float)Math.PI / 2f,
					(float)Math.PI,
					4.712389f
				}, new float[1] { Global.BPM * 2f }, 0f, 0f, 0f);
				lights[num].SetHueBehavior(_smoothHue: true, null, 0f, 180f, 0f, 360f);
				lights[num].TurnOn();
				num++;
			}
		}
	}

	private void DoPunctuatedLights()
	{
		for (int i = 0; i < 6; i++)
		{
			lights[i].ResetPosition(new Vector2((float)(150 + 196 * i), 360f), new Vector2(Global.RandomBetween(300f, 500f), Global.RandomBetween(300f, 500f)));
			lights[i].SetHueBehavior(_smoothHue: true, null, 0f, 360f, i * 60, 360f);
			lights[i].SetRotationBehavior(_smoothRotation: false, new float[8]
			{
				0f,
				(float)Math.PI / 4f,
				(float)Math.PI / 2f,
				(float)Math.PI * 3f / 4f,
				(float)Math.PI,
				3.926991f,
				4.712389f,
				5.4977875f
			}, new float[1] { Global.BPM }, 0f, 0f, 0f);
			lights[i].TurnOn();
		}
	}

	private void DoSwayingLights()
	{
		for (int i = 0; i < 4; i++)
		{
			lights[i].ResetVelocity(new Vector2(Global.RandomBetween(200f, 400f), Global.RandomBetween(200f, 400f)));
			lights[i].SetHueBehavior(_smoothHue: false, new float[6]
			{
				i * 60 % 360,
				(i + 1) * 60 % 360,
				(i + 2) * 60 % 360,
				(i + 3) * 60 % 360,
				(i + 4) * 60 % 360,
				(i + 5) * 60 % 360
			}, Global.BPM, 0f, 0f, 0f);
			lights[i].SetRotationBehavior(_smoothRotation: true, null, null, (float)IsOdd(i) * (float)Math.PI / (Global.BPM * 2f), MathHelper.Min(0f, (float)IsOdd(i) * (float)Math.PI), MathHelper.Max(0f, (float)IsOdd(i) * (float)Math.PI));
		}
		for (int i = 4; i < 6; i++)
		{
			lights[i].ResetPosition(new Vector2((float)(440 + 400 * (i - 4)), 360f), new Vector2(Global.RandomBetween(200f, 400f), Global.RandomBetween(200f, 400f)));
			lights[i].SetHueBehavior(_smoothHue: false, new float[6]
			{
				i * 60 % 360,
				(i + 1) * 60 % 360,
				(i + 2) * 60 % 360,
				(i + 3) * 60 % 360,
				(i + 4) * 60 % 360,
				(i + 5) * 60 % 360
			}, Global.BPM, 0f, 0f, 0f);
			lights[i].SetRotationBehavior(_smoothRotation: true, null, null, (float)IsOdd(i) * (float)Math.PI / (Global.BPM * 2f), MathHelper.Min(0f, (float)IsOdd(i) * (float)Math.PI), MathHelper.Max(0f, (float)IsOdd(i) * (float)Math.PI));
			lights[i].TurnOn();
		}
	}

	private void DoFinaleLights()
	{
		for (int i = 0; i < 6; i++)
		{
			lights[i].ResetPosition(new Vector2((float)(150 + 196 * i), 360f), new Vector2(Global.RandomBetween(300f, 500f), Global.RandomBetween(300f, 500f)));
			lights[i].SetHueBehavior(_smoothHue: false, new float[7] { 0f, 60f, 120f, 180f, 240f, 300f, 360f }, 0f, 0f, 0f, 0f);
			lights[i].SetRotationBehavior(_smoothRotation: false, new float[8]
			{
				0f,
				(float)Math.PI / 4f,
				(float)Math.PI / 2f,
				(float)Math.PI * 3f / 4f,
				(float)Math.PI,
				3.926991f,
				4.712389f,
				5.4977875f
			}, new float[1] { Global.BPM }, 0f, 0f, 0f);
			lights[i].TurnOn();
		}
	}

	private void DoPianoLights()
	{
		for (int i = 0; i < 6; i++)
		{
			lights[i].ResetPosition(new Vector2((float)(150 + 196 * i), 360f), new Vector2(Global.RandomBetween(300f, 500f), Global.RandomBetween(300f, 500f)));
			lights[i].SetHueBehavior(_smoothHue: false, new float[7] { 0f, 60f, 120f, 180f, 240f, 300f, 360f }, 0f, 0f, 0f, 0f);
			lights[i].SetRotationBehavior(_smoothRotation: false, new float[8]
			{
				0f,
				(float)Math.PI / 4f,
				(float)Math.PI / 2f,
				(float)Math.PI * 3f / 4f,
				(float)Math.PI,
				3.926991f,
				4.712389f,
				5.4977875f
			}, new float[19]
			{
				0f, 0.161f, 0.605f, 0.869f, 1.129f, 1.488f, 1.663f, 2.012f, 2.272f, 2.546f,
				2.904f, 3.065f, 3.433f, 3.936f, 4.316f, 4.486f, 4.85f, 5.105f, 5.364f
			}, 0f, 0f, 0f);
			lights[i].TurnOn();
		}
	}

	private void ContinuePianoLights()
	{
		for (int i = 0; i < 6; i++)
		{
			lights[i].ContinueLights(new float[10] { 0f, 0.172f, 0.531f, 0.797f, 1.056f, 1.415f, 1.663f, 1.943f, 2.204f, 2.471f });
		}
	}

	private void TurnOnLasers()
	{
		for (int i = 0; i < 5; i++)
		{
			lasers[i].Set(new Vector2((float)(128 + 256 * i), 0f), Color.LimeGreen);
			lasers[i].TurnOn();
		}
	}

	private void TurnOffLasers()
	{
		for (int i = 0; i < 5; i++)
		{
			lasers[i].TurnOff();
		}
	}

	private void DoStarShower()
	{
		float num = 130f;
		starCount = 0;
		starsInitiated = 0;
		dx = 100f;
		dy = 300f;
		for (int i = 0; i < 20; i++)
		{
			shootingStars[i].Reset(new Vector2(num, (float)(-i) * Global.BPM * 2f * dy), new Vector2(0f, 600f));
			shootingStars[i].TurnOn();
			num += dx;
			if (num > 1150f)
			{
				num -= 2f * dx;
				dx = 0f - dx;
			}
		}
		starShowerActive = true;
	}

	private void DoOpeningStars()
	{
		for (int i = 0; i < shootingStars.Length; i++)
		{
			shootingStars[i].Reset(new Vector2((float)(i * 60), (float)(-i * 20)), new Vector2(100f, 300f));
			shootingStars[i].TurnOn();
		}
	}

	private void DoLeftStars()
	{
		for (int i = 0; i < 5; i++)
		{
			shootingStars[i].Reset(new Vector2((float)(i * 60 + 300), (float)(-i * 300) * Global.BPM / 3f), new Vector2(-100f, 300f));
			shootingStars[i].TurnOn();
		}
	}

	private void DoRightStars()
	{
		for (int i = 5; i < 10; i++)
		{
			shootingStars[i].Reset(new Vector2((float)(i * 60 + 300), (float)(-(i - 5) * 300) * Global.BPM / 3f), new Vector2(100f, 300f));
			shootingStars[i].TurnOn();
		}
	}

	private void DoHeartStars(Vector2 _position, Vector2 _velocity)
	{
		for (int i = 0; i < 16; i++)
		{
			shootingStars[i].Reset(_heartPositions[i] + _position + new Vector2(0f, -300f), _velocity);
			shootingStars[i].TurnOn();
		}
	}

	private void DoStarStars(Vector2 _position, Vector2 _velocity)
	{
		for (int i = 0; i < 20; i++)
		{
			shootingStars[i].Reset(_starPositions[i] + _position + new Vector2(0f, -300f), _velocity);
			shootingStars[i].TurnOn();
		}
	}

	private void ResetGame(bool meat)
	{
		scoreSystem.Reset();
		ResetChoreography(1, alternating: false, meat);
		tunnel.Reset(40, 3);
		helicopter.Reset();
		bSpriteManager.Reset();
		currEvent = 0;
	}

	private void ResetChoreography(int index, bool alternating, bool meat)
	{
		ResetLights();
		ResetShootingStars();
		ResetHeart(index, alternating);
		fireworks.TurnOff();
		tunnel.Set(TunnelEffect.Normal);
		tunnel.SetColor(Color.White, Color.Black, Color.White);
		scoreSystem.TurnOffBass();
		scoreSystem.TurnOffMovement();
		background.SetVelocity(200f);
		Laser[] array = lasers;
		foreach (Laser laser in array)
		{
			laser.TurnOff();
		}
		spreadLaser.TurnOff();
		lyricEffect.TurnOff();
		for (int j = 0; j < shootingStars.Length; j++)
		{
			shootingStars[j].TurnOff();
		}
		hand.Reset();
		rainbow.Reset();
		eyes.TurnOff();
		flashManager.Reset();
		shineManager.Reset();
		fluctuationManager.Reset();
		butterflyEffect.Reset();
		karaokeLyrics.TurnOff();
		equalizer.TurnOff();
		meatToMouth.TurnOff();
		explosionManager.TurnOff();
		dancerManager.TurnOff();
		heartsManager.TurnOff();
		Camera.Reset();
		if (!meat)
		{
			Camera.SetEffect(-1);
		}
	}

	private void ResetLights()
	{
		Light[] array = lights;
		foreach (Light light in array)
		{
			light.TurnOff();
		}
	}

	private void ResetShootingStars()
	{
		ParticleEmitter[] array = shootingStars;
		foreach (ParticleEmitter particleEmitter in array)
		{
			particleEmitter.TurnOff();
		}
	}

	private void ResetHeart(int index, bool alternating)
	{
		heart.Reset(index, alternating);
	}
}
