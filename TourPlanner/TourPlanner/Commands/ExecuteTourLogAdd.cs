using System;
using System.Windows.Input;
using TourPlannerBL;

namespace TourPlanner
{
    class ExecuteTourLogAdd : ExecuteSelectedItemsBase
    {
        public ExecuteTourLogAdd(TourVM viewModel) : base(viewModel)
        {
        }

        public override void Execute(object? parameter)
        {
            TourLogHandler.AddTourLog(_viewModel.CurTour.Id);
        }
    }
}
