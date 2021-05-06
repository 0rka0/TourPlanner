using System.Collections.Generic;
using TourPlannerModels.TourObject;
using TourPlannerModels;
using TourPlannerDAL.Databases;

namespace TourPlannerDAL.PDF
{
    public static class PdfDataSource
    {
        public static PdfModel GetDetailsAllTours()
        {
            IDatabase db = TourDatabaseHandler.GetInstance();
            PdfModel pdfModel = new PdfModel();
            IEnumerable<ITourObject> tourList = db.SelectEntries();
            db = TourLogDatabaseHandler.GetInstance();
            foreach (Tour tour in tourList)
            {
                tour.LogList.Clear();
                tour.LogList.AddRange((IEnumerable<TourLog>)db.SelectEntries(tour.Id));
                pdfModel.Tours.Add(tour);
            }

            return pdfModel;
        }

        public static PdfModel GetDetailsSingleTour(Tour curTour)
        {
            PdfModel pdfModel = new PdfModel();
            IDatabase db = TourLogDatabaseHandler.GetInstance();
            curTour.LogList.Clear();
            curTour.LogList.AddRange((IEnumerable<TourLog>)db.SelectEntries(curTour.Id));
            pdfModel.Tours.Add(curTour);

            return pdfModel;
        }
    }
}
