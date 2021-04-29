using System;
using System.Windows.Input;
using TourPlannerBL;

namespace TourPlanner
{
    class ExecuteCreateReport : ICommand
    {
        private readonly TourVM _viewModel;

        public ExecuteCreateReport(TourVM viewModel)
        {
            _viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            PdfCreator.CreatePdf();
        }

        public event EventHandler CanExecuteChanged;
    }
}
