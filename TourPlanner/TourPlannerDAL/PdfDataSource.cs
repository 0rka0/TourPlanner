using System.Collections.Generic;
using TourPlannerModels.TourObject;
using TourPlannerModels;
using TourPlannerDAL.Databases;

namespace TourPlannerDAL.PDF
{
    public static class PdfDataSource
    {
        public static PdfModel GetDetails()
        {
            IDatabase db = TourDatabaseHandler.GetInstance();
            PdfModel pdfModel = new PdfModel();
            IEnumerable<ITourObject> tourList = db.SelectEntries();
            db = TourLogDatabaseHandler.GetInstance();
            foreach (Tour tour in tourList)
            {
                db = TourLogDatabaseHandler.GetInstance();
                tour.LogList.AddRange((IEnumerable<TourLog>)db.SelectEntries(tour.Id));
                pdfModel.Tours.Add(tour);
            }

            return pdfModel;
        }
    }
}
