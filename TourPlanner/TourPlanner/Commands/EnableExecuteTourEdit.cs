using System;
using System.Windows.Input;
using TourPlanner.Viewmodels;

namespace TourPlanner.Commands
{
    class EnableExecuteTourEdit : ExecuteSelectedItemsBase
    {
        public EnableExecuteTourEdit(TourVM viewModel) : base(viewModel)
        {
        }

        public override void Execute(object? parameter)
        {
        }
    }
}
