using System.ComponentModel;

namespace DemoWebApp.Models
{
    public class Books
    {
        
        public int Id { get; set; }
        [DisplayName("Book Number")]
        public int BookNumber { get; set; }
        [DisplayName("Book Name")]
        public string? BookName { get; set; }
        [DisplayName("Author")]
        public string? Author { get; set; }
        [DisplayName("Lending Duration")]

        public int lending_Duration { get; set; }

    }


}
