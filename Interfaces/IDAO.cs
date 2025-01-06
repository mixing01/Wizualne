using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IDAO
    {
        IEnumerable<IProducer> GetAllProducers();
        IEnumerable<IGame> GetAllGames();

        IGame CreateNewGame();
        void AddGame(IGame game);
        void UpdateGame(IGame game);
        void RemoveGame(IGame game);

        IProducer CreateNewProducer();
        void AddProducer(IProducer producer);
        void UpdateProducer(IProducer producer);
        void RemoveProducer(IProducer producer);
        
        void SaveChanges();

        void UndoChanges();
        

    }

}
