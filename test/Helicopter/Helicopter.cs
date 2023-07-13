using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Helicopter
{
    public class Helicopter : AnimatedSpriteA
    {
        private ParticleEmitter particleEmitter;

        private RainbowTrail rainbowTrail;

        private Vector2 velocity;

        private Vector2 acceleration = Vector2.Zero;

        private float maxVelocity = 320f;

        private float maxVelocityG = 600f;

        public Vector2 position;

        private Rectangle collisionRect = new Rectangle(-16, -6, 40, 22);

        private float rotation = 0f;

        private float rotationVelocity = -1f;

        private float minRotation = 0.25f;

        private float maxRotation = 0.5f;

        private bool dead;

        private float deadTime = 1f;

        private float deadTimer;

        private Fireworks fireworks;

        public Rectangle CollisionRect => new Rectangle((int)this.position.X + this.collisionRect.X, (int)this.position.Y + this.collisionRect.Y, this.collisionRect.Width, this.collisionRect.Height);

        public Helicopter()
            : base(Global.cats)
        {
            this.Reset();
            this.fireworks = new Fireworks(5);
            this.rainbowTrail = new RainbowTrail();
            this.particleEmitter = new ParticleEmitter(this.position, Vector2.Zero, 4);
            this.particleEmitter.particleSystem.worldVelocity = new Vector2(-476f, 0f);
            this.ChangeAnimation(0);
        }

        public void HandleInput(InputState input, Tunnel tunnel, ScoreSystem scoreSystem, float dt)
        {
            this.acceleration.Y = 2250f;
            if (input.IsButtonDown(Buttons.A) && !this.IsDead())
            {
                if (!tunnel.IsOn())
                {
                    tunnel.TurnOn();
                    scoreSystem.Begin();
                    this.velocity = Vector2.Zero;
                    this.rotation += this.rotationVelocity * dt;
                    this.acceleration.Y -= 4500f;
                }
                else
                {
                    this.rotation += this.rotationVelocity * dt;
                    this.acceleration.Y -= 4500f;
                }
            }
            else if (tunnel.IsOn())
            {
                this.rotation -= this.rotationVelocity * dt;
            }
        }

        public void Update(float dt, Tunnel tunnel, ScoreSystem scoreSystem)
        {
            this.rainbowTrail.Update(dt, this.position.Y - 8f);
            this.particleEmitter.currPosition = this.position + new Vector2(-12f, 5f);
            this.particleEmitter.Update(dt);
            if (this.dead)
            {
                this.position = new Vector2(400f, 360f);
                this.deadTimer += dt;
                if (this.deadTimer > this.deadTime)
                {
                    this.Reset();
                    scoreSystem.SetZero();
                }
            }
            if (tunnel.IsOn())
            {
                this.velocity += this.acceleration * dt;
                if (this.velocity.Y > this.maxVelocityG)
                {
                    this.velocity.Y = this.maxVelocityG;
                }
                else if (this.velocity.Y < 0f - this.maxVelocity)
                {
                    this.velocity.Y = 0f - this.maxVelocity;
                }
                this.position += this.velocity * dt;
                if (this.rotation < this.minRotation)
                {
                    this.rotation = this.minRotation;
                }
                if (this.rotation > this.maxRotation)
                {
                    this.rotation = this.maxRotation;
                }
            }
            this.fireworks.Update(dt);
            base.Update(dt);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!this.dead)
            {
                this.particleEmitter.Draw(spriteBatch);
                this.rainbowTrail.Draw(spriteBatch);
                base.Draw(spriteBatch, this.position, this.rotation, 1f, Color.White, SpriteEffects.None);
            }
            else
            {
                this.fireworks.Draw(spriteBatch);
            }
        }

        public void Reset()
        {
            this.dead = false;
            this.deadTimer = 0f;
            this.position = new Vector2(400f, 360f);
            this.rotation = this.maxRotation;
        }

        public void Kill()
        {
            Global.PlayCatSound();
            this.dead = true;
            this.fireworks.SetOff(this.position);
            this.fireworks.SetOff(this.position);
            this.fireworks.SetOff(this.position);
        }

        public bool IsDead()
        {
            return this.dead;
        }

        public void ChangeAnimation(int newAnimation)
        {
            switch (newAnimation)
            {
                case 0:
                    base.SetAnimation(new Rectangle(162, 0, 84, 70), 4, 0.1f); //jetpack kitten
                    break;
                case 1:
                    base.SetAnimation(new Rectangle(0, 596, 100, 54), 4, 0.05f); //byarf kitten
                    break;
                case 2:
                    base.SetAnimation(new Rectangle(498, 0, 84, 75), 4, 0.1f); //butterfly kitten
                    break;
                case 3:
                    base.SetAnimation(new Rectangle(0, 0, 81, 70), 2, 0.3f);  //kitten with hat
                    break;
                case 4:
                    base.SetAnimation(new Rectangle(920, 0, 87, 67), 4, 0.05f); //mermaid kitten
                    break;
                case 5:
                    base.SetAnimation(new Rectangle(506, 300, 76, 76), 5, 0.1f); //purple kitten
                    break;
                case 6:
                    base.SetAnimation(new Rectangle(0, 300, 84, 84), 6, 0.1f); //cupid kitten
                    break;
                case 7:
                    base.SetAnimation(new Rectangle(0, 384, 75, 75), 6, 0.1f); //angel kitten
                    break;
                case 8:
                    base.SetAnimation(new Rectangle(920, 70, 111, 84), 4, 0.07f); //reaper kitten
                    break;
                case 9:
                    base.SetAnimation(new Rectangle(0, 75, 80, 90), 11, 0.025f); //bat kitten
                    break;
                case 10:
                    base.SetAnimation(new Rectangle(0, 165, 81, 63), 11, 0.025f); //fire kitten
                    break;
                case 11:
                    base.SetAnimation(new Rectangle(0, 228, 74, 72), 11, 0.025f); //lava kitten
                    break;
                case 12:
                    base.SetAnimation(new Rectangle(920, 156, 134, 88), 4, 0.07f); //dragon kitten
                    break;
                case 13:
                    base.SetAnimation(new Rectangle(556, 376, 76, 76), 4, 0.05f); //steak kitten
                    break;
                case 14:
                    base.SetAnimation(new Rectangle(0, 512, 84, 84), 4, 0.05f); //bacon kitten
                    break;
                case 15:
                    base.SetAnimation(new Rectangle(338, 512, 84, 84), 6, 0.05f); //sausage kitten
                    break;
                case 16:
                    base.SetAnimation(new Rectangle(920, 246, 83, 71), 4, 0.05f); //burger kitten
                    break;
                case 17:
                    base.SetAnimation(new Rectangle(0, 652, 91, 80), 17, 0.05f); //alien kitten
                    break;
                case 18:
                    base.SetAnimation(new Rectangle(0, 734, 92, 80), 15, 0.05f); //smile kitten
                    break;
                case 19:
                    base.SetAnimation(new Rectangle(0, 816, 81, 70), 19, 0.05f); //mcdonalds kitten
                    break;
                case 21:
                    base.SetAnimation(new Rectangle(8, 896, 85, 53), 6, 0.07f); //nyan cat
                    break;
                case 22:
                    base.SetAnimation(new Rectangle(6, 956, 84, 53), 12, 0.07f); //tac nyan
                    break;
                case 23:
                    base.SetAnimation(new Rectangle(9, 1028, 84, 53), 12, 0.07f); //gameboy cat
                    break;
            }
            if (newAnimation == 3)
            {
                this.particleEmitter.TurnOn();
            }
            else
            {
                this.particleEmitter.TurnOff();
            }
            if (newAnimation == 1)
            {
                this.rainbowTrail.TurnOn();
            }
            else
            {
                this.rainbowTrail.TurnOff();
            }
            if (newAnimation == 0 || newAnimation == 10 || newAnimation == 7)
            {
                this.minRotation = 0f;
                this.maxRotation = 0.25f;
                this.rotation = this.maxRotation;
            }
            else if (newAnimation == 1 || newAnimation == 19)
            {
                this.minRotation = -0.3f;
                this.maxRotation = 0.05f;
                this.rotation = this.maxRotation;
            }
            else if (newAnimation == 4 || newAnimation == 8 || newAnimation == 16)
            {
                this.minRotation = -0.099064894f;
                this.maxRotation = 0.150936067f;
                this.rotation = this.maxRotation;
            }
            else if (newAnimation == 12)
            {
                this.minRotation = -0.203784645f;
                this.maxRotation = 0.0462163165f;
                this.rotation = this.maxRotation;
            }
            else if (newAnimation == 21 || newAnimation == 22 || newAnimation == 23)
            {
                this.minRotation = 0f;
                this.maxRotation = 0f;
                this.rotation = this.maxRotation;
            }
            else
            {
                this.minRotation = 0.25f;
                this.maxRotation = 0.5f;
                this.rotation = this.maxRotation;
            }
        }
    }
}