using Interfaces;

namespace DAOMock2
{
    public class DAOMock2 : IDAO
    {

        private List<IProducer> _producers;

        private List<IGame> _games;

        public DAOMock2()
        {
            _producers = new List<IProducer>()
            {
                new Producer() { Id = 1, Name="Capcom", Continent = Continent.AS, EstYear = 1979},
                new Producer() { Id = 2, Name="Electronic Arts", Continent = Continent.NA, EstYear = 1982},
            };

            _games = new List<IGame>()
            {
                new Game() { Id = 1, Producer = _producers[0], Name="Street Fighter II", Genre=GameGenre.Combat, DiskSpace = 4, Price = 1.99, Rating = 9, ReleaseYear = 1991},
                new Game() { Id = 2, Producer = _producers[0], Name="Devil May Cry", Genre=GameGenre.Action, DiskSpace = 9, Price = 17, Rating = 7, ReleaseYear = 2007},
                new Game() { Id = 3, Producer = _producers[1], Name="FIFA 23", Genre=GameGenre.Sports, DiskSpace = 100, Price = 59.99, Rating = 8, ReleaseYear = 2022},
                new Game() { Id = 4, Producer = _producers[1], Name="Dead Space", Genre=GameGenre.Horror, DiskSpace = 12.5, Price = 5.99, Rating = 7, ReleaseYear = 2008},
            };
        }

        public IGame CreateNewGame()
        {
            return new Game();
        }
        public IProducer CreateNewProducer()
        {
            return new Producer();
        }
        public void AddGame(IGame game)
        {
            Game g = game as Game;
            g.Id = _games.Count() + 1;
            _games.Add(g);
        }
        public void AddProducer(IProducer producer)
        {
            Producer p = producer as Producer;
            _producers.Add(p);
        }
        public void RemoveGame(IGame game)
        {
            _games.Remove(game);
        }
        public void RemoveProducer(IProducer producer)
        {
            _producers.Remove(producer);
        }
        public IEnumerable<IProducer> GetAllProducers()
        {
            return _producers;
        }
        public IEnumerable<IGame> GetAllGames()
        {
            return _games;
        }
        public void UpdateGame(IGame game)
        {
            throw new NotImplementedException();
        }
        public void UpdateProducer(IProducer producer)
        {
            throw new NotImplementedException();
        }
        public void SaveChanges() { }

        public void UndoChanges() { }
    }
}
