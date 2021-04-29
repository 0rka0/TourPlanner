using System;
using System.Windows.Input;
using TourPlannerBL;

namespace TourPlanner
{
    class ExecuteTourCopy : ExecuteSelectedItemsBase
    {
        public ExecuteTourCopy(TourVM viewModel) : base(viewModel)
        {
        }

        public override void Execute(object? parameter)
        {
            TourHandler.CopyTour(_viewModel.CurTour);
            _viewModel.RefreshTourList();
        }
    }
}
