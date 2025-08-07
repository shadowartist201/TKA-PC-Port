using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.MediaFoundation;

namespace Helicopter
{
	public class Tunnel
	{
		private bool visible_ = true;

		private TunnelEffects tunnelEffects_;

		private Color normalColor;

		private Color BColor;

		private Color WColor;

		public float velocity = 476f; //speed of the tunnel

		private float colorRate_ = 1080f;
        private float colorHue_ = 0f;

        private Vector2[] vertices; //tunnel points for top line, 40 at once

		private Vector2[] vertices2; //symbol positions, 3 symbols at once

        private int[] animFrames;

		private int animOffset;

		private float animTimer = 0f;

		private float animTime = 0.04166666f; //1/24

		private int[] symbolIndexes;

		private Vector2[][] symbolLineInfo;

		private Rectangle[][] symbolCollisionRects;

		private int firstIndex;

		private int lastIndex;

		private int width; //width between tunnel points, usually 32

        private int width2; //width between symbol points, usually 100

		private int lineWidth = 3;

		private int height = 500; //tunnel thickness

		private int minHeight = 350; //min tunnel thickness

		private float a; //amplitude of sine wave

        private float b; //frequency of sine wave

        private float c; //period of sine wave

        private float d; //vertical shift of sine wave

        private float x;

		private float maxSlope = 2f;

		private TunnelEffect effect;

		private int colorIndex;

		private float t2 = 0f;

		private float t;

		public float rainbowFactor = 360f / 1280f;

		public float rainbowOffset = 0f;

		public bool isNyanRainbow = false;

		public Tunnel(int _num, int _num2)
		{
			this.tunnelEffects_ = new TunnelEffects();
			this.Reset(_num, _num2);
			this.animFrames = new int[_num + 1];
			for (int i = 0; i <= _num; i++)
			{
				this.animFrames[i] = Global.Random.Next(18);
			}
			this.symbolIndexes = new int[3];
			for (int i = 0; i < this.symbolIndexes.Length; i++)
			{
				this.symbolIndexes[i] = Global.Random.Next(10);
			}
			this.symbolLineInfo = new Vector2[10][]
			{
				new Vector2[16] //heart
				{
					new Vector2(50f, 88f),
					new Vector2(32f, 69f),
					new Vector2(13f, 57f),
					new Vector2(6f, 45f),
					new Vector2(6f, 29f),
					new Vector2(14f, 15f),
					new Vector2(28f, 11f),
					new Vector2(42f, 16f),
					new Vector2(50f, 27f),
					new Vector2(58f, 16f),
					new Vector2(72f, 11f),
					new Vector2(86f, 15f),
					new Vector2(94f, 29f),
					new Vector2(94f, 45f),
					new Vector2(87f, 57f),
					new Vector2(68f, 69f)
				},
				new Vector2[4] //square
				{
					new Vector2(14f, 14f),
					new Vector2(86f, 14f),
					new Vector2(86f, 86f),
					new Vector2(14f, 86f)
				},
				new Vector2[10] //star
				{
					new Vector2(2f, 37f),
					new Vector2(33f, 32f),
					new Vector2(50f, 4f),
					new Vector2(66f, 32f),
					new Vector2(98f, 37f),
					new Vector2(74f, 61f),
					new Vector2(78f, 92f),
					new Vector2(50f, 79f),
					new Vector2(21f, 92f),
					new Vector2(24f, 61f)
				},
				new Vector2[29] //umbrella
				{
					new Vector2(9f, 52f),
					new Vector2(10f, 41f),
					new Vector2(13f, 31f),
					new Vector2(21f, 21f),
					new Vector2(30f, 15f),
					new Vector2(40f, 11f),
					new Vector2(48f, 9f),
					new Vector2(54f, 10f),
					new Vector2(62f, 11f),
					new Vector2(72f, 14f),
					new Vector2(80f, 20f),
					new Vector2(88f, 31f),
					new Vector2(91f, 42f),
					new Vector2(92f, 51f),
					new Vector2(83f, 47f),
					new Vector2(73f, 52f),
					new Vector2(64f, 47f),
					new Vector2(54f, 52f),
					new Vector2(54f, 85f),
					new Vector2(48f, 93f),
					new Vector2(40f, 93f),
					new Vector2(32f, 88f),
					new Vector2(35f, 81f),
					new Vector2(41f, 86f),
					new Vector2(46f, 80f),
					new Vector2(46f, 52f),
					new Vector2(36f, 47f),
					new Vector2(27f, 52f),
					new Vector2(19f, 46f)
				},
				new Vector2[5] //pentagon
				{
					new Vector2(75f, 90f),
					new Vector2(25f, 90f),
					new Vector2(9f, 42f),
					new Vector2(49f, 12f),
					new Vector2(89f, 42f)
				},
				new Vector2[3] //triangle
				{
					new Vector2(6f, 82f),
					new Vector2(50f, 8f),
					new Vector2(94f, 82f)
				},
				new Vector2[7] //arrow
				{
					new Vector2(5f, 25f),
					new Vector2(5f, 75f),
					new Vector2(50f, 75f),
					new Vector2(50f, 95f),
					new Vector2(95f, 50f),
					new Vector2(50f, 5f),
					new Vector2(50f, 25f)
				},
				new Vector2[12] //circle
				{
					new Vector2(10f, 50f),
					new Vector2(15f, 30f),
					new Vector2(30f, 15f),
					new Vector2(50f, 10f),
					new Vector2(70f, 15f),
					new Vector2(85f, 30f),
					new Vector2(90f, 50f),
					new Vector2(85f, 70f),
					new Vector2(70f, 85f),
					new Vector2(50f, 90f),
					new Vector2(30f, 85f),
					new Vector2(15f, 70f)
				},
				new Vector2[6] //hexagon
				{
					new Vector2(5f, 50f),
					new Vector2(28f, 10f),
					new Vector2(74f, 10f),
					new Vector2(95f, 50f),
					new Vector2(74f, 90f),
					new Vector2(28f, 90f)
				},
				new Vector2[16] //moon
				{
					new Vector2(9f, 69f),
					new Vector2(22f, 74f),
					new Vector2(39f, 73f),
					new Vector2(57f, 58f),
					new Vector2(62f, 40f),
					new Vector2(57f, 22f),
					new Vector2(40f, 7f),
					new Vector2(57f, 6f),
					new Vector2(77f, 15f),
					new Vector2(91f, 33f),
					new Vector2(93f, 58f),
					new Vector2(87f, 76f),
					new Vector2(69f, 91f),
					new Vector2(49f, 95f),
					new Vector2(28f, 90f),
					new Vector2(15f, 80f)
				}
			};
			this.symbolCollisionRects = new Rectangle[10][]
			{
				new Rectangle[8] //heart
				{
					new Rectangle(17, 12, 22, 7),
					new Rectangle(10, 19, 34, 28),
					new Rectangle(21, 47, 60, 15),
					new Rectangle(31, 62, 41, 12),
					new Rectangle(41, 74, 18, 11),
					new Rectangle(44, 22, 12, 25),
					new Rectangle(56, 18, 34, 29),
					new Rectangle(62, 12, 22, 6)
				},
				new Rectangle[1] //square
				{
					new Rectangle(14, 14, 72, 72)
				},
				new Rectangle[9] //star
				{
					new Rectangle(23, 82, 13, 8),
					new Rectangle(63, 82, 14, 8),
					new Rectangle(25, 54, 50, 28),
					new Rectangle(17, 47, 66, 7),
					new Rectangle(10, 39, 80, 8),
					new Rectangle(30, 35, 43, 4),
					new Rectangle(37, 25, 8, 10),
					new Rectangle(45, 12, 10, 23),
					new Rectangle(55, 25, 8, 10)
				},
				new Rectangle[7] //umbrella
				{
					new Rectangle(42, 11, 22, 4),
					new Rectangle(37, 15, 38, 5),
					new Rectangle(24, 20, 54, 7),
					new Rectangle(19, 27, 65, 9),
					new Rectangle(14, 36, 75, 13),
					new Rectangle(44, 49, 12, 39),
					new Rectangle(34, 83, 10, 11)
				},
				new Rectangle[5] //pentagon
				{
					new Rectangle(15, 39, 69, 19),
					new Rectangle(19, 59, 61, 13),
					new Rectangle(26, 73, 48, 16),
					new Rectangle(29, 28, 40, 10),
					new Rectangle(41, 19, 6, 8)
				},
				new Rectangle[8] //triangle
				{
					new Rectangle(15, 75, 71, 6),
					new Rectangle(20, 64, 61, 11),
					new Rectangle(26, 54, 49, 10),
					new Rectangle(31, 45, 39, 9),
					new Rectangle(36, 35, 30, 10),
					new Rectangle(39, 26, 24, 9),
					new Rectangle(44, 17, 14, 9),
					new Rectangle(48, 11, 6, 6)
				},
				new Rectangle[7] //arrow
				{
					new Rectangle(53, 85, 2, 6),
					new Rectangle(52, 75, 9, 9),
					new Rectangle(79, 42, 8, 16),
					new Rectangle(73, 34, 5, 33),
					new Rectangle(52, 9, 2, 7),
					new Rectangle(6, 26, 66, 48),
					new Rectangle(52, 16, 9, 9)
				},
				new Rectangle[7] //circle
				{
					new Rectangle(11, 45, 78, 12),
					new Rectangle(14, 33, 72, 12),
					new Rectangle(21, 24, 58, 12),
					new Rectangle(38, 12, 24, 12),
					new Rectangle(14, 57, 72, 12),
					new Rectangle(21, 69, 58, 12),
					new Rectangle(38, 81, 24, 10)
				},
				new Rectangle[7] //hexagon
				{
					new Rectangle(11, 42, 6, 15),
					new Rectangle(17, 30, 6, 40),
					new Rectangle(23, 27, 7, 57),
					new Rectangle(30, 13, 43, 74),
					new Rectangle(73, 22, 7, 56),
					new Rectangle(80, 31, 6, 36),
					new Rectangle(86, 44, 5, 12)
				},
				new Rectangle[9] //moon
				{
					new Rectangle(45, 8, 6, 7),
					new Rectangle(51, 10, 9, 12),
					new Rectangle(60, 12, 12, 60),
					new Rectangle(72, 19, 9, 13),
					new Rectangle(72, 32, 16, 41),
					new Rectangle(48, 61, 12, 11),
					new Rectangle(14, 72, 68, 8),
					new Rectangle(19, 80, 54, 8),
					new Rectangle(32, 88, 29, 5)
				}
			};
		}

		public void Update(float dt, Helicopter helicopter, ScoreSystem scoreSystem, int stageIndex, int catIndex)
		{
			this.rainbowOffset += 1280 * 2 * dt;
			if (this.rainbowOffset > 1280)
			{
				this.rainbowOffset -= 1280;
            }
			this.t2 += dt;
			this.animTimer += dt;
            if (this.animTimer > this.animTime) //if animTimer > 1/24
			{
				this.animTimer = 0f;
				this.animOffset = (this.animOffset + 1) % 18; //increment by 1, clamp to under 18
			}
			if (!this.IsOn() && !helicopter.IsDead())
			{
				this.Reset(40, 3);
			}
			if (this.IsOn())
			{
				this.Collides(helicopter, scoreSystem, stageIndex, catIndex);
				this.t += dt;
				if (this.t > 0.5f)
				{
					if (this.height > this.minHeight) //if height > 350
					{
						this.height--; //shrink the tunnel
					}
					this.t = 0f;
				}
				this.Shift(dt);
			}
			switch (this.effect)
			{
			case TunnelEffect.Normal:
				Global.tunnelColor = this.normalColor;
				this.isNyanRainbow = false;
				break;
			case TunnelEffect.BW:
				this.isNyanRainbow = false;
				if (this.t2 > Global.BPM) //swap between two colors according to bpm
				{
					if (Global.tunnelColor == this.WColor)
					{
						Global.tunnelColor = this.BColor;
					}
					else
					{
						Global.tunnelColor = this.WColor;
					}
					this.t2 = 0f;
				}
				break;
			case TunnelEffect.BWDouble:
                this.isNyanRainbow = false;
                if (this.t2 > Global.BPM / 2f) //swap between two colors 2x faster
				{
					if (Global.tunnelColor == this.WColor)
					{
						Global.tunnelColor = this.BColor;
					}
					else
					{
						Global.tunnelColor = this.WColor;
					}
					this.t2 = 0f;
				}
				break;
			case TunnelEffect.BWQuad:
				this.isNyanRainbow = false;
				if (this.t2 > Global.BPM / 4f) //swap between two colors 4x faster
				{
					if (Global.tunnelColor == this.WColor)
					{
						Global.tunnelColor = this.BColor;
					}
					else
					{
						Global.tunnelColor = this.WColor;
					}
					this.t2 = 0f;
				}
				break;
			case TunnelEffect.Rainbow: //rainbow
				this.colorIndex = (this.colorIndex + 1) % 6;
                this.isNyanRainbow = false;
                Global.tunnelColor = Global.rainbowColors[this.colorIndex];
				break;
			case TunnelEffect.RainbowPunctuated: //rainbow according to bpm
                this.isNyanRainbow = false;
                if (this.t2 > Global.BPM)
				{
					this.colorIndex = (this.colorIndex + 3) % 8;
					Global.tunnelColor = Global.rainbowColors8[this.colorIndex];
					this.t2 = 0f;
				}
				break;
			case TunnelEffect.Disappear: //flicker according to bpm
				if (this.t2 > Global.BPM)
				{
					this.visible_ = !this.visible_;
					this.t2 = 0f;
				}
				break;
			case TunnelEffect.Nyan:
					/*colorHue_ += colorRate_ * dt;
					if (colorHue_ < 0f)
					{
						colorHue_ = 0f;
						colorRate_ = 0f - colorRate_;
					}
					else if (colorHue_ > 360f)
					{
						colorHue_ = 360f;
						colorRate_ = 0f - colorRate_;
					}
					Global.tunnelColor = Camera.GetColor(colorHue_ % 360f);*/
					this.isNyanRainbow = true;
                break;
            }
			this.tunnelEffects_.UpdateTunnel(dt, this.vertices, this.width, this.height, this.velocity);
			this.tunnelEffects_.UpdateSymbols(dt, this.vertices2, this.symbolLineInfo[this.symbolIndexes[0]], this.symbolLineInfo[this.symbolIndexes[1]], this.symbolLineInfo[this.symbolIndexes[2]], this.velocity);
		}

		private void Shift(float dt)
		{
			for (int i = 0; i < this.vertices.Length; i++)
			{
				this.vertices[i].X -= this.velocity * dt; //move all vertices left over time
			}
			for (int j = 0; j < this.vertices2.Length; j++)
			{
				this.vertices2[j].X -= this.velocity * dt; //move all symbols left over time
				if (this.vertices2[j].X < (float)(-this.width2)) //if symbol is off screen to the left
				{
					this.vertices2[j].X = Math.Max(this.vertices2[(j + 1) % 3].X, this.vertices2[(j + 2) % 3].X) + 500f; //grab furthest symbol?
					this.symbolIndexes[j] = Global.Random.Next(10); //choose random symbol shape
					this.vertices2[j].Y = this.GenerateBlock(this.vertices[this.lastIndex].Y); //generate symbol height?
				}
			}
			if (this.vertices[this.firstIndex].X < (float)(-this.width)) //if first vertice is off screen to the left
			{
				this.vertices[this.firstIndex].X = this.vertices[this.lastIndex].X + (float)this.width; //wrap around
				this.vertices[this.firstIndex].Y = this.GenerateHeight(this.vertices[this.lastIndex].Y, (float)(648 - this.height) - this.vertices[this.lastIndex].Y); //generate height of vertice
				this.lastIndex = this.firstIndex; //lastIndex is the index of the last vertex, which is the first vertex that was shifted off screen
                this.firstIndex = (this.firstIndex + 1) % this.vertices.Length; //increment firstIndex, wrap around if needed
            }
		}

		private float GenerateHeight(float dH1, float dH2) //dH2 is usually smaller than dH1
		{
			this.x += this.width;
			if (this.x > this.c)
			{
				this.x = 0f;
				this.d = dH1; //where d is vertical shift
				this.c = Global.Random.Next(3, 640 / this.width) * this.width; //width usually 32, random(3,20)*32 = 96~640
                this.b = (float)Math.PI / this.c; //Assuming 96~640, then 0.004908~0.032724
                float num = 0f - dH1 + 72f; //usually negative
				this.a = Global.RandomBetween((num > (0f - this.maxSlope) / this.b) ? num : ((0f - this.maxSlope) / this.b), (dH2 < this.maxSlope / this.b) ? dH2 : (this.maxSlope / this.b)); // randomize amplitude of sine wave
				//if (num > -2) then num else -2/b, if (dH2 < 2/b) then dH2 else 2/b
            }
            return this.d + this.a * ((float)Math.Cos((double)(this.b * this.x) + Math.PI) + 1f) / 2f; // sine wave formula
        }

		private float GenerateBlock(float dH1) //dH1 defaults to 72 but can go higher up to like 300
		{
			return dH1 + Global.RandomBetween(0f, this.height - 100); //if 72, turns out to be 322~472
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			if (this.visible_)
			{
				this.tunnelEffects_.Draw(spriteBatch, this);
			}
		}

		private void DrawRectangle(SpriteBatch spriteBatch, Vector2 position_, Rectangle rect_)
		{
			spriteBatch.Draw(Global.pixel, new Rectangle((int)position_.X + rect_.X, (int)position_.Y + rect_.Y, rect_.Width, this.lineWidth), Global.tunnelColor);
			spriteBatch.Draw(Global.pixel, new Rectangle((int)position_.X + rect_.X, (int)position_.Y + rect_.Y + rect_.Height, rect_.Width, this.lineWidth), Global.tunnelColor);
			spriteBatch.Draw(Global.pixel, new Rectangle((int)position_.X + rect_.X, (int)position_.Y + rect_.Y, this.lineWidth, rect_.Height), Global.tunnelColor);
			spriteBatch.Draw(Global.pixel, new Rectangle((int)position_.X + rect_.X + rect_.Width, (int)position_.Y + rect_.Y, this.lineWidth, rect_.Height), Global.tunnelColor);
		}

		public void Reset(int _num, int _num2) //initial reset to 40,3
		{
			this.t = 0f;
			this.height = 500;
			this.width = 1280 / _num; //width = 32
			this.width2 = 100;
			this.vertices = new Vector2[_num + 1]; //41 vertices
			for (int i = 0; i <= _num; i++)
			{
				ref Vector2 reference = ref this.vertices[i];
				reference = new Vector2(i * this.width, 72f); //i*32,72
			}
			this.firstIndex = 0;
			this.lastIndex = _num; //40
			this.vertices2 = new Vector2[_num2]; //3 vertices, maximum shapes on screen at once?
			for (int j = 0; j < _num2; j++)
			{
				ref Vector2 reference2 = ref this.vertices2[j];
				reference2 = new Vector2(1280 + j * 500, this.GenerateBlock(72f)); //1280+(0/500/1000), 72~472
            }
			this.velocity = 0f;
			this.a = 0f;
			this.b = 0f;
			this.c = 0f;
			this.d = 0f;
			this.x = 0f;
			if (this.effect != TunnelEffect.Disappear)
			{
				this.visible_ = true;
			}
		}

		public void TurnOn()
		{
			this.velocity = 476f;
		}

		public void TurnOff()
		{
			this.velocity = 0f;
		}

		public bool IsOn()
		{
			return this.velocity != 0f;
		}

		public void Set(TunnelEffect tunnelEffect)
		{
			this.effect = tunnelEffect;
			this.t2 = 0f;
			this.visible_ = true;
		}

		public void SetColor(Color normalColor, Color bColor, Color wColor)
		{
			this.normalColor = normalColor;
			this.BColor = bColor;
			this.WColor = wColor;
		}

		private void Collides(Helicopter helicopter, ScoreSystem scoreSystem, int stageIndex, int catIndex)
		{
			if (helicopter.IsDead())
			{
				return;
			}
			for (int i = 0; i < this.vertices2.Length; i++)
			{
				for (int j = 0; j < this.symbolCollisionRects[this.symbolIndexes[i]].Length; j++)
				{
					if (new Rectangle((int)this.vertices2[i].X + this.symbolCollisionRects[this.symbolIndexes[i]][j].X, (int)this.vertices2[i].Y + this.symbolCollisionRects[this.symbolIndexes[i]][j].Y, this.symbolCollisionRects[this.symbolIndexes[i]][j].Width, this.symbolCollisionRects[this.symbolIndexes[i]][j].Height).Intersects(helicopter.CollisionRect))
					{
						helicopter.Kill();
						Global.SetVibrationTemp(on: true);
						this.velocity = 0f;
						scoreSystem.End(stageIndex, catIndex);
						return;
					}
				}
			}
			float num = this.vertices[this.firstIndex].X;
			int num2 = (int)(((float)helicopter.CollisionRect.X - num) / (float)this.width);
			int num3 = (int)(((float)(helicopter.CollisionRect.X + helicopter.CollisionRect.Width) - num) / (float)this.width);
			for (int i = num2; i <= num3; i++)
			{
				if (this.vertices[(this.firstIndex + i) % this.vertices.Length].Y > (float)helicopter.CollisionRect.Y || this.vertices[(this.firstIndex + i) % this.vertices.Length].Y + (float)this.height < (float)(helicopter.CollisionRect.Y + helicopter.CollisionRect.Height))
				{
					helicopter.Kill();
					Global.SetVibrationTemp(on: true);
					this.velocity = 0f;
					scoreSystem.End(stageIndex, catIndex);
					break;
				}
			}
		}
	}
}
