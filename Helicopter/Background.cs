using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Helicopter
{
    public class Background
	{
		private ContentManager Content;

		private int backPartStaticLayer_;

		private StaticBackgroundElement backPartStatic_;

		private BackgroundElement[] backParts_ = new BackgroundElement[4];

		private BackgroundElement midPart_;

		public Background(Game1 game)
		{
			this.Content = new ContentManager((IServiceProvider)game.Services, "Content/Graphics/Background");
			Global.BGTexture = this.Content.Load<Texture2D>("SeaBGXbox");
			for (int i = 0; i < this.backParts_.Length; i++)
			{
				this.backParts_[i] = new BackgroundElement();
			}
			this.midPart_ = new BackgroundElement();
			this.backPartStatic_ = new StaticBackgroundElement();
		}

		public void Update(float dt)
		{
			for (int i = 0; i < this.backParts_.Length; i++)
			{
				this.backParts_[i].Update(dt);
			}
			this.midPart_.Update(dt);
		}

		public void DrawBackBack(SpriteBatch spriteBatch)
		{
			if (this.backPartStaticLayer_ == 0)
			{
				this.backPartStatic_.Draw(spriteBatch);
			}
			this.backParts_[0].Draw(spriteBatch);
		}

		public void DrawMiddleBack(SpriteBatch spriteBatch)
		{
			if (this.backParts_.Length > 1)
			{
				if (this.backPartStaticLayer_ == 1)
				{
					this.backPartStatic_.Draw(spriteBatch);
				}
				this.backParts_[1].Draw(spriteBatch);
			}
		}

		public void DrawBack(SpriteBatch spriteBatch)
		{
			for (int i = 2; i < this.backParts_.Length; i++)
			{
				if (i == this.backPartStaticLayer_)
				{
					this.backPartStatic_.Draw(spriteBatch);
				}
				this.backParts_[i].Draw(spriteBatch);
			}
			if (this.backPartStaticLayer_ >= this.backParts_.Length)
			{
				this.backPartStatic_.Draw(spriteBatch);
			}
		}

		public void DrawMiddle(SpriteBatch spriteBatch)
		{
			this.midPart_.Draw(spriteBatch);
		}

		public void DrawFore(SpriteBatch spriteBatch)
		{
		}

		public void SetAcceleration(float acceleration)
		{
			this.midPart_.SetAcceleration(acceleration);
		}

		public void SetVelocity(float velocity)
		{
			this.midPart_.SetVelocity(velocity);
		}

		public float GetVelocity()
		{
			return this.midPart_.TexVelocity;
		}

		public void LoadNewBackground(int backgroundIndex)
		{
			Global.BGTexture.Dispose();
			this.Content.Unload();
			switch (backgroundIndex)
			{
			case 0:
			{
				this.backPartStaticLayer_ = 4;
				Global.BGTexture = this.Content.Load<Texture2D>("SeaBGXbox");
				this.backParts_[0].Reset(Global.BGTexture, Vector2.Zero, new Vector2(4096f, 514f), Vector2.Zero, TexDirection.RIGHT, 100f);
				for (int i = 1; i < this.backParts_.Length; i++)
				{
					this.backParts_[i].Reset(Global.BGTexture, Vector2.Zero, Vector2.Zero, Vector2.Zero, TexDirection.RIGHT, 0f);
				}
				this.midPart_.Reset(Global.BGTexture, new Vector2(0f, 514f), new Vector2(2560f, 720f), Vector2.Zero, TexDirection.RIGHT, 200f);
				this.backPartStatic_.Reset(Global.BGTexture, new Rectangle(2560, 514, 1280, 720), Vector2.Zero);
				break;
			}
			case 1:
				this.backPartStaticLayer_ = 2;
				Global.BGTexture = this.Content.Load<Texture2D>("cloudBGXbox");
				this.backParts_[0].Reset(Global.BGTexture, Vector2.Zero, new Vector2(2560f, 627f), new Vector2(0f, 0f), TexDirection.RIGHT, 50f);
				this.backParts_[1].Reset(Global.BGTexture, new Vector2(0f, 627f), new Vector2(2560f, 437f), new Vector2(0f, 283f), TexDirection.RIGHT, 100f);
				this.backPartStatic_.Reset(Global.BGTexture, new Rectangle(0, 1762, 1077, 559), new Vector2(348f, 0f));
				this.backParts_[2].Reset(Global.BGTexture, new Vector2(0f, 1064f), new Vector2(2560f, 485f), new Vector2(0f, 235f), TexDirection.RIGHT, 150f);
				this.backParts_[3].Reset(Global.BGTexture, new Vector2(0f, 0f), new Vector2(0f, 0f), new Vector2(0f, 0f), TexDirection.RIGHT, 0f);
				this.midPart_.Reset(Global.BGTexture, new Vector2(0f, 1549f), new Vector2(2560f, 213f), new Vector2(0f, 507f), TexDirection.RIGHT, 300f);
				break;
			case 2:
				this.backPartStaticLayer_ = 4;
				Global.BGTexture = this.Content.Load<Texture2D>("lavaBGXbox");
				this.backParts_[0].Reset(Global.BGTexture, Vector2.Zero, new Vector2(4096f, 720f), new Vector2(0f, 0f), TexDirection.RIGHT, 50f);
				this.backParts_[1].Reset(Global.BGTexture, new Vector2(0f, 720f), new Vector2(4096f, 720f), new Vector2(0f, 0f), TexDirection.RIGHT, 150f);
				this.backParts_[2].Reset(Global.BGTexture, Vector2.Zero, Vector2.Zero, Vector2.Zero, TexDirection.RIGHT, 0f);
				this.backParts_[3].Reset(Global.BGTexture, new Vector2(0f, 0f), new Vector2(0f, 0f), new Vector2(0f, 0f), TexDirection.RIGHT, 0f);
				this.midPart_.Reset(Global.BGTexture, new Vector2(0f, 1440f), new Vector2(4096f, 92f), new Vector2(0f, 628f), TexDirection.RIGHT, 300f);
				this.backPartStatic_.Reset(Global.BGTexture, new Rectangle(0, 0, 0, 0), new Vector2(0f, 0f));
				break;
			case 3:
				this.backPartStaticLayer_ = -1;
				Global.BGTexture = this.Content.Load<Texture2D>("meatBGXbox");
				this.backParts_[0].Reset(Global.BGTexture, new Vector2(0f, 0f), new Vector2(2560f, 720f), new Vector2(0f, 0f), TexDirection.RIGHT, 75f);
				this.backParts_[1].Reset(Global.BGTexture, new Vector2(0f, 1329f), new Vector2(2560f, 258f), new Vector2(0f, 272f), TexDirection.RIGHT, 150f);
				this.backParts_[2].Reset(Global.BGTexture, new Vector2(0f, 720f), new Vector2(2560f, 457f), new Vector2(0f, 263f), TexDirection.RIGHT, 175f);
				this.backParts_[3].Reset(Global.BGTexture, new Vector2(0f, 0f), new Vector2(0f, 0f), new Vector2(0f, 0f), TexDirection.RIGHT, 300f);
				this.backPartStatic_.Reset(Global.BGTexture, new Rectangle(0, 0, 0, 0), new Vector2(0f, 0f));
				this.midPart_.Reset(Global.BGTexture, new Vector2(0f, 1177f), new Vector2(2560f, 152f), new Vector2(0f, 598f), TexDirection.RIGHT, 200f);
				break;
			case 4:
				this.backPartStaticLayer_ = 2;
				Global.BGTexture = this.Content.Load<Texture2D>("ronBGXbox");
				this.backParts_[0].Reset(Global.BGTexture, new Vector2(0f, 0f), new Vector2(2560f, 720f), new Vector2(0f, 0f), TexDirection.RIGHT, 75f);
				this.backParts_[1].Reset(Global.BGTexture, new Vector2(0f, 720f), new Vector2(2560f, 317f), new Vector2(0f, 403f), TexDirection.RIGHT, 150f);
				this.backParts_[2].Reset(Global.BGTexture, new Vector2(0f, 1040f), new Vector2(2560f, 350f), new Vector2(0f, 370f), TexDirection.RIGHT, 175f);
				this.backParts_[3].Reset(Global.BGTexture, new Vector2(0f, 1390f), new Vector2(2560f, 254f), new Vector2(0f, 466f), TexDirection.RIGHT, 300f);
				this.backPartStatic_.Reset(Global.BGTexture, new Rectangle(0, 0, 0, 0), new Vector2(0f, 0f));
				this.midPart_.Reset(Global.BGTexture, new Vector2(0f, 0f), new Vector2(0f, 0f), new Vector2(0f, 0f), TexDirection.RIGHT, 300f);
				break;
			case 5:
				this.backPartStaticLayer_ = 2;
				Global.BGTexture = this.Content.Load<Texture2D>("nyanBGios");
				this.backParts_[0].Reset(Global.BGTexture, new Vector2(0f, 0f), new Vector2(2047f, 567f), new Vector2(0f, 0f), TexDirection.RIGHT, 50f);
                this.backParts_[1].Reset(Global.BGTexture, new Vector2(0f, 0f), new Vector2(2047f, 567f), new Vector2(0f, 0f), TexDirection.RIGHT, 50f);
                this.backParts_[2].Reset(Global.BGTexture, new Vector2(0f, 568f), new Vector2(2047f, 213f), new Vector2(0f, 475f), TexDirection.RIGHT, 100f);
                this.backParts_[3].Reset(Global.BGTexture, new Vector2(0f, 782f), new Vector2(2047f, 168f), new Vector2(0f, 575f), TexDirection.RIGHT, 150f);
                this.backPartStatic_.Reset(Global.BGTexture, new Rectangle(0, 0, 0, 0), new Vector2(0f, 0f));
				this.midPart_.Reset(Global.BGTexture, new Vector2(0f, 0f), new Vector2(0f, 0f), new Vector2(0f, 0f), TexDirection.RIGHT, 300f);
                break;
			}
		}
	}
}
