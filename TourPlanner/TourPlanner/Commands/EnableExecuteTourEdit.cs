using System;
using System.Windows.Input;
using TourPlanner.Viewmodels;

namespace TourPlanner.Commands
{
    class EnableExecuteTourEdit : ExecuteSelectedTourBase
    {
        public EnableExecuteTourEdit(MainViewModel viewModel) : base(viewModel)
        {
        }

        public override void Execute(object? parameter)
        {
        }
    }
}
