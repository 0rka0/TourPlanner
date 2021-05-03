using System;
using System.Windows.Input;
using TourPlanner.Viewmodels;

namespace TourPlanner.Commands
{
    abstract class ExecuteSelectedTourBase : ICommand
    {
        protected readonly TourVM _viewModel;

        public ExecuteSelectedTourBase(TourVM viewModel)
        {
            _viewModel = viewModel;

            _viewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "CurTour")
                {
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
            };
        }

        public bool CanExecute(object parameter)
        {
            if (_viewModel.CurTour != null)
            {
                return true;
            }
            return false;
        }

        public abstract void Execute(object parameter);

        public event EventHandler? CanExecuteChanged;
    }
}
