using System;
using System.Windows.Input;
using TourPlanner.Viewmodels;

namespace TourPlanner.Commands
{
    abstract class ExecuteNoConditionBase : ICommand
    {
        protected readonly MainViewModel _viewModel;

        public ExecuteNoConditionBase(MainViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public abstract void Execute(object parameter);

        public event EventHandler? CanExecuteChanged;
    }
}
