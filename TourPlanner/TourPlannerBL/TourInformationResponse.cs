using System;


namespace TourPlannerBL
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Lr
    {
        public double lng { get; set; }
        public double lat { get; set; }
    }

    public class Ul
    {
        public double lng { get; set; }
        public double lat { get; set; }
    }

    public class BoundingBox
    {
        public Lr lr { get; set; }
        public Ul ul { get; set; }
    }

    public class Route
    {
        public BoundingBox boundingBox { get; set; }
        public double distance { get; set; }
        public string formattedTime { get; set; }
        public string sessionId { get; set; }
        public int time { get; set; }
    }

    public class TourInformationResponse
    {
        readonly string _size = "size=640,480";
        readonly string _defaultMarker = "defaultMarker=none";
        readonly string _zoom = "zoom=11";
        readonly string _rand = "rand=737758036";

        public Route route { get; set; }

        public string ReturnString()
        {
            string returnString = String.Format("{0}&{1}&{2}&{3}&session={4}&boundingBox={5},{6},{7},{8}", 
                _size, _defaultMarker, _zoom, _rand, route.sessionId, route.boundingBox.ul.lat.ToString().Replace(",","."), 
                route.boundingBox.ul.lng.ToString().Replace(",", "."), route.boundingBox.lr.lat.ToString().Replace(",", "."), route.boundingBox.lr.lng.ToString().Replace(",", "."));

            return returnString;
        }
    }


}
