

namespace AutomationInfra.ExtensionsMethods
{
    public static class Utills
    {
        public static int RandomInt(int lowRange, int highRang)
        {
            var random = new Random();

            return random.Next(lowRange, highRang);
        }
    }
}
