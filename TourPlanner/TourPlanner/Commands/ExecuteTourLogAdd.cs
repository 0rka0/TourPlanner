using TourPlannerBL.TourObjectHandling;
using TourPlanner.Viewmodels;

namespace TourPlanner.Commands
{
    class ExecuteTourLogAdd : ExecuteSelectedTourBase
    {
        public ExecuteTourLogAdd(MainViewModel viewModel) : base(viewModel)
        {
        }

        public override void Execute(object? parameter)
        {
            TourLogHandler.AddNewTourLog(_viewModel.CurTour.Id);

            _viewModel.RefreshLogList();
        }
    }
}
