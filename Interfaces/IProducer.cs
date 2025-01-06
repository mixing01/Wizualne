﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IProducer
    {
        int Id { get; set; }

        string Name { get; set; }

        int EstYear { get; set; }

        Continent Continent { get; set; }
    }
}
