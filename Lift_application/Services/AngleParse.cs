using AngleSharp;
using AngleSharp.Dom;

namespace Lift_application.Services
{
    public class AngleParse
    {
        public async Task<string> Parse(string url)
        {
            var config = Configuration.Default.WithDefaultLoader();
            using var context = BrowsingContext.New(config);

            

            using var doc = await context.OpenAsync(url);
            // var title = doc.QuerySelector("title").InnerHtml;
            var title = doc.Title;

            string result = title + "\n";
           

            var pars = doc.QuerySelectorAll("a");

            foreach (var par in pars)
            {
                result+= par.Text().Trim();
            }

            return result;
        }
    }
}
