using System;
using System.Windows.Input;
using TourPlannerBL;

namespace TourPlanner
{
    class ExecuteEdit : ICommand
    {
        private readonly ViewModel _viewModel;
        public ExecuteEdit(ViewModel viewModel)
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
            //Edit selected tour
        }

        public event EventHandler? CanExecuteChanged;
    }
}
