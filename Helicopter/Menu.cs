using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Helicopter
{
	public class Menu
	{
		protected List<MenuItem> menuItems_;

		protected int index_;

		private bool horizontal_;

		protected Menu(bool horizontal)
		{
			this.horizontal_ = horizontal;
			this.menuItems_ = new List<MenuItem>();
		}

		protected void AddMenuItem(MenuItem menuItem)
		{
			this.menuItems_.Add(menuItem);
		}

		protected void Update(float dt, InputState currInput)
		{
			for (int i = 0; i < this.menuItems_.Count; i++)
			{
				this.menuItems_[i].Update(dt, i == this.index_);
			}
			this.SetItemVertices();
			Global.itemSelectedEffect.Update(dt);
			if (this.horizontal_)
			{
				if (currInput.IsButtonPressed(Buttons.DPadRight) && this.index_ + 1 < this.menuItems_.Count)
				{
					this.index_++;
				}
				if (currInput.IsButtonPressed(Buttons.DPadLeft) && this.index_ > 0)
				{
					this.index_--;
				}
			}
			else
			{
				if (currInput.IsButtonPressed(Buttons.DPadDown) && this.index_ + 1 < this.menuItems_.Count)
				{
					this.index_++;
				}
				if (currInput.IsButtonPressed(Buttons.DPadUp) && this.index_ > 0)
				{
					this.index_--;
				}
			}
		}

		protected void SetItemVertices()
		{
			if (this.index_ < this.menuItems_.Count)
			{
				Global.itemSelectedEffect.SetItemVertices(this.menuItems_[this.index_].CollisionRect);
			}
		}

		protected void Draw(SpriteBatch spriteBatch)
		{
			for (int i = 0; i < this.menuItems_.Count; i++)
			{
				this.menuItems_[i].Draw(spriteBatch);
			}
			Global.itemSelectedEffect.Draw(spriteBatch);
		}
	}
}
