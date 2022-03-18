using System.ComponentModel;

namespace DemoWebApp.Models
{
    public class Teacher
    {

        public string Id { get; set; }
        public string Name { get; set; }

        public string Designation { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        [DisplayName("Join Date")]
        public string JoinDate { get; set; }

    }
}
