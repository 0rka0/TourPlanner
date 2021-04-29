using TourPlannerModels;

namespace TourPlannerDAL
{
    public static class PdfDataSource
    {
        public static PdfModel GetDetails()
        {
            IDatabase db = TourDatabaseHandler.GetInstance();
            PdfModel pdfModel = new PdfModel();
            foreach (Tour tour in db.SelectEntries())
            {
                pdfModel.Tours.Add(tour);
            }

            return pdfModel;
        }
    }
}
