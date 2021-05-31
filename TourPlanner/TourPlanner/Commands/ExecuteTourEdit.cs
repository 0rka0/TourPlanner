using System;
using System.Windows.Input;
using TourPlanner.Viewmodels;
using TourPlannerBL.TourObjectHandling;

namespace TourPlanner.Commands
{
    class ExecuteTourEdit : ICommand
    {
        private readonly MainViewModel _viewModel;
        public ExecuteTourEdit(MainViewModel viewModel)
        {
            _viewModel = viewModel;

            _viewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "StartInput" || args.PropertyName == "DescriptionInput" || args.PropertyName == "InformationInput")
                {
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
            };
        }

        public bool CanExecute(object? parameter)
        {
            return (!string.IsNullOrWhiteSpace(_viewModel.StartInput) || !string.IsNullOrWhiteSpace(_viewModel.DescriptionInput) || !string.IsNullOrWhiteSpace(_viewModel.InformationInput));
        }

        public void Execute(object? parameter)
        {
            TourHandler.EditTour(_viewModel.StartInput, _viewModel.DescriptionInput, _viewModel.InformationInput, _viewModel.CurTour);

            _viewModel.RefreshTourList();

            _viewModel.StartInput = string.Empty;
            _viewModel.DescriptionInput = string.Empty;
            _viewModel.InformationInput = string.Empty;
        }

        public event EventHandler? CanExecuteChanged;
    }
}
