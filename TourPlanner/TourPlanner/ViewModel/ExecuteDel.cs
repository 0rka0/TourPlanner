using System;
using System.Windows.Input;

namespace TourPlanner
{
    class ExecuteDel : ICommand 
    {
        private readonly ViewModel _viewModel;
        public ExecuteDel(ViewModel viewModel)
        {
            _viewModel = viewModel;
            
            //get selected tour
        }

        public bool CanExecute(object? parameter)
        {
            //if a tour is selected
            return true;
        }

        public void Execute(object? parameter)
        {
            //Delete selected tour
        }

        public event EventHandler? CanExecuteChanged;
    }
}
