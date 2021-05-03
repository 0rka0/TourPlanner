﻿using TourPlannerBL;
using TourPlanner.Viewmodels;

namespace TourPlanner.Commands
{
    class ExecuteTourCopy : ExecuteSelectedTourBase
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
