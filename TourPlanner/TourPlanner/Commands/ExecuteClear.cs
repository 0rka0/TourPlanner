using System;
using System.Windows.Input;

namespace TourPlanner
{
    class ExecuteClear : ICommand
    {
        private readonly TourVM _viewModel;

        public ExecuteClear(TourVM viewModel)
        {
            _viewModel = viewModel;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? paramter)
        {
            _viewModel.FilterOutput = string.Empty;
            Clear();
        }

        public void Clear()
        {
            _viewModel.RefreshTourList();
        }

        public event EventHandler? CanExecuteChanged;
    }
}
