namespace Wecker
{
    public class Wecker
    {
        private static Wecker instance = new Wecker();
        public List<Alarm> alarms = new List<Alarm>(); //constucor liste

        private Wecker()
        {
            
        }

        public static Wecker getInstance()
        {
            return instance;
        }
    }
}