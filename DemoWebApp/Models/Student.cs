namespace DemoWebApp.Models
{
    public class Student
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Birthdate { get; set; } = DateTime.MinValue.ToString();
        public int Age 
        { 
            get
            {
                DateTime birthDate = Convert.ToDateTime(this.Birthdate);
                int age = DateTime.Now.Subtract(birthDate).Days;
                age = age / 365;
                return age;
            } 
        }
        public string Email { get; set; }
        public string Section { get; set; }
        public float CGPA { get; set; } = 0;
    }
}
