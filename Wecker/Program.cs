using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Wecker
{
    public class Program
    {
        private Wecker w;
        private static Program p;

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

        private static void checkAlarms()
        {
            while (true)
            {
                Thread.Sleep(5000);
                List<Alarm> al = p.checkForAlarms();

                foreach (var ausgeloesterAlarm in al)
                {
                    Console.WriteLine("Alarm " + ausgeloesterAlarm.getName() + " wurde ausgelöst");
                }
            }
        } 

        private static void Main(string[] args)
        {
            p = new Program();
            
            p.w =  Wecker.getInstance();

            DateTime aktuelleZeit = DateTime.Now;
            DateTime aktuelleZeitPlus10Sek = aktuelleZeit.AddSeconds(10); 
            
            List<Alarm> neueAlarme = new List<Alarm>();
            Alarm neuerAlarm = new Alarm("Alarm1" , "Morgen");
            neuerAlarm.setTime(aktuelleZeitPlus10Sek);

            neueAlarme.Add(neuerAlarm);
            p.addAlarms(neueAlarme);

            Thread t = new Thread(checkAlarms);
            t.Start();
            
            while (true)
            {
                Console.WriteLine("Stop eingeben um Wecker zu beenden");
                string eingabe = Console.ReadLine();
                if (eingabe == "Stop")
                {
                    Environment.Exit(0);
                }
            }

        }
    }
}