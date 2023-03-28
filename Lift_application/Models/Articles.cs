using Microsoft.Build.ObjectModelRemoting;
using System.ComponentModel.DataAnnotations;


namespace Lift_application.Models
{
    public class Articles
    {

        public int Id { get; set; }

        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(400, MinimumLength = 20, ErrorMessage = "Длина строки должна быть от 3 до 100 символов")]
        public string Description { get; set; }
        [DataType(DataType.MultilineText)]

        public string Text { get; set; }
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Длина строки должна быть от 3 до 100 символов")]
        public string? Author { get; set; }
        public string date { get; set; }
        public string? SourceInfo { get; set; }
        public string? Image { get; set; }


        public List<Category> Category { get; set; }

    }


    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Articles> Article { get; set; }
    } 
}

    
