using System;
using System.Diagnostics;
using System.Windows.Input;
using TourPlannerBL;

namespace TourPlanner
{
    class ExecuteTourDel : ExecuteSelectedItemsBase 
    {
        public ExecuteTourDel(TourVM viewModel) : base(viewModel)
        {
        }

        public override void Execute(object? parameter)
        {
            TourHandler.DeleteTour(_viewModel.CurTour);
            _viewModel.RefreshTourList();
            _viewModel.CurTour = null;
        }
    }
}
