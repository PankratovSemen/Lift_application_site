using System.ComponentModel.DataAnnotations;

namespace Lift_application.Models
{
    public class Articles
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }
        public string Author { get; set; }
        public string date { get; set; }
        public string SourceInfo { get; set; }
    }
}
