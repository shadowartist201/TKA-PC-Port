using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
//using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Helicopter
{
	public static class Global
	{
		public static ItemSelectedEffect itemSelectedEffect;

		public static StorageDeviceManager DeviceManager;

		public static bool IsTrialMode = false;

		public static PlayerIndex? playerIndex = null;

		public static Effect negative;

		public static Texture2D pixel;

		public static Texture2D tunnelStar;

		public static SpriteFont spriteFont;

		public static SpriteFont menuFont;

		public static Texture2D scoreTexture;

		public static Texture2D highScoreTexture;

		public static Texture2D numbersTexture;

		public static Texture2D BGTexture;

		public static Texture2D bigStar;

		public static Texture2D[] stars;

		public static Texture2D heartsTex;

		public static Texture2D butterflyParticles;

		public static Texture2D equalizerBar;

		public static Texture2D backgroundSpritesTexture;

		public static Texture2D AButtonTexture;

		public static Texture2D YButtonTexture;

		public static Texture2D searchingEyes;

		public static Texture2D rainbow2;

		public static Texture2D rainbow3;

		public static Texture2D lightEffect;

		public static Texture2D hugestar;

		public static Texture2D hugeHeart;

		public static Texture2D rainbow;

		public static Texture2D hand;

		public static Texture2D reachingHand;

		public static Texture2D hugeAtom;

		public static Texture2D hugeButterfly;

		public static Texture2D hugeCat;

		public static Texture2D hugeCrown;

		public static Texture2D hugeMoon;

		public static Texture2D hugeRabbit;

		public static Texture2D[] shineShapes;

		public static Texture2D feelWantTouch;

		public static Texture2D fluctuationShape;

		public static Texture2D mouth;

		public static Texture2D meatsToMouth;

		public static Texture2D nyanLetters;

		public static Texture2D nyanHands;

		public static Texture2D pelvicTex;

		public static Texture2D explosionTex;

		public static Texture2D cats;

		public static Texture2D creditsTex;

		public static Texture2D selectCatTex;

		public static Texture2D selectStageTex;

		public static Texture2D pauseTex;

		public static Texture2D optionsTex;

		public static Texture2D leaderboardTex;

		public static Texture2D trialTex;

		public static Texture2D splashTex;

		public static Texture2D mainTex;

		public static Texture2D mainCatTex;

		public static Texture2D mainCircleTex;

		public static Texture2D mainStarTex;

		public static Texture2D mainTitleTex;

		public static Texture2D mainPressStartTex;

		public static Texture2D option_apply;

		public static Texture2D option_display;

		public static Texture2D option_fullOff;

		public static Texture2D option_fullOn;

		public static Texture2D option_res1080;

		public static Texture2D option_res720;

		public static Texture2D option_res480;

		public static Texture2D sound_levels;

		public static float mountainVelocity = 200f;

		public static AudioEngine audioEngine;

		public static WaveBank waveBank;

		public static SoundBank soundBank;

		public static bool debugCatUnlock = false;

		public static Color tunnelColor = Color.White;

		public static Color[] rainbowColors = new Color[6]
		{
			Color.Red,
			Color.Orange,
			Color.Yellow,
			Color.Green,
			Color.Blue,
			Color.Violet
		};

		public static Color[] rainbowColors8 = new Color[8]
		{
			Color.Red,
			Color.Orange,
			Color.Yellow,
			Color.GreenYellow,
			Color.Green,
			Color.Blue,
			Color.Indigo,
			Color.Violet
		};

		public static Vector2 menuItemOffset = Vector2.Zero;

		public static Vector2 menuItemVelocity = new Vector2(200f, 134f);

		public static float BPM;

		private static Random random = new Random();

		private static bool vibrationOn = true;

		private static bool vibratingTemp = false;

		private static bool vibratingEndless = false;

		private static bool vibratingPaused = false;

		private static float vibrationTimer = 0f;

		private static float vibrationTime = 0.1f;

		public static bool fullscreenOn;

		public static Vector2 resolution;

		public static Random Random => Global.random;

		public static void setPixel(GraphicsDevice graphicsDevice)
		{
			Color[] data = new Color[1] { Color.White };
			Global.pixel = new Texture2D(graphicsDevice, 1, 1, mipmap: false, SurfaceFormat.Color);
			Global.pixel.SetData<Color>(data);
		}

		public static float RandomBetween(float min, float max)
		{
			return min + (float)Global.random.NextDouble() * (max - min);
		}

		public static Color RandomColor()
		{
			return Global.random.Next(0, 6) switch
			{
				0 => Color.Red, 
				1 => Color.Orange, 
				2 => Color.Yellow, 
				3 => Color.Green, 
				4 => Color.Blue, 
				5 => Color.Violet, 
				_ => Color.White, 
			};
		}

		public static Vector2 PickDirection(float alpha, float beta)
		{
			float num = Global.RandomBetween(alpha, beta);
			return new Vector2((float)Math.Cos(num), (float)Math.Sin(num));
		}

		public static void PlayCatSound()
		{
            switch (Global.Random.Next(0, 4))
			{
			case 0:
				Global.soundBank.PlayCue("cat_01");
				break;
			case 1:
				Global.soundBank.PlayCue("cat_02");
				break;
			case 2:
				Global.soundBank.PlayCue("cat_03");
				break;
			case 3:
				Global.soundBank.PlayCue("cat_04");
				break;
			}
		}

		public static bool CanBuyGame()
		{
			if (Global.playerIndex.HasValue)
			{
				/*SignedInGamer signedInGamer = Gamer.SignedInGamers[Global.playerIndex.Value];
				if (signedInGamer == null || !signedInGamer.IsSignedInToLive || Guide.IsVisible)
				{
					return false;
				}
				return signedInGamer.Privileges.AllowPurchaseContent;*/
			}
			return false;
		}

		public static void SetVibrationPause()
		{
			Global.vibratingPaused = true;
			GamePad.SetVibration(Global.playerIndex.Value, 0f, 0f);
		}

		public static void SetVibrationResume()
		{
			Global.vibratingPaused = false;
			if (Global.vibrationOn)
			{
				if (Global.vibratingEndless)
				{
					GamePad.SetVibration(Global.playerIndex.Value, 0.3f, 0.3f);
				}
				if (Global.vibratingTemp)
				{
					GamePad.SetVibration(Global.playerIndex.Value, 1f, 1f);
				}
			}
		}

		public static void ResetVibration()
		{
			GamePad.SetVibration(Global.playerIndex.Value, 0f, 0f);
			Global.vibratingTemp = false;
			Global.vibratingEndless = false;
			Global.vibratingPaused = false;
			Global.vibrationTimer = 0f;
		}

		public static void SetVibrationOn(bool on)
		{
			if (on)
			{
				Global.vibrationOn = true;
				Global.ResetVibration();
			}
			else
			{
				Global.vibrationOn = false;
				Global.ResetVibration();
			}
		}

		public static void SetVibrationTemp(bool on)
		{
			if (!Global.vibrationOn)
			{
				return;
			}
			if (on)
			{
				GamePad.SetVibration(Global.playerIndex.Value, 1f, 1f);
				Global.vibratingTemp = true;
				Global.vibrationTimer = 0f;
				return;
			}
			if (Global.vibratingEndless)
			{
				GamePad.SetVibration(Global.playerIndex.Value, 0.3f, 0.3f);
			}
			else
			{
				GamePad.SetVibration(Global.playerIndex.Value, 0f, 0f);
			}
			Global.vibratingTemp = false;
			Global.vibrationTimer = 0f;
		}

		public static void SetVibrationEndless(bool on)
		{
			if (!Global.vibrationOn)
			{
				return;
			}
			if (on)
			{
				if (!Global.vibratingTemp)
				{
					GamePad.SetVibration(Global.playerIndex.Value, 0.3f, 0.3f);
				}
				Global.vibratingEndless = true;
			}
			else
			{
				if (!Global.vibratingTemp)
				{
					GamePad.SetVibration(Global.playerIndex.Value, 0f, 0f);
				}
				Global.vibratingEndless = false;
			}
		}

		public static void UpdateVibration(float dt)
		{
			if (Global.vibrationOn && Global.vibratingTemp && !Global.vibratingPaused)
			{
				Global.vibrationTimer += dt;
				if (Global.vibrationTimer > Global.vibrationTime)
				{
					Global.SetVibrationTemp(on: false);
				}
			}
		}

        public static void SetFullscreenOn(bool on)
        {
            if (on)
            {
                Global.fullscreenOn = true;
            }
            else
            {
                Global.fullscreenOn = false;
            }
        }

		public static void SetResolution(int index)
		{
			switch (index)
			{
				case 1:
					Global.resolution = new Vector2(854, 480);
					break;
				case 2:
					Global.resolution = new Vector2(1280, 720);
					break;
				case 3:
					Global.resolution = new Vector2(1920, 1080);
					break;
			}
		}
    }
}
