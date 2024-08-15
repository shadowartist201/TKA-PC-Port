using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Helicopter
{
    internal class Letter
    {
        private bool visible_ = false;

        public Vector2 position_;

        private Vector2 velocity_;

        public Rectangle frameInfo;

        private int[] nums = { -1, 1 };

        private Random random = new Random();

        public Letter()
        {
            position_ = Vector2.Zero;
            velocity_ = new Vector2(nums[random.Next(nums.Length)] *400, nums[random.Next(nums.Length)]* 400);
            frameInfo = Rectangle.Empty;
        }

        public void Update(float dt)
        {
            if (this.visible_)
            {
                this.position_ += this.velocity_ * dt;
                if (Math.Sign(this.velocity_.X) == 1 && this.position_.X > (float)(1280 + frameInfo.Width / 2)) //when too far right
                {
                    this.position_.X -= 1280 + frameInfo.Width;
                    this.position_.Y = Global.RandomBetween(0f, 720f);
                }
                if (Math.Sign(this.velocity_.X) == -1 && this.position_.X < (float)(-frameInfo.Width / 2)) //when too far left
                {
                    this.position_.X += 1280 + frameInfo.Width;
                    this.position_.Y = Global.RandomBetween(0f, 720f);
                }
                if (Math.Sign(this.velocity_.Y) == -1 && this.position_.Y > (float)(720 + frameInfo.Height / 2)) //when too far up
                {
                    this.position_.X = Global.RandomBetween(0f, 1080f);
                    this.position_.Y -= 720 + frameInfo.Height;
                } 
                if (Math.Sign(this.velocity_.Y) == -1 && this.position_.Y < (float)(-frameInfo.Height / 2)) //when too far down
                {
                    this.position_.X = Global.RandomBetween(0f, 1080f);
                    this.position_.Y += 720 + frameInfo.Height;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            if (this.visible_)
            {
                spriteBatch.Draw(Global.nyanLetters, position, (Rectangle?)new Rectangle(this.frameInfo.X, this.frameInfo.Y, this.frameInfo.Width, this.frameInfo.Height), Color.White, 0f, new Vector2(this.frameInfo.Width / 2, this.frameInfo.Height / 2), 1f, SpriteEffects.None, 1f);
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
