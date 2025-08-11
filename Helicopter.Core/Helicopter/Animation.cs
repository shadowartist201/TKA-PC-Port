using Microsoft.Xna.Framework;

namespace Helicopter.Core
{
	public class Animation
	{
		private Rectangle[] frames;

		private int currentFrame = 0;

		private float timer = 0f;

		private float frameLength;

		public Rectangle CurrentFrame => this.frames[this.currentFrame];

		public int FramesPerSecond => (int)(1f / this.frameLength);

		public Animation(Rectangle[] Frames, float FrameLength)
		{
			this.frames = new Rectangle[Frames.Length];
			for (int i = 0; i < Frames.Length; i++)
			{
				ref Rectangle reference = ref this.frames[i];
				reference = Frames[i];
			}
			this.frameLength = FrameLength;
		}

		public void Update(float dt)
		{
			this.timer += dt;
			if (this.timer >= this.frameLength)
			{
				this.timer = 0f;
				this.currentFrame = (this.currentFrame + 1) % this.frames.Length;
			}
		}

		public void Reset()
		{
			this.currentFrame = 0;
			this.timer = 0f;
		}
	}
}
