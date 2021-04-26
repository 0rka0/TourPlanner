﻿using System;
using System.Windows.Input;
using TourPlannerBL;

namespace TourPlanner
{
    class ExecuteDel : ICommand 
    {
        private readonly ViewModel _viewModel;
        public ExecuteDel(ViewModel viewModel)
        {
            _viewModel = viewModel;

            _viewModel.PropertyChanged += (sender, args) =>
            {
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            };
        }

        public bool CanExecute(object? parameter)
        {
            if(_viewModel.CurTour != null)
            {
                return true;
            }
            return false;
        }

        public void Execute(object? parameter)
        {
            TourHandler.DeleteTour(_viewModel.CurTour.Id);
        }

        public event EventHandler? CanExecuteChanged;
    }
}
