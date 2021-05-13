using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using TourPlannerModels.TourObject;
using TourPlannerModels;

namespace TourPlannerBL.PDF
{
    class TourSummary : IDocument
    {
        public PdfModel Model { get; }

        public TourSummary(PdfModel model)
        {
            Model = model;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IContainer container)
        {
            container
                .PaddingHorizontal(50)
                .PaddingVertical(50)
                .Page(page =>
                {
                    page.Header(ComposeHeader);
                    page.Content(ComposeContent);
                    page.Footer().AlignCenter().PageNumber("Page {number}");
                });
        }

        void ComposeHeader(IContainer container)
        {
            container.Row(row =>
            {
                row.RelativeColumn().Stack(stack =>
                {
                    stack.Element().Text($"TourPlanner Summary", TextStyle.Default.Size(20));
                    stack.Element().Text($"Creation Date: {Model.CreationDate:d}");
                });
            });
        }

        void ComposeContent(IContainer container)
        {
            container.PaddingVertical(40).PageableStack(stack =>
            {
                stack.Spacing(5);
                stack.Element(ComposeTable);
            });
        }

        void ComposeTable(IContainer container)
        {
            float totalTime = 0;
            float totalDistance = 0;
            float timeByTour;
            float distanceByTour;

            container.PaddingTop(10).Section(section =>
            {
                section.Content().PageableStack(stack =>
                {
                    foreach (Tour tour in Model.Tours)
                    {
                        timeByTour = 0;
                        distanceByTour = 0;
                        stack.Element().BorderBottom(1).BorderColor("CCC").Padding(5).Row(row =>
                        {
                            row.RelativeColumn().Text($"ID: {tour.Id}");
                            row.RelativeColumn().Text($"Tourname: {tour.Name}");
                            row.RelativeColumn().Text($"Distance: {tour.Distance} km");
                        });

                        foreach (TourLog log in tour.LogList)
                        {
                            timeByTour += log.TotalTime;
                            distanceByTour += log.Distance;
                        }

                        stack.Element().Padding(5).Text($"Time spent on this tour: {timeByTour} h");
                        stack.Element().Padding(5).Text($"Distance travelled on this tour: {distanceByTour} km");

                        totalTime += timeByTour;
                        totalDistance += distanceByTour;

                        stack.Element().BorderBottom(1).BorderColor("000").Padding(5);
                    }

                    stack.Element().Padding(5).Text($"Total time spent on tours: {totalTime} h");
                    stack.Element().Padding(5).Text($"Total Distance travelled on tours: {totalDistance} km");
                });
            });
        }
    }
}
