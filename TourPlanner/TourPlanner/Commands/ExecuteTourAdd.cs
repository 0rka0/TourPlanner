﻿using System;
using System.Windows.Input;
using TourPlannerBL.TourObjectHandling;
using TourPlanner.Viewmodels;

namespace TourPlanner.Commands
{
    class ExecuteTourAdd : ICommand
    {
        private readonly TourVM _viewModel;

        public ExecuteTourAdd(TourVM viewModel)
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
            TourHandler.AddTour(_viewModel.StartInput, _viewModel.EndInput, _viewModel.DescriptionInput, _viewModel.InformationInput);

            _viewModel.RefreshTourList();

            _viewModel.StartInput = string.Empty;
            _viewModel.EndInput = string.Empty;
            _viewModel.DescriptionInput = string.Empty;
            _viewModel.InformationInput = string.Empty;
        }

        public event EventHandler? CanExecuteChanged;
    }
}