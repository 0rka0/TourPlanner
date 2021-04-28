using System;
using System.Collections.Generic;

namespace TourPlannerModels
{
    public class PdfModel
    {
        public DateTime CreationDate { get; set; }
        public List<Tour> Tours { get; set; }

        public PdfModel()
        {
            Tours = new List<Tour>();
            CreationDate = DateTime.Now;
        }
    }
}
