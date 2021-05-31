using System;
using System.Windows.Input;
using System.Collections.Generic;
using TourPlannerBL.TourObjectHandling;
using TourPlannerModels.TourObject;
using TourPlanner.Viewmodels;

namespace TourPlanner.Commands
{
    class ExecuteSearch : ICommand
    {
        private readonly MainViewModel _viewModel;

        public ExecuteSearch(MainViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "FilterInput")
                {
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
            };
        }

        public bool CanExecute(object? parameter)
        {
            return !string.IsNullOrWhiteSpace(_viewModel.FilterInput);
        }

        public void Execute(object? paramter)
        {
            _viewModel.FilterOutput = "filter: " + _viewModel.FilterInput;
            Search(_viewModel.FilterInput);
            _viewModel.FilterInput = string.Empty;
        }

        public void Search(string filter)
        {
            IEnumerable<Tour> foundTours = TourSelector.Search(filter); 
            _viewModel.TourList.Clear();
            
            foreach(Tour tour in foundTours)
            {
                _viewModel.TourList.Add(tour);   //to be reworked, will be transferred to viewmodel so that methods can remain private
            }
        }

        public event EventHandler? CanExecuteChanged;
    }
}
