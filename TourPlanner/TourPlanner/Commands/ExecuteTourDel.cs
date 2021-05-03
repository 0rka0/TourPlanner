using TourPlannerBL.TourObjectHandling;
using TourPlanner.Viewmodels;

namespace TourPlanner.Commands
{
    class ExecuteTourDel : ExecuteSelectedTourBase 
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
