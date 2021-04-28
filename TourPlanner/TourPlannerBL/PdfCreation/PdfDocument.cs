﻿using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using TourPlannerModels;

namespace TourPlannerBL
{
    class PdfDocument : IDocument
    {
        public PdfModel Model { get; }

        public PdfDocument(PdfModel model)
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
                    foreach (Tour tour in Model.Tours)
                    {
                        stack.Element().BorderBottom(1).BorderColor("000").Padding(5).Row(row =>
                        {
                            row.RelativeColumn().Text($"ID: {tour.Id}");
                            row.RelativeColumn().Text($"Tourname: {tour.Name}");
                            row.RelativeColumn().Text($"Distance: {tour.Distance} km");
                        });

                        if (tour.LogList.Count > 0)
                        {
                            stack.Element().BorderBottom(1).BorderColor("000").Padding(5).Row(row =>
                            {
                                row.RelativeColumn().Text($"Date");
                            });
                            stack.Element().BorderBottom(1).BorderColor("CCC").Padding(5);

                            foreach (TourLog log in tour.LogList)
                            {
                                stack.Element().BorderBottom(1).BorderColor("CCC").Padding(5).Row(row =>
                                {
                                    row.RelativeColumn().Text(log.Date);
                                });
                            }

                            stack.Element().BorderBottom(1).BorderColor("000").Padding(5);
                        }
                    }
                });
            });
        }
    }
}