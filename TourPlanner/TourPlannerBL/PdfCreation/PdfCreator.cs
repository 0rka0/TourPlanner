using TourPlannerModels;
using TourPlannerDAL.PDF;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using log4net;
using System.Reflection;
using System;
using TourPlannerBL.StringPrep;
using TourPlannerModels.TourObject;

namespace TourPlannerBL.PDF
{
    public static class PdfCreator
    {
        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static void CreateSummary()
        {
            _logger.Info("Attempting to create the summary");

            try
            {
                PdfModel model = PdfDataSource.GetDetailsAllTours();
                IDocument document = new TourSummary(model);
                document.GeneratePdf($"{Configuration.ReportPath}{StringPreparer.BuildSummaryName(model.CreationDate)}");
            }
            catch (Exception e)
            {
                _logger.Error("Creation process led to following error: " + e.Message);
            }
        }

        public static void CreateTourReport(Tour tour)
        {
            _logger.Info("Attempting to create a tour report");

            try
            {
                PdfModel model = PdfDataSource.GetDetailsSingleTour(tour);
                IDocument document = new TourReport(model);
                document.GeneratePdf($"{Configuration.ReportPath}{StringPreparer.BuildReportName(model.CreationDate)}");
            }
            catch (Exception e)
            {
                _logger.Error("Creation process led to following error: " + e.Message);
            }
        }
    }
}
