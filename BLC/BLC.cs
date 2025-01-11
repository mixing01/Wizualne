using Interfaces;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace BLC
{
    public class BLC
    {
        // Klasa jest singletonem(tylko jedna instancja)
        private static BLC _instance;
        private static readonly object Lock = new object();

        private IDAO _dao;
        public IDAO Dao => _dao;

        public BLC(IConfiguration configuration)
        {
            string libraryName = configuration.GetValue<string>("LibraryName");
            CreateDao(libraryName);
        }
        private BLC(string libraryName)
        {
            CreateDao(libraryName);
        }

        private void CreateDao(string libraryName)
        {
            // Wczytujemy podaną bibliotekę dll
            Assembly assembly = Assembly.UnsafeLoadFrom(libraryName); // Jak tu wywala błąd to zapewne trzeba zmienić App.config w CarApp2 lub CarsApp2 lub appsettings.json w CarAppWeb

            // Szukamy obiektu implementującego interface IDAO
            Type typeToCreate = null;
            foreach (Type t in assembly.GetTypes())
            {
                if (t.IsAssignableTo(typeof(IDAO)))
                {
                    typeToCreate = t;
                    break;
                }
            }

            _dao = Activator.CreateInstance(typeToCreate) as IDAO;
        }
        // Jeżeli istnieje już instacja BLC to ją zwracamy, jak nie to tworzymy nową
        // lock jest po to żeby w kilku miejscach na raz nikt nie utworzył instancji
        public static BLC GetInstance(string libraryName)
        {
            lock (Lock)
            {
                if (_instance == null)
                {
                    _instance = new BLC(libraryName);
                }
            }
            return _instance;
        }
    }
}
