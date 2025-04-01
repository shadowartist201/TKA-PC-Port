using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace Helicopter
{
	internal class RainbowTrail
	{
		private bool visible_;

		public static bool nyancat;
		public static bool tacnyan;
		public static bool gameboy;

        private float progress_;

		private float progressRate_ = 160f;

		private Vector2[] positions_ = new Vector2[95]
		{
			new Vector2(366f, 0f),
			new Vector2(372f, 0f),
			new Vector2(368f, 0f),
			new Vector2(364f, 0f),
			new Vector2(360f, 0f),
			new Vector2(356f, 0f),
			new Vector2(352f, 0f),
			new Vector2(348f, 0f),
			new Vector2(344f, 0f),
			new Vector2(340f, 0f),
			new Vector2(336f, 0f),
			new Vector2(332f, 0f),
			new Vector2(328f, 0f),
			new Vector2(324f, 0f),
			new Vector2(320f, 0f),
			new Vector2(316f, 0f),
			new Vector2(312f, 0f),
			new Vector2(308f, 0f),
			new Vector2(304f, 0f),
			new Vector2(300f, 0f),
			new Vector2(296f, 0f),
			new Vector2(292f, 0f),
			new Vector2(288f, 0f),
			new Vector2(284f, 0f),
			new Vector2(280f, 0f),
			new Vector2(276f, 0f),
			new Vector2(272f, 0f),
			new Vector2(268f, 0f),
			new Vector2(264f, 0f),
			new Vector2(260f, 0f),
			new Vector2(256f, 0f),
			new Vector2(252f, 0f),
			new Vector2(248f, 0f),
			new Vector2(244f, 0f),
			new Vector2(240f, 0f),
			new Vector2(236f, 0f),
			new Vector2(232f, 0f),
			new Vector2(228f, 0f),
			new Vector2(224f, 0f),
			new Vector2(220f, 0f),
			new Vector2(216f, 0f),
			new Vector2(212f, 0f),
			new Vector2(208f, 0f),
			new Vector2(204f, 0f),
			new Vector2(200f, 0f),
			new Vector2(196f, 0f),
			new Vector2(192f, 0f),
			new Vector2(188f, 0f),
			new Vector2(184f, 0f),
			new Vector2(180f, 0f),
			new Vector2(176f, 0f),
			new Vector2(172f, 0f),
			new Vector2(168f, 0f),
			new Vector2(164f, 0f),
			new Vector2(160f, 0f),
			new Vector2(156f, 0f),
			new Vector2(152f, 0f),
			new Vector2(148f, 0f),
			new Vector2(144f, 0f),
			new Vector2(140f, 0f),
			new Vector2(136f, 0f),
			new Vector2(132f, 0f),
			new Vector2(128f, 0f),
			new Vector2(124f, 0f),
			new Vector2(120f, 0f),
			new Vector2(116f, 0f),
			new Vector2(112f, 0f),
			new Vector2(108f, 0f),
			new Vector2(104f, 0f),
			new Vector2(100f, 0f),
			new Vector2(96f, 0f),
			new Vector2(92f, 0f),
			new Vector2(88f, 0f),
			new Vector2(84f, 0f),
			new Vector2(80f, 0f),
			new Vector2(76f, 0f),
			new Vector2(72f, 0f),
			new Vector2(68f, 0f),
			new Vector2(64f, 0f),
			new Vector2(60f, 0f),
			new Vector2(56f, 0f),
			new Vector2(52f, 0f),
			new Vector2(48f, 0f),
			new Vector2(44f, 0f),
			new Vector2(40f, 0f),
			new Vector2(36f, 0f),
			new Vector2(32f, 0f),
			new Vector2(28f, 0f),
			new Vector2(24f, 0f),
			new Vector2(20f, 0f),
			new Vector2(16f, 0f),
			new Vector2(12f, 0f),
			new Vector2(8f, 0f),
			new Vector2(4f, 0f),
			new Vector2(0f, 0f)
		};

		public void Update(float dt, float positionY)
		{
			if (!this.visible_)
			{
				return;
			}
			this.progress_ += this.progressRate_ * dt;
			while (this.progress_ >= 1f)
			{
				for (int num = this.positions_.Length - 1; num > 0; num--)
				{
					this.positions_[num].Y = this.positions_[num - 1].Y;
				}
				this.positions_[0].Y = positionY;
				this.progress_ -= 1f;
			}
		}

        public void Draw(SpriteBatch spriteBatch)
        {
            if (this.visible_)
            {
                for (int i = 0; i < this.positions_.Length; i++)
                {
                    if (nyancat)
                        spriteBatch.Draw(Global.cats, new Vector2(this.positions_[i].X, this.positions_[i].Y - 12), (Rectangle?)new Rectangle(533, 900, 4, 42), Color.White);
                    else if (tacnyan)
                        spriteBatch.Draw(Global.cats, new Vector2(this.positions_[i].X, this.positions_[i].Y - 12), (Rectangle?)new Rectangle(1042, 964, 4, 42), Color.White);
                    else if (gameboy)
                        spriteBatch.Draw(Global.cats, new Vector2(this.positions_[i].X, this.positions_[i].Y - 12), (Rectangle?)new Rectangle(1049, 1033, 4, 42), Color.White);
                    else
                        spriteBatch.Draw(Global.cats, this.positions_[i], (Rectangle?)new Rectangle(410, 596, 4, 16), Color.White);
                }
            }
        }

        public void TurnOn()
		{
			this.visible_ = true;
		}

		public void TurnOff()
		{
			this.visible_ = false;
		}
	}
}
