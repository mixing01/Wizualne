using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;

namespace GamesDesktopApp.ViewModels
{
    public class GameViewModel : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        // _game jest obiektem który przechowuje dane, które są udostępniane na zewnątrz za pomocą własności(property)
        private IGame _game;
        public IGame game => _game;

        public GameViewModel(IGame game)
        {
            _game = game;
            _isChanged = false;
        }
        private void RaisePropertyChanged( string propertyName)
        {
            if ( PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            if (propertyName != nameof(HasErrors))
                Validate();
        }
        [Required]
        public GameGenre Genre
        {
            get => _game.Genre;
            set
            {
                IsChanged = true;
                _game.Genre = value;
                RaisePropertyChanged(nameof(Genre));
            }
        }

        // Adnotacje typu [Required] są wykorzystywane do obsługi błędów
        [Required]
        [Range(1900, 2025, ErrorMessage = "Invalid release year")]
        public int ReleaseYear
        {
            get => _game.ReleaseYear;
            set
            {
                IsChanged = true;
                _game.ReleaseYear = value;
                RaisePropertyChanged(nameof(ReleaseYear));
            }
        }
        [Required]
        [Range(0, 10, ErrorMessage = "Invalid rating")]
        public int Rating
        {
            get => _game.Rating;
            set
            {
                IsChanged = true;
                _game.Rating = value;
                RaisePropertyChanged(nameof(Rating));
            }
        }
        [Required]
        [Range(0, 10000, ErrorMessage = "Invalid price")]
        public double Price
        {
            get => _game.Price;
            set
            {
                IsChanged = true;
                _game.Price = Math.Round(value, 2);
                RaisePropertyChanged(nameof(Price));
            }
        }
        [Required]
        [Range(0.1, 1000, ErrorMessage = "Invalid disk space")]
        public double DiskSpace
        {
            get => _game.DiskSpace;
            set
            {
                IsChanged = true;
                _game.DiskSpace = Math.Round(value, 2);
                RaisePropertyChanged(nameof(DiskSpace));
            }
        }
        [Required]
        public IProducer Producer
        {
            get => _game.Producer;
            set
            {
                IsChanged = true;
                _game.Producer = value;
                RaisePropertyChanged( nameof(Producer) );
            }
        }
        [Required(ErrorMessage = "Name field cannot be empty")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Name length must be between 1 and 100 characters")]
        public string Name
        {
            get { return _game.Name; }
            set
            {
                IsChanged = true;
                _game.Name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }

        public int Id
        {
            get => _game.Id;
            set
            {
                _game.Id = value;
                RaisePropertyChanged(nameof(Id));
            }
        }

        // #region pozwala na zaznaczenie obszaru w edytorze. Nie wpływa na kod
        #region INotyifyDataErrorInfo Implementation
        

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        public bool HasErrors => errorsCollection.Count > 0;
        public IEnumerable GetErrors(string? propertyName)
        {
            if (string.IsNullOrEmpty(propertyName) || !errorsCollection.ContainsKey(propertyName))
                return null;
            return errorsCollection[propertyName];
        }

        protected void RiseErrorChange(string propertyName)
        {
            if(ErrorsChanged != null)
            {
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
                RaisePropertyChanged(nameof(HasErrors));
            }
        }

        private Dictionary<string, ICollection<string>> errorsCollection = new Dictionary<string, ICollection<string>>();

        public void Validate()
        {
            var validationContext = new ValidationContext(this, null, null);
            var validationResults = new List<ValidationResult>();

            Validator.TryValidateObject(this, validationContext, validationResults, true);

            foreach (var kv in errorsCollection.ToList())
            {
                if(validationResults.All(r => r.MemberNames.All(m => m != kv.Key)))
                {
                    errorsCollection.Remove(kv.Key);
                    RiseErrorChange(kv.Key);

                }
            }

            var q = from result in validationResults
                    from member in result.MemberNames
                    group result by member into gr
                    select gr;

            foreach (var prop in q)
            {
                var messages = prop.Select(r => r.ErrorMessage).ToList();

                if (errorsCollection.ContainsKey(prop.Key))
                {
                    errorsCollection.Remove(prop.Key);
                }
                errorsCollection.Add(prop.Key, messages);
                RiseErrorChange(prop.Key);
            }
        }

        #endregion

        private bool _isChanged;
        public bool IsChanged
        {
            get { return _isChanged; }
            set
            {
                _isChanged = value;
                RaisePropertyChanged(nameof(IsChanged));
            }
        }
    }
}
