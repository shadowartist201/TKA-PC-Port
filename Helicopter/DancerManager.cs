using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Helicopter
{
	internal class DancerManager
	{
		private bool on_ = false;

		private Dancer[] dancers_ = new Dancer[5];

		public DancerManager()
		{
			for (int i = 0; i < this.dancers_.Length; i++)
			{
				this.dancers_[i] = new Dancer();
			}
		}

		public void Update(float dt)
		{
			if (this.on_)
			{
				Dancer[] array = this.dancers_;
				foreach (Dancer dancer in array)
				{
					dancer.Update(dt);
				}
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			if (this.on_)
			{
				Dancer[] array = this.dancers_;
				foreach (Dancer dancer in array)
				{
					dancer.Draw(spriteBatch);
				}
			}
		}

		public void TurnOn(int index)
		{
			this.on_ = true;
			if (index == 0)
			{
				for (int i = 0; i < this.dancers_.Length; i++)
				{
					Color color = Global.rainbowColors[i];
					this.dancers_[i].TurnOn(new Vector2(1458 / this.dancers_.Length * i, 360f), 0, Color.White);
				}
				return;
			}
			Dancer[] array = this.dancers_;
			foreach (Dancer dancer in array)
			{
				dancer.ReverseDirection();
			}
		}

		public void TurnOff()
		{
			this.on_ = false;
			Dancer[] array = this.dancers_;
			foreach (Dancer dancer in array)
			{
				dancer.TurnOff();
			}
		}
	}
}
