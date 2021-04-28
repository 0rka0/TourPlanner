using TourPlannerModels;

namespace TourPlannerDAL
{
    public static class PdfDataSource
    {
        public static PdfModel GetDetails()
        {
            DatabaseHandler db = DatabaseHandler.GetInstance();
            PdfModel pdfModel = new PdfModel();
            foreach (Tour tour in db.SelectTourEntries())
            {
                pdfModel.Tours.Add(tour);
            }

            return pdfModel;
        }
    }
}
