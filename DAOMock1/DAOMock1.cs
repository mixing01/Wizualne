using Interfaces;

namespace DAOMock1
{
    public class DAOMock1 : IDAO
    {

        private List<IProducer> _producers;

        private List<IGame> _cars;

        public DAOMock1()
        {
            _producers = new List<IProducer>()
            {
                new Producer() { Id = 1, Name="VW"},
                new Producer() { Id = 2, Name="BMW"},
            };

            _cars = new List<IGame>()
            {
                new Car() { Id = 1, Producer = _producers[0], Name="Passat", Genre=GameGenre.Automatic },
                new Car() { Id = 2, Producer = _producers[0], Name="Golf", Genre=GameGenre.Manual},
                new Car() { Id = 3, Producer = _producers[0], Name="Polo", Genre=GameGenre.Manual },
                new Car() { Id = 4, Producer = _producers[1], Name="3", Genre=GameGenre.Automatic },
            };
        }

        public void AddCar(IGame car)
        {
            throw new NotImplementedException();
        }

        public void AddProducer(IProducer producer)
        {
            throw new NotImplementedException();
        }

        public IGame CreateNewCar()
        {
            throw new NotImplementedException();
        }

        public IProducer CreateNewProducer()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IGame> GetAllCars()
        {
            return _cars;
        }

        public IEnumerable<IProducer> GetAllProducers()
        {
            return _producers;
        }

        public void RemoveCar(IGame car)
        {
            throw new NotImplementedException();
        }

        public void RemoveProducer(IProducer producer)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void UndoChanges()
        {
            throw new NotImplementedException();
        }

        public void UpdateCar(IGame car)
        {
            throw new NotImplementedException();
        }

        public void UpdateProducer(IProducer producer)
        {
            throw new NotImplementedException();
        }
    }
}
