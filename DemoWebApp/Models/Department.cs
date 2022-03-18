using System.ComponentModel;

namespace DemoWebApp.Models
{
    public class Department
    {
        [DisplayName("Department Id")]
        public string DepartmentId { get; set; }
        [DisplayName("Department Name")]
        public string DepartmentName { get; set; }

        [DisplayName("Total Students")]
        public int TotalStudents { get; set; }

        [DisplayName("Department Head")]
        public string DepartmentHead { get; set; }

        [DisplayName("Total Credits")]
        public int TotalCredits { get; set; }
    }
}
