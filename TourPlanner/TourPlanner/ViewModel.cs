using System.Diagnostics;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace TourPlanner
{
    class ViewModel : INotifyPropertyChanged
    {
        private string _output = "";
        private string _filter;
        private string _start;
        private string _end;

        public string FilterInput
        {
            get
            {
                return _filter;
            }
            set
            {
                if (FilterInput != value)
                {
                    _filter = value;
                    OnPropertyChanged(nameof(FilterInput));
                }
            }
        }

        public string StartInput
        {
            get
            {
                return _start;
            }
            set
            {
                if (StartInput != value)
                {
                    _start = value;
                    OnPropertyChanged(nameof(StartInput));
                }
            }
        }

        public string EndInput
        {
            get
            {
                return _end;
            }
            set
            {
                if (EndInput != value)
                {
                    _end = value;
                    OnPropertyChanged(nameof(EndInput));
                }
            }
        }

        public string FilterOutput
        {
            get
            {
                return _output;
            }
            set
            {
                if(_output != value)
                {
                    _output = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand ExecuteSearch { get; }

        public ICommand ExecuteAdd { get; }

        public ICommand ExecuteDel { get; }

        public ICommand ExecuteEdit { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public ViewModel()
        {
            this.ExecuteSearch = new ExecuteSearch(this);
            this.ExecuteAdd = new ExecuteAdd(this);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
