using Microsoft.Win32;
using System.Windows;
using TourPlanner.Viewmodels;
using TourPlannerBL.TourObjectHandling;

namespace TourPlanner.Commands
{
    class ExecuteImport : ExecuteNoConditionBase
    {
        public ExecuteImport(TourVM viewModel) : base(viewModel)
        {
        }

        public override void Execute(object? parameter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "json files (*.json)|*.json";
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == true)
                TourHandler.ImportTours(openFileDialog.FileName);

            _viewModel.RefreshTourList();
        }
    }
}
