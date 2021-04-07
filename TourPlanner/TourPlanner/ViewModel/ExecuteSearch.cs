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
                if (args.PropertyName == "FilterInput")
                {
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
            };
        }

        public bool CanExecute(object? parameter)
        {
            return !string.IsNullOrWhiteSpace(_viewModel.FilterInput);
        }

        public void Execute(object? paramter)
        {
            _viewModel.FilterOutput = "filter: " + _viewModel.FilterInput;
            TmpFilter(_viewModel.FilterInput);
            _viewModel.FilterInput = string.Empty;
        }

        public void TmpFilter(string filter)
        {
            Debug.Print("fitler for " + filter);
        }

        public event EventHandler? CanExecuteChanged;
    }
}
