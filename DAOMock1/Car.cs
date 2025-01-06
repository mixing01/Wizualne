using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOMock1
{
    public class Car : IGame
    {
        public int Id { get; set; }
        public IProducer Producer { get; set; }
        public string Name { get; set; }
        public GameGenre Genre { get; set; }
        public int ProdYear { get; set; }
        public int Mileage { get; set; }
        public int ProducerId { get; set; }
        public int Engine { get; set; }
    }
}
