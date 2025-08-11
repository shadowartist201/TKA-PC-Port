using Android.OS;
using Helicopter.Core;

namespace Helicopter.Android
{
    public class AndroidVibration : IVibrationService
    {
        public void VibrationOff()
        {
            MainActivity.vibrator.Cancel();
        }
        public void VibrationOn()
        {
            MainActivity.vibrator.Vibrate(VibrationEffect.CreateOneShot(300000, 255));
        }
    }
}
