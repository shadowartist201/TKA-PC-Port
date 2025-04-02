

namespace Helicopter
{
    public static class Achievements
    {
        public static int deathCount;
        public static int playCount;
        public static bool firstPause;

        public static void CheckHighScore(int score)
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
