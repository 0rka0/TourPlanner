using System;
using System.Windows.Input;
using TourPlanner.Viewmodels;
using TourPlannerBL.PDF;

namespace TourPlanner.Commands
{
    class ExecuteCreateSummary : ExecuteNoConditionBase
    {
        public ExecuteCreateSummary(MainViewModel viewModel) : base(viewModel)
        {
        }

        public override void Execute(object parameter)
        {
            PdfCreator.CreateSummary();
        }
    }
}
