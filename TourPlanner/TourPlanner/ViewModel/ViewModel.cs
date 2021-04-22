using System.Diagnostics;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Collections.ObjectModel;
using TourPlannerBL;
using TourPlannerDAL;

namespace TourPlanner
{
    class ViewModel : INotifyPropertyChanged
    {
        private string _output = "";
        private string _filter;
        private string _start;
        private string _end;

        private ObservableCollection<Tour> _tourList = new ObservableCollection<Tour>();

        public ObservableCollection<Tour> TourList
        {
            get 
            {
                return _tourList;
            }
            set
            {
                if (_tourList != value)
                {
                    _tourList = value;
                    OnPropertyChanged();
                }
            }
        }

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

        public ICommand ExecuteImport { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public ViewModel()
        {
            this.ExecuteSearch = new ExecuteSearch(this);
            this.ExecuteAdd = new ExecuteAdd(this);
            this.ExecuteDel = new ExecuteDel(this);
            this.ExecuteEdit = new ExecuteEdit(this);
            this.ExecuteImport = new ExecuteImport(this);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
