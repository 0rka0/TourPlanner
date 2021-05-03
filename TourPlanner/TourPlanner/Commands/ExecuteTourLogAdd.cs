using TourPlannerBL.TourObjectHandling;
using TourPlanner.Viewmodels;

namespace TourPlanner.Commands
{
    class ExecuteTourLogAdd : ExecuteSelectedTourBase
    {
        public ExecuteTourLogAdd(TourVM viewModel) : base(viewModel)
        {
        }

        public override void Execute(object? parameter)
        {
            TourLogHandler.AddTourLog(_viewModel.CurTour.Id);

            _viewModel.RefreshLogList();
        }
    }
}
