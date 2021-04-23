using System;
using System.Windows.Input;
using System.Collections.Generic;
using TourPlannerModels;

namespace TourPlanner
{
    class ExecuteClear : ICommand
    {
        private readonly ViewModel _viewModel;

        public ExecuteClear(ViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? paramter)
        {
            _viewModel.FilterOutput = string.Empty;
            Clear();
        }

        public void Clear()
        {
            _viewModel.TourList.Clear();
            _viewModel.FillTourList();          //to be reworked, will be transferred to viewmodel so that methods can remain private
        }

        public event EventHandler? CanExecuteChanged;
    }
}
