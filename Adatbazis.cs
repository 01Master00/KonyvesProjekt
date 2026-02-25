using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonyvesProjekt
{
    internal class Adatbazis
    {
        string connectionString = "server=localhost;user=root;database=mini_konyvtar;port=3306;password=";
        List<Konyv> konyvek = new List<Konyv>();

        internal List<Konyv> Konyvek { get => konyvek; set => konyvek = value; }

        public Adatbazis()
        {
            BeolvasKnyveket();
           // Menu();
        }

        public  void AddToList(Konyv k)
        {
            konyvek.Add(k);
        }

        public void Menu()
        {
              /* 
            string valasztas;
            do
            {
            Console.Clear();
                Console.WriteLine("=== Mini Könyvtár Menü ===");
                Console.WriteLine("1. Könyvek listája");
                Console.WriteLine("2. Új könyv felvitele");
                Console.WriteLine("3. Könyv módosítása");
                Console.WriteLine("4. Könyv törlése");
                Console.WriteLine("5. Új olvasó felvitele");
                Console.WriteLine("6. Olvasó módosítása");
                Console.WriteLine("7. Olvasó törlése");
                Console.WriteLine("8. Kölcsönzés");
                Console.WriteLine("9. Visszahozás");
                Console.WriteLine("10. Késők listája");
                Console.WriteLine("0. Kilépés");
                Console.Write("Válassz egy menüpontot: ");

                valasztas = Console.ReadLine();

                switch (valasztas)
                {
                    case "1":
                       BeolvasKnyveket();
                        break;
                    case "2":
                        // Új könyv felvitele
                        break;
                    case "3":
                        // Könyv módosítása
                        break;
                    case "4":
                        // Könyv törlése
                        break;
                    case "5":
                        // Új olvasó felvitele
                        break;
                    case "6":
                        // Olvasó módosítása
                        break;
                    case "7":
                        // Olvasó törlése
                        break;
                    case "8":
                        // Kölcsönzés
                        break;
                    case "9":
                        // Visszahozás
                        break;
                    case "10":
                        // Késők listája
                        break;
                    case "0":
                        Console.WriteLine("Kilépés...");
                        break;
                    default:
                        Console.WriteLine("Érvénytelen választás. Nyomj egy gombot a folytatáshoz.");
                        Console.ReadKey();
                        break;
                }
            }
            while (valasztas != "0");*/
        }

        public void BeolvasKnyveket()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT konyv_id, szerzo, cim, mikortol, olvaso.nev FROM konyv, olvaso WHERE kinel_van=olvaso.olvaso_id";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    List<string> konyvLista = new List<string>();

                    while (reader.Read())
                    {
                        string id = reader["konyv_id"].ToString().PadRight(4);
                        string szerzo = reader["szerzo"].ToString().TrimEnd().PadRight(33);
                        string cim = reader["cim"].ToString().TrimEnd().PadRight(36);
                        string kinel = reader["nev"].ToString().TrimEnd().PadRight(30);
                        DateTime datum;
                        string mikortol;

                        if (DateTime.TryParse(reader["mikortol"].ToString(), out datum))
                        {
                            mikortol = datum.ToString("yyyy-MM-dd").PadRight(10);
                        }
                        else
                        {
                            mikortol = "nincs dátum".PadRight(10);
                        }

                        konyvek.Add(new Konyv(id, szerzo, cim, kinel, mikortol));
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Hiba történt: " + ex.Message);
                   // Environment.Exit();
                }
            }
        }
        public void Listaz()
        {
            // Lapozás 10 soronként -- ehhez nincs hozzáfűznivalóm
            int oldalMeret = 10;
            int osszesSor = konyvek.Count;
            int oldalSzam = 0;

            while (oldalSzam * oldalMeret < osszesSor)
            {
              
                Console.WriteLine($"Könyvek listája - {oldalSzam + 1}. oldal\n");

                for (int i = oldalSzam * oldalMeret; i < Math.Min((oldalSzam + 1) * oldalMeret, osszesSor); i++)
                {
                    Console.WriteLine(konyvek[i]);
                }

                oldalSzam++;
                if (oldalSzam * oldalMeret < osszesSor)
                {
                    Console.WriteLine("\nNyomj Entert a következő oldalhoz...");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("\nEz volt az utolsó oldal. Nyomj egy gombot a visszalépéshez.");
                    Console.ReadKey();
                }
            }
        }

        //adatbázisba uj könyv felvétele
        public void UjKonyv(Konyv k)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO konyv (konyv_id, szerzo, cim, kinel_van, mikortol) VALUES (@id, @szerzo, @cim, @kinel_van, @mikortol)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", k.Id);
                    cmd.Parameters.AddWithValue("@szerzo", k.Szerzo);
                    cmd.Parameters.AddWithValue("@cim", k.Cim);
                    cmd.Parameters.AddWithValue("@kinel_van", k.Kinel);
                    cmd.Parameters.AddWithValue("@mikortol", k.Mikortol);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Hiba történt: " + ex.Message);
                  
                }
            }
        }


    }
}

