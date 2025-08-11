using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Helicopter.Core
{
    internal class LetterManager
    {
        private bool on_ = false;

        private Letter[] letters_ = new Letter[40];

        public LetterManager()
        {
            for (int i = 0; i < this.letters_.Length; i++)
            {
                this.letters_[i] = new Letter();
                this.letters_[i].position_ = new Vector2(Global.RandomBetween(0f, 1080f), Global.RandomBetween(0f, 720f));
                if (i < 20)
                {
                    this.letters_[i].frameInfo = new Rectangle(0, 0, 32, 50);
                }
                else if (i < 30)
                {
                    this.letters_[i].frameInfo = new Rectangle(32, 0, 32, 50);
                }
                else if (i < 40)
                {
                    this.letters_[i].frameInfo = new Rectangle(64, 0, 32, 50);
                }
            }
        }

        public void Update(float dt)
        {
            if (this.on_)
            {
                Letter[] array = this.letters_;
                foreach (Letter letter in array)
                {
                    letter.Update(dt);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (this.on_)
            {
                Letter[] array = this.letters_;
                foreach (Letter letter in array)
                {
                    letter.Draw(spriteBatch, letter.position_);
                }
            }
        }

        public void TurnOn(int index)
        {
            this.on_ = true;
            Letter[] array = this.letters_;
            foreach (Letter letter in array)
            {
                letter.TurnOn();
            }
        }

        public void TurnOff()
        {
            this.on_ = false;
            Letter[] array = this.letters_;
            foreach (Letter letter in array)
            {
                letter.TurnOff();
            }
        }
    }
}
