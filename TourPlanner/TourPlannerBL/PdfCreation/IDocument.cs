using QuestPDF.Drawing;
using QuestPDF.Infrastructure;

namespace TourPlannerBL
{
    public interface IDocuments
    {
        DocumentMetadata GetMetadata();
        void Compose(IContainer container);
    }
}
