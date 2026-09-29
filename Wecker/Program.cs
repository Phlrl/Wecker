using Microsoft.Data.Sqlite;

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
            string connectionString = "Data Source=wecker.db"; // string names connectionString erstellt und zuweisung des wertes durch =

            using SqliteConnection connection = new SqliteConnection(connectionString); // using benutzt obj nur solange wie es muss in dem fall connection vom typ SqliteConnection (wird hier neu instanziiert und Zuwioesung von parametern)
            connection.Open(); //öffnet die connection

            Console.WriteLine("verbunden mit der datenbak");

            // create tabel
            string sql = """
                    CREATE TABLE IF NOT EXISTS Wecker (
                        ID INTEGER PRIMARY KEY AUTOINCREMENT,
                        Uhrzeit TEXT,
                        Aktiv INTEGER
                    );
                    """; //mehrzeiliger string also 6 * " alternative -> \n

            using (SqliteCommand uebergebungscommand = new SqliteCommand(sql, connection)) //erzeugt neuen sql befehlübergeben wird "sql" und "connection" -> obj wird in der variable command gespeicher (typ : sqlite Command)
            {
                uebergebungscommand.ExecuteNonQuery(); // Methodenaufruf aud der variable command
            }

            Console.WriteLine("create ta ble erfolgreich");


            // Insert into
            string insertSql = """
                    INSERT INTO Wecker (Uhrzeit, Aktiv)
                    VALUES ('07:30', 1);
                    """;

            using (SqliteCommand commandInsert = new SqliteCommand(insertSql, connection)) //wieder using mit neuem command und diesmal werden andere parameter übergeben "insertSql, connection"
            {
                commandInsert.ExecuteNonQuery(); //wird executet und danach nicht mehr benutzt
            }

            Console.WriteLine("Wecker wurde gespeichert");

            // select
            string selectSql = "SELECT ID, Uhrzeit, Aktiv FROM Wecker"; //neuer string mit sql statement als werte

            using (SqliteCommand commandSelect = new SqliteCommand(selectSql, connection))  //neuer Command erstellt übergebung von variablen in der ()
                                // ^
                                // |
            //dadurch das man an command die beiden variabeln übergeben hat wirder der string von oben im command gespeichert

            {
                using (SqliteDataReader reader = commandSelect.ExecuteReader()) //vibe code
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(
                            $"ID: {reader["ID"]}, Uhrzeit: {reader["Uhrzeit"]}, Aktiv: {reader["Aktiv"]}" //while reader führt die query aus und gibt die werte zurück
                        );
                    }
                } // vibe code
            }

            p = new Program();

            p.w = Wecker.getInstance();

            DateTime aktuelleZeit = DateTime.Now;
            DateTime aktuelleZeitPlus10Sek = aktuelleZeit.AddSeconds(10);

            List<Alarm> neueAlarme = new List<Alarm>();
            Alarm neuerAlarm = new Alarm("Alarm1", "Morgen");
            neuerAlarm.setTime(aktuelleZeitPlus10Sek);

            neueAlarme.Add(neuerAlarm);
            p.addAlarms(neueAlarme);

            Thread t = new Thread(checkAlarms);
            t.Start();

            while (true)
            {
                //Console.Beep(1000, 500);
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
