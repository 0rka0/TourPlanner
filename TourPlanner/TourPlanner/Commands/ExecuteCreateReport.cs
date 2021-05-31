using System;
using System.Windows.Input;
using TourPlannerBL.PDF;
using TourPlanner.Viewmodels;

namespace TourPlanner.Commands
{
    class ExecuteCreateReport : ExecuteSelectedTourBase
    {
        public ExecuteCreateReport(MainViewModel viewModel) : base(viewModel)
        {
        }

        public override void Execute(object parameter)
        {
            PdfCreator.CreateTourReport(_viewModel.CurTour);
        }
    }
}
