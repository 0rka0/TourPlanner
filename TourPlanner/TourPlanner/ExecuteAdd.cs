using System.Diagnostics;
using System;
using System.Windows.Input;

namespace TourPlanner
{
    class ExecuteAdd : ICommand
    {
        private readonly ViewModel _viewModel;

        public ExecuteAdd(ViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "StartInput" || args.PropertyName == "EndInput")
                {
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
            };

        }
        public bool CanExecute(object? parameter)
        {
            return (!string.IsNullOrWhiteSpace(_viewModel.StartInput) && !string.IsNullOrWhiteSpace(_viewModel.EndInput));
        }

        public void Execute(object? paramter)
        {
            Debug.Print(_viewModel.StartInput);
            Debug.Print(_viewModel.EndInput);
            //Add a tour
            _viewModel.StartInput = string.Empty;
            _viewModel.EndInput = string.Empty;
        }

        public event EventHandler? CanExecuteChanged;
    }
}
