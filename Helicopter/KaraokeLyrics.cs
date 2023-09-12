using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Helicopter
{
	public class KaraokeLyrics
	{
		private bool visible_;

		private SpriteFont font_;

		private float fontScale_ = 1f;

		private Vector2 position_ = new Vector2(640f, 86f);

		private Color highlightedColor_ = Color.White;

		private Color nonHighlightedColor_ = new Color(new Vector4(0f, 0f, 0f, 0.7f));

		private int currentLineIndex_ = -1;

		private float lineProgress_;

		private float[] lineTimes_ = new float[44]
		{
			10500f, 13284f, 14959f, 17838f, 21142f, 23902f, 26710f, 29330f, 43818f, 49150f,
			54436f, 59651f, 64040f, 67296f, 69891f, 71354f, 75177f, 76357f, 77749f, 79023f,
			80418f, 81713f, 83011f, 111916f, 114630f, 116329f, 119019f, 122440f, 125272f, 127962f,
			130675f, 133271f, 134686f, 135913f, 137730f, 140397f, 143818f, 145305f, 146626f, 149316f,
			152006f, 154696f, 158000f, 160524f
		};

		private float[] lineProgressRates_ = new float[44]
		{
			0.597014964f,
			0.6349206f,
			0.5042864f,
			5f / 6f,
			0.7692308f,
			0.625f,
			0.714285731f,
			0.7692308f,
			0.349283963f,
			0.238322213f,
			0.287191272f,
			0.247279912f,
			40f / 117f,
			0.415110022f,
			0.770416f,
			0.426803261f,
			1f,
			1f,
			1f,
			1f,
			1f,
			1f,
			5f / 6f,
			0.597014964f,
			0.6349206f,
			0.5042864f,
			5f / 6f,
			0.7692308f,
			0.625f,
			0.714285731f,
			0.7692308f,
			1f,
			1f,
			0.7692308f,
			0.6349206f,
			0.5042864f,
			5f / 6f,
			5f / 6f,
			0.7692308f,
			0.714285731f,
			0.7692308f,
			0.336360574f,
			0.446229368f,
			0.344471216f
		};

		private string[] lines_ = new string[44]
		{
			"Hungry for your love", "Hungry for your love", "Youre a taste of heaven", "I kissed a star", "Craving for your love", "Just cant get enough", "Feed me more", "Let it pour", "Ive never felt this way before", "I have this appetite for more of you",
			"I feel the world has carved a special place for me", "And since Ive laid my eyes on you its been a dream", "Theres something burning deep inside", "Its growing stronger I cant hide", "Its taking over", "I cant fight it", "You pick me up", "You bring me down", "Cant feel my feet", "Im off the ground",
			"No gravity", "In love Ive found", "Dont wanna come down", "Hungry for your love", "Hungry for your love", "Youre a taste of heaven", "I kissed a star", "Craving for your love", "Just cant get enough", "Feed me more",
			"Let it pour", "Hungry for you", "Hungry for you", "Hungry for your love", "Youre a taste of heaven", "I kissed a star", "Craving for you", "Craving for you", "Craving for your love", "Feed me more",
			"Let it pour", "You make me wanna touch the sky", "And Im soaring flying high", "When Im with you the sun is shining"
		};

		private string CurrentLine
		{
			get
			{
				if (this.currentLineIndex_ > -1)
				{
					return this.lines_[this.currentLineIndex_];
				}
				return "";
			}
		}

		public KaraokeLyrics()
		{
			this.font_ = Global.spriteFont;
		}

		public void Update(float dt, float elapsedMilliseconds)
		{
			if (this.visible_)
			{
				if (this.currentLineIndex_ > -1)
				{
					this.lineProgress_ += this.lineProgressRates_[this.currentLineIndex_] * dt;
				}
				if (this.lineTimes_.Length > this.currentLineIndex_ + 1 && this.lineTimes_[this.currentLineIndex_ + 1] < elapsedMilliseconds)
				{
					this.lineProgress_ = 0f;
					this.currentLineIndex_++;
				}
				if (this.currentLineIndex_ == this.lineTimes_.Length - 1 && this.lineProgress_ > 2f)
				{
					this.TurnOff();
				}
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			if (this.visible_)
			{
				if (this.lineProgress_ <= 1f)
				{
					int num = (int)(this.lineProgress_ * (float)this.CurrentLine.Length);
					string text = this.CurrentLine.Substring(0, num);
					string text2 = this.CurrentLine.Substring(num, this.CurrentLine.Length - num);
					Vector2 vector = this.font_.MeasureString(this.CurrentLine);
					Vector2 vector2 = this.font_.MeasureString(text);
					spriteBatch.DrawString(this.font_, text, this.position_ + new Vector2((0f - vector.X) / 2f, 0f * vector.Y / 2f), this.highlightedColor_, 0f, Vector2.Zero, this.fontScale_, SpriteEffects.None, 0f);
					spriteBatch.DrawString(this.font_, text2, this.position_ + new Vector2(vector2.X - vector.X / 2f, 0f * vector.Y / 2f), this.nonHighlightedColor_, 0f, Vector2.Zero, this.fontScale_, SpriteEffects.None, 0f);
				}
				else if (this.lineProgress_ < 1.6f)
				{
					spriteBatch.DrawString(origin: new Vector2(this.font_.MeasureString(this.CurrentLine).X / 2f, 0f), spriteFont: this.font_, text: this.CurrentLine, position: this.position_, color: this.highlightedColor_, rotation: 0f, scale: this.fontScale_, effects: SpriteEffects.None, layerDepth: 0f);
				}
			}
		}

		public void TurnOn()
		{
			this.visible_ = true;
			this.currentLineIndex_ = -1;
			this.lineProgress_ = 0f;
		}

		public void TurnOff()
		{
			this.visible_ = false;
		}
	}
}
