using TourPlannerBL.TourObjectHandling;
using TourPlanner.Viewmodels;
using Microsoft.Win32;

namespace TourPlanner.Commands
{
    class ExecuteExport : ExecuteNoConditionBase
    {
        public ExecuteExport(TourVM viewModel) : base(viewModel)
        {
        }

        public override void Execute(object? parameter)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "json file (*.json)|*.json";
            saveFileDialog.FilterIndex = 1;
            if(saveFileDialog.ShowDialog() == true)
                TourHandler.ExportTours(saveFileDialog.FileName);                        
        }
    }
}
