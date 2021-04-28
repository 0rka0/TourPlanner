using TourPlannerModels;
using TourPlannerDAL;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using log4net;
using System.Reflection;
using System;

namespace TourPlannerBL
{
    public static class PdfCreator
    {
        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static void CreatePdf()
        {
            _logger.Info("Attempting to create a report");

            try
            {
                PdfModel model = PdfDataSource.GetDetails();
                IDocument document = new PdfDocument(model);
                document.GeneratePdf($"{Configuration.ReportPath}{StringPreparer.BuildPdfName(model.CreationDate)}");
            }
            catch (Exception e)
            {
                _logger.Error("Creation process led to following error: " + e.Message);
            }
        }
    }
}
