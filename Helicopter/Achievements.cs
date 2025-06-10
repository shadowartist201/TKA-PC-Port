

using Android.Locations;

namespace Helicopter
{
    public static class Achievements
    {
        public static int deathCount = 0;
        public static int playCount = 0;
        public static bool firstPause = false;
        public static bool wholeSongFinished = false;
        public static bool songStartCheckpoint = false;
        public static bool songEndCheckpoint = false;
        public static bool gameBeingPlayed = false;
        public static bool playCounterIncremented = false;

        public static void CheckAchievements(ScoreInfo scoreInfo, int score)
        {
            CheckWholeSongFinished(); //done
            CheckFirstPause(); //done
            CheckPlayCount();
            CheckDeathCount(); //done
            CheckCatUnlock(scoreInfo); //done
            CheckHighScore(score); //done
        }

        private static void CheckWholeSongFinished()
        {
            if (wholeSongFinished)
            {
                Activity1.UnlockAchievement("CgkI0-u5kb8NEAIQBA");
            }
        }

        private static void CheckFirstPause()
        {
            if (firstPause)
            {
                Activity1.UnlockAchievement("CgkI0-u5kb8NEAIQAQ");
            }
        }

        private static void CheckPlayCount()
        {
            if (playCount == 25)
            {
                Activity1.UnlockAchievement("CgkI0-u5kb8NEAIQBg");
            }
            else if (playCount == 50)
            {
                Activity1.UnlockAchievement("CgkI0-u5kb8NEAIQBw");
            }
            else if (playCount == 100)
            {
                Activity1.UnlockAchievement("CgkI0-u5kb8NEAIQCA");
            }
        }

        private static void CheckDeathCount()
        {
            if (deathCount == 1)
            {
                Activity1.UnlockAchievement("CgkI0-u5kb8NEAIQAw");
            }
            else if (deathCount == 9)
            {
                Activity1.UnlockAchievement("CgkI0-u5kb8NEAIQBQ");
            }
        }

        private static void CheckCatUnlock(ScoreInfo scoreInfo)
        {
            if (scoreInfo.seaEightyUnlocked && scoreInfo.ronSixtyUnlocked && 
                scoreInfo.meatEightyUnlocked && scoreInfo.lavaEightyUnlocked &&
                scoreInfo.cloudEightyUnlocked && scoreInfo.nyanSixtyUnlocked)
            {
                Activity1.UnlockAchievement("CgkI0-u5kb8NEAIQAg");
            }
        }

        private static void CheckHighScore(int score)
        {
            if (score >= 9001)
                Activity1.UnlockAchievement("CgkI0-u5kb8NEAIQCQ");
            if (score >= 30000)
                Activity1.UnlockAchievement("CgkI0-u5kb8NEAIQCg");
            if (score >= 50000)
                Activity1.UnlockAchievement("CgkI0-u5kb8NEAIQCw");
            if (score >= 75000)
                Activity1.UnlockAchievement("CgkI0-u5kb8NEAIQDA");
            if (score >= 100000)
                Activity1.UnlockAchievement("CgkI0-u5kb8NEAIQDQ");
            if (score >= 125000)
                Activity1.UnlockAchievement("CgkI0-u5kb8NEAIQDg");
            if (score >= 150000)
                Activity1.UnlockAchievement("CgkI0-u5kb8NEAIQDw");
            if (score >= 200000)
                Activity1.UnlockAchievement("CgkI0-u5kb8NEAIQEA");
            if (score >= 300000)
                Activity1.UnlockAchievement("CgkI0-u5kb8NEAIQEQ");
            if (score >= 400000)
                Activity1.UnlockAchievement("CgkI0-u5kb8NEAIQEg");
            if (score >= 500000)
                Activity1.UnlockAchievement("CgkI0-u5kb8NEAIQEw");
            if (score >= 1000000)
                Activity1.UnlockAchievement("CgkI0-u5kb8NEAIQFA");
        }
    }
}
