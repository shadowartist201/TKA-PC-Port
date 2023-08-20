using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Helicopter
{
	internal class OpeningMenu : Menu
	{
		public OpeningMenu()
			: base(horizontal: true)
		{
			base.AddMenuItem(new MenuItem(Global.mainPressStartTex, new Rectangle(0, 0, 534, 135), new Vector2(640f, 600f)));
		}

		public void Update(float dt, InputState currInput, ref GameState gameState)
		{
			base.Update(dt, currInput);
		}

		public new void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
		}
	}
}
