using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace Helicopter.Core
{
	public class ScoreSystem
	{
		public ScoreInfo scoreInfo;

		private int currScore;

		private bool scoring;

		private Vector2 positionNormal = new Vector2(128f, 623f);

		private Vector2 positionHigh = new Vector2(900f, 623f);

		private float scale = 0.75f;

		private bool toTheBass = false;

		private float scaleChange = 4.399988f;

		private bool toMovement = false;

		private Vector2 velocityNormal;

		private Vector2 velocityHigh;

		public bool seaFortyUnlocked => this.scoreInfo.seaFortyUnlocked;

		public bool seaSixtyUnlocked => this.scoreInfo.seaSixtyUnlocked;

		public bool seaEightyUnlocked => this.scoreInfo.seaEightyUnlocked;

		public bool cloudFortyUnlocked => this.scoreInfo.cloudFortyUnlocked;

		public bool cloudSixtyUnlocked => this.scoreInfo.cloudSixtyUnlocked;

		public bool cloudEightyUnlocked => this.scoreInfo.cloudEightyUnlocked;

		public bool lavaFortyUnlocked => this.scoreInfo.lavaFortyUnlocked;

		public bool lavaSixtyUnlocked => this.scoreInfo.lavaSixtyUnlocked;

		public bool lavaEightyUnlocked => this.scoreInfo.lavaEightyUnlocked;

		public bool meatFortyUnlocked => this.scoreInfo.meatFortyUnlocked;

		public bool meatSixtyUnlocked => this.scoreInfo.meatSixtyUnlocked;

		public bool meatEightyUnlocked => this.scoreInfo.meatEightyUnlocked;

		public bool ronFortyUnlocked => this.scoreInfo.ronFortyUnlocked;

		public bool ronSixtyUnlocked => this.scoreInfo.ronSixtyUnlocked;

		public bool ronEightyUnlocked => this.scoreInfo.ronEightyUnlocked;

        public bool nyanFortyUnlocked => this.scoreInfo.nyanFortyUnlocked;

        public bool nyanSixtyUnlocked => this.scoreInfo.nyanSixtyUnlocked;

        public bool nyanEightyUnlocked => this.scoreInfo.nyanEightyUnlocked;

        public int CurrScore
		{
			get
			{
				return this.currScore;
			}
			set
			{
				this.currScore = value;
			}
		}

		public ScoreSystem()
		{
			this.scoreInfo = new ScoreInfo(0);
		}

		public void LoadInfo()
		{
			this.scoreInfo = ScoreInfo.LoadInfo();
			
		}

		public void SaveInfo()
		{
			if (this.scoreInfo.HighScore != 0)
			{
				this.scoreInfo.SaveInfo();
			}
		}

		public void Update(float dt)
		{
			if (this.scoring)
			{
				this.currScore += (int)(dt * 1000f);
			}
			if (this.toTheBass)
			{
				this.scale += this.scaleChange * dt;
				if (this.scale > 1.5f)
				{
					this.scale = 1.5f;
					this.scaleChange = 0f - this.scaleChange;
				}
				if (this.scale < 0f)
				{
					this.scale = 0f;
					this.scaleChange = 0f - this.scaleChange;
				}
			}
			else
			{
				this.scale = 0.75f;
			}
			if (this.toMovement)
			{
				this.positionNormal += this.velocityNormal * dt;
				this.positionHigh += this.velocityHigh * dt;
				if (this.positionNormal.X < 0f)
				{
					this.positionNormal.X = 0f;
					this.velocityNormal.X = 0f - this.velocityNormal.X;
				}
				if (this.positionNormal.X > 900f)
				{
					this.positionNormal.X = 900f;
					this.velocityNormal.X = 0f - this.velocityNormal.X;
				}
				if (this.positionNormal.Y < 0f)
				{
					this.positionNormal.Y = 0f;
					this.velocityNormal.Y = 0f - this.velocityNormal.Y;
				}
				if (this.positionNormal.Y > 687f)
				{
					this.positionNormal.Y = 687f;
					this.velocityNormal.Y = 0f - this.velocityNormal.Y;
				}
				if (this.positionHigh.X < 0f)
				{
					this.positionHigh.X = 0f;
					this.velocityHigh.X = 0f - this.velocityHigh.X;
				}
				if (this.positionHigh.X > 900f)
				{
					this.positionHigh.X = 900f;
					this.velocityHigh.X = 0f - this.velocityHigh.X;
				}
				if (this.positionHigh.Y < 0f)
				{
					this.positionHigh.Y = 0f;
					this.velocityHigh.Y = 0f - this.velocityHigh.Y;
				}
				if (this.positionHigh.Y > 687f)
				{
					this.positionHigh.Y = 687f;
					this.velocityHigh.Y = 0f - this.velocityHigh.Y;
				}
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			if (SongManager.IsNyanPack)
			{
				spriteBatch.Draw(Global.scoreTexture, this.positionNormal, (Rectangle?)null, Color.White, 0f, Vector2.Zero, this.scale, SpriteEffects.None, 0f);
				this.DrawNumber(spriteBatch, this.currScore, this.positionNormal + new Vector2(204f * this.scale, 0f), Vector2.Zero);
				spriteBatch.Draw(Global.highScoreTexture, this.positionHigh, (Rectangle?)null, Color.White, 0f, Vector2.Zero, this.scale, SpriteEffects.None, 0f);
				this.DrawNumber(spriteBatch, this.scoreInfo.HighScore, this.positionHigh + new Vector2(144f * this.scale, 0f), Vector2.Zero);
			}
			else
			{
				spriteBatch.Draw(Global.scoreTexture, this.positionNormal, (Rectangle?)null, Global.tunnelColor, 0f, Vector2.Zero, this.scale, SpriteEffects.None, 0f);
				this.DrawNumber(spriteBatch, this.currScore, this.positionNormal + new Vector2(204f * this.scale, 0f), Vector2.Zero);
				spriteBatch.Draw(Global.highScoreTexture, this.positionHigh, (Rectangle?)null, Global.tunnelColor, 0f, Vector2.Zero, this.scale, SpriteEffects.None, 0f);
				this.DrawNumber(spriteBatch, this.scoreInfo.HighScore, this.positionHigh + new Vector2(144f * this.scale, 0f), Vector2.Zero);
			}
		}

		private void DrawNumber(SpriteBatch spriteBatch, int number, Vector2 startingPosition, Vector2 startingOrigin)
		{
			string text = number.ToString();
			for (int i = 0; i < text.Length; i++)
			{
				if (SongManager.IsNyanPack)
				{
					int num = (int)char.GetNumericValue(text[i]);
					spriteBatch.Draw(Global.numbersTexture, startingPosition, (Rectangle?)new Rectangle(num * 36, 0, 36, 32), Color.White, 0f, startingOrigin, this.scale, SpriteEffects.None, 0f);
					startingPosition.X += 38f * this.scale;
				}
				else
				{
					int num = (int)char.GetNumericValue(text[i]);
					spriteBatch.Draw(Global.numbersTexture, startingPosition, (Rectangle?)new Rectangle(num * 36, 0, 36, 32), Global.tunnelColor, 0f, startingOrigin, this.scale, SpriteEffects.None, 0f);
					startingPosition.X += 38f * this.scale;
				}
			}
		}

		public void DrawAllScores(SpriteBatch spriteBatch)
		{
			this.scoreInfo.DrawAllScores(spriteBatch);
		}

		public void Begin()
		{
			this.SetZero();
			this.scoring = true;
		}

		public void SetZero()
		{
			this.currScore = 0;
		}

		public void End(int stageIndex, int catIndex)
		{
			this.scoreInfo.AddScore(stageIndex, catIndex, this.currScore);
			this.scoring = false;
		}

		public void Reset()
		{
			this.scoring = false;
			this.currScore = 0;
		}

		public void TurnOnBass()
		{
			this.toTheBass = true;
			this.scaleChange = 1.5f / Global.BPM;
		}

		public void TurnOffBass()
		{
			this.toTheBass = false;
		}

		public void TurnOnMovement()
		{
			this.toMovement = true;
			this.positionNormal = new Vector2(128f, 623f);
			this.positionHigh = new Vector2(900f, 623f);
			this.velocityNormal = new Vector2(600f, -1200f);
			this.velocityHigh = new Vector2(-1200f, -600f);
		}

		public void TurnOffMovement()
		{
			this.toMovement = false;
			this.positionNormal = new Vector2(128f, 623f);
			this.positionHigh = new Vector2(900f, 623f);
		}
	}
}
