using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace Helicopter;

public class SongManager
{
	private ContentManager Content;

	private Song song;

	public Song CurrentSong => song;

	public SongManager(Game1 game)
	{
						Content = new ContentManager((IServiceProvider)((Game)game).Services, "Content\\Music");
		song = Content.Load<Song>("MenuSong");
	}

	public void LoadNewSong(int currentLevel)
	{
		song.Dispose();
		Content.Unload();
		switch (currentLevel)
		{
		case -1:
			song = Content.Load<Song>("MenuSong");
			break;
		case 0:
			song = Content.Load<Song>("SeaOfLove");
			Global.BPM = 15f / 44f;
			break;
		case 1:
			song = Content.Load<Song>("LikeARainbow");
			Global.BPM = 12f / 35f;
			break;
		case 2:
			song = Content.Load<Song>("YoureShining");
			Global.BPM = 0.3529412f;
			break;
		case 3:
			song = Content.Load<Song>("TasteOfHeaven");
			Global.BPM = 1f / 3f;
			break;
		case 4:
			song = Content.Load<Song>("IntergalacticalHigh");
			Global.BPM = 0.3448276f;
			break;
		}
	}
}
