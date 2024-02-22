using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace Helicopter
{
	public class SongManager
	{
		private ContentManager Content;

		private Song song;

		public Song CurrentSong => this.song;

		public static bool IsNyanPack = false;

		public SongManager(Game1 game)
		{
			this.Content = new ContentManager((IServiceProvider)game.Services, "Content\\Music");
			this.song = this.Content.Load<Song>("MenuSong");
		}

		public void LoadNewSong(int currentLevel)
		{
			this.song.Dispose();
			this.Content.Unload();
			switch (currentLevel)
			{
			case -1:
				this.song = this.Content.Load<Song>("MenuSong");
				IsNyanPack = false;
                break;
			case 0:
				this.song = this.Content.Load<Song>("SeaOfLove");
                IsNyanPack = false;
                Global.BPM = 15f / 44f;
				break;
			case 1:
				this.song = this.Content.Load<Song>("LikeARainbow");
                IsNyanPack = false;
                Global.BPM = 12f / 35f;
				break;
			case 2:
				this.song = this.Content.Load<Song>("YoureShining");
                IsNyanPack = false;
                Global.BPM = 0.3529412f;
				break;
			case 3:
				this.song = this.Content.Load<Song>("TasteOfHeaven");
                IsNyanPack = false;
                Global.BPM = 0.333333343f;
				break;
			case 4:
				this.song = this.Content.Load<Song>("IntergalacticalHigh");
                IsNyanPack = false;
                Global.BPM = 0.3448276f;
				break;
			case 5:
				this.song = this.Content.Load<Song>("MyRainbow");
				IsNyanPack = true;
				Global.BPM = 60f / 170f;
				break;
			}
		}
	}
}
