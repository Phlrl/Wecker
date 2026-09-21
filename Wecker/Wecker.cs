using System.Runtime.InteropServices;

namespace Wecker
{
    public class Wecker
    {
        private static Wecker instance;
        public List<Alarm> alarms = new List<Alarm>(); //constucor liste

        private Wecker()
        {
            
        }

        public static Wecker getInstance()
        {
            if(instance == null) //lazy instantziieren
            {
                instance = new Wecker();
            }
            return instance;
        }
    }
}