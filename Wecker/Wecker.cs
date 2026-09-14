using System.Runtime.InteropServices;
using Microsoft.VisualBasic;

namespace Wecker
{
    public class Wecker
    {
        private Wecker instance;
        public List<Alarm> alarms = new List<Alarm>(); //constucor liste

        public Wecker()
        {
            
        }

        public Wecker getInstance()
        {
            return instance;
        }
    }
}