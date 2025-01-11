using Interfaces;

namespace DAOMock2
{
    public class Producer: Interfaces.IProducer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int EstYear { get; set; }
        public Continent Continent { get; set; }
    }
}
