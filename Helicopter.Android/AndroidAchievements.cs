using Helicopter.Core;

namespace Helicopter.Android
{
    public class AndroidAchievements : IAchievementService
    {
        public void UnlockAchievement(string achievementName)
        {
            MainActivity.UnlockAchievement(achievementName);
        }
    }
}
