using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using TourPlannerModels.TourObject;
using TourPlannerModels;

namespace TourPlannerBL.PDF
{
    public class TourReport : IDocument
    {
        public PdfModel Model { get; }
        private Tour tour;

        public TourReport(PdfModel model)
        {
            Model = model;
            tour = Model.Tours[0];
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
                    stack.Element().Text($"TourPlanner Report", TextStyle.Default.Size(20));
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
            container.PaddingTop(10).Section(section =>
            {
                section.Content().PageableStack(stack =>
                {
                    stack.Element().BorderBottom(1).BorderColor("000").Padding(5).Row(row =>
                    {
                        row.RelativeColumn().PageableStack(stack =>
                        {
                            stack.Element().BorderBottom(1).BorderColor("CCC").Padding(5).Text($"ID: {tour.Id}");
                            stack.Element().BorderBottom(1).BorderColor("CCC").Padding(5).Text($"Tourname: {tour.Name}");
                            stack.Element().BorderBottom(1).BorderColor("CCC").Padding(5).Text($"Distance: {tour.Distance} km");
                            stack.Element().Height(100).AlignBottom().Padding(5).Text($"Logs: ");
                        });
                        row.RelativeColumn().Image(System.IO.File.ReadAllBytes(Configuration.ImagePath + tour.Image));
                    });

                    if (tour.LogList.Count > 0)
                    {
                        stack.Element().BorderBottom(1).BorderColor("CCC").Padding(10).Row(row =>
                        {
                            row.RelativeColumn(1.5f).AlignCenter().Text($"Date");
                            row.RelativeColumn(1).AlignCenter().Text($"Distance");
                            row.RelativeColumn(1).AlignCenter().Text($"Total Time");
                            row.RelativeColumn(1).AlignCenter().Text($"Rating");
                            row.RelativeColumn(1).AlignCenter().Text($"Average speed");
                            row.RelativeColumn(1).AlignCenter().Text($"Weather");
                            row.RelativeColumn(1).AlignCenter().Text($"Traffic");
                            row.RelativeColumn(1).AlignCenter().Text($"Breaks");
                            row.RelativeColumn(1).AlignCenter().Text($"Group Size");
                        });

                        foreach (TourLog log in tour.LogList)
                        {
                            stack.Element().BorderBottom(1).BorderColor("CCC").Padding(10).Row(row =>
                            {
                                row.RelativeColumn(1.5f).AlignCenter().Text(log.Date);
                                row.RelativeColumn(1).AlignCenter().Text(log.Distance + " km");
                                row.RelativeColumn(1).AlignCenter().Text(log.TotalTime + " h");
                                row.RelativeColumn(1).AlignCenter().Text(log.Rating);
                                row.RelativeColumn(1).AlignCenter().Text(log.AvgSpeed + " km/h");
                                row.RelativeColumn(1).AlignCenter().Text(log.Weather);
                                row.RelativeColumn(1).AlignCenter().Text(log.Traffic);
                                row.RelativeColumn(1).AlignCenter().Text(log.Breaks);
                                row.RelativeColumn(1).AlignCenter().Text(log.GroupSize);
                            });
                            stack.Element().BorderBottom(1).BorderColor("CCC").Padding(10).Row(row =>
                            {
                                row.RelativeColumn().AlignCenter().Text(log.Report);
                            });
                        }

                        stack.Element().BorderBottom(1).BorderColor("000").Padding(5);
                    }
                });
            });
        }

    }
}
