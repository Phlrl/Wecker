using System.Text.RegularExpressions;

namespace Wecker
{
    public class Program
    {
        private Wecker w;

        private void addAlarms(List<Alarm> alarms)
        {
            w.alarms = alarms;
        }

        private List<Alarm> checkForAlarms() 
        {
            List<Alarm> ausgeloesteAlarme = new List<Alarm>();
            foreach (var alarm in w.alarms)
            {
                if (alarm.istAusgeloest(DateTime.Now))
                {
                    ausgeloesteAlarme.Add(alarm);
                }  
            }
            return ausgeloesteAlarme;
        }

        private static void Main(string[] args)
        {
            Program p = new Program();
            
            p.w = new Wecker();

            DateTime aktuelleZeit = DateTime.Now;
            DateTime aktuelleZeitPlus10Sek = aktuelleZeit.AddSeconds(10); 
            Console.WriteLine(aktuelleZeit);
            Console.WriteLine(aktuelleZeitPlus10Sek);
            
            List<Alarm> neueAlarme = new List<Alarm>();
            Alarm neuerAlarm = new Alarm("Alarm1" , "Morgen");
            neuerAlarm.setTime(aktuelleZeitPlus10Sek);

            neueAlarme.Add(neuerAlarm);
            p.addAlarms(neueAlarme);


            for (int i = 0; i < 11; i++)
            {
                Thread.Sleep(1000);
                List<Alarm> al = p.checkForAlarms();

                foreach (var ausgeloesterAlarm in al)
                {
                    Console.WriteLine("Alarm " + ausgeloesterAlarm.getName() + " wurde ausgelöst");
                }

            }
        }


    }
}