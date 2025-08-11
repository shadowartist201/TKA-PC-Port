namespace Helicopter.Core
{

    public interface IVibrationService
    {
        void VibrationOff();
        void VibrationOn();
    }

    public class Vibration
    {
        public static IVibrationService VibrationService;
        public static void MobileOff()
        {
            if (Game1.IsMobile)
            {
                VibrationService.VibrationOff();
            }
        }

        public static void MobileOn()
        {
            if (Game1.IsMobile)
            {
                VibrationService.VibrationOn();
            }
        }
    }
}
