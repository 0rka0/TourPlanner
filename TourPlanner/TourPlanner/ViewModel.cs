using System.Diagnostics;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace TourPlanner
{
    class ViewModel : INotifyPropertyChanged
    {
        private string _output = "";
        private string _input;

        public string Input
        {
            get
            {
                Debug.Print("reading input");
                return _input;
            }
            set
            {
                Debug.Print("writing input");
                if (Input != value)
                {
                    Debug.Print("setting input value");
                    _input = value;

                    Debug.Print("firing propertyChanged: input");
                    OnPropertyChanged(nameof(Input));
                }
            }
        }

        public string Output
        {
            get
            {
                Debug.Print("reading output");
                return _output;
            }
            set
            {
                Debug.Print("writing output");
                if(_output != value)
                {
                    Debug.Print("setting output value");
                    _output = value;

                    Debug.Print("firing propertyChanged: output");
                    OnPropertyChanged();
                }
            }
        }

        public ICommand ExecuteSearch { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public ViewModel()
        {
            this.ExecuteSearch = new ExecuteSearch(this);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            Debug.Print("prop changed: " + propertyName);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
