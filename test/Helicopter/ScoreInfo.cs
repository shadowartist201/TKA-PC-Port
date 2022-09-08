using System;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Storage;

namespace Helicopter
{
	public struct ScoreInfo
	{
		public int highScore_;

		public int seaHigh_;

		public int cloudHigh_;

		public int lavaHigh_;

		public int meatHigh_;

		public int ronHigh_;

		public int[] stageIndexes_;

		public int[] catIndexes_;

		public int[] scores_;

		public int HighScore => this.highScore_;

		public bool seaFortyUnlocked => this.seaHigh_ >= 40000;

		public bool seaSixtyUnlocked => this.seaHigh_ >= 60000;

		public bool seaEightyUnlocked => this.seaHigh_ >= 80000;

		public bool cloudFortyUnlocked => this.cloudHigh_ >= 40000;

		public bool cloudSixtyUnlocked => this.cloudHigh_ >= 60000;

		public bool cloudEightyUnlocked => this.cloudHigh_ >= 80000;

		public bool lavaFortyUnlocked => this.lavaHigh_ >= 40000;

		public bool lavaSixtyUnlocked => this.lavaHigh_ >= 60000;

		public bool lavaEightyUnlocked => this.lavaHigh_ >= 80000;

		public bool meatFortyUnlocked => this.meatHigh_ >= 40000;

		public bool meatSixtyUnlocked => this.meatHigh_ >= 60000;

		public bool meatEightyUnlocked => this.meatHigh_ >= 80000;

		public bool ronFortyUnlocked => this.ronHigh_ >= 40000;

		public bool ronSixtyUnlocked => this.ronHigh_ >= 60000;

		public bool ronEightyUnlocked => this.ronHigh_ >= 80000;

		public ScoreInfo(int someInt)
		{
			this.stageIndexes_ = new int[10];
			this.catIndexes_ = new int[10];
			this.scores_ = new int[10];
			this.highScore_ = 0;
			this.seaHigh_ = 0;
			this.cloudHigh_ = 0;
			this.lavaHigh_ = 0;
			this.meatHigh_ = 0;
			this.ronHigh_ = 0;
			this.stageIndexes_[0] = 0;
			this.stageIndexes_[1] = 0;
			this.stageIndexes_[2] = 1;
			this.stageIndexes_[3] = 1;
			this.stageIndexes_[4] = 2;
			this.stageIndexes_[5] = 2;
			this.stageIndexes_[6] = 3;
			this.stageIndexes_[7] = 3;
			this.stageIndexes_[8] = 4;
			this.stageIndexes_[9] = 4;
			this.catIndexes_[0] = 0;
			this.catIndexes_[1] = 1;
			this.catIndexes_[2] = 3;
			this.catIndexes_[3] = 4;
			this.catIndexes_[4] = 6;
			this.catIndexes_[5] = 7;
			this.catIndexes_[6] = 9;
			this.catIndexes_[7] = 10;
			this.catIndexes_[8] = 12;
			this.catIndexes_[9] = 13;
			this.scores_[0] = 0;
			this.scores_[1] = 0;
			this.scores_[2] = 0;
			this.scores_[3] = 0;
			this.scores_[4] = 0;
			this.scores_[5] = 0;
			this.scores_[6] = 0;
			this.scores_[7] = 0;
			this.scores_[8] = 0;
			this.scores_[9] = 0;
		}

		public void AddScore(int stageIndex, int catIndex, int score)
		{
			switch (stageIndex)
			{
			case 0:
				if (score > this.seaHigh_)
				{
					this.seaHigh_ = score;
				}
				break;
			case 1:
				if (score > this.cloudHigh_)
				{
					this.cloudHigh_ = score;
				}
				break;
			case 2:
				if (score > this.lavaHigh_)
				{
					this.lavaHigh_ = score;
				}
				break;
			case 3:
				if (score > this.meatHigh_)
				{
					this.meatHigh_ = score;
				}
				break;
			case 4:
				if (score > this.ronHigh_)
				{
					this.ronHigh_ = score;
				}
				break;
			}
			for (int i = 0; i < 10; i++)
			{
				if (score > this.scores_[i])
				{
					if (i == 0)
					{
						this.highScore_ = score;
					}
					for (int num = 8; num >= i; num--)
					{
						this.stageIndexes_[num + 1] = this.stageIndexes_[num];
						this.catIndexes_[num + 1] = this.catIndexes_[num];
						this.scores_[num + 1] = this.scores_[num];
					}
					this.stageIndexes_[i] = stageIndex;
					this.catIndexes_[i] = catIndex;
					this.scores_[i] = score;
					break;
				}
			}
		}

		//public static ScoreInfo LoadInfo()
		//{
			//ScoreInfo result;
			//if (Global.DeviceManager.Device != null && Global.DeviceManager.Device.IsConnected)
			//{
				//IAsyncResult asyncResult = Global.DeviceManager.Device.BeginOpenContainer("Techno Kitten Adventure", (AsyncCallback)null, (object)null);
				//asyncResult.AsyncWaitHandle.WaitOne();
				//StorageContainer storageContainer = Global.DeviceManager.Device.EndOpenContainer(asyncResult);
				//asyncResult.AsyncWaitHandle.Close();
				//string text = "ScoreInfo";
				//if (!storageContainer.FileExists(text))
				//{
				//	result = new ScoreInfo(0);
				//	storageContainer.Dispose();
				//}
				//else
				//{
				//	Stream stream = storageContainer.OpenFile(text, FileMode.Open);
				//	XmlSerializer xmlSerializer = new XmlSerializer(typeof(ScoreInfo));
				//	result = (ScoreInfo)xmlSerializer.Deserialize(stream);
				//	stream.Close();
				//	storageContainer.Dispose();
				//}
			//}
			//else
			//{
				//result = new ScoreInfo(0);
			//}
			//return result;
		//}

		public void SaveInfo()
		{
			/*if (Global.DeviceManager.Device != null && Global.DeviceManager.Device.IsConnected)
			{
				IAsyncResult asyncResult = Global.DeviceManager.Device.BeginOpenContainer("Techno Kitten Adventure", (AsyncCallback)null, (object)null);
				asyncResult.AsyncWaitHandle.WaitOne();
				//StorageContainer storageContainer = Global.DeviceManager.Device.EndOpenContainer(asyncResult);
				asyncResult.AsyncWaitHandle.Close();
				string text = "ScoreInfo";
				//if (storageContainer.FileExists(text))
				//{
				//	storageContainer.DeleteFile(text);
				//}
				//Stream stream = storageContainer.CreateFile(text);
				//XmlSerializer xmlSerializer = new XmlSerializer(typeof(ScoreInfo));
				//xmlSerializer.Serialize(stream, (object)this);
				//stream.Close();
				//storageContainer.Dispose();
			}*/
		}

		public void DrawAllScores(SpriteBatch spriteBatch)
		{
			string[] array = new string[5] { "Dream", "Cloud", "Lava", "Meat", "Popaganda" };
			string[] array2 = new string[20]
			{
				"Jet Pack", "Byarf", "Butterfly", "Dream", "Mermaid", "Baby", "Love", "Angel", "Death", "Bat",
				"Fire", "Rock", "Dragon", "Steak", "Bacon", "HotDog", "Burger", "Alien", "Grin", "MC"
			};
			float num = 148f;
			float num2 = 51f;
			float num3 = num;
			for (int i = 0; i < 10; i++)
			{
				if (this.stageIndexes_[i] < array.Length)
				{
					spriteBatch.DrawString(Global.menuFont, array[this.stageIndexes_[i]], new Vector2(255f, num3), Color.White, 0f, Global.menuFont.MeasureString(array[this.stageIndexes_[i]]) / 2f, 1f, SpriteEffects.None, 0f);
				}
				if (this.catIndexes_[i] < array2.Length)
				{
					spriteBatch.DrawString(Global.menuFont, array2[this.catIndexes_[i]], new Vector2(660f, num3), Color.White, 0f, Global.menuFont.MeasureString(array2[this.catIndexes_[i]]) / 2f, 1f, SpriteEffects.None, 0f);
				}
				this.DrawNumber(spriteBatch, this.scores_[i], new Vector2(1034f, num3));
				num3 += num2;
			}
		}

		private void DrawNumber(SpriteBatch spriteBatch, int number, Vector2 startingPosition)
		{
			string text = number.ToString();
			Vector2 vector = new Vector2((float)text.Length * 20.1875f / 2f, 16f);
			for (int i = 0; i < text.Length; i++)
			{
				int num = (int)char.GetNumericValue(text[i]);
				spriteBatch.Draw(Global.numbersTexture, startingPosition, (Rectangle?)new Rectangle(num * 36, 0, 36, 32), Color.White, 0f, vector, 17f / 32f, SpriteEffects.None, 0f);
				startingPosition.X += 20.1875f;
			}
		}
	}
}
