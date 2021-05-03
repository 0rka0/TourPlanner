using System;
using System.Windows.Input;
using TourPlanner.Viewmodels;
using TourPlannerBL.TourObjectHandling;

namespace TourPlanner.Commands
{
    class ExecuteTourLogDel : ICommand
    {
        private readonly TourVM _viewModel;

        public ExecuteTourLogDel(TourVM viewModel)
        {
            _viewModel = viewModel;

            _viewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "CurTour" || args.PropertyName == "CurTourLog")
                {
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
            };
        }

        public bool CanExecute(object parameter)
        {
            if (_viewModel.CurTour != null && _viewModel.CurTourLog != null)
            {
                return true;
            }
            return false;
        }

        public void Execute(object? parameter)
        {
            TourLogHandler.DelTourLog(_viewModel.CurTourLog.Id);

            _viewModel.RefreshLogList();
        }

        public event EventHandler? CanExecuteChanged;
    }
}
