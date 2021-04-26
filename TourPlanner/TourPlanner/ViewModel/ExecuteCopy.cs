using System;
using System.Windows.Input;
using TourPlannerBL;

namespace TourPlanner
{
    class ExecuteCopy : ICommand
    {
        private readonly ViewModel _viewModel;
        public ExecuteCopy(ViewModel viewModel)
        {
            _viewModel = viewModel;

            _viewModel.PropertyChanged += (sender, args) =>
            {
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            };
        }

        public bool CanExecute(object? parameter)
        {
            if (_viewModel.CurTour != null)
            {
                return true;
            }
            return false;
        }

        public void Execute(object? parameter)
        {
            TourHandler.CopyTour(_viewModel.CurTour.Id);
        }

        public event EventHandler? CanExecuteChanged;
    }
}
