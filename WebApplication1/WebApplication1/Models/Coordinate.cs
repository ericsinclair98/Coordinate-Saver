namespace WebApplication1.Models
{
    public class Coordinate
    {

        public int Id { get; set; }

        public string Description { get; set; }
        public int XValue { get; set; }
        public int YValue { get; set; }
        public int ZValue { get; set; }
        public string Owner { get; set; }


        public Coordinate()
        {

        }

    }
}
