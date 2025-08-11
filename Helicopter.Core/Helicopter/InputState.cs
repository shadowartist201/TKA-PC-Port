using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace Helicopter.Core
{
	public class InputState
	{
		private GamePadState prevInput;

		private GamePadState currInput;

		private KeyboardState prevKeyInput;

		private KeyboardState currKeyInput;

        private TouchCollection prevTouches;

        private TouchCollection currTouches;

        public void Update()
		{
			if (Global.playerIndex.HasValue)
			{
				this.currInput = GamePad.GetState(Global.playerIndex.Value);
			}
			this.currKeyInput = Keyboard.GetState();
            this.currTouches = TouchPanel.GetState();
        }

		public void EndUpdate()
		{
			this.prevInput = this.currInput;
			this.prevKeyInput = this.currKeyInput;
            this.prevTouches = this.currTouches;
        }

        public bool IsThingTouched()
        {
            bool flag = this.currTouches.Count > 0 && this.prevTouches.Count == 0;
            bool flag2 = false;
            return flag || flag2;
        }

        public bool IsButtonPressed(Buttons button)
		{
			bool flag = this.currInput.IsButtonDown(button) && this.prevInput.IsButtonUp(button);
			bool flag2 = false;
			switch (button)
			{
			case Buttons.A:
				flag2 = this.currKeyInput.IsKeyDown(Keys.Space) && this.prevKeyInput.IsKeyUp(Keys.Space);
				break;
			case Buttons.B:
				flag2 = this.currKeyInput.IsKeyDown(Keys.B) && this.prevKeyInput.IsKeyUp(Keys.B);
				break;
			case Buttons.DPadLeft:
				flag = flag || (this.currInput.ThumbSticks.Left.X < -0.5f && this.prevInput.ThumbSticks.Left.X >= -0.5f);
				flag2 = this.currKeyInput.IsKeyDown(Keys.Left) && this.prevKeyInput.IsKeyUp(Keys.Left);
				break;
			case Buttons.DPadRight:
				flag = flag || (this.currInput.ThumbSticks.Left.X > 0.5f && this.prevInput.ThumbSticks.Left.X <= 0.5f);
				flag2 = this.currKeyInput.IsKeyDown(Keys.Right) && this.prevKeyInput.IsKeyUp(Keys.Right);
				break;
			case Buttons.DPadUp:
				flag = flag || (this.currInput.ThumbSticks.Left.Y > 0.5f && this.prevInput.ThumbSticks.Left.Y <= 0.5f);
				flag2 = this.currKeyInput.IsKeyDown(Keys.Up) && this.prevKeyInput.IsKeyUp(Keys.Up);
				break;
			case Buttons.DPadDown:
				flag = flag || (this.currInput.ThumbSticks.Left.Y < -0.5f && this.prevInput.ThumbSticks.Left.Y >= -0.5f);
				flag2 = this.currKeyInput.IsKeyDown(Keys.Down) && this.prevKeyInput.IsKeyUp(Keys.Down);
				break;
			case Buttons.Start:
				flag2 = this.currKeyInput.IsKeyDown(Keys.S) && this.prevKeyInput.IsKeyUp(Keys.S);
				break;
			case Buttons.BigButton:
				flag2 = this.currKeyInput.IsKeyDown(Keys.F1) && this.prevKeyInput.IsKeyUp(Keys.F1);
				break;
			}
			return flag || flag2;
		}

		public bool IsButtonUp(Buttons button)
		{
			bool flag = this.currInput.IsButtonUp(button);
			bool flag2 = false;
			switch (button)
			{
			case Buttons.A:
				flag2 = this.currKeyInput.IsKeyUp(Keys.Space);
				break;
			case Buttons.B:
				flag2 = this.currKeyInput.IsKeyUp(Keys.B);
				break;
			case Buttons.DPadLeft:
				flag2 = this.currKeyInput.IsKeyUp(Keys.Left);
				break;
			case Buttons.DPadRight:
				flag2 = this.currKeyInput.IsKeyUp(Keys.Right);
				break;
			case Buttons.DPadUp:
				flag2 = this.currKeyInput.IsKeyUp(Keys.Up);
				break;
			case Buttons.DPadDown:
				flag2 = this.currKeyInput.IsKeyUp(Keys.Down);
				break;
			case Buttons.Start:
				flag2 = this.currKeyInput.IsKeyUp(Keys.S);
				break;
            case Buttons.BigButton:
                flag2 = this.currKeyInput.IsKeyUp(Keys.F1);
                break;
            }
			return flag || flag2;
		}

		public bool IsButtonDown(Buttons button)
		{
			bool flag = this.currInput.IsButtonDown(button);
			bool flag2 = false;
			switch (button)
			{
			case Buttons.A:
				flag2 = this.currKeyInput.IsKeyDown(Keys.Space);
				break;
			case Buttons.B:
				flag2 = this.currKeyInput.IsKeyDown(Keys.B);
				break;
			case Buttons.DPadLeft:
				flag2 = this.currKeyInput.IsKeyDown(Keys.Left);
				break;
			case Buttons.DPadRight:
				flag2 = this.currKeyInput.IsKeyDown(Keys.Right);
				break;
			case Buttons.DPadUp:
				flag2 = this.currKeyInput.IsKeyDown(Keys.Up);
				break;
			case Buttons.DPadDown:
				flag2 = this.currKeyInput.IsKeyDown(Keys.Down);
				break;
			case Buttons.Start:
				flag2 = this.currKeyInput.IsKeyDown(Keys.S);
				break;
            case Buttons.BigButton:
                flag2 = this.currKeyInput.IsKeyDown(Keys.F1);
                break;
            }
			return flag || flag2;
		}
	}
}
