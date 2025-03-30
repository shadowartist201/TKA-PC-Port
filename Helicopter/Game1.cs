using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input.Touch;

namespace Helicopter
{
    public class Game1 : Game
    {
        private RenderTarget2D renderTarget;

        private TouchLocation[] del = {new TouchLocation(0, TouchLocationState.Released, Vector2.Zero)};
        public static TouchCollection touchLocations;

        public static Vector2 resolutionDifference;

        private bool clappersOn, lettersOn;

        public static Texture2D overlay;

        private bool rainbowOverlayEnabled = false;

        public static Rectangle safeSpace;

        public static Vector2 touchOffset;

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

		public static SpriteBatch spriteBatch;

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

		private ClapperManager clapperManager;

		private LetterManager letterManager;

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

        private Vector2[] _heartPositions = new Vector2[16]
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

        private Vector2[] _starPositions = new Vector2[20]
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
            ((Collection<IGameComponent>)(object)base.Components).Add((IGameComponent)Global.DeviceManager);
            Global.DeviceManager.DeviceSelectorCanceled += DeviceSelectorCanceled;
            Global.DeviceManager.DeviceDisconnected += DeviceDisconnected;
            Global.DeviceManager.PromptForDevice();
            base.Exiting += (EventHandler<EventArgs>)OnExit;
            this.graphics = new GraphicsDeviceManager(this);
            base.Content.RootDirectory = "Content";
            Resolution.Init(ref graphics);
            Resolution.SetVirtualResolution(1280, 720);
            Resolution.SetResolution(1280,720, false);
            base.IsFixedTimeStep = false;
        }

        private Viewport ResetViewport()
        {
            float targetAspectRatio = 1280f/720f;
            // figure out the largest area that fits in this resolution at the desired aspect ratio
            int width = graphics.PreferredBackBufferWidth;
            int height = (int)(width / targetAspectRatio + .5f);

            if (height > graphics.PreferredBackBufferHeight)
            {
                height = graphics.PreferredBackBufferHeight;
                // PillarBox
                width = (int)(height * targetAspectRatio + .5f);
            }

            // set up the new viewport centered in the backbuffer
            Viewport viewport = new Viewport();

            viewport.X = (graphics.PreferredBackBufferWidth / 2) - (width / 2);
            viewport.Y = (graphics.PreferredBackBufferHeight / 2) - (height / 2);
            viewport.Width = width;
            viewport.Height = height;
            viewport.MinDepth = 0;
            viewport.MaxDepth = 1;

            return viewport;
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
            //MediaPlayer.IsVisualizationEnabled = true;
            this.renderTarget = new RenderTarget2D(base.GraphicsDevice, 1280, 720, mipMap: false, SurfaceFormat.Color, DepthFormat.None);
			Global.audioEngine = new AudioEngine("Content/Music/newXactProject.xgs");
			Global.waveBank = new WaveBank(Global.audioEngine, "Content/Music/Wave Bank.xwb");
			Global.soundBank = new SoundBank(Global.audioEngine, "Content/Music/Sound Bank.xsb");
            Global.itemSelectedEffect = new ItemSelectedEffect();
            Viewport test = ResetViewport();
            safeSpace = test.Bounds;
            touchOffset = new Vector2(test.X, test.Y);
            resolutionDifference = new Vector2(1280f/test.Width, 720f/test.Height);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            Global.pixel = new Texture2D(base.GraphicsDevice, 1, 1);
            spriteBatch = new SpriteBatch(base.GraphicsDevice);
            this.LoadAssets();
            this.LoadMenus();
            this.LoadBackground();
            this.LoadHelicopter();
            this.LoadForeground();
            this.LoadEventInfo(0);
            MediaPlayer.Play(this.songManager.CurrentSong);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.10f;
            base.LoadContent();
        }

        protected override void UnloadContent()
        {
        }

        private void OnExit(object o, EventArgs e)
        {
            this.scoreSystem.SaveInfo();
        }

        protected override void Update(GameTime gameTime)
        {
            touchLocations = TouchPanel.GetState();
            if (touchLocations.Count == 0)
                touchLocations = new TouchCollection(del);
            Rectangle startButton = new(373,533,534,135);
            Rectangle pauseButtonRec = new(50, 50, 64, 64);
            float num = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float elapsedMilliseconds = (float)MediaPlayer.PlayPosition.TotalMilliseconds;
            //Global.IsTrialMode = Guide.IsTrialMode;
            this.currInput.Update();
            if (this.currInput.IsButtonUp(Buttons.A))
            {
                this.justStarted = false;
            }
            Global.mountainVelocity = this.background.GetVelocity();
            switch (this.gameState)
            {
                case GameState.OPENING:
                    {
                        if (this.splashScreen)
                        {
                            this.splashScreenTimer += num;
                            if (this.splashScreenTimer > this.splashScreenTime)
                            {
                                if (this.splashScreenIndex == 1)
                                {
                                    this.splashScreen = false;
                                }
                                else
                                {
                                    this.splashScreenIndex = 1;
                                }
                                this.splashScreenTimer = 0f;
                            }
                            break;
                        }
                        this.openingMenu.Update(num, this.currInput, ref this.gameState);
                        this.mainMenu.UpdateBackground(num);
                        for (PlayerIndex playerIndex = PlayerIndex.One; playerIndex <= PlayerIndex.Four; playerIndex++)
                        {
                            if (GamePad.GetState(playerIndex).IsButtonDown(Buttons.Start) || (startButton.Contains((touchLocations[0].Position-touchOffset) * resolutionDifference) && currInput.IsThingTouched()))
                            {
                                Global.PlayCatSound();
                                Global.playerIndex = playerIndex;
                                this.scoreSystem.LoadInfo();
                                this.gameState = GameState.MAIN_MENU;
                                break;
                            }
                        }
                        if (this.currInput.IsButtonDown(Buttons.Start) || (startButton.Contains((touchLocations[0].Position - touchOffset) * resolutionDifference) && currInput.IsThingTouched()))
                        {
                            Global.PlayCatSound();
                            if (!Global.playerIndex.HasValue)
                            {
                                Global.playerIndex = PlayerIndex.One;
                            }
                            this.scoreSystem.LoadInfo();
                            this.gameState = GameState.MAIN_MENU;
                        }
                        break;
                    }
                case GameState.MAIN_MENU:
                    this.mainMenu.Update(num, this.currInput, ref this.gameState);
                    switch (this.gameState)
                    {
                        case GameState.OPTIONS:
                            this.optionsMenu.SetLastGameState(GameState.MAIN_MENU);
                            break;
                        case GameState.STAGE_SELECT:
                            this.stageSelectMenu.SetLastGameState(GameState.MAIN_MENU);
                            break;
                        case GameState.LEADERBOARDS:
                            this.leaderboardsMenu.SetLastGameState(GameState.MAIN_MENU);
                            break;
                    }
                    break;
                case GameState.STAGE_SELECT:
                    this.stageSelectMenu.Update(num, this.currInput, ref this.gameState);
                    switch (this.gameState)
                    {
                        case GameState.CAT_SELECT:
                            this.songManager.LoadNewSong(this.stageSelectMenu.getCurrentLevel());
                            this.LoadEventInfo(this.stageSelectMenu.getCurrentLevel());
                            this.background.LoadNewBackground(this.stageSelectMenu.getCurrentLevel());
                            this.bSpriteManager.LoadNewBackground(this.stageSelectMenu.getCurrentLevel());
                            this.ResetGame(this.stageSelectMenu.getCurrentLevel() == 3);
                            MediaPlayer.Play(this.songManager.CurrentSong);
                            this.catSelectMenu.SetLastGameState(GameState.STAGE_SELECT);
                            break;
                        case GameState.PLAY:
                            this.songManager.LoadNewSong(this.stageSelectMenu.getCurrentLevel());
                            this.LoadEventInfo(this.stageSelectMenu.getCurrentLevel());
                            this.background.LoadNewBackground(this.stageSelectMenu.getCurrentLevel());
                            this.bSpriteManager.LoadNewBackground(this.stageSelectMenu.getCurrentLevel());
                            this.ResetGame(this.stageSelectMenu.getCurrentLevel() == 3);
                            if (this.stageSelectMenu.getCurrentLevel() == 4)
                            {
                                Camera.SetEffect(-1);
                            }
                            MediaPlayer.Play(this.songManager.CurrentSong);
                            this.justStarted = true;
                            Global.ResetVibration();
                            break;
                    }
                    break;
                case GameState.CAT_SELECT:
                    this.catSelectMenu.Update(num, this.currInput, ref this.gameState, this.stageSelectMenu.getCurrentLevel(), this.scoreSystem);
                    switch (this.gameState)
                    {
                        case GameState.PLAY:
                            this.helicopter.ChangeAnimation(this.catSelectMenu.getCurrentCat());
                            Global.SetVibrationResume();
                            MediaPlayer.Resume();
                            break;
                        case GameState.STAGE_SELECT:
                            Global.ResetVibration();
                            MediaPlayer.Stop();
                            this.songManager.LoadNewSong(-1);
                            MediaPlayer.Play(this.songManager.CurrentSong);
                            break;
                    }
                    if (this.catSelectMenu.LastGameState != GameState.PAUSE)
                    {
                        this.UpdateChoreography(num, elapsedMilliseconds, this.stageSelectMenu.getCurrentLevel());
                        this.UpdateBackground(num);
                        this.UpdateForeground(num);
                    }
                    break;
                case GameState.PLAY:
                    this.UpdateChoreography(num, elapsedMilliseconds, this.stageSelectMenu.getCurrentLevel());
                    this.UpdateBackground(num);
                    this.UpdateHelicopter(num);
                    this.UpdateForeground(num);
                    if (this.currInput.IsButtonPressed(Buttons.Start) || (pauseButtonRec.Contains((touchLocations[0].Position - touchOffset) * resolutionDifference) && currInput.IsThingTouched()))
                    {
                        MediaPlayer.Pause();
                        Global.SetVibrationPause();
                        this.gameState = GameState.PAUSE;
                    }
                    break;
                case GameState.PAUSE:
                    this.pauseMenu.Update(num, this.currInput, ref this.gameState);
                    switch (this.gameState)
                    {
                        case GameState.PLAY:
                            Global.SetVibrationResume();
                            MediaPlayer.Resume();
                            break;
                        case GameState.OPTIONS:
                            this.optionsMenu.SetLastGameState(GameState.PAUSE);
                            break;
                        case GameState.MAIN_MENU:
                            Global.ResetVibration();
                            MediaPlayer.Stop();
                            this.songManager.LoadNewSong(-1);
                            MediaPlayer.Play(this.songManager.CurrentSong);
                            break;
                        case GameState.STAGE_SELECT:
                            this.stageSelectMenu.SetLastGameState(GameState.PAUSE);
                            break;
                        case GameState.CAT_SELECT:
                            this.catSelectMenu.SetLastGameState(GameState.PAUSE);
                            break;
                        case GameState.LEADERBOARDS:
                            this.leaderboardsMenu.SetLastGameState(GameState.PAUSE);
                            break;
                    }
                    break;
                case GameState.TRIAL_PAUSE:
                    this.trialMenu.Update(num, this.currInput, ref this.gameState);
                    if (this.gameState == GameState.MAIN_MENU)
                    {
                        Global.ResetVibration();
                        MediaPlayer.Stop();
                        this.songManager.LoadNewSong(-1);
                        MediaPlayer.Play(this.songManager.CurrentSong);
                    }
                    break;
                case GameState.OPTIONS:
                    this.optionsMenu.Update(num, this.currInput, ref this.gameState);
                    break;
                case GameState.LEADERBOARDS:
                    this.leaderboardsMenu.Update(num, this.currInput, ref this.gameState);
                    break;
                case GameState.CREDITS:
                    this.creditsMenu.Update(num, this.currInput, ref this.gameState);
                    break;
                case GameState.EXIT:
                    base.Exit();
                    break;
            }
            /*if (Global.IsTrialMode && this.scoreSystem.CurrScore > 30000 && !this.helicopter.IsDead())
			{
				this.scoreSystem.CurrScore = 30000;
				this.helicopter.Kill();
				this.tunnel.velocity = 0f;
				this.scoreSystem.End(this.stageSelectMenu.getCurrentLevel(), this.catSelectMenu.getCurrentCat());
				MediaPlayer.Pause();
				Global.SetVibrationPause();
				this.gameState = GameState.TRIAL_PAUSE;
				this.trialMenu.ResetStartTimer();
			}*/
            this.currInput.EndUpdate();
            //Global.audioEngine.Update();
            Global.UpdateVibration(num);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            Resolution.BeginDraw();
            float num = (float)gameTime.ElapsedGameTime.TotalSeconds;
            this.total += num;
            if (this.total >= 1f)
            {
                this.frameTime = this.fps;
                this.fps = 0f;
                this.total = 0f;
            }
            this.fps += 1f;
            if (this.gameState == GameState.CAT_SELECT || this.gameState == GameState.PLAY || this.gameState == GameState.PAUSE)
            {
                base.GraphicsDevice.SetRenderTarget(this.renderTarget);
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, null, null, null, null);
                this.DrawStage(num, gameTime);
                spriteBatch.End();
                Camera.Draw(spriteBatch, this.renderTarget, this.graphics, base.GraphicsDevice);
            }
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, null, null, null, null, Resolution.getTransformationMatrix());
            this.DrawMenu();
            spriteBatch.End();
        }

        private void LoadAssets()
        {
            Global.spriteFont = base.Content.Load<SpriteFont>("font");
            Global.menuFont = base.Content.Load<SpriteFont>("Fonts/MenuFont");
            Global.tunnelStar = base.Content.Load<Texture2D>("Graphics/Tunnel/star");
            Global.scoreTexture = base.Content.Load<Texture2D>("Graphics/Score/score");
            Global.highScoreTexture = base.Content.Load<Texture2D>("Graphics/Score/high");
            Global.numbersTexture = base.Content.Load<Texture2D>("Graphics/Score/numbers");
            Global.creditsTex = base.Content.Load<Texture2D>("Graphics/Menu/Credits/credits_bgMenu");
            Global.splashTex = base.Content.Load<Texture2D>("Graphics/Menu/Splash/splash_bgMenu");
            Global.selectCatTex = base.Content.Load<Texture2D>("Graphics/Menu/KittenSelect/selectCat_bgMenu");
            Global.selectStageTex = base.Content.Load<Texture2D>("Graphics/Menu/StageSelect/selectStage_bgMenu");
            Global.pauseTex = base.Content.Load<Texture2D>("Graphics/Menu/Pause/pause_bgMenu");
            Global.optionsTex = base.Content.Load<Texture2D>("Graphics/Menu/Options/options_bgMenu");
            Global.leaderboardTex = base.Content.Load<Texture2D>("Graphics/Menu/Leaderboards/leaderboards_bgMenu");
            Global.trialTex = base.Content.Load<Texture2D>("Graphics/Menu/Trial/trial_bgMenu");
            Global.mainTex = base.Content.Load<Texture2D>("Graphics/Menu/Main/main_bgMenu");
            Global.mainCatTex = base.Content.Load<Texture2D>("Graphics/Menu/Main/main_cat");
            Global.mainCircleTex = base.Content.Load<Texture2D>("Graphics/Menu/Main/main_circle");
            Global.mainStarTex = base.Content.Load<Texture2D>("Graphics/Menu/Main/main_star");
            Global.mainTitleTex = base.Content.Load<Texture2D>("Graphics/Menu/Main/main_title");
            Global.mainPressStartTex = base.Content.Load<Texture2D>("Graphics/Menu/Main/main_pressStart");
            Global.bigStar = base.Content.Load<Texture2D>("Graphics/StarEffect/bigstar");
            Global.stars = new Texture2D[7]
            {
                base.Content.Load<Texture2D>("Graphics/StarEffect/star1"),
                base.Content.Load<Texture2D>("Graphics/StarEffect/star2"),
                base.Content.Load<Texture2D>("Graphics/StarEffect/star3"),
                base.Content.Load<Texture2D>("Graphics/StarEffect/star4"),
                base.Content.Load<Texture2D>("Graphics/StarEffect/star5"),
                base.Content.Load<Texture2D>("Graphics/StarEffect/star6"),
                base.Content.Load<Texture2D>("Graphics/StarEffect/star7")
            };
            Global.heartsTex = base.Content.Load<Texture2D>("Graphics/Effects/hearts");
            Global.butterflyParticles = base.Content.Load<Texture2D>("Graphics/Effects/Butterflies");
            Global.equalizerBar = base.Content.Load<Texture2D>("Graphics/Effects/EqualizerBar");
            Global.mouth = base.Content.Load<Texture2D>("Graphics/Effects/mouth");
            Global.meatsToMouth = base.Content.Load<Texture2D>("Graphics/Effects/meatsToMouth");
            Global.pelvicTex = base.Content.Load<Texture2D>("Graphics/Effects/danceFull");
            Global.explosionTex = base.Content.Load<Texture2D>("Graphics/Effects/explosion");
            Global.searchingEyes = base.Content.Load<Texture2D>("Graphics/Effects/SearchingEyes");
            Global.rainbow2 = base.Content.Load<Texture2D>("Graphics/BackgroundSprites/rainbow3");
            Global.rainbow3 = base.Content.Load<Texture2D>("Graphics/BackgroundSprites/hugeRainbow");
            Global.shineShapes = new Texture2D[3];
            Global.shineShapes[0] = base.Content.Load<Texture2D>("Graphics/Effects/lightShape1");
            Global.shineShapes[1] = base.Content.Load<Texture2D>("Graphics/Effects/lightShape2");
            Global.shineShapes[2] = base.Content.Load<Texture2D>("Graphics/Effects/lightShape3");
            Global.fluctuationShape = base.Content.Load<Texture2D>("Graphics/Effects/fluctuationShape");
            Global.feelWantTouch = base.Content.Load<Texture2D>("Graphics/Effects/FeelWantTouch");
            Global.lightEffect = base.Content.Load<Texture2D>("Graphics/light2");
            Global.hugestar = base.Content.Load<Texture2D>("Graphics/Symbols/hugestar");
            Global.hugeHeart = base.Content.Load<Texture2D>("Graphics/Symbols/hugeheart");
            Global.hand = base.Content.Load<Texture2D>("Graphics/hand");
            Global.reachingHand = base.Content.Load<Texture2D>("Graphics/Effects/reachingHand");
            Global.rainbow = base.Content.Load<Texture2D>("Graphics/rainbow2");
            Global.hugeAtom = base.Content.Load<Texture2D>("Graphics/Symbols/hugeAtom");
            Global.hugeButterfly = base.Content.Load<Texture2D>("Graphics/Symbols/hugeButterfly");
            Global.hugeCat = base.Content.Load<Texture2D>("Graphics/Symbols/hugeCat");
            Global.hugeCrown = base.Content.Load<Texture2D>("Graphics/Symbols/hugeCrown");
            Global.hugeMoon = base.Content.Load<Texture2D>("Graphics/Symbols/hugeMoon");
            Global.hugeRabbit = base.Content.Load<Texture2D>("Graphics/Symbols/hugeRabit");
            Global.nyanHands = base.Content.Load<Texture2D>("Graphics/Effects/nyanHands");
            Global.nyanLetters = base.Content.Load<Texture2D>("Graphics/Effects/nyanLetters");
            Global.cats = base.Content.Load<Texture2D>("Graphics/cats");
            Global.AButtonTexture = base.Content.Load<Texture2D>("Graphics/xboxControllerButtonA");
            Global.YButtonTexture = base.Content.Load<Texture2D>("Graphics/xboxControllerButtonY");
            Global.pauseButton = base.Content.Load<Texture2D>("Graphics/pause");
            Global.soundEffects.Add(Content.Load<SoundEffect>("Music/cat_01"));
            Global.soundEffects.Add(Content.Load<SoundEffect>("Music/cat_02"));
            Global.soundEffects.Add(Content.Load<SoundEffect>("Music/cat_03"));
            Global.soundEffects.Add(Content.Load<SoundEffect>("Music/cat_04"));
            for (int i = 0; i < Camera.effects.Length; i++)
            {
                //Camera.effects[i] = base.Content.Load<Effect>("Effects/effect" + i);
            }
            Camera.effects[5] = base.Content.Load<Effect>("drunk");
            //Camera.effects[4] = base.Content.Load<Effect>("shakezigzag");
            Camera.effects[4] = base.Content.Load<Effect>("Effects/effect4");
            Camera.effects[3] = base.Content.Load<Effect>("wave");
            Camera.effects[2] = base.Content.Load<Effect>("circles");
            Camera.effects[1] = base.Content.Load<Effect>("outline");
            //Camera.effects[0] = base.Content.Load<Effect>("shakeblur");
            Camera.effects[0] = base.Content.Load<Effect>("Effects/effect0");
            overlay = base.Content.Load<Texture2D>("rainbowOverlay");
            this.scoreSystem = new ScoreSystem();
            this.songManager = new SongManager(this);
            Global.setPixel(GraphicsDevice);
        }

        private void LoadMenus()
        {
            this.openingMenu = new OpeningMenu();
            this.pauseMenu = new PauseMenu();
            this.trialMenu = new TrialMenu();
            this.mainMenu = new MainMenu();
            this.optionsMenu = new OptionsMenu();
            this.creditsMenu = new CreditsMenu();
            this.stageSelectMenu = new StageSelectMenu();
            this.catSelectMenu = new CatSelectMenu();
            this.leaderboardsMenu = new LeaderboardsMenu();
        }

        private void LoadBackground()
        {
            this.background = new Background(this);
            this.tunnel = new Tunnel(40, 3);
            this.bSpriteManager = new BSpriteManager(this);
        }

        private void LoadHelicopter()
        {
            this.helicopter = new Helicopter();
        }

        private void LoadForeground()
        {
            this.heart = new Heart();
            this.rainbow = new Rainbow();
            this.eyes = new Eyes();
            this.hand = new Hand();
            this.fireworks = new Fireworks(8);
            for (int i = 0; i < this.shootingStars.Length; i++)
            {
                this.shootingStars[i] = new ParticleEmitter(new Vector2(i * 60, -i * 20), new Vector2(100f, 300f), 2);
            }
            for (int i = 0; i < this.lights.Length; i++)
            {
                this.lights[i] = new Light(new Vector2(i * 140, 0f), Vector2.Zero);
            }
            for (int i = 0; i < this.lasers.Length; i++)
            {
                this.lasers[i] = new Laser(Vector2.Zero, Color.LimeGreen);
            }
            this.spreadLaser = new SpreadLaser();
            this.lyricEffect = new LyricEffect();
            this.flashManager = new FlashManager();
            this.shineManager = new ShineEffectManager();
            this.fluctuationManager = new FluctuationManager();
            this.butterflyEffect = new ButterflyEffect();
            this.karaokeLyrics = new KaraokeLyrics();
            this.equalizer = new Equalizer();
            this.meatToMouth = new MeatToMouth();
            this.explosionManager = new ExplosionManager();
            this.dancerManager = new DancerManager();
			this.letterManager = new LetterManager();
			this.clapperManager = new ClapperManager();
            this.heartsManager = new HeartsManager();
        }

        private void LoadEventInfo(int currentLevel)
        {
            switch (currentLevel)
            {
                case 0:
                    this.LoadEventInfoSeaOfLove();
                    break;
                case 1:
                    this.LoadEventInfoLikeARainbow();
                    break;
                case 2:
                    this.LoadEventInfoYoureShining();
                    break;
                case 3:
                    this.LoadEventInfoTasteOfHeaven();
                    break;
                case 4:
                    this.LoadEventInfoIntergalacticalHigh();
                    break;
                case 5:
                    this.LoadEventInfoMyRainbow();
                    break;
            }
        }

        private void LoadEventInfoSeaOfLove()
        {
            this.eventTimes[0] = 0f;
            this.eventTimes[1] = 1594.5f;
            this.eventTimes[2] = 6900f;
            this.eventTimes[3] = 12100f;
            this.eventTimes[4] = 12300f;
            this.eventTimes[5] = 17800f;
            this.eventTimes[6] = 23000f;
            this.eventTimes[7] = 25500f;
            this.eventTimes[8] = 28000f;
            this.eventTimes[9] = 28500f;
            this.eventTimes[10] = 31000f;
            this.eventTimes[11] = 32800f;
            this.eventTimes[12] = 34200f;
            this.eventTimes[13] = 45300f;
            this.eventTimes[14] = 56000f;
            this.eventTimes[15] = 57000f;
            this.eventTimes[16] = 59000f;
            this.eventTimes[17] = 70000f;
            this.eventTimes[18] = 74500f;
            this.eventTimes[19] = 79000f;
            this.eventTimes[20] = 80500f;
            this.eventTimes[21] = 102000f;
            this.eventTimes[22] = 102680f;
            this.eventTimes[23] = 124500f;
            this.eventTimes[24] = 129700f;
            this.eventTimes[25] = 135200f;
            this.eventTimes[26] = 140700f;
            this.eventTimes[27] = 146200f;
            this.eventTimes[28] = 151200f;
            this.eventTimes[29] = 157100f;
            this.eventTimes[30] = 157000f;
            this.eventTimes[31] = 159500f;
            this.eventTimes[32] = 162000f;
            this.eventTimes[33] = 162600f;
            this.eventTimes[34] = 165500f;
            this.eventTimes[35] = 167400f;
            this.eventTimes[36] = 168000f;
            this.eventTimes[37] = 180500f;
            this.eventTimes[38] = 189500f;
            this.eventTimes[39] = 192500f;
            this.eventTimes[40] = 235000f;
            this.eventTimes[41] = 235500f;
        }

        private void LoadEventInfoLikeARainbow()
        {
            this.eventTimes[0] = 0f;
            this.eventTimes[1] = 726f;
            this.eventTimes[2] = 766f;
            this.eventTimes[3] = 6192f;
            this.eventTimes[4] = 11650f;
            this.eventTimes[5] = 17148f;
            this.eventTimes[6] = 22742f;
            this.eventTimes[7] = 44662f;
            this.eventTimes[8] = 58873f;
            this.eventTimes[9] = 66526f;
            this.eventTimes[10] = 66598f;
            this.eventTimes[11] = 69111f;
            this.eventTimes[12] = 75168f;
            this.eventTimes[13] = 81073f;
            this.eventTimes[14] = 85837f;
            this.eventTimes[15] = 88510f;
            this.eventTimes[16] = 93960f;
            this.eventTimes[17] = 99418f;
            this.eventTimes[18] = 99594f;
            this.eventTimes[19] = 104916f;
            this.eventTimes[20] = 105004f;
            this.eventTimes[21] = 107502f;
            this.eventTimes[22] = 107645f;
            this.eventTimes[23] = 109085f;
            this.eventTimes[24] = 110542f;
            this.eventTimes[25] = 113015f;
            this.eventTimes[26] = 118865f;
            this.eventTimes[27] = 124809f;
            this.eventTimes[28] = 129685f;
            this.eventTimes[29] = 131065f;
            this.eventTimes[30] = 132510f;
            this.eventTimes[31] = 150065f;
            this.eventTimes[32] = 154358f;
            this.eventTimes[33] = 165354f;
            this.eventTimes[34] = 170764f;
            this.eventTimes[35] = 173517f;
            this.eventTimes[36] = 176318f;
            this.eventTimes[37] = 198206f;
            this.eventTimes[38] = 200760f;
            this.eventTimes[39] = 206729f;
            this.eventTimes[40] = 209234f;
            this.eventTimes[41] = 213323f;
            this.eventTimes[42] = 214748f;
            this.eventTimes[43] = 217445f;
            this.eventTimes[44] = 218802f;
            this.eventTimes[45] = 220190f;
            this.eventTimes[46] = 242142f;
            this.eventTimes[47] = 244640f;
            this.eventTimes[48] = 250609f;
            this.eventTimes[49] = 257098f;
            this.eventTimes[50] = 259905f;
            this.eventTimes[51] = 261334f;
            this.eventTimes[52] = 264015f;
        }

        private void LoadEventInfoYoureShining()
        {
            this.eventTimes[0] = 0f;
            this.eventTimes[1] = 2900f;
            this.eventTimes[2] = 5743f;
            this.eventTimes[3] = 8566f;
            this.eventTimes[4] = 11019f;
            this.eventTimes[5] = 11370f;
            this.eventTimes[6] = 16676f;
            this.eventTimes[7] = 22152f;
            this.eventTimes[8] = 27918f;
            this.eventTimes[9] = 33540f;
            this.eventTimes[10] = 34429f;
            this.eventTimes[11] = 35887f;
            this.eventTimes[12] = 37522f;
            this.eventTimes[13] = 39156f;
            this.eventTimes[14] = 41525f;
            this.eventTimes[15] = 45235f;
            this.eventTimes[16] = 45637f;
            this.eventTimes[17] = 47085f;
            this.eventTimes[18] = 48751f;
            this.eventTimes[19] = 52970f;
            this.eventTimes[20] = 56587f;
            this.eventTimes[21] = 68200f;
            this.eventTimes[22] = 69694f;
            this.eventTimes[23] = 71092f;
            this.eventTimes[24] = 72528f;
            this.eventTimes[25] = 73925f;
            this.eventTimes[26] = 75323f;
            this.eventTimes[27] = 76683f;
            this.eventTimes[28] = 78118f;
            this.eventTimes[29] = 79549f;
            this.eventTimes[30] = 80917f;
            this.eventTimes[31] = 82664f;
            this.eventTimes[32] = 86532f;
            this.eventTimes[33] = 90873f;
            this.eventTimes[34] = 92248f;
            this.eventTimes[35] = 93951f;
            this.eventTimes[36] = 97912f;
            this.eventTimes[37] = 101803f;
            this.eventTimes[38] = 101850f;
            this.eventTimes[39] = 107578f;
            this.eventTimes[40] = 110402f;
            this.eventTimes[41] = 113047f;
            this.eventTimes[42] = 113225f;
            this.eventTimes[43] = 113462f;
            this.eventTimes[44] = 114889f;
            this.eventTimes[45] = 116049f;
            this.eventTimes[46] = 116300f;
            this.eventTimes[47] = 117712f;
            this.eventTimes[48] = 118873f;
            this.eventTimes[49] = 119139f;
            this.eventTimes[50] = 120529f;
            this.eventTimes[51] = 121692f;
            this.eventTimes[52] = 121940f;
            this.eventTimes[53] = 122595f;
            this.eventTimes[54] = 124345f;
            this.eventTimes[55] = 146931f;
            this.eventTimes[56] = 147331f;
            this.eventTimes[57] = 148772f;
            this.eventTimes[58] = 149775f;
            this.eventTimes[59] = 150468f;
            this.eventTimes[60] = 152571f;
            this.eventTimes[61] = 154514f;
            this.eventTimes[62] = 155409f;
            this.eventTimes[63] = 158226f;
            this.eventTimes[64] = 158582f;
            this.eventTimes[65] = 159980f;
            this.eventTimes[66] = 161042f;
            this.eventTimes[67] = 161690f;
            this.eventTimes[68] = 163851f;
            this.eventTimes[69] = 165816f;
            this.eventTimes[70] = 166697f;
            this.eventTimes[71] = 169520f;
            this.eventTimes[72] = 170008f;
            this.eventTimes[73] = 171398f;
            this.eventTimes[74] = 172810f;
            this.eventTimes[75] = 174222f;
            this.eventTimes[76] = 175029f;
            this.eventTimes[77] = 175633f;
            this.eventTimes[78] = 177038f;
            this.eventTimes[79] = 178457f;
            this.eventTimes[80] = 179528f;
            this.eventTimes[81] = 179876f;
            this.eventTimes[82] = 180808f;
            this.eventTimes[83] = 192619f;
            this.eventTimes[84] = 194002f;
            this.eventTimes[85] = 195428f;
            this.eventTimes[86] = 196825f;
            this.eventTimes[87] = 198223f;
            this.eventTimes[88] = 199620f;
            this.eventTimes[89] = 201032f;
            this.eventTimes[90] = 202473f;
            this.eventTimes[91] = 203972f;
            this.eventTimes[92] = 205325f;
            this.eventTimes[93] = 206999f;
            this.eventTimes[94] = 210842f;
            this.eventTimes[95] = 215150f;
            this.eventTimes[96] = 216518f;
            this.eventTimes[97] = 218352f;
            this.eventTimes[98] = 222267f;
            this.eventTimes[99] = 225993f;
        }

        private void LoadEventInfoTasteOfHeaven()
        {
            this.eventTimes[0] = 0f;
            this.eventTimes[1] = 7999f;
            this.eventTimes[2] = 8648f;
            this.eventTimes[3] = 9332f;
            this.eventTimes[4] = 10712f;
            this.eventTimes[5] = 21330f;
            this.eventTimes[6] = 32008f;
            this.eventTimes[7] = 37340f;
            this.eventTimes[8] = 39983f;
            this.eventTimes[9] = 41352f;
            this.eventTimes[10] = 42661f;
            this.eventTimes[11] = 63992f;
            this.eventTimes[12] = 83955f;
            this.eventTimes[13] = 85347f;
            this.eventTimes[14] = 90644f;
            this.eventTimes[15] = 112011f;
            this.eventTimes[16] = 133342f;
            this.eventTimes[17] = 143960f;
            this.eventTimes[18] = 154661f;
            this.eventTimes[19] = 157339f;
            this.eventTimes[20] = 159993f;
            this.eventTimes[21] = 160678f;
            this.eventTimes[22] = 161327f;
            this.eventTimes[23] = 165362f;
            this.eventTimes[24] = 207988f;
            this.eventTimes[25] = 210000f;
        }

        private void LoadEventInfoIntergalacticalHigh()
        {
            this.eventTimes[0] = 0.528f;
            this.eventTimes[1] = 5561f;
            this.eventTimes[2] = 11411f;
            this.eventTimes[3] = 20776f;
            this.eventTimes[4] = 22015f;
            this.eventTimes[5] = 44290f;
            this.eventTimes[6] = 66094f;
            this.eventTimes[7] = 75795f;
            this.eventTimes[8] = 77111f;
            this.eventTimes[9] = 99155f;
            this.eventTimes[10] = 110143f;
            this.eventTimes[11] = 121190f;
            this.eventTimes[12] = 143205f;
            this.eventTimes[13] = 154184f;
            this.eventTimes[14] = 176247f;
            this.eventTimes[15] = 198234f;
            this.eventTimes[16] = 220383f;
            this.eventTimes[17] = 231247f;
            this.eventTimes[18] = 232553f;
        }

        private void LoadEventInfoMyRainbow()
        {
            this.eventTimes[0] = 0f;
            this.eventTimes[1] = 11268f;
            this.eventTimes[2] = 22537f;
            this.eventTimes[3] = 28059f;
            this.eventTimes[4] = 33806f;
            this.eventTimes[5] = 39440f;
            this.eventTimes[6] = 45075f;
            this.eventTimes[7] = 55104f;
            this.eventTimes[8] = 56456f;
            this.eventTimes[9] = 79107f;
            this.eventTimes[10] = 88291f;
            this.eventTimes[11] = 90376f;
            this.eventTimes[12] = 98200f;
            this.eventTimes[13] = 101644f;
            this.eventTimes[14] = 112913f;
            this.eventTimes[15] = 118435f;
            this.eventTimes[16] = 124182f;
            this.eventTimes[17] = 127112f;
            this.eventTimes[18] = 138494f;
            this.eventTimes[19] = 171061f;
            this.eventTimes[20] = 183456f;
            this.eventTimes[21] = 240139f;
            this.eventTimes[22] = 250000f;
        }

        private void UpdateBackground(float dt)
        {
            this.background.Update(dt);
            this.bSpriteManager.Update(dt);
            this.tunnel.Update(dt, this.helicopter, this.scoreSystem, this.stageSelectMenu.getCurrentLevel(), this.catSelectMenu.getCurrentCat());
        }

        private void UpdateHelicopter(float dt)
        {
            if (!this.justStarted)
            {
                this.helicopter.HandleInput(this.currInput, this.tunnel, this.scoreSystem, dt);
            }
            this.helicopter.Update(dt, this.tunnel, this.scoreSystem);
        }

        private void UpdateForeground(float dt)
        {
            this.fireworks.Update(dt);
            ParticleEmitter[] array = this.shootingStars;
            foreach (ParticleEmitter particleEmitter in array)
            {
                particleEmitter.Update(dt);
            }
            this.UpdateStarShower();
            Light[] array2 = this.lights;
            foreach (Light light in array2)
            {
                light.Update(dt);
            }
            Laser[] array3 = this.lasers;
            foreach (Laser laser in array3)
            {
                laser.Update(dt);
            }
            this.spreadLaser.Update(dt);
            this.lyricEffect.Update(dt);
            this.heart.Update(dt);
            this.rainbow.Update(dt);
            this.eyes.Update(dt);
            this.hand.Update(dt);
            this.flashManager.Update(dt);
            this.shineManager.Update(dt, this.helicopter.position);
            this.fluctuationManager.Update(dt, this.helicopter.position);
            this.butterflyEffect.Update(dt, this.helicopter.position);
            this.equalizer.Update(dt);
            this.meatToMouth.Update(dt);
            this.explosionManager.Update(dt);
            this.dancerManager.Update(dt);
            this.heartsManager.Update(dt);
			this.clapperManager.Update(dt);
			this.letterManager.Update(dt);
            this.scoreSystem.Update(dt);
        }

        private void UpdateStarShower()
        {
            if (!this.starShowerActive)
            {
                return;
            }
            if (this.starsInitiated == 13)
            {
                this.dy = 150f;
            }
            if (this.starsInitiated == 43)
            {
                this.dy = 75f;
            }
            if (this.shootingStars[this.starCount].currPosition.Y > 800f)
            {
                if (this.shootingStars[(this.starCount + 19) % 20].currPosition.X + this.dx < 130f || this.shootingStars[(this.starCount + 19) % 20].currPosition.X + this.dx > 1150f)
                {
                    this.dx = 0f - this.dx;
                }
                this.shootingStars[this.starCount].currPosition.X = this.shootingStars[(this.starCount + 19) % 20].currPosition.X + this.dx;
                this.shootingStars[this.starCount].currPosition.Y = this.shootingStars[(this.starCount + 19) % 20].currPosition.Y - Global.BPM * 2f * this.dy;
                this.starCount = (this.starCount + 1) % 20;
                this.starsInitiated++;
            }
        }

        private void DrawStage(float elapsed, GameTime gameTime)
        {
            if (this.gameState == GameState.PLAY || this.gameState == GameState.PAUSE || this.gameState == GameState.TRIAL_PAUSE)
            {
                this.DrawGame(elapsed, gameTime);
            }
            if (this.gameState == GameState.CAT_SELECT)
            {
                this.DrawBackground(gameTime);
                this.DrawForeground();
            }
        }

        private void DrawMenu()
        {
            switch (this.gameState)
            {
                case GameState.OPENING:
                    if (this.splashScreen)
                    {
                        if (this.splashScreenIndex == 0)
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
					this.mainMenu.DrawBackground(spriteBatch);
					this.openingMenu.Draw(spriteBatch);
                    }
                    break;
                case GameState.MAIN_MENU:
				this.mainMenu.Draw(spriteBatch);
                    break;
                case GameState.STAGE_SELECT:
				this.stageSelectMenu.Draw(spriteBatch);
                    break;
                case GameState.CAT_SELECT:
				this.catSelectMenu.Draw(spriteBatch, this.stageSelectMenu.getCurrentLevel(), this.scoreSystem);
                    break;
                case GameState.PLAY:
                    this.DisplayPauseButton();
                    if (!this.tunnel.IsOn() && !this.helicopter.IsDead())
                    {
                        this.DisplayInstructions();
                    }
                    break;
                case GameState.PAUSE:
				this.pauseMenu.Draw(spriteBatch);
                    break;
                case GameState.TRIAL_PAUSE:
				this.trialMenu.Draw(spriteBatch);
                    break;
                case GameState.OPTIONS:
				this.optionsMenu.Draw(spriteBatch);
                    break;
                case GameState.LEADERBOARDS:
				this.leaderboardsMenu.Draw(spriteBatch, this.scoreSystem);
                    break;
                case GameState.CREDITS:
				this.creditsMenu.Draw(spriteBatch);
                    break;
                case GameState.EXIT:
                    break;
            }
        }

        private void DisplayPauseButton()
        {
            spriteBatch.Draw(Global.pauseButton, new Rectangle(50, 50, 64, 64), Color.White);
        }
        private void DrawGame(float elapsed, GameTime gameTime)
        {
            this.DrawBackground(gameTime);
            this.DrawHelicopter();
            this.DrawForeground();
            this.scoreSystem.Draw(spriteBatch);
        }

        private void DrawBackground(GameTime gameTime)
        {
			this.background.DrawBackBack(spriteBatch);
			this.bSpriteManager.DrawBack(spriteBatch);
			this.background.DrawMiddleBack(spriteBatch);
			this.bSpriteManager.DrawMiddleBack(spriteBatch);
			this.background.DrawBack(spriteBatch);
			this.bSpriteManager.DrawMiddle(spriteBatch);
			this.background.DrawMiddle(spriteBatch);
			this.bSpriteManager.DrawFore(spriteBatch);
			this.tunnel.Draw(spriteBatch);
        }

        private void DrawHelicopter()
        {
			this.helicopter.Draw(spriteBatch);
        }

        private void DrawForeground()
        {
			this.meatToMouth.Draw(spriteBatch);
			this.heartsManager.Draw(spriteBatch);
			this.explosionManager.Draw(spriteBatch);
			this.dancerManager.Draw(spriteBatch);
			this.letterManager.Draw(spriteBatch);
			this.clapperManager.Draw(spriteBatch);
			this.karaokeLyrics.Draw(spriteBatch);
			this.equalizer.Draw(spriteBatch);
			this.shineManager.Draw(spriteBatch);
			this.fluctuationManager.Draw(spriteBatch);
			this.butterflyEffect.Draw(spriteBatch);
            ParticleEmitter[] array = this.shootingStars;
            foreach (ParticleEmitter particleEmitter in array)
            {
				particleEmitter.Draw(spriteBatch);
            }
            Light[] array2 = this.lights;
            foreach (Light light in array2)
            {
				light.Draw(spriteBatch);
            }
            Laser[] array3 = this.lasers;
            foreach (Laser laser in array3)
            {
				laser.Draw(spriteBatch);
            }
			this.spreadLaser.Draw(spriteBatch);
			this.lyricEffect.Draw(spriteBatch);
			this.heart.Draw(spriteBatch);
			this.rainbow.Draw(spriteBatch);
			this.eyes.Draw(spriteBatch);
			this.hand.Draw(spriteBatch);
			this.fireworks.Draw(spriteBatch);
			this.flashManager.Draw(spriteBatch);
            this.RainbowOverlay(spriteBatch);
        }

        private void DisplayInstructions()
        {
			if (SongManager.IsNyanPack)
			{
				spriteBatch.DrawString(Global.spriteFont, "Touch To Start", new Vector2(545f, 190f), Color.White, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
				//spriteBatch.Draw(Global.AButtonTexture, new Rectangle(612, 192, 30, 30), Color.White);
				spriteBatch.DrawString(Global.spriteFont, "Hold to go up\nRelease to go down", new Vector2(543f, 269f), Color.White, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
				//spriteBatch.Draw(Global.AButtonTexture, new Rectangle(597, 272, 30, 30), Color.White);
        }
			else
			{
				spriteBatch.DrawString(Global.spriteFont, "Touch To Start", new Vector2(545f, 190f), Global.tunnelColor, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
				//spriteBatch.Draw(Global.AButtonTexture, new Rectangle(612, 192, 30, 30), Color.White);
				spriteBatch.DrawString(Global.spriteFont, "Hold to go up\nRelease to go down", new Vector2(543f, 269f), Global.tunnelColor, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);
				//spriteBatch.Draw(Global.AButtonTexture, new Rectangle(597, 272, 30, 30), Color.White);
			}
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
                    this.UpdateChoreographySeaOfLove(dt, elapsedMilliseconds);
                    break;
                case 1:
                    this.UpdateChoreographyLikeARainbow(dt, elapsedMilliseconds);
                    break;
                case 2:
                    this.UpdateChoreographyYoureShining(dt, elapsedMilliseconds);
                    break;
                case 3:
                    this.UpdateChoreographyTasteOfHeaven(dt, elapsedMilliseconds);
                    break;
                case 4:
                    this.UpdateChoreographyRon(dt, elapsedMilliseconds);
                    break;
                case 5:
                    this.UpdateChoreographyNyan(dt, elapsedMilliseconds);
                    break;
            }
        }

        private void UpdateChoreographySeaOfLove(float dt, float elapsedMilliseconds)
        {
            if (elapsedMilliseconds > this.eventTimes[this.currEvent])
            {
                switch (this.currEvent)
                {
                    case 0:
                        this.DoOpeningStars();
                        break;
                    case 1:
                        this.DoStationaryLights();
                        this.flashManager.DoFlash();
                        this.bSpriteManager.SetBRainbow();
                        this.bSpriteManager.SetBigRainbow();
                        break;
                    case 2:
                        this.ResetShootingStars();
                        this.DoLeftStars();
                        break;
                    case 3:
                        this.ResetShootingStars();
                        this.DoRightStars();
                        break;
                    case 4:
                        this.DoSwayingLights();
                        break;
                    case 5:
                        this.ResetShootingStars();
                        this.DoLeftStars();
                        break;
                    case 6:
                        this.ResetShootingStars();
                        this.DoRightStars();
                        break;
                    case 7:
                        this.rainbow.TurnOn();
                        break;
                    case 8:
                        this.hand.TurnOn(directedDownward_: true);
                        break;
                    case 9:
                        this.ResetShootingStars();
                        this.DoLeftStars();
                        break;
                    case 10:
                        this.DoRightStars();
                        break;
                    case 11:
                        this.ResetLights();
                        break;
                    case 12:
                        this.ResetShootingStars();
                        this.tunnel.Set(TunnelEffect.BW);
                        this.scoreSystem.TurnOnBass();
                        this.TurnOnLasers();
                        break;
                    case 13:
                        this.TurnOffLasers();
                        this.fireworks.TurnOn();
                        break;
                    case 14:
                        this.tunnel.Set(TunnelEffect.Normal);
                        this.tunnel.SetColor(Color.White, Color.Black, Color.White);
                        this.scoreSystem.TurnOffBass();
                        this.fireworks.TurnOff();
                        break;
                    case 16:
                        this.DoStarShower();
                        break;
                    case 17:
                        this.background.SetAcceleration(100f);
                        break;
                    case 19:
                        this.starShowerActive = false;
                        break;
                    case 20:
                        this.ResetHeart(1, alternating: false);
                        this.heart.TurnOn();
                        this.tunnel.Set(TunnelEffect.Rainbow);
                        Global.SetVibrationEndless(on: true);
                        this.scoreSystem.TurnOnBass();
                        this.background.SetAcceleration(0f);
                        this.fireworks.TurnOn();
                        this.DoStarStars(new Vector2(490f, 0f), new Vector2(0f, 400f));
                        break;
                    case 21:
                        this.ResetShootingStars();
                        this.ResetHeart(0, alternating: false);
                        this.tunnel.Set(TunnelEffect.Normal);
                        this.tunnel.SetColor(Color.White, Color.Black, Color.White);
                        Global.SetVibrationEndless(on: false);
                        this.scoreSystem.TurnOffBass();
                        this.fireworks.TurnOff();
                        break;
                    case 22:
                        this.heart.TurnOn();
                        this.tunnel.Set(TunnelEffect.Rainbow);
                        Global.SetVibrationEndless(on: true);
                        this.scoreSystem.TurnOnBass();
                        this.DoHeartStars(new Vector2(490f, 0f), new Vector2(0f, 400f));
                        this.DoPunctuatedLights();
                        break;
                    case 23:
                        this.ResetHeart(0, alternating: true);
                        Global.SetVibrationEndless(on: false);
                        this.tunnel.Set(TunnelEffect.BW);
                        break;
                    case 24:
                        this.DoHeartStars(new Vector2(-300f, 510f), new Vector2(500f, 0f));
                        break;
                    case 25:
                        this.DoStarStars(new Vector2(1280f, 510f), new Vector2(-500f, 0f));
                        break;
                    case 26:
                        this.DoHeartStars(new Vector2(-300f, 510f), new Vector2(500f, 0f));
                        break;
                    case 27:
                        this.tunnel.Set(TunnelEffect.Normal);
                        this.tunnel.SetColor(Color.White, Color.Black, Color.White);
                        this.scoreSystem.TurnOffBass();
                        this.background.SetVelocity(200f);
                        this.bSpriteManager.SetBRainbow();
                        this.bSpriteManager.SetBigRainbow();
                        this.ResetLights();
                        this.DoRightStars();
                        break;
                    case 28:
                        this.DoLeftStars();
                        break;
                    case 29:
                        this.DoRightStars();
                        break;
                    case 30:
                        this.fireworks.TurnOn();
                        break;
                    case 31:
                        this.rainbow.TurnOn();
                        break;
                    case 32:
                        this.hand.TurnOn(directedDownward_: true);
                        break;
                    case 33:
                        this.DoLeftStars();
                        break;
                    case 34:
                        this.DoRightStars();
                        break;
                    case 35:
                        this.fireworks.TurnOff();
                        break;
                    case 36:
                        this.DoStarShower();
                        break;
                    case 37:
                        this.background.SetAcceleration(100f);
                        break;
                    case 38:
                        this.starShowerActive = false;
                        break;
                    case 39:
                        this.background.SetAcceleration(0f);
                        this.heart.TurnOn();
                        this.fireworks.TurnOn();
                        this.tunnel.Set(TunnelEffect.Rainbow);
                        Global.SetVibrationEndless(on: true);
                        this.scoreSystem.TurnOnBass();
                        this.DoPunctuatedLights();
                        this.TurnOnLasers();
                        break;
                    case 40:
                        this.ResetChoreography(1, alternating: false, meat: false);
                        Global.SetVibrationEndless(on: false);
                        break;
                    case 41:
                        MediaPlayer.Stop();
                        MediaPlayer.Play(this.songManager.CurrentSong);
                        break;
                }
                this.currEvent++;
                if (this.currEvent == 42)
                {
                    this.currEvent = 0;
                }
            }
        }

        private void UpdateChoreographyLikeARainbow(float dt, float elapsedMilliseconds)
        {
            if (elapsedMilliseconds > this.eventTimes[this.currEvent])
            {
                switch (this.currEvent)
                {
                    case 0:
                        this.tunnel.Set(TunnelEffect.Normal);
					this.tunnel.SetColor(Color.White, Color.Red, Color.Blue);
                        break;
                    case 1:
                        this.hand.TurnOn(directedDownward_: false);
                        break;
                    case 2:
                        this.tunnel.Set(TunnelEffect.RainbowPunctuated);
                        this.scoreSystem.TurnOnBass();
                        break;
                    case 3:
                        this.hand.TurnOn(directedDownward_: false);
                        break;
                    case 4:
                        this.hand.TurnOn(directedDownward_: false);
                        break;
                    case 5:
                        this.hand.TurnOn(directedDownward_: false);
                        break;
                    case 6:
                        this.tunnel.Set(TunnelEffect.Normal);
					this.tunnel.SetColor(Color.White, Color.Red, Color.Blue);
                        this.scoreSystem.TurnOffBass();
                        this.butterflyEffect.TurnOn1();
                        break;
                    case 7:
                        this.butterflyEffect.TurnOn1();
                        break;
                    case 9:
                        this.hand.TurnOn(directedDownward_: false);
                        break;
                    case 10:
                        this.butterflyEffect.TurnOn1();
                        break;
                    case 11:
                        this.bSpriteManager.SetBigRainbow();
                        this.rainbow.TurnOn();
                        break;
                    case 12:
                        this.flashManager.DoFlash();
                        break;
                    case 13:
                        this.bSpriteManager.setPirateBoat();
                        break;
                    case 14:
                        this.eyes.TurnOn();
                        break;
                    case 15:
                        this.hand.TurnOn(directedDownward_: false);
                        this.butterflyEffect.TurnOn2();
                        break;
                    case 16:
                        this.hand.TurnOn(directedDownward_: false);
                        break;
                    case 17:
                        this.hand.TurnOn(directedDownward_: false);
                        break;
                    case 18:
                        this.tunnel.Set(TunnelEffect.BW);
                        break;
                    case 19:
                        this.hand.TurnOn(directedDownward_: false);
                        break;
                    case 20:
                        this.tunnel.Set(TunnelEffect.BWDouble);
                        break;
                    case 21:
                        this.butterflyEffect.TurnOff2();
                        break;
                    case 22:
                        this.tunnel.Set(TunnelEffect.BWQuad);
                        break;
                    case 23:
                        this.tunnel.Set(TunnelEffect.Normal);
					this.tunnel.SetColor(Color.White, Color.Red, Color.Blue);
                        break;
                    case 24:
                        this.hand.TurnOn(directedDownward_: false);
                        this.scoreSystem.TurnOnMovement();
                        this.scoreSystem.TurnOnBass();
                        this.tunnel.Set(TunnelEffect.BWQuad);
                        this.heart.Reset(0, _alternating: true);
                        this.heart.TurnOn();
                        this.DoFinaleLights();
                        break;
                    case 25:
                        this.bSpriteManager.SetBigRainbow();
                        this.rainbow.TurnOn();
                        break;
                    case 26:
                        this.flashManager.DoFlash();
                        break;
                    case 27:
                        this.bSpriteManager.setPirateBoat();
                        break;
                    case 28:
                        this.eyes.TurnOn();
                        break;
                    case 29:
                        this.heart.TurnOff();
                        this.ResetLights();
                        break;
                    case 30:
                        this.heart.TurnOn();
                        this.DoFinaleLights();
                        break;
                    case 31:
                        this.heart.TurnOff();
                        this.ResetLights();
                        this.tunnel.Set(TunnelEffect.BWDouble);
                        break;
                    case 32:
                        this.tunnel.Set(TunnelEffect.RainbowPunctuated);
                        this.scoreSystem.TurnOffMovement();
                        this.hand.TurnOn(directedDownward_: false);
                        break;
                    case 33:
                        this.hand.TurnOn(directedDownward_: false);
                        break;
                    case 34:
                        this.hand.TurnOn(directedDownward_: false);
                        break;
                    case 35:
                        this.tunnel.Set(TunnelEffect.Normal);
                        this.scoreSystem.TurnOffBass();
                        break;
                    case 36:
                        this.tunnel.Set(TunnelEffect.BW);
                        this.scoreSystem.TurnOnBass();
                        break;
                    case 37:
                        this.hand.TurnOn(directedDownward_: false);
                        this.tunnel.Set(TunnelEffect.Normal);
                        this.scoreSystem.TurnOffBass();
                        this.butterflyEffect.TurnOn1();
                        break;
                    case 38:
                        this.bSpriteManager.SetBigRainbow();
                        this.rainbow.TurnOn();
                        break;
                    case 39:
                        this.flashManager.DoFlash();
                        break;
                    case 40:
                        this.tunnel.Set(TunnelEffect.BW);
                        this.flashManager.DoStrobe(Global.BPM);
                        break;
                    case 41:
                        this.bSpriteManager.setPirateBoat();
                        break;
                    case 42:
                        this.tunnel.Set(TunnelEffect.BWDouble);
                        this.flashManager.DoStrobe(Global.BPM / 2f);
                        break;
                    case 43:
                        this.eyes.TurnOn();
                        this.tunnel.Set(TunnelEffect.BWQuad);
                        this.flashManager.DoStrobe(Global.BPM / 4f);
                        break;
                    case 44:
                        this.tunnel.Set(TunnelEffect.Normal);
					this.tunnel.SetColor(Color.White, Color.Red, Color.Blue);
                        this.flashManager.Reset();
                        break;
                    case 45:
                        this.scoreSystem.TurnOnMovement();
                        this.scoreSystem.TurnOnBass();
                        this.tunnel.Set(TunnelEffect.BWQuad);
                        this.heart.Reset(0, _alternating: true);
                        this.heart.TurnOn();
                        this.DoFinaleLights();
                        this.flashManager.DoStrobe(Global.BPM);
                        break;
                    case 46:
                        this.hand.TurnOn(directedDownward_: false);
                        break;
                    case 47:
                        this.bSpriteManager.SetBigRainbow();
                        this.rainbow.TurnOn();
                        break;
                    case 48:
                        this.flashManager.DoFlash();
                        break;
                    case 49:
                        this.bSpriteManager.setPirateBoat();
                        break;
                    case 50:
                        this.heart.TurnOff();
                        this.ResetLights();
                        this.flashManager.Reset();
                        break;
                    case 51:
                        this.eyes.TurnOn();
                        break;
                    case 52:
                        this.ResetChoreography(1, alternating: false, meat: false);
                        Global.SetVibrationEndless(on: false);
                        MediaPlayer.Stop();
                        MediaPlayer.Play(this.songManager.CurrentSong);
                        break;
                }
                this.currEvent++;
                if (this.currEvent == 53)
                {
                    this.currEvent = 0;
                }
            }
        }

        private void UpdateChoreographyYoureShining(float dt, float elapsedMilliseconds)
        {
            if (elapsedMilliseconds > this.eventTimes[this.currEvent])
            {
                switch (this.currEvent)
                {
                    case 0:
                        this.flashManager.DoFade(2.9f);
                        this.shineManager.SetCircle(333f);
                        break;
                    case 1:
                        this.shineManager.SetCircle(250f);
                        break;
                    case 2:
                        this.shineManager.SetCircle(167f);
                        break;
                    case 3:
                        this.shineManager.SetCircle(83f);
                        break;
                    case 4:
                        this.shineManager.TurnOn1();
                        break;
                    case 5:
                        this.flashManager.DoFlash();
                        break;
                    case 6:
                        this.shineManager.Continue1();
                        break;
                    case 7:
                        this.shineManager.Continue1();
                        break;
                    case 8:
                        this.shineManager.Continue1();
                        break;
                    case 9:
                        this.shineManager.TurnOn2();
                        break;
                    case 10:
                        this.lyricEffect.TurnOn(0);
                        break;
                    case 11:
                        this.lyricEffect.TurnOn(1);
                        break;
                    case 12:
                        this.lyricEffect.TurnOn(2);
                        break;
                    case 13:
                        this.shineManager.Continue2();
                        break;
                    case 14:
                        this.flashManager.DoFlash();
                        break;
                    case 15:
                        this.fluctuationManager.TurnOn();
                        this.tunnel.Set(TunnelEffect.BWDouble);
                        break;
                    case 16:
                        this.lyricEffect.TurnOn(0);
                        break;
                    case 17:
                        this.lyricEffect.TurnOn(1);
                        break;
                    case 18:
                        this.lyricEffect.TurnOn(2);
                        break;
                    case 19:
                        this.flashManager.DoFlash();
                        break;
                    case 20:
                        this.fluctuationManager.TurnOff();
                        this.tunnel.Set(TunnelEffect.BW);
                        this.scoreSystem.TurnOnBass();
                        this.TurnOnLasers();
                        break;
                    case 21:
                        this.flashManager.DoFlash();
                        break;
                    case 22:
                        this.flashManager.DoFlash();
                        break;
                    case 23:
                        this.flashManager.DoFlash();
                        break;
                    case 24:
                        this.flashManager.DoFlash();
                        break;
                    case 25:
                        this.flashManager.DoFlash();
                        break;
                    case 26:
                        this.flashManager.DoFlash();
                        break;
                    case 27:
                        this.flashManager.DoFlash();
                        break;
                    case 28:
                        this.flashManager.DoFlash();
                        break;
                    case 29:
                        this.lyricEffect.TurnOn(0);
                        break;
                    case 30:
                        this.lyricEffect.TurnOn(1);
                        break;
                    case 31:
                        this.lyricEffect.TurnOn(2);
                        break;
                    case 32:
                        this.flashManager.DoFlash();
                        break;
                    case 33:
                        this.lyricEffect.TurnOn(0);
                        break;
                    case 34:
                        this.lyricEffect.TurnOn(1);
                        break;
                    case 35:
                        this.lyricEffect.TurnOn(2);
                        break;
                    case 36:
                        this.flashManager.DoFlash();
                        break;
                    case 37:
                        this.tunnel.Set(TunnelEffect.Normal);
                        this.tunnel.SetColor(Color.White, Color.Black, Color.White);
                        this.scoreSystem.TurnOffBass();
                        this.TurnOffLasers();
                        break;
                    case 38:
                        this.DoPianoLights();
                        break;
                    case 39:
                        this.ContinuePianoLights();
                        break;
                    case 40:
                        this.ContinuePianoLights();
                        break;
                    case 41:
                        this.scoreSystem.TurnOnBass();
                        break;
                    case 42:
                        this.ContinuePianoLights();
                        break;
                    case 43:
                        this.flashManager.DoFlash();
                        break;
                    case 44:
                        this.flashManager.DoFlash();
                        break;
                    case 45:
                        this.ContinuePianoLights();
                        break;
                    case 46:
                        this.flashManager.DoFlash();
                        break;
                    case 47:
                        this.flashManager.DoFlash();
                        break;
                    case 48:
                        this.ContinuePianoLights();
                        break;
                    case 49:
                        this.flashManager.DoFlash();
                        break;
                    case 50:
                        this.flashManager.DoFlash();
                        break;
                    case 51:
                        this.ContinuePianoLights();
                        break;
                    case 52:
                        this.flashManager.DoFlash();
                        break;
                    case 53:
                        this.scoreSystem.TurnOffBass();
                        break;
                    case 54:
                        this.ResetLights();
                        this.shineManager.TurnOn3();
                        break;
                    case 55:
                        this.shineManager.TurnOff3();
                        this.shineManager.TurnOn4();
                        break;
                    case 56:
                        this.lyricEffect.TurnOn(0);
                        break;
                    case 57:
                        this.lyricEffect.TurnOn(1);
                        break;
                    case 58:
                        this.shineManager.Continue4(directUpwards: true);
                        break;
                    case 59:
                        this.lyricEffect.TurnOn(2);
                        break;
                    case 60:
                        this.shineManager.Continue4(directUpwards: true);
                        break;
                    case 61:
                        this.flashManager.DoFlash();
                        break;
                    case 62:
                        this.shineManager.Continue4(directUpwards: false);
                        break;
                    case 63:
                        this.shineManager.Continue4(directUpwards: true);
                        break;
                    case 64:
                        this.lyricEffect.TurnOn(0);
                        break;
                    case 65:
                        this.lyricEffect.TurnOn(1);
                        break;
                    case 66:
                        this.shineManager.Continue4(directUpwards: true);
                        break;
                    case 67:
                        this.lyricEffect.TurnOn(2);
                        break;
                    case 68:
                        this.shineManager.Continue4(directUpwards: true);
                        break;
                    case 69:
                        this.flashManager.DoFlash();
                        break;
                    case 70:
                        this.shineManager.Continue4(directUpwards: false);
                        break;
                    case 71:
                        this.fluctuationManager.TurnOn();
                        this.tunnel.Set(TunnelEffect.BWDouble);
                        break;
                    case 72:
                        this.flashManager.DoFlash();
                        break;
                    case 73:
                        this.flashManager.DoFlash();
                        break;
                    case 74:
                        this.flashManager.DoFlash();
                        break;
                    case 75:
                        this.flashManager.DoFlash();
                        break;
                    case 77:
                        this.flashManager.DoFlash();
                        break;
                    case 78:
                        this.flashManager.DoFlash();
                        break;
                    case 79:
                        this.flashManager.DoFlash();
                        break;
                    case 80:
                        this.fluctuationManager.TurnOff();
                        this.tunnel.Set(TunnelEffect.Normal);
                        this.tunnel.SetColor(Color.White, Color.Black, Color.White);
                        break;
                    case 81:
                        this.flashManager.DoFlash();
                        break;
                    case 82:
                        this.tunnel.Set(TunnelEffect.BW);
                        this.TurnOnLasers();
                        this.DoFinaleLights();
                        this.scoreSystem.TurnOnBass();
                        this.shineManager.TurnOn3();
                        break;
                    case 83:
                        this.flashManager.DoFlash();
                        break;
                    case 84:
                        this.flashManager.DoFlash();
                        break;
                    case 85:
                        this.flashManager.DoFlash();
                        break;
                    case 86:
                        this.flashManager.DoFlash();
                        break;
                    case 87:
                        this.flashManager.DoFlash();
                        break;
                    case 88:
                        this.flashManager.DoFlash();
                        break;
                    case 89:
                        this.flashManager.DoFlash();
                        break;
                    case 90:
                        this.flashManager.DoFlash();
                        break;
                    case 91:
                        this.lyricEffect.TurnOn(0);
                        break;
                    case 92:
                        this.lyricEffect.TurnOn(1);
                        break;
                    case 93:
                        this.lyricEffect.TurnOn(2);
                        break;
                    case 94:
                        this.flashManager.DoFlash();
                        break;
                    case 95:
                        this.lyricEffect.TurnOn(0);
                        break;
                    case 96:
                        this.lyricEffect.TurnOn(1);
                        break;
                    case 97:
                        this.lyricEffect.TurnOn(2);
                        break;
                    case 98:
                        this.flashManager.DoFlash();
                        break;
                    case 99:
                        this.ResetChoreography(1, alternating: false, meat: false);
                        Global.SetVibrationEndless(on: false);
                        MediaPlayer.Stop();
                        MediaPlayer.Play(this.songManager.CurrentSong);
                        break;
                }
                this.currEvent++;
                if (this.currEvent == 100)
                {
                    this.currEvent = 0;
                }
            }
        }

        private void UpdateChoreographyTasteOfHeaven(float dt, float elapsedMilliseconds)
        {
            Camera.Update(dt);
            this.karaokeLyrics.Update(dt, elapsedMilliseconds);
            if (elapsedMilliseconds > this.eventTimes[this.currEvent])
            {
                switch (this.currEvent)
                {
                    case 0:
                        this.karaokeLyrics.TurnOn();
                        break;
                    case 1:
                        this.bSpriteManager.SetSausageRainbowSmall();
                        this.bSpriteManager.SetSausageRainbowLarge();
                        Camera.DoShake(2f, 0.16666f);
                        break;
                    case 2:
                        Camera.DoShake(2f, 0.16666f);
                        break;
                    case 3:
                        Camera.DoShakes(4, 0.333333343f, 2f, 0.16666f);
                        break;
                    case 6:
                        this.meatToMouth.TurnOn();
                        break;
                    case 7:
                        this.bSpriteManager.SetSausageRainbowSmall();
                        this.bSpriteManager.SetSausageRainbowLarge();
                        Camera.DoShake(2f, 0.16666f);
                        break;
                    case 8:
                        Camera.DoShake(2f, 0.16666f);
                        break;
                    case 9:
                        Camera.DoShakes(4, 0.333333343f, 2f, 0.16666f);
                        break;
                    case 10:
                        this.meatToMouth.TurnOff();
                        this.equalizer.TurnOn(rainbowed: true, isVisualizationA: true);
                        Camera.DoRotating(0.333333343f);
                        Camera.DoFlipping(0.333333343f);
                        break;
                    case 12:
                        this.equalizer.TurnOff();
                        Camera.StopRotating();
                        Camera.StopFlipping();
                        break;
                    case 14:
                        Camera.DoScaling(0.333333343f);
                        break;
                    case 15:
                        Camera.StopScaling();
                        break;
                    case 17:
                        this.meatToMouth.TurnOn();
                        break;
                    case 18:
                        this.equalizer.TurnOn(rainbowed: false, isVisualizationA: false);
                        break;
                    case 19:
                        this.bSpriteManager.SetSausageRainbowSmall();
                        this.bSpriteManager.SetSausageRainbowLarge();
                        Camera.DoShake(2f, 0.16666f);
                        break;
                    case 20:
                        Camera.DoShake(2f, 0.16666f);
                        break;
                    case 21:
                        Camera.DoShake(2f, 0.16666f);
                        break;
                    case 22:
                        Camera.DoShakes(4, 0.333333343f, 2f, 0.16666f);
                        break;
                    case 23:
                        equalizer.ChangeRainbow(rainbowed: true);
                        Camera.GoCrazy(0.333333343f);
                        break;
                    case 24:
                        this.ResetChoreography(1, alternating: false, meat: true);
                        break;
                    case 25:
                        Global.SetVibrationEndless(on: false);
                        MediaPlayer.Stop();
                        MediaPlayer.Play(this.songManager.CurrentSong);
                        break;
                }
                this.currEvent++;
                if (this.currEvent == 26)
                {
                    this.currEvent = 0;
                }
            }
        }

        public void UpdateChoreographyRon(float dt, float elapsedMilliseconds)
        {
            Camera.Update(dt);
            if (elapsedMilliseconds > this.eventTimes[this.currEvent])
            {
                switch (this.currEvent)
                {
                    case 0:
                        this.explosionManager.TurnOn();
                        Camera.SetEffect(0); //broken (index0,shakeblur,aka blurdirectional)
                        break;
                    case 4:
                        this.explosionManager.TurnOff();
                        Camera.SetEffect(-1);
                        this.dancerManager.TurnOn(0);
                        break;
                    case 5:
                        this.dancerManager.TurnOn(1);
                        break;
                    case 6:
                        this.dancerManager.TurnOff();
                        Camera.SetEffect(1); 
                        this.heartsManager.TurnOn();
                        break;
                    case 8:
                        Camera.SetEffect(2); //broken (index4,shakezigzag,aka drunk)
                        break;
                    case 9:
                        this.heartsManager.TurnOff();
                        Camera.SetEffect(-1);
                        this.tunnel.Set(TunnelEffect.Disappear);
                        break;
                    case 10:
                        Camera.SetEffect(5); 
                        this.tunnel.Set(TunnelEffect.Normal);
                        break;
                    case 11:
                        Camera.SetEffect(4);
                        break;
                    case 12:
                        Camera.SetEffect(1); 
                        this.heartsManager.TurnOn();
                        break;
                    case 13:
                        this.heartsManager.TurnOff();
                        this.tunnel.Set(TunnelEffect.Disappear);
                        Camera.SetEffect(3); //broken (index5,drunk,aka blurdirectional)
                        this.dancerManager.TurnOn(0);
                        break;
                    case 14:
                        this.tunnel.Set(TunnelEffect.Normal);
                        this.explosionManager.TurnOn();
                        Camera.SetEffect(0);
                        break;
                    case 15:
                        Camera.SetEffect(2); //broken (index4, shakezigzag aka drunk)
                        this.dancerManager.TurnOn(1);
                        this.heartsManager.TurnOn();
                        break;
                    case 16:
                        this.explosionManager.TurnOff();
                        Camera.SetEffect(-1);
                        break;
                    case 17:
                        this.ResetChoreography(1, alternating: false, meat: false);
                        break;
                    case 18:
                        Global.SetVibrationEndless(on: false);
                        MediaPlayer.Stop();
                        MediaPlayer.Play(this.songManager.CurrentSong);
                        break;
                }
                this.currEvent++;
                if (this.currEvent == 19)
                {
                    this.currEvent = 0;
                }
            }
        }

        public void UpdateChoreographyNyan(float dt, float elapsedMilliseconds)
        {
            Camera.Update(dt);
            if (elapsedMilliseconds > this.eventTimes[this.currEvent])
            {
                switch (this.currEvent)
                {
                    case 0:
                        //MediaPlayer.Play(this.songManager.CurrentSong);
                        break;
                    case 1:
                        //TurnOnClappers();
						clapperManager.TurnOn(0);
						//clappersOn = true;
                        break;
                    case 2:
                        this.tunnel.Set(TunnelEffect.Nyan);
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        //TurnOffClappers();
						//clappersOn = false;
						clapperManager.TurnOff();
                        break;
                    case 7:
                        break;
                    case 8:
                        //TurnOnLetters();
						//lettersOn = true;
						letterManager.TurnOn(0);
                        Camera.DoRotatingNyan(Global.BPM * 8f);
                        break;
                    case 9:
                        //TurnOnClappers();
						//clappersOn = true;
						clapperManager.TurnOn(0); //this is out of sync
                        //TurnOffLetters();
						//lettersOn = false;
						letterManager.TurnOff();
                        Camera.StopRotating();
                        break;
                    case 10:
                        //TurnOffClappers();
						//clappersOn = false;
						clapperManager.TurnOff();
                        break;
                    case 11:
                        break;
                    case 12:
                        break;
                    case 13:
                        this.tunnel.Set(TunnelEffect.Disappear);
                        Camera.SetEffect(6);
                        rainbowOverlayEnabled = true;
                        break;
                    case 14:
                        break;
                    case 15:
                        break;
                    case 16:
                        this.tunnel.Set(TunnelEffect.Normal);
                        Camera.SetEffect(-1);
                        rainbowOverlayEnabled = false;
                        break;
                    case 17:
                        this.tunnel.Set(TunnelEffect.Nyan);
                        Camera.DoFlippingNyan(Global.BPM * 8f);
                        break;
                    case 18:
                        break;
                    case 19:
                        this.tunnel.Set(TunnelEffect.Normal);
                        Camera.StopFlipping();
                        break;
                    case 20:
                        this.tunnel.Set(TunnelEffect.Disappear);
                        Camera.DoRotatingNyan(Global.BPM * 8f); //out of
                        Camera.DoFlippingNyan(Global.BPM * 8f); //sync
                        Camera.SetEffect(6);
                        rainbowOverlayEnabled = true;
                        //TurnOnLetters();
						letterManager.TurnOn(0);
						//lettersOn = true;
                        break;
                    case 21:
						//lettersOn = false;
                        this.ResetChoreography(1, alternating: false, meat: false);
                        Camera.SetEffect(-1);
                        Camera.StopFlipping();
                        Camera.StopRotating();
                        rainbowOverlayEnabled = false;
                        break;
                    case 22:
                        MediaPlayer.Stop();
                        MediaPlayer.Play(this.songManager.CurrentSong);
                        break;
                }
                this.currEvent++;
				if (this.currEvent == 23)
                {
                    this.currEvent = 0;
                }
            }
        }

        private void RainbowOverlay(SpriteBatch spriteBatch)
        {
            if (rainbowOverlayEnabled)
            {
                spriteBatch.Draw(overlay, new Rectangle(0, 0, 1280, 720), new Rectangle(0,0,1,768), new Color(255,255,255,0.5f), 0f, Vector2.Zero, SpriteEffects.None, 0f);
            }
            if (clappersOn)
            {
                spriteBatch.DrawString(Global.spriteFont, "Clappers on", new Vector2(10, 70), Color.White);
            }
            if (lettersOn)
            {
                spriteBatch.DrawString(Global.spriteFont, "Letters on", new Vector2(10, 90), Color.White);
            }
        }

        private void DoStationaryLights()
        {
            int num = 0;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    this.lights[num].ResetPosition(new Vector2(150 + 980 * i, 150 + 420 * j), Vector2.Zero);
                    this.lights[num].SetRotationBehavior(_smoothRotation: false, new float[4]
                    {
                        0f,
                        (float)Math.PI / 2f,
                        (float)Math.PI,
                        4.712389f
                    }, new float[1] { Global.BPM * 2f }, 0f, 0f, 0f);
                    this.lights[num].SetHueBehavior(_smoothHue: true, null, 0f, 180f, 0f, 360f);
                    this.lights[num].TurnOn();
                    num++;
                }
            }
        }

        private void DoPunctuatedLights()
        {
            for (int i = 0; i < 6; i++)
            {
                this.lights[i].ResetPosition(new Vector2(150 + 196 * i, 360f), new Vector2(Global.RandomBetween(300f, 500f), Global.RandomBetween(300f, 500f)));
                this.lights[i].SetHueBehavior(_smoothHue: true, null, 0f, 360f, i * 60, 360f);
                this.lights[i].SetRotationBehavior(_smoothRotation: false, new float[8]
                {
                    0f,
                    (float)Math.PI / 4f,
                    (float)Math.PI / 2f,
                    (float)Math.PI * 3f / 4f,
                    (float)Math.PI,
                    3.926991f,
                    4.712389f,
                    5.49778748f
                }, new float[1] { Global.BPM }, 0f, 0f, 0f);
                this.lights[i].TurnOn();
            }
        }

        private void DoSwayingLights()
        {
            for (int i = 0; i < 4; i++)
            {
                this.lights[i].ResetVelocity(new Vector2(Global.RandomBetween(200f, 400f), Global.RandomBetween(200f, 400f)));
                this.lights[i].SetHueBehavior(_smoothHue: false, new float[6]
                {
                    i * 60 % 360,
                    (i + 1) * 60 % 360,
                    (i + 2) * 60 % 360,
                    (i + 3) * 60 % 360,
                    (i + 4) * 60 % 360,
                    (i + 5) * 60 % 360
                }, Global.BPM, 0f, 0f, 0f);
                this.lights[i].SetRotationBehavior(_smoothRotation: true, null, null, (float)Game1.IsOdd(i) * (float)Math.PI / (Global.BPM * 2f), MathHelper.Min(0f, (float)Game1.IsOdd(i) * (float)Math.PI), MathHelper.Max(0f, (float)Game1.IsOdd(i) * (float)Math.PI));
            }
            for (int i = 4; i < 6; i++)
            {
                this.lights[i].ResetPosition(new Vector2(440 + 400 * (i - 4), 360f), new Vector2(Global.RandomBetween(200f, 400f), Global.RandomBetween(200f, 400f)));
                this.lights[i].SetHueBehavior(_smoothHue: false, new float[6]
                {
                    i * 60 % 360,
                    (i + 1) * 60 % 360,
                    (i + 2) * 60 % 360,
                    (i + 3) * 60 % 360,
                    (i + 4) * 60 % 360,
                    (i + 5) * 60 % 360
                }, Global.BPM, 0f, 0f, 0f);
                this.lights[i].SetRotationBehavior(_smoothRotation: true, null, null, (float)Game1.IsOdd(i) * (float)Math.PI / (Global.BPM * 2f), MathHelper.Min(0f, (float)Game1.IsOdd(i) * (float)Math.PI), MathHelper.Max(0f, (float)Game1.IsOdd(i) * (float)Math.PI));
                this.lights[i].TurnOn();
            }
        }

        private void DoFinaleLights()
        {
            for (int i = 0; i < 6; i++)
            {
                this.lights[i].ResetPosition(new Vector2(150 + 196 * i, 360f), new Vector2(Global.RandomBetween(300f, 500f), Global.RandomBetween(300f, 500f)));
                this.lights[i].SetHueBehavior(_smoothHue: false, new float[7] { 0f, 60f, 120f, 180f, 240f, 300f, 360f }, 0f, 0f, 0f, 0f);
                this.lights[i].SetRotationBehavior(_smoothRotation: false, new float[8]
                {
                    0f,
                    (float)Math.PI / 4f,
                    (float)Math.PI / 2f,
                    (float)Math.PI * 3f / 4f,
                    (float)Math.PI,
                    3.926991f,
                    4.712389f,
                    5.49778748f
                }, new float[1] { Global.BPM }, 0f, 0f, 0f);
                this.lights[i].TurnOn();
            }
        }

        private void DoPianoLights()
        {
            for (int i = 0; i < 6; i++)
            {
                this.lights[i].ResetPosition(new Vector2(150 + 196 * i, 360f), new Vector2(Global.RandomBetween(300f, 500f), Global.RandomBetween(300f, 500f)));
                this.lights[i].SetHueBehavior(_smoothHue: false, new float[7] { 0f, 60f, 120f, 180f, 240f, 300f, 360f }, 0f, 0f, 0f, 0f);
                this.lights[i].SetRotationBehavior(_smoothRotation: false, new float[8]
                {
                    0f,
                    (float)Math.PI / 4f,
                    (float)Math.PI / 2f,
                    (float)Math.PI * 3f / 4f,
                    (float)Math.PI,
                    3.926991f,
                    4.712389f,
                    5.49778748f
                }, new float[19]
                {
                    0f, 0.161f, 0.605f, 0.869f, 1.129f, 1.488f, 1.663f, 2.012f, 2.272f, 2.546f,
                    2.904f, 3.065f, 3.433f, 3.936f, 4.316f, 4.486f, 4.85f, 5.105f, 5.364f
                }, 0f, 0f, 0f);
                this.lights[i].TurnOn();
            }
        }

        private void ContinuePianoLights()
        {
            for (int i = 0; i < 6; i++)
            {
                this.lights[i].ContinueLights(new float[10] { 0f, 0.172f, 0.531f, 0.797f, 1.056f, 1.415f, 1.663f, 1.943f, 2.204f, 2.471f });
            }
        }

        private void TurnOnLasers()
        {
            for (int i = 0; i < 5; i++)
            {
                this.lasers[i].Set(new Vector2(128 + 256 * i, 0f), Color.LimeGreen);
                this.lasers[i].TurnOn();
            }
        }

        private void TurnOffLasers()
        {
            for (int i = 0; i < 5; i++)
            {
                this.lasers[i].TurnOff();
            }
        }

        private void DoStarShower()
        {
            float num = 130f;
            this.starCount = 0;
            this.starsInitiated = 0;
            this.dx = 100f;
            this.dy = 300f;
            for (int i = 0; i < 20; i++)
            {
                this.shootingStars[i].Reset(new Vector2(num, (float)(-i) * Global.BPM * 2f * this.dy), new Vector2(0f, 600f));
                this.shootingStars[i].TurnOn();
                num += this.dx;
                if (num > 1150f)
                {
                    num -= 2f * this.dx;
                    this.dx = 0f - this.dx;
                }
            }
            this.starShowerActive = true;
        }

        private void DoOpeningStars()
        {
            for (int i = 0; i < this.shootingStars.Length; i++)
            {
                this.shootingStars[i].Reset(new Vector2(i * 60, -i * 20), new Vector2(100f, 300f));
                this.shootingStars[i].TurnOn();
            }
        }

        private void DoLeftStars()
        {
            for (int i = 0; i < 5; i++)
            {
                this.shootingStars[i].Reset(new Vector2(i * 60 + 300, (float)(-i * 300) * Global.BPM / 3f), new Vector2(-100f, 300f));
                this.shootingStars[i].TurnOn();
            }
        }

        private void DoRightStars()
        {
            for (int i = 5; i < 10; i++)
            {
                this.shootingStars[i].Reset(new Vector2(i * 60 + 300, (float)(-(i - 5) * 300) * Global.BPM / 3f), new Vector2(100f, 300f));
                this.shootingStars[i].TurnOn();
            }
        }

        private void DoHeartStars(Vector2 _position, Vector2 _velocity)
        {
            for (int i = 0; i < 16; i++)
            {
                this.shootingStars[i].Reset(this._heartPositions[i] + _position + new Vector2(0f, -300f), _velocity);
                this.shootingStars[i].TurnOn();
            }
        }

        private void DoStarStars(Vector2 _position, Vector2 _velocity)
        {
            for (int i = 0; i < 20; i++)
            {
                this.shootingStars[i].Reset(this._starPositions[i] + _position + new Vector2(0f, -300f), _velocity);
                this.shootingStars[i].TurnOn();
            }
        }

        private void ResetGame(bool meat)
        {
            this.scoreSystem.Reset();
            this.ResetChoreography(1, alternating: false, meat);
            this.tunnel.Reset(40, 3);
            this.helicopter.Reset();
            this.bSpriteManager.Reset();
            this.currEvent = 0;
        }

        private void ResetChoreography(int index, bool alternating, bool meat)
        {
            this.ResetLights();
            this.ResetShootingStars();
            this.ResetHeart(index, alternating);
            this.fireworks.TurnOff();
            this.tunnel.Set(TunnelEffect.Normal);
            this.tunnel.SetColor(Color.White, Color.Black, Color.White);
            this.scoreSystem.TurnOffBass();
            this.scoreSystem.TurnOffMovement();
            this.background.SetVelocity(200f);
            Laser[] array = this.lasers;
            foreach (Laser laser in array)
            {
                laser.TurnOff();
            }
            this.spreadLaser.TurnOff();
            this.lyricEffect.TurnOff();
            for (int j = 0; j < this.shootingStars.Length; j++)
            {
                this.shootingStars[j].TurnOff();
            }
            this.hand.Reset();
            this.rainbow.Reset();
            this.eyes.TurnOff();
            this.flashManager.Reset();
            this.shineManager.Reset();
            this.fluctuationManager.Reset();
            this.butterflyEffect.Reset();
            this.karaokeLyrics.TurnOff();
            this.equalizer.TurnOff();
            this.meatToMouth.TurnOff();
            this.explosionManager.TurnOff();
            this.dancerManager.TurnOff();
            this.heartsManager.TurnOff();
			this.letterManager.TurnOff();
			this.clapperManager.TurnOff();
            Camera.StopFlipping();
            Camera.StopRotating();
            this.rainbowOverlayEnabled = false;
            Camera.Reset();
            if (!meat)
            {
                Camera.SetEffect(-1);
            }
        }

        private void ResetLights()
        {
            Light[] array = this.lights;
            foreach (Light light in array)
            {
                light.TurnOff();
            }
        }

        private void ResetShootingStars()
        {
            ParticleEmitter[] array = this.shootingStars;
            foreach (ParticleEmitter particleEmitter in array)
            {
                particleEmitter.TurnOff();
            }
        }

        private void ResetHeart(int index, bool alternating)
        {
            this.heart.Reset(index, alternating);
        }
    }
}
