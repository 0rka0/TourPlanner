namespace TourPlannerModels.TourObject
{
    public class Attraction : ITourObject
    {
        public int Id { get; set; }
        public string Pid { get; set; }
        public string Name { get; set; }
        public float Rating { get; set; }
        public int TotalRatings { get; set; }
        public string Address { get; set; }
        public int TourId { get; set; }

        public Attraction()
        { }

        public Attraction(int id, string pid, string name, float rating, int totalR, string address, int tid)
        {
            Id = id;
            Pid = pid;
            Name = name;
            Rating = rating;
            TotalRatings = totalR;
            Address = address;
            TourId = tid;
        }

        public Attraction(string pid, string name, float rating, int totalR, string address, int tid)
        {
            Pid = pid;
            Name = name;
            Rating = rating;
            TotalRatings = totalR;
            Address = address;
            TourId = tid;
        }
    }
}
