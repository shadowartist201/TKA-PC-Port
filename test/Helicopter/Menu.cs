using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Helicopter;

public class Menu
{
	protected List<MenuItem> menuItems_;

	protected int index_;

	private bool horizontal_;

	protected Menu(bool horizontal)
	{
		horizontal_ = horizontal;
		menuItems_ = new List<MenuItem>();
	}

	protected void AddMenuItem(MenuItem menuItem)
	{
		menuItems_.Add(menuItem);
	}

	protected void Update(float dt, InputState currInput)
	{
		for (int i = 0; i < menuItems_.Count; i++)
		{
			menuItems_[i].Update(dt, i == index_);
		}
		SetItemVertices();
		Global.itemSelectedEffect.Update(dt);
		if (horizontal_)
		{
			if (currInput.IsButtonPressed((Buttons)8) && index_ + 1 < menuItems_.Count)
			{
				index_++;
			}
			if (currInput.IsButtonPressed((Buttons)4) && index_ > 0)
			{
				index_--;
			}
		}
		else
		{
			if (currInput.IsButtonPressed((Buttons)2) && index_ + 1 < menuItems_.Count)
			{
				index_++;
			}
			if (currInput.IsButtonPressed((Buttons)1) && index_ > 0)
			{
				index_--;
			}
		}
	}

	protected void SetItemVertices()
	{
				if (index_ < menuItems_.Count)
		{
			Global.itemSelectedEffect.SetItemVertices(menuItems_[index_].CollisionRect);
		}
	}

	protected void Draw(SpriteBatch spriteBatch)
	{
		for (int i = 0; i < menuItems_.Count; i++)
		{
			menuItems_[i].Draw(spriteBatch);
		}
		Global.itemSelectedEffect.Draw(spriteBatch);
	}
}
