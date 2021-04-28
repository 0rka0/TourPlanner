using TourPlannerModels;
using TourPlannerDAL;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace TourPlannerBL
{
    public static class PdfCreator
    {
        public static void CreatePdf()
        {
            PdfModel model = PdfDataSource.GetDetails();
            IDocument document = new PdfDocument(model);
            document.GeneratePdf($"{Configuration.ReportPath}{StringPreparer.BuildPdfName(model.CreationDate)}.pdf");
        }
    }
}
