using System.ComponentModel;

namespace DemoWebApp.Models
{
    public class Course
    {
        [DisplayName("Course Name")]
        public string CourseName { get; set; }
        [DisplayName("Course ID")]
        public string CourseID { get; set; }
        [DisplayName("Course Credit")]
        public int CourseCredit { get; set; }
        [DisplayName("Course Teacher")]
        public string CourseTeacher { get; set; }
        [DisplayName("Department")]
        public string Department { get; set;}
    }
}
