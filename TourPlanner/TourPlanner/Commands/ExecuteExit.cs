
using System.Windows;
using TourPlanner.Viewmodels;

namespace TourPlanner.Commands
{
    class ExecuteExit : ExecuteNoConditionBase
    {
        public ExecuteExit(TourVM viewModel) : base(viewModel)
        { }

        public override void Execute(object parameter)
        {
            Application.Current.MainWindow.Close();
        }
    }
}
