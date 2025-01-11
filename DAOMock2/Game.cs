using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOMock2
{
    public class Game : Interfaces.IGame
    {
        public int Id { get; set; }
        public IProducer Producer { get; set; }
        public string Name { get; set; }
        public int ReleaseYear { get; set; }
        public double Price { get; set; }
        public double DiskSpace { get; set; }
        public int Rating { get; set; }
        public GameGenre Genre { get; set; }
    }
}
