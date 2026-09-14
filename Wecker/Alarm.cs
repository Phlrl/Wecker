using System.Dynamic;

namespace Wecker
{
    public class Alarm
    {
        private string name;
        private string description;
        private DateTime time;

        public Alarm(string name, string description)
        {
            
        }

        public void setTime(DateTime t)
        {
            time = t;
        }

        public DateTime getTime()
        {
            return time;
        }

        public string getName()
        {
            return name;
        }

        public string getDescrition()
        {
            return description;
        }
    }
}