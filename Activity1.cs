using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Gms.Games;
using Android.Gms.Games.Snapshot;
using Android.Gms.Tasks;
using Android.OS;
using Android.Views;
using AndroidX.Core.View;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using System.IO;


namespace Helicopter
{
    [Activity(
        Label = "@string/app_name",
        MainLauncher = true,
        Icon = "@drawable/icon",
        AlwaysRetainTaskState = true,
        LaunchMode = LaunchMode.SingleInstance,
        ScreenOrientation = ScreenOrientation.Landscape,
        ConfigurationChanges = ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden
    )]
    public class Activity1 : AndroidGameActivity
    {
        private Game1 _game;
        private View _view;
        private VibratorManager _vibratorManager;
        public static Vibrator vibrator;
        private static IAchievementsClient _achievementsClient;
        public static IGamesSignInClient gamesSignInClient;
        private static ISnapshotContents _snapshotContents;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            PlayGamesSdk.Initialize(this);
            gamesSignInClient = PlayGames.GetGamesSignInClient(this);
            gamesSignInClient.IsAuthenticated().AddOnCompleteListener(new TaskCompleteListener());

            _achievementsClient = PlayGames.GetAchievementsClient(this);

            _game = new Game1();
            _view = _game.Services.GetService(typeof(View)) as View;

            SetContentView(_view);

            WindowInsetsControllerCompat windowInsetsController = WindowCompat.GetInsetsController(Window, Window.DecorView);
            windowInsetsController.SystemBarsBehavior = WindowInsetsControllerCompat.BehaviorShowTransientBarsBySwipe;
            windowInsetsController.Hide(WindowInsetsCompat.Type.SystemBars());

            Window.Attributes.LayoutInDisplayCutoutMode = LayoutInDisplayCutoutMode.ShortEdges;
            Window.AddFlags(WindowManagerFlags.TranslucentStatus);

            _vibratorManager = GetSystemService(Context.VibratorManagerService) as VibratorManager;
            vibrator = _vibratorManager.DefaultVibrator;

            _game.Run();
        }

        protected override void OnStop()
        {
            base.OnStop();
            _game.saveData();
        }

        protected override void OnPause()
        {
            base.OnPause();
            _game.saveData();
        }

        public void PlayAuthSuccess()
        {
            Achievements();
            Leaderboards();
        }

        protected void Achievements()
        {
            _achievementsClient = PlayGames.GetAchievementsClient(this);
        }

        protected void Leaderboards() 
        {
            PlayGames.GetLeaderboardsClient(this);
            //PlayGames.GetLeaderboardsClient(this).SubmitScore("leaderboard_id", 1337);
            /*int RC_LEADERBOARD_UI = 9004;
            PlayGames.GetLeaderboardsClient(this).GetLeaderboardIntent("leaderboard_id").AddOnSuccessListener(new OnSuccessListener<Intent>) {
                void onSuccess(Intent intent)
                {
                    StartActivityForResult(intent, RC_LEADERBOARD_UI);
                }
            };*/
        }

        public static void UnlockAchievement(string achievementId)
        {
            _achievementsClient.Unlock(achievementId);
        }
    }

    public class TaskCompleteListener : Java.Lang.Object, IOnCompleteListener
    {
        private Activity1 activity1 = new Activity1();
        public void OnComplete(Task task)
        {
            var isAuthenticated = task.IsSuccessful &&
                ((AuthenticationResult)task.Result).IsAuthenticated;

            if (isAuthenticated)
            {
                activity1.PlayAuthSuccess();
            }
            else
            {

            }
        }
    }
}
