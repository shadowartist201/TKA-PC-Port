using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Helicopter.Core
{
    internal class ClapperManager
    {
        private bool on_ = false;

        private Clapper[] clappers_ = new Clapper[2];

        public ClapperManager()
        {
            for (int i = 0; i < this.clappers_.Length; i++)
            {
                this.clappers_[i] = new Clapper();
            }
        }

        public void Update(float dt)
        {
            if (this.on_)
            {
                Clapper[] array = this.clappers_;
                foreach (Clapper clapper in array)
                {
                    clapper.Update(dt);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (this.on_)
            {
                Clapper[] array = this.clappers_;
                foreach (Clapper clapper in array)
                {
                    clapper.Draw(spriteBatch);
                }
            }
        }

        public void TurnOn(int index)
        {
            this.on_ = true;
            /*if (index == 0)
            {
                this.clappers_[0].TurnOn(new Vector2(1280 * 0.3125f, 360f), 0, Color.White);
                this.clappers_[1].TurnOn(new Vector2(1280 * 0.6875f, 360f), 0, Color.White);
                return;
            }*/
            this.clappers_[0].TurnOn(new Vector2(1280 * 0.3125f, 360f), index, Color.White);
            this.clappers_[1].TurnOn(new Vector2(1280 * 0.6875f, 360f), index, Color.White);
            return;
        }

        public void TurnOff()
        {
            this.on_ = false;
            Clapper[] array = this.clappers_;
            foreach (Clapper clapper in array)
            {
                clapper.TurnOff();
            }
        }
    }
}