using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Kémia
{
    class Program
    {
        static List<Felfedezesek> adatok = new List<Felfedezesek>();
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            //Beolvasás
            StreamReader Olvas = new StreamReader("felfedezesek.csv", Encoding.Default);
            string fejlec = Olvas.ReadLine();
            while (!Olvas.EndOfStream)
            {
                adatok.Add(new Felfedezesek(Olvas.ReadLine()));
            }
            Olvas.Close();
            Kemiai_Elemek_Szama();//3. feladat
            Felfedezesek_Szama_Ókorban(); //4. feladat
            string Vegyjel = Vegyjel_Bekeres(); //5.feladat
            Kereses(Vegyjel);//6. feladat
            KetElemKozottLeghoszabb(); //7. feladat
            StatisztikaLinQ(); //8. feladat
            Console.ReadLine();
        }
        private static void Kemiai_Elemek_Szama()
        {
            Console.WriteLine($"3. feladat: Elemek száma: {adatok.Count}");
        }
        private static void Felfedezesek_Szama_Ókorban()
        {
            Console.WriteLine($"4. feladat: felfedezések száma az Ókórban: {adatok.Count(x=>x.Ev=="Ókor")}");
        }
        private static string Vegyjel_Bekeres()
        {
            string Pattern = @"^[a-zA-Z]+$";
            Regex rx = new Regex(Pattern);
            Match match;
            string vegyjel;
            Console.Write("5. feladat: ");
            do
            {
              Console.Write("Kérek egy vegyjelet: ");
              vegyjel = Console.ReadLine();
              match = rx.Match(vegyjel);
            } while (!(vegyjel.Length == 1 || vegyjel.Length == 2) && match.Success);
              return vegyjel;
        }
        private static void Kereses(string vegyjel)
        {
            bool vanevegyjel = false;
            for (int i = 0; i < adatok.Count; i++)
            {
                Console.WriteLine("6. feladat: Keresés");
                if (adatok[i].Vegyjel.ToUpper() == vegyjel.ToUpper())
                {
                    vanevegyjel = true;
                    Console.WriteLine($"\tAz elem vegyjele:{adatok[i].Vegyjel}");
                    Console.WriteLine($"\tAz elem neve:{adatok[i].Nev}");
                    Console.WriteLine($"\tRendszáma:{adatok[i].Rendszam}");
                    Console.WriteLine($"\tFelfedezés éve:{adatok[i].Ev}");
                    Console.WriteLine($"\tFelfedező:{adatok[i].Felfedezo}");
                }
            }
            if (vanevegyjel == false)
            {
                Console.WriteLine("Nincs ilyen elem az adatbázisban!");
            }
        }
        private static void KetElemKozottLeghoszabb()
        {
            int LeghosszabEv = 0;
            for (int i = 0; i < adatok.Count - 1; i++)
            {
                if (adatok[i].Ev != "Ókor")
                {
                    if (Convert.ToInt32(adatok[i + 1].Ev) - Convert.ToInt32(adatok[i].Ev) > LeghosszabEv)
                    {
                        LeghosszabEv = Convert.ToInt32(adatok[i + 1].Ev) - Convert.ToInt32(adatok[i].Ev);
                    }
                }
            }
            Console.WriteLine($"7. feladat: {LeghosszabEv} év volt a leghosszabb időszak két elem felfedezése között.");
        }
        private static void StatisztikaLinQ()
        {
            Console.WriteLine("8. feladat: Statisztika");
            adatok.GroupBy(j => j.Ev).Where(g => g.Count() > 3 && g.Key != "Ókor").ToList().ForEach(a => Console.WriteLine($"\t{a.Key}: {a.Count()} db"));
        }
    }
    class Felfedezesek 
    {
        public Felfedezesek(string sor)
        {
            string[] sorelemek = sor.Split(';');
            this.Ev = sorelemek[0];
            this.Nev = sorelemek[1];
            this.Vegyjel = sorelemek[2];
            this.Rendszam = Convert.ToInt32(sorelemek[3]);
            this.Felfedezo = sorelemek[4];
        }
     public string Ev { get; set; }
     public string Nev { get; set; }
     public string Vegyjel { get; set; }
     public int Rendszam { get; set; }
     public string Felfedezo { get; set; }   
    }
}
