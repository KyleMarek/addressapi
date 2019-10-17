using System.Collections.Generic;

namespace MassAddress.Models.Google
{
    public class GooglePlacesModel
    {
        public IEnumerable<Candidates> candidates { get; set; } = new List<Candidates>();
    }

    public class Candidates
    {
        public string formatted_address { get; set; }
        public Geometry geometry { get; set; }
        public string name { get; set; }
        public OpeningHours opening_hours { get; set; }
        public Photo[] photos{ get; set; }
        public decimal rating { get; set; }
    }
    public class Geometry
    {
        public Location location { get; set; }
        public Viewport viewport { get; set; }
    }
    public class Location
    {
        public decimal lat { get; set; }
        public decimal lng { get; set; }
    }
    public class Viewport
    {
        public Location northeast { get; set; }
        public Location southwest { get; set; }
    }
    public class OpeningHours
    {
        public bool open_now { get; set; }
    }
    public class Photo
    {
        public decimal height { get; set; }
        public decimal width { get; set; }
        public string photo_reference { get; set; }
    }
}
