namespace DemoWebApp.Models
{
    public class Semester
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; } = 0;
        public int Credit { get; set; }

        public string Number { get; set; }
        public string Duration { get; set; }
    }
}
