using Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesDesktopApp.ViewModels
{
    public class ProducerViewModel : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        // _producer jest obiektem który przechowuje dane, które są udostępniane na zewnątrz za pomocą własności(property)
        private IProducer _producer;
        public IProducer Producer => _producer;

        public ProducerViewModel(IProducer producer)
        {
            _producer = producer;
            _isChanged = false;
        }
        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            if (propertyName != nameof(HasErrors))
                Validate();
        }
        public int Id
        {
            get => _producer.Id;
            set
            {
                _producer.Id = value;
                RaisePropertyChanged(nameof(Id));
            }
        }

        [Required(ErrorMessage = "Name field cannot be empty")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Name length must be between 1 and 100 characters")]
        public string Name
        {
            get { return _producer.Name; }
            set
            {
                IsChanged = true;
                _producer.Name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }



        // Adnotacje typu [Required] są wykorzystywane do obsługi błędów
        [Required]
        [Range(1800, 2025, ErrorMessage = "Invalid established year")]
        public int EstYear
        {
            get => _producer.EstYear;
            set
            {
                IsChanged = true;
                _producer.EstYear = value;
                RaisePropertyChanged(nameof(EstYear));
            }
        }
        [Required]
        public Continent Continent
        {
            get => _producer.Continent;
            set
            {
                IsChanged = true;
                _producer.Continent = value;
                RaisePropertyChanged(nameof(Continent));
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
            if (ErrorsChanged != null)
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
                if (validationResults.All(r => r.MemberNames.All(m => m != kv.Key)))
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
