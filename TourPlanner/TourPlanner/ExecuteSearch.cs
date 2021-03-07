using System.Diagnostics;
using System;
using System.Windows.Input;

namespace TourPlanner
{
    class ExecuteSearch : ICommand
    {
        private readonly ViewModel _viewModel;

        public ExecuteSearch(ViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.PropertyChanged += (sender, args) =>
            {
                Debug.Print("cmd: recv prop changed");
                if (args.PropertyName == "Input")
                {
                    Debug.Print("cmd: recv prop changed: Input");
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
            };
        }

        public bool CanExecute(object? parameter)
        {
            Debug.Print("cmd: can exec");
            return !string.IsNullOrWhiteSpace(_viewModel.Input);
        }

        public void Execute(object? paramter)
        {
            Debug.Print("cmd: exec");
            _viewModel.Output = "filter: " + _viewModel.Input;
            TmpFilter(_viewModel.Input);
            _viewModel.Input = string.Empty;
        }

        public void TmpFilter(string filter)
        {
            Debug.Print("fitler for " + filter);
        }

        public event EventHandler? CanExecuteChanged;
    }
}
