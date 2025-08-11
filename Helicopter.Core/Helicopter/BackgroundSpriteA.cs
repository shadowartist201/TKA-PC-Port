using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Helicopter.Core
{
	internal class BackgroundSpriteA
	{
		private PositionBehavior positionBehavior;

		private Vector2 position;

		private Vector2 origin;

		private float rotation;

		private Vector2 velocity;

		private Vector2 acceleration;

		protected float a = 50f;

		protected float b = 3f;

		protected Vector2 actualPosition;

		private float speed = 1f;

		private float radius = 500f;

		private Vector2 center = new Vector2(640f, 970f);

		private float movementTimer;

		private float movementTime;

		private Rectangle frameInfo;

		private int currentFrame;

		private int numFrames;

		private int framesX;

		private int framesY;

		private float frameTimer;

		private float frameTime;

		private bool animateBackToFirst = false;

		private int animateBackToFirstDirection = 1;

		private bool flipped = false;

		private float minPositionX;

		private float maxPositionX;

		private float deploymentTimer;

		private float deploymentTime;

		private float minDeploymentTime;

		private float maxDeploymentTime;

		private float minPositionY;

		private float maxPositionY;

		private float minVelocityX;

		private float maxVelocityX;

		private void SetSpriteLinear(Vector2 acceleration_, float minDeploymentTime_, float maxDeploymentTime_, float minPositionY_, float maxPositionY_, float minVelocityX_, float maxVelocityX_, Rectangle frameInfo_, int numFrames_, int framesX_, int framesY_, float frameTime_, bool flipped_)
		{
			this.animateBackToFirst = false;
			this.animateBackToFirstDirection = 1;
			frameInfo_ = new Rectangle(frameInfo_.X, frameInfo_.Y, frameInfo_.Width / framesX_, frameInfo_.Height / framesY_);
			this.positionBehavior = PositionBehavior.Linear;
			this.frameInfo = frameInfo_;
			this.currentFrame = 0;
			this.numFrames = numFrames_;
			this.framesX = framesX_;
			this.framesY = framesY_;
			this.frameTimer = 0f;
			this.frameTime = frameTime_;
			this.flipped = flipped_;
			this.rotation = 0f;
			this.origin = new Vector2(frameInfo_.Width / 2, frameInfo_.Height / 2);
			this.acceleration = acceleration_;
			this.minPositionX = 0f - this.origin.X;
			this.maxPositionX = 1280f + this.origin.X;
			this.deploymentTimer = 0f;
			this.minDeploymentTime = minDeploymentTime_;
			this.maxDeploymentTime = maxDeploymentTime_;
			this.minPositionY = minPositionY_ + this.origin.Y;
			this.maxPositionY = maxPositionY_ - this.origin.Y;
			this.minVelocityX = minVelocityX_;
			this.maxVelocityX = maxVelocityX_;
			this.DeployLinear();
		}

		private void SetSpriteLinear(Vector2 acceleration_, float minDeploymentTime_, float maxDeploymentTime_, float minPositionY_, float maxPositionY_, float minVelocityX_, float maxVelocityX_, Rectangle frameInfo_, int numFrames_, float frameTime_)
		{
			this.animateBackToFirst = false;
			this.animateBackToFirstDirection = 1;
			this.positionBehavior = PositionBehavior.Linear;
			this.frameInfo = frameInfo_;
			this.currentFrame = 0;
			this.numFrames = numFrames_;
			this.framesX = numFrames_;
			this.framesY = 1;
			this.frameTimer = 0f;
			this.frameTime = frameTime_;
			this.flipped = false;
			this.rotation = 0f;
			this.origin = new Vector2(frameInfo_.Width / 2, frameInfo_.Height / 2);
			this.acceleration = acceleration_;
			this.minPositionX = 0f - this.origin.X;
			this.maxPositionX = 1280f + this.origin.X;
			this.deploymentTimer = 0f;
			this.minDeploymentTime = minDeploymentTime_;
			this.maxDeploymentTime = maxDeploymentTime_;
			this.minPositionY = minPositionY_ + this.origin.Y;
			this.maxPositionY = maxPositionY_ - this.origin.Y;
			this.minVelocityX = minVelocityX_;
			this.maxVelocityX = maxVelocityX_;
			this.DeployLinear();
		}

		private void SetSpriteSinusoidal(Vector2 acceleration_, float minDeploymentTime_, float maxDeploymentTime_, float minPositionY_, float maxPositionY_, float minVelocityX_, float maxVelocityX_, Rectangle frameInfo_, int numFrames_, float frameTime_, float a_, float b_)
		{
			this.a = a_;
			this.b = b_;
			this.SetSpriteLinear(acceleration_, minDeploymentTime_, maxDeploymentTime_, minPositionY_, maxPositionY_, minVelocityX_, maxVelocityX_, frameInfo_, numFrames_, frameTime_);
			this.positionBehavior = PositionBehavior.Sinusoidal;
		}

		private void SetSpriteCircular(Vector2 center_, float radius_, float speed_, float movementTime_, float minDeploymentTime_, float maxDeploymentTime_, Rectangle frameInfo_, int numFrames_, float frameTime_)
		{
			this.animateBackToFirst = false;
			this.animateBackToFirstDirection = 1;
			this.positionBehavior = PositionBehavior.Circular;
			this.frameInfo = frameInfo_;
			this.currentFrame = 0;
			this.numFrames = numFrames_;
			this.framesX = numFrames_;
			this.framesY = 1;
			this.frameTimer = 0f;
			this.frameTime = frameTime_;
			this.flipped = false;
			this.rotation = 0f;
			this.origin = new Vector2(frameInfo_.Width / 2, frameInfo_.Height / 2);
			this.deploymentTimer = 0f;
			this.minDeploymentTime = minDeploymentTime_;
			this.maxDeploymentTime = maxDeploymentTime_;
			this.speed = speed_;
			this.radius = radius_;
			this.center = center_;
			this.movementTime = movementTime_;
			this.DeployCircular();
		}

		public void Update(float dt)
		{
			this.frameTimer += dt;
			if (this.frameTimer > this.frameTime)
			{
				if (this.animateBackToFirst)
				{
					if (this.currentFrame == this.numFrames - 1)
					{
						this.animateBackToFirstDirection = -1;
					}
					if (this.currentFrame == 0)
					{
						this.animateBackToFirstDirection = 1;
					}
					this.currentFrame += this.animateBackToFirstDirection;
					this.frameTimer = 0f;
				}
				else
				{
					this.currentFrame = (this.currentFrame + 1) % this.numFrames;
					this.frameTimer = 0f;
				}
			}
			if (this.positionBehavior == PositionBehavior.Linear || this.positionBehavior == PositionBehavior.Sinusoidal)
			{
				if (this.position.X < this.minPositionX || this.position.X > this.maxPositionX)
				{
					this.deploymentTimer += dt;
					if (this.deploymentTimer > this.deploymentTime)
					{
						this.DeployLinear();
					}
				}
				else
				{
					this.UpdateMovement(dt);
				}
			}
			if (this.positionBehavior != PositionBehavior.Circular)
			{
				return;
			}
			if (Math.Abs(this.movementTimer) > this.movementTime)
			{
				this.deploymentTimer += dt;
				if (this.deploymentTimer > this.deploymentTime)
				{
					this.DeployCircular();
					this.UpdateMovement(dt);
				}
			}
			else
			{
				this.UpdateMovement(dt);
			}
		}

		private void DeployLinear()
		{
			this.movementTimer = 0f;
			this.position.Y = Global.RandomBetween(this.minPositionY, this.maxPositionY);
			this.velocity = new Vector2((float)(Global.Random.Next(-1, 1) * 2 + 1) * Global.RandomBetween(this.minVelocityX, this.maxVelocityX), 0f);
			if (Math.Abs(this.velocity.X) < Global.mountainVelocity)
			{
				this.position.X = this.maxPositionX;
			}
			else if (Math.Sign(this.velocity.X) == 1)
			{
				this.position.X = this.minPositionX;
			}
			else
			{
				this.position.X = this.maxPositionX;
			}
			this.deploymentTimer = 0f;
			this.deploymentTime = Global.RandomBetween(this.minDeploymentTime, this.maxDeploymentTime);
		}

		private void DeployCircular()
		{
			this.deploymentTimer = 0f;
			this.movementTimer = 0f;
			this.deploymentTime = Global.RandomBetween(this.minDeploymentTime, this.maxDeploymentTime);
		}

		private void UpdateMovement(float dt)
		{
			switch (this.positionBehavior)
			{
			case PositionBehavior.Linear:
				this.MoveLinear(dt);
				break;
			case PositionBehavior.Sinusoidal:
				this.MoveSinusoidal(dt);
				break;
			case PositionBehavior.Circular:
				this.MoveCircle(dt);
				break;
			}
		}

		private void MoveCircle(float dt)
		{
			this.movementTimer -= this.speed * dt;
			this.position = this.center + this.radius * new Vector2(0f - (float)Math.Cos(this.movementTimer), (float)Math.Sin(this.movementTimer));
			this.rotation = 0f - this.movementTimer - (float)Math.PI / 2f;
		}

		private void MoveSinusoidal(float dt)
		{
			this.movementTimer += dt;
			float num = this.velocity.Length() * dt;
			float num2 = this.a * (float)Math.Cos(this.b * this.movementTimer);
			float num3 = (float)Math.Asin(this.velocity.Y / this.velocity.Length());
			Vector2 vector = new Vector2(num2 * this.velocity.Y / this.velocity.Length(), num2 * this.velocity.X / this.velocity.Length());
			this.position += new Vector2(this.velocity.X - Global.mountainVelocity, this.velocity.Y) * dt;
			this.actualPosition = this.position + vector;
		}

		private void MoveLinear(float dt)
		{
			this.velocity += this.acceleration * dt;
			this.position += new Vector2(this.velocity.X - Global.mountainVelocity, this.velocity.Y) * dt;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			switch (this.positionBehavior)
			{
			case PositionBehavior.Sinusoidal:
				if (this.velocity.X > 0f)
				{
					if (this.flipped)
					{
						this.DrawAt(spriteBatch, this.actualPosition, SpriteEffects.FlipHorizontally);
					}
					else
					{
						this.DrawAt(spriteBatch, this.actualPosition, SpriteEffects.None);
					}
				}
				else if (this.flipped)
				{
					this.DrawAt(spriteBatch, this.actualPosition, SpriteEffects.None);
				}
				else
				{
					this.DrawAt(spriteBatch, this.actualPosition, SpriteEffects.FlipHorizontally);
				}
				break;
			case PositionBehavior.Linear:
				if (this.velocity.X > 0f)
				{
					if (this.flipped)
					{
						this.DrawAt(spriteBatch, this.position, SpriteEffects.FlipHorizontally);
					}
					else
					{
						this.DrawAt(spriteBatch, this.position, SpriteEffects.None);
					}
				}
				else if (this.flipped)
				{
					this.DrawAt(spriteBatch, this.position, SpriteEffects.None);
				}
				else
				{
					this.DrawAt(spriteBatch, this.position, SpriteEffects.FlipHorizontally);
				}
				break;
			case PositionBehavior.Circular:
				this.DrawAt(spriteBatch, this.position, SpriteEffects.None);
				break;
			}
		}

		private void DrawAt(SpriteBatch spriteBatch, Vector2 position_, SpriteEffects spriteEffect_)
		{
			spriteBatch.Draw(Global.backgroundSpritesTexture, position_, (Rectangle?)new Rectangle(this.frameInfo.X + this.currentFrame % this.framesX * this.frameInfo.Width, this.frameInfo.Y + this.currentFrame / this.framesX * this.frameInfo.Height, this.frameInfo.Width, this.frameInfo.Height), Color.White, this.rotation, this.origin, 1f, spriteEffect_, 0f);
		}

		public void Reset()
		{
			switch (this.positionBehavior)
			{
			case PositionBehavior.Linear:
				this.DeployLinear();
				break;
			case PositionBehavior.Sinusoidal:
				this.DeployLinear();
				break;
			case PositionBehavior.Circular:
				this.DeployCircular();
				break;
			}
		}

		//dream pack

		public void SetToUnicorn()
		{
			this.SetSpriteLinear(Vector2.Zero, 10f, 20f, 349f, 507f, 759f, 759f, new Rectangle(0, 300, 231, 158), 12, 0.05f);
		}

		public void SetToBird()
		{
			this.SetSpriteLinear(Vector2.Zero, 3f, 6f, 30f, 270f, 500f, 600f, new Rectangle(0, 0, 92, 80), 4, 0.15f);
		}

		public void SetToPartyBoat()
		{
			this.SetSpriteLinear(Vector2.Zero, 3f, 6f, 400f, 560f, 400f, 500f, new Rectangle(0, 100, 371, 192), 6, Global.BPM);
		}

		public void SetToSpeedBoat()
		{
			this.SetSpriteSinusoidal(Vector2.Zero, 3f, 6f, 520f, 610f, 400f, 500f, new Rectangle(546, 0, 181, 60), 1, 1f, 50f, 3f);
		}

		public void SetToUFO()
		{
			this.SetSpriteSinusoidal(Vector2.Zero, 3f, 6f, 50f, 280f, 500f, 600f, new Rectangle(729, 0, 169, 88), 4, 0.25f, 50f, 3f);
		}

		public void SetToDolphin()
		{
			this.SetSpriteCircular(new Vector2(640f, 970f), 500f, 1f, (float)Math.PI * 2f, 15f, 30f, new Rectangle(370, 0, 174, 93), 1, 1f);
		}

		public void SetToNarwal()
		{
			this.SetSpriteCircular(new Vector2(640f, 970f), 500f, -1f, (float)Math.PI * 2f, 15f, 30f, new Rectangle(2454, 100, 172, 82), 1, 1f);
		}

		public void SetToBlimp()
		{
			this.SetSpriteLinear(Vector2.Zero, 3f, 6f, 30f, 270f, 500f, 600f, new Rectangle(1282, 460, 185, 152), 4, 0.15f);
		}

        public void SetToAngel()
		{
			this.SetSpriteLinear(Vector2.Zero, 3f, 6f, 0f, 720f, 500f, 600f, new Rectangle(1908, 614, 204, 167), 4, 0.1f);
		}

		public void SetToCupid()
		{
			this.SetSpriteLinear(Vector2.Zero, 3f, 6f, 0f, 720f, 500f, 600f, new Rectangle(1282, 783, 165, 167), 4, 0.1f);
		}

		public void SetToButterfly()
		{
			this.SetSpriteLinear(Vector2.Zero, 3f, 6f, 0f, 720f, 500f, 600f, new Rectangle(2024, 460, 113, 124), 8, 0.1f);
		}

		public void SetToPterodactyl()
		{
			this.SetSpriteLinear(Vector2.Zero, 3f, 6f, 0f, 720f, 500f, 600f, new Rectangle(1282, 614, 156, 122), 4, 0.1f);
		}

		public void SetToAlligator()
		{
			this.SetSpriteLinear(Vector2.Zero, 3f, 6f, 670f, 730f, 700f, 900f, new Rectangle(1407, 0, 220, 60), 6, 0.1f);
		}

		public void SetToHeart()
		{
			this.SetSpriteLinear(Vector2.Zero, 3f, 6f, 0f, 720f, 500f, 600f, new Rectangle(1036, 1442, 147, 142), 6, 0.1f);
		}

		public void SetToDragon()
		{
			this.SetSpriteLinear(Vector2.Zero, 3f, 6f, 0f, 720f, 500f, 600f, new Rectangle(0, 1763, 253, 200), 11, 0.05f);
		}

		public void SetToShark()
		{
			this.SetSpriteCircular(new Vector2(640f, 970f), 500f, 1f, (float)Math.PI * 2f, 7.5f, 15f, new Rectangle(0, 2167, 279, 187), 11, 0.05f);
		}

		public void SetToDemon()
		{
			this.SetSpriteLinear(Vector2.Zero, 3f, 6f, 520f, 720f, 500f, 600f, new Rectangle(0, 1965, 233, 200), 11, 0.05f);
		}

		public void SetToDemonTwo()
		{
			this.SetSpriteLinear(Vector2.Zero, 3f, 6f, 0f, 720f, 500f, 600f, new Rectangle(1036, 1038, 167, 200), 11, 0.05f);
		}

		public void SetToGargoyle()
		{
			this.SetSpriteLinear(Vector2.Zero, 3f, 6f, 0f, 720f, 500f, 600f, new Rectangle(1036, 1240, 149, 200), 11, 0.05f);
		}

		public void SetToPheonix()
		{
			this.SetSpriteLinear(Vector2.Zero, 3f, 6f, 0f, 720f, 500f, 600f, new Rectangle(0, 2356, 209, 200), 11, 0.05f);
		}

		public void SetToSubmarine()
		{
			this.SetSpriteLinear(Vector2.Zero, 3f, 6f, 0f, 720f, 500f, 600f, new Rectangle(0, 2771, 279, 194), 11, 0.05f);
		}

		public void SetToGiraffe()
		{
			this.SetSpriteLinear(Vector2.Zero, 3f, 6f, 470f, 720f, 500f, 600f, new Rectangle(0, 2967, 128, 250), 11, 0.05f);
		}

		public void SetToHotDog()
		{
			this.SetSpriteLinear(Vector2.Zero, 3f, 6f, 0f, 400f, 500f, 600f, new Rectangle(0, 0, 110, 94), 4, 0.1f);
		}

		public void SetToFish()
		{
			this.SetSpriteLinear(Vector2.Zero, 3f, 6f, 498f, 617f, 50f, 100f, new Rectangle(450, 0, 158, 119), 6, 0.05f);
		}

		public void SetToDrumsticks()
		{
			this.SetSpriteLinear(Vector2.Zero, 3f, 6f, 0f, 400f, 500f, 600f, new Rectangle(1400, 0, 156, 144), 4, 0.1f);
		}

		public void SetToMeat()
		{
			this.SetSpriteLinear(Vector2.Zero, 3f, 6f, 521f, 720f, 500f, 600f, new Rectangle(2030, 0, 161, 119), 6, 0.1f);
		}

		public void SetToPig()
		{
			this.SetSpriteLinear(Vector2.Zero, 3f, 6f, 0f, 400f, 500f, 600f, new Rectangle(0, 120, 158, 89), 6, 0.1f);
		}

		public void SetToBacon()
		{
			this.SetSpriteLinear(Vector2.Zero, 3f, 6f, 0f, 400f, 500f, 600f, new Rectangle(950, 146, 193, 51), 4, 0.1f);
		}

		public void SetToChicken()
		{
			this.SetSpriteLinear(Vector2.Zero, 3f, 6f, 503f, 720f, 500f, 600f, new Rectangle(1724, 146, 114, 137), 6, 0.1f);
		}

		public void SetToDuck()
		{
			this.SetSpriteLinear(Vector2.Zero, 3f, 6f, 0f, 400f, 500f, 600f, new Rectangle(0, 210, 173, 81), 6, 0.1f);
		}

		public void SetToSausage()
		{
			this.SetSpriteLinear(Vector2.Zero, 3f, 6f, 0f, 400f, 500f, 600f, new Rectangle(1040, 210, 132, 107), 4, 0.1f);
		}

		public void SetToYangCircle()
		{
			this.SetSpriteLinear(Vector2.Zero, 4f, 6f, 0f, 500f, 500f, 600f, new Rectangle(0, 0, 480, 468), 9, 3, 3, 0.05f, flipped_: false);
		}

		public void SetToYangHeart()
		{
			this.SetSpriteLinear(Vector2.Zero, 4f, 6f, 0f, 500f, 500f, 600f, new Rectangle(482, 0, 512, 492), 15, 4, 4, 0.05f, flipped_: false);
		}

		public void SetToBusinessMan()
		{
			this.SetSpriteLinear(Vector2.Zero, 4f, 6f, 504f, 720f, 350f, 450f, new Rectangle(1000, 0, 500, 624), 19, 5, 4, 0.05f, flipped_: true);
		}

		public void SetToMicRobot()
		{
			this.SetSpriteLinear(Vector2.Zero, 4f, 6f, 0f, 500f, 400f, 500f, new Rectangle(1502, 0, 1008, 684), 14, 6, 3, 0.05f, flipped_: true);
		}

		public void SetToPurpleAlien()
		{
			this.SetSpriteLinear(Vector2.Zero, 4f, 6f, 393f, 720f, 350f, 450f, new Rectangle(0, 514, 944, 554), 15, 8, 2, 0.05f, flipped_: false);
		}

		public void SetToSkate()
		{
			this.SetSpriteLinear(Vector2.Zero, 4f, 6f, 469f, 720f, 350f, 450f, new Rectangle(1000, 686, 1016, 764), 27, 8, 4, 0.05f, flipped_: false);
		}

		public void SetToSkullRobot()
		{
			this.SetSpriteLinear(Vector2.Zero, 4f, 6f, 0f, 500f, 400f, 500f, new Rectangle(2018, 686, 840, 1344), 14, 4, 4, 0.05f, flipped_: true);
		}

		public void SetToDarkAlien()
		{
			this.SetSpriteLinear(Vector2.Zero, 4f, 6f, 442f, 720f, 350f, 450f, new Rectangle(0, 1070, 508, 912), 13, 4, 4, 0.05f, flipped_: false);
		}

		public void SetToTongue()
		{
			this.SetSpriteLinear(Vector2.Zero, 4f, 6f, 0f, 500f, 400f, 500f, new Rectangle(1000, 1452, 990, 720), 17, 5, 5, 0.025f, flipped_: true);
			this.animateBackToFirst = true;
		}

		public void SetToGun()
		{
			this.SetSpriteLinear(Vector2.Zero, 4f, 6f, 0f, 500f, 500f, 600f, new Rectangle(0, 2174, 1848, 220), 14, 7, 2, 0.05f, flipped_: true);
		}

		public void SetToPlane()
		{
			this.SetSpriteLinear(Vector2.Zero, 4f, 6f, 0f, 500f, 500f, 600f, new Rectangle(1850, 2174, 141, 136), 1, 1, 1, 0.05f, flipped_: false);
		}

		public void SetToSkull()
		{
			this.SetSpriteLinear(Vector2.Zero, 4f, 6f, -60f, 624f, 400f, 400f, new Rectangle(2512, 0, 506, 684), 1, 1f);
		}

		public void SetToToaster()
		{
            this.SetSpriteLinear(Vector2.Zero, 3f, 6f, 0f, 720f, 500f, 600f, new Rectangle(0, 0, 111, 75), 6, 0.05f);
        }

		public void SetToBee()
		{
            this.SetSpriteLinear(Vector2.Zero, 3f, 6f, 0f, 720f, 500f, 600f, new Rectangle(0, 77, 64, 56), 6, 0.05f);
        }

		public void SetToStar()
		{
            this.SetSpriteLinear(Vector2.Zero, 3f, 6f, 0f, 720f, 500f, 600f, new Rectangle(0, 135, 62, 58), 6, 0.05f);
        }

		public void SetToSatelite()
		{
            this.SetSpriteLinear(Vector2.Zero, 3f, 6f, 0f, 720f, 500f, 600f, new Rectangle(0, 219, 148, 84), 6, 0.05f);
        }

		public void SetToNyanHeart()
		{
            this.SetSpriteLinear(Vector2.Zero, 3f, 6f, 0f, 720f, 500f, 600f, new Rectangle(386, 77, 34, 32), 6, 0.05f);
        }

		public void SetToNyanUFO()
		{
            this.SetSpriteLinear(Vector2.Zero, 3f, 6f, 0f, 720f, 500f, 600f, new Rectangle(386, 173, 70, 44), 6, 0.05f);
        }

		public void SetToHead()
		{
            this.SetSpriteLinear(Vector2.Zero, 3f, 6f, 0f, 720f, 500f, 600f, new Rectangle(386, 111, 66, 60), 6, 0.05f);
        }

		public void SetToBurger()
		{
            this.SetSpriteLinear(Vector2.Zero, 3f, 6f, 0f, 720f, 500f, 600f, new Rectangle(668, 0, 64, 44), 5, 0.05f);
        }

		public void SetToPizza()
		{
            this.SetSpriteLinear(Vector2.Zero, 3f, 6f, 0f, 720f, 500f, 600f, new Rectangle(668, 46, 54, 52), 6, 0.05f);
        }

		public void SetToMoon()
		{
            this.SetSpriteLinear(Vector2.Zero, 4f, 6f, 0f, 0f, 50f, 50f, new Rectangle(0, 304, 600, 375), 1, 1f);
        }
		public void SetToInactive()
		{
			this.SetSpriteLinear(Vector2.Zero, 100f, 100f, 0f, 0f, 0f, 0f, new Rectangle(0, 0, 0, 0), 1, 1f);
		}
	}
}
