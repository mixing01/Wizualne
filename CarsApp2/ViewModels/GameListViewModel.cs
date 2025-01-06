using DAOEF.BO;
using Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Windows.Data;

namespace GamesDesktopApp.ViewModels
{
    public class GameListViewModel : INotifyPropertyChanged
    {
        private ListCollectionView viewGames;
        private ListCollectionView viewProducers;
        // PropertyChanged służy temu żeby UI się odświerzało jak dane się zmienią
        public event PropertyChangedEventHandler? PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

        }

        private ObservableCollection<GameViewModel> _games;
        public ObservableCollection<GameViewModel> Games
        {
            get { return _games; }
            set { _games = value; RaisePropertyChanged(nameof(Games)); }
        }

        private GameViewModel _selectedGame;
        public GameViewModel SelectedGame
        {
            get => _selectedGame;
            set
            {
                _selectedGame = value;
                if (CanAddNewGame())
                    EditedGame = SelectedGame;
                RaisePropertyChanged(nameof(SelectedGame));

            }
        }

        private GameViewModel _editedGame;
        public GameViewModel EditedGame
        {
            get => _editedGame;
            set
            {
                _editedGame = value;
                RaisePropertyChanged(nameof(EditedGame));

            }
        }

        private ObservableCollection<ProducerViewModel> _producers;
        public ObservableCollection<ProducerViewModel> Producers
        {
            get { return _producers; }
            set { _producers = value; RaisePropertyChanged(nameof(Producers)); }
        }

        private ProducerViewModel _selectedProducer;
        public ProducerViewModel SelectedProducer
        {
            get => _selectedProducer;
            set
            {
                _selectedProducer = value;
                if (CanAddNewProducer())
                    EditedProducer = SelectedProducer;
                RaisePropertyChanged(nameof(SelectedProducer));

            }
        }

        private ProducerViewModel _editedProducer;
        public ProducerViewModel EditedProducer
        {
            get => _editedProducer;
            set
            {
                _editedProducer = value;
                RaisePropertyChanged(nameof(EditedProducer));

            }
        }

        private IDAO dao;

        public GameListViewModel()
        {
            // Tworzymy obiekt typu IDAO na podstawie ścieżki z App.config
            string libraryName = ConfigurationManager.AppSettings["libraryFile"];
            dao = BLC.BLC.GetInstance(libraryName).DAO;

            //_games jest kolekcją typu GameViewModel a nie IGame, bo samochody są wyświetlane na ekran
            _games = new ObservableCollection<GameViewModel>();
            _producers = new ObservableCollection<ProducerViewModel>(); //Jak tu wywala błąd to zapewne trzeba zmienić ścieżkę w DAOEF/DAOSqlite.cs

            foreach (var game in dao.GetAllGames())
            {
                Games.Add(new GameViewModel(game));
            }

            foreach (var producer in dao.GetAllProducers())
            {
                Producers.Add(new ProducerViewModel(producer));
            }

            viewGames = (ListCollectionView)CollectionViewSource.GetDefaultView(_games);
            viewProducers = (ListCollectionView)CollectionViewSource.GetDefaultView(_producers);

            _addNewGameCommand = new RelayCommand(param => AddNewGame(), _ => CanAddNewGame());
            _saveGameCommand = new RelayCommand(param => SaveGame(), _ => CanSaveGame());
            _filterGamesDataCommand = new RelayCommand(param => FilterGamesData());
            _undoGamesChangesCommand = new RelayCommand(param => UndoGamesChanges(), _ => CanUndoGamesChanges());

            _addNewProducerCommand = new RelayCommand(param => AddNewProducer(), _ => CanAddNewProducer());
            _saveProducerCommand = new RelayCommand(param => SaveProducer(), _ => CanSaveProducer());
            _filterProducersDataCommand = new RelayCommand(param => FilterProducersData());
            _undoProducersChangesCommand = new RelayCommand(param => UndoProducersChanges(), _ => CanUndoProducersChanges());
        }

        private RelayCommand _addNewGameCommand;
        public RelayCommand AddNewGameCommand
        {
            get => _addNewGameCommand;
        }

        private RelayCommand _saveGameCommand;
        public RelayCommand SaveGameCommand
        {
            get => _saveGameCommand;
        }

        private void AddNewGame()
        {
            IGame newGame = dao.CreateNewGame();
            GameViewModel cvm = new GameViewModel(newGame);
            EditedGame = cvm;
            EditedGame.IsChanged = true;
            SelectedGame = null;

        }

        // Jak id == 0 to znaczy że dodajemy nowe auto do bazy. W przeciwnym razie edytujemy istniejące
        private void SaveGame()
        {
            if (EditedGame.HasErrors)
                return;
            if (EditedGame.Id == 0)
            {
                _games.Add(EditedGame);
                // dao przyjmuje obiekty typu IGame, a SelectedGame jest typu GameViewModel. Dlatego korzystamy z właściwość(property) Game w klasie GameViewModel
                dao.AddGame(EditedGame.game);
            }
            EditedGame.IsChanged = false;
            dao.SaveChanges();
            EditedGame = null;
        }

        private bool CanAddNewGame()
        {
            if ((EditedGame == null) || (!EditedGame.IsChanged)) return true;
            return false;
        }

        private bool CanSaveGame()
        {
            if ((EditedGame == null) || !EditedGame.IsChanged) return false;
            return !EditedGame.HasErrors;
        }

        private bool CanUndoGamesChanges()
        {
            if ((EditedGame == null)) return false;
            return true;
        }

        private string _filterGames;
        public string FilterGames
        {
            get => _filterGames;
            set
            {
                _filterGames = value;
                RaisePropertyChanged(nameof(FilterGames));
            }
        }

        private RelayCommand _filterGamesDataCommand;
        public RelayCommand FilterGamesDataCommand { get => _filterGamesDataCommand; }

        private void FilterGamesData()
        {
            if (string.IsNullOrEmpty(_filterGames))
            {
                viewGames.Filter = null;
            }
            else
            {
                viewGames.Filter = c => ((GameViewModel)c).Name.Contains(_filterGames);
            }
        }

        private void UndoGamesChanges()
        {
            if(EditedGame.Id != 0)
            {
                dao.UndoChanges();
                IGame game = dao.GetAllGames().First(c => c.Id == EditedGame.Id);
                int index = Games.IndexOf(EditedGame);
                Games[index] = new GameViewModel(game);
            }
            EditedGame = null;
        }
        private RelayCommand _undoGamesChangesCommand;
        public RelayCommand UndoChangesCommand
        {
            get => _undoGamesChangesCommand;
        }

        private RelayCommand _addNewProducerCommand;
        public RelayCommand AddNewProducerCommand
        {
            get => _addNewProducerCommand;
        }

        private RelayCommand _saveProducerCommand;
        public RelayCommand SaveProducerCommand
        {
            get => _saveProducerCommand;
        }

        private void AddNewProducer()
        {
            IProducer newProducer = dao.CreateNewProducer();
            ProducerViewModel pvm = new ProducerViewModel(newProducer);
            EditedProducer = pvm;
            EditedProducer.IsChanged = true;
            SelectedProducer = null;

        }

        // Jak id == 0 to znaczy że dodajemy nowe auto do bazy. W przeciwnym razie edytujemy istniejące
        private void SaveProducer()
        {
            if (EditedProducer.HasErrors)
                return;
            if (EditedProducer.Id == 0)
            {
                _producers.Add(EditedProducer);
                // dao przyjmuje obiekty typu IGame, a SelectedGame jest typu GameViewModel. Dlatego korzystamy z właściwość(property) Game w klasie GameViewModel
                dao.AddProducer(EditedProducer.producer);
            }
            EditedProducer.IsChanged = false;
            dao.SaveChanges();
            EditedProducer = null;
        }

        private bool CanAddNewProducer()
        {
            if ((EditedProducer == null) || (!EditedProducer.IsChanged)) return true;
            return false;
        }

        private bool CanSaveProducer()
        {
            if ((EditedProducer == null) || !EditedProducer.IsChanged) return false;
            return !EditedProducer.HasErrors;
        }

        private bool CanUndoProducersChanges()
        {
            if ((EditedProducer == null)) return false;
            return true;
        }

        private string _filterProducers;
        public string FilterProducers
        {
            get => _filterProducers;
            set
            {
                _filterProducers = value;
                RaisePropertyChanged(nameof(FilterProducers));
            }
        }

        private RelayCommand _filterProducersDataCommand;
        public RelayCommand FilterProducersDataCommand { get => _filterProducersDataCommand; }

        private void FilterProducersData()
        {
            if (string.IsNullOrEmpty(_filterProducers))
            {
                viewProducers.Filter = null;
            }
            else
            {
                viewProducers.Filter = p => ((ProducerViewModel)p).Name.Contains(_filterProducers);
            }
        }

        private void UndoProducersChanges()
        {
            if (EditedProducer.Id != 0)
            {
                dao.UndoChanges();
                IProducer producer = dao.GetAllProducers().First(p => p.Id == EditedProducer.Id);
                int index = Producers.IndexOf(EditedProducer);
                Producers[index] = new ProducerViewModel(producer);
            }
            EditedProducer = null;
        }
        private RelayCommand _undoProducersChangesCommand;
        public RelayCommand UndoProducersChangesCommand
        {
            get => _undoProducersChangesCommand;
        }
    }
}
