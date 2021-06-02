using System;
using System.Windows.Input;
using TourPlannerBL.TourObjectHandling;
using TourPlanner.Viewmodels;
using System.Windows;

namespace TourPlanner.Commands
{
    class ExecuteTourAdd : ICommand
    {
        private readonly MainViewModel _viewModel;

        public ExecuteTourAdd(MainViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "StartInput" || args.PropertyName == "EndInput")
                {
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
            };
        }

        public bool CanExecute(object? parameter)
        {
            return (!string.IsNullOrWhiteSpace(_viewModel.StartInput) && !string.IsNullOrWhiteSpace(_viewModel.EndInput));
        }

        public void Execute(object? parameter)
        {
            //Add a tour
            string message = TourHandler.AddTour(_viewModel.StartInput, _viewModel.EndInput, _viewModel.DescriptionInput, _viewModel.InformationInput);

            if (!String.IsNullOrEmpty(message))
            {
                _viewModel.ErrorOutput = "Error - Tour could not be added with message: " + message;
            }                

            _viewModel.RefreshTourList();

            _viewModel.StartInput = string.Empty;
            _viewModel.EndInput = string.Empty;
            _viewModel.DescriptionInput = string.Empty;
            _viewModel.InformationInput = string.Empty;
        }

        public event EventHandler? CanExecuteChanged;
    }
}
