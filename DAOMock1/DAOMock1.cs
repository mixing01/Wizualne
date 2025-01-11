using System.Runtime.CompilerServices;
using Interfaces;

namespace DAOMock1
{
    public class DAOMock1 : IDAO
    {

        private List<IProducer> _producers;

        private List<IGame> _games;

        public DAOMock1()
        {
            _producers = new List<IProducer>()
            {
                new Producer() { Id = 1, Name="Nintendo", Continent = Continent.AS, EstYear = 1889},
                new Producer() { Id = 2, Name="Ubisoft Entertainment SA", Continent = Continent.EU, EstYear = 1986},
            };

            _games = new List<IGame>()
            {
                new Game() { Id = 1, Producer = _producers[0], Name="Donkey Kong", Genre=GameGenre.Platform, DiskSpace = 0.1, Price = 24.99, Rating = 8, ReleaseYear = 1981},
                new Game() { Id = 2, Producer = _producers[0], Name="Super Mario Odyssey", Genre=GameGenre.Adventure, DiskSpace = 5.7, Price = 59.99, Rating = 9, ReleaseYear = 2017},
                new Game() { Id = 3, Producer = _producers[1], Name="Assassin’s Creed IV: Black Flag", Genre=GameGenre.Adventure, DiskSpace = 30, Price = 39.99, Rating = 8, ReleaseYear = 2013},
                new Game() { Id = 4, Producer = _producers[1], Name="For Honor", Genre=GameGenre.Combat, DiskSpace = 90, Price = 29.99, Rating = 6, ReleaseYear = 2016},
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
