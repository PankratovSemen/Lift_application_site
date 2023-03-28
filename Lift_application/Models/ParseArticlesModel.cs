namespace Lift_application.Models
{
    public class ParseArticlesModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }

        public string? Description { get; set; }
        public string? Text { get; set; }
        public string? Date { set; get; }
        public string SourceInfo { get; set; }
        public string? Image { get; set; }
    }
}
