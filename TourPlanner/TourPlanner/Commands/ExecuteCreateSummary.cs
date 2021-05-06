using System;
using System.Windows.Input;
using TourPlanner.Viewmodels;
using TourPlannerBL.PDF;

namespace TourPlanner.Commands
{
    class ExecuteCreateSummary : ICommand
    {
        private readonly TourVM _viewModel;

        public ExecuteCreateSummary(TourVM viewmodel)
        {
            _viewModel = viewmodel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            PdfCreator.CreateSummary();
        }

        public event EventHandler CanExecuteChanged;
    }
}
