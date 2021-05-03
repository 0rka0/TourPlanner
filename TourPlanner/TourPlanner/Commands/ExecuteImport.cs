﻿using System;
using System.Windows;
using System.Windows.Input;
using TourPlanner.Viewmodels;


namespace TourPlanner.Commands
{
    class ExecuteImport : ICommand
    {
        private readonly TourVM _viewModel;
        public ExecuteImport(TourVM viewModel)
        {
            _viewModel = viewModel;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            MessageBox.Show("Import something");
        }

        public event EventHandler? CanExecuteChanged;
    }
}
