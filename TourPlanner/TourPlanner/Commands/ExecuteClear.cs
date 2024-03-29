﻿using TourPlanner.Viewmodels;

namespace TourPlanner.Commands
{
    class ExecuteClear : ExecuteNoConditionBase
    {
        public ExecuteClear(MainViewModel viewModel) : base(viewModel)
        {
        }

        public override void Execute(object? paramter)
        {
            _viewModel.FilterOutput = string.Empty;
            _viewModel.FilterInput = string.Empty;
            _viewModel.ErrorOutput = string.Empty;
            Clear();
        }

        public void Clear()
        {
            _viewModel.RefreshTourList();
        }
    }
}
