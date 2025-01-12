using Interfaces;
using System.Reflection;
using System.Configuration;

namespace CarApp2
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Adres pliku DAOEF.dll wzięty z pliku App.config w CarApp2
            string libraryName = ConfigurationManager.AppSettings["libraryFile"];


            IDAO dao = BLC.BLC.GetInstance(libraryName).Dao;

            // Wypisanie bazy danych na konsolę
            Console.WriteLine("** PRODUCERS ** \n");
            foreach (IProducer p in dao.GetAllProducers())
            {
                Console.WriteLine($"{p.Id}: {p.Name} [Zalozony w {p.EstYear}, kontynent: {p.Continent}]");
            }

            Console.WriteLine("\n** GAMES ** \n");
            foreach (IGame c in dao.GetAllGames())
            {
                Console.WriteLine($"{c.Id}: {c.Name}, {c.Producer.Name}");
            }

            // Dodanie do bazy danych producenta i samochodu. Wykonuje się przy zamykaniu aplikacji.
            // Trzeba ponownie odpalić aplikację żeby wczytać zmiany.

        }
    }
}
