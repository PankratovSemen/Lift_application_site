using System.Linq;
using Lift_application.Models;


namespace Lift_application
{
    public class SampleData
    {
        public static void Initialize(ArticlesContext context) 
        { 
            if(!context.Articles.Any())
            {
                context.Articles.AddRange
                    (
                        new Articles
                        {
                            Title = "Наш или не наш, вот в чем вопрос?",
                            Text = "Lorem fsssssssssssssssssssssssssssssssssssssssssssssss ooigiogiohgeigiegjeigh uiegegieg ie iegoeogeieg ieii" +
                            "eegegegegegegeg",
                            Description = "Lorem fsssssssssssssssssssssssssssssssssssssssssssssss ooigiogiohgeigiegjeigh uiegegieg ie iegoeogeieg ieii" +
                            "eegegegegegegeg",
                            Author = "Панкратов Семён",
                            date = DateTime.UtcNow.ToString(),
                            SourceInfo = ""
                        },
                        new Articles
                        {
                            Title = "Наш или не наш, вот в чем вопрос?",
                            Text = "Lorem fsssssssssssssssssssssssssssssssssssssssssssssss ooigiogiohgeigiegjeigh uiegegieg ie iegoeogeieg ieii" +
                            "eegegegegegegeg",
                            Description = "Lorem fsssssssssssssssssssssssssssssssssssssssssssssss ooigiogiohgeigiegjeigh uiegegieg ie iegoeogeieg ieii" +
                            "eegegegegegegeg",
                            Author = "Панкратов Семён",
                            date = DateTime.UtcNow.ToString(),
                            SourceInfo = "https://metanit.com/sharp/aspnet5/3.4.php"
                        },
                        new Articles
                        {
                            Title = "Наш или не наш, вот в чем вопрос?",
                            Text = "Lorem fsssssssssssssssssssssssssssssssssssssssssssssss ooigiogiohgeigiegjeigh uiegegieg ie iegoeogeieg ieii" +
                            "eegegegegegegeg",
                            Description = "Lorem fsssssssssssssssssssssssssssssssssssssssssssssss ooigiogiohgeigiegjeigh uiegegieg ie iegoeogeieg ieii" +
                            "eegegegegegegeg",
                            Author = "Панкратов Семён",
                            date = DateTime.UtcNow.ToString(),
                            SourceInfo = ""
                        }
                    );
               
                context.SaveChanges();
            }
            
        }
    }
}
