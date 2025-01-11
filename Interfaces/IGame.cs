using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IGame
    {
        int Id { get; set; }

        IProducer Producer { get; set; }

        string Name { get; set; }

        int ReleaseYear { get; set; }
        double Price { get; set; }
        double DiskSpace {  get; set; }
        int Rating { get; set; }

        GameGenre Genre {  get; set; }
    }
}


