using AngleSharp;
using AngleSharp.Dom;

namespace Lift_application.Services
{
    public class AngleParse
    {
        public async Task<List<string>> ParseLink(string url)
        {
            var config = Configuration.Default.WithDefaultLoader();
            using var context = BrowsingContext.New(config);

            

            using var doc = await context.OpenAsync(url);




            var pars = doc.QuerySelectorAll("a").Select(el => el.GetAttribute("href")).ToArray();



            if (url == "https://myrosmol.ru/measures")
            {
                pars = doc.QuerySelectorAll("a.event").Select(el => el.GetAttribute("href")).ToArray();
            }
            if (url == "https://роскультцентр.рф")
            {
                pars = doc.QuerySelectorAll("a.js-product-link").Select(el => el.GetAttribute("href")).ToArray();
            }
            if (url == "https://будьвдвижении.рф/news")
            {
                pars = doc.QuerySelectorAll("a.news-page__item").Select(el => el.GetAttribute("href")).ToArray();
            }
            if (url == "https://vsekonkursy.ru")
            {
                pars = doc.QuerySelectorAll("a[rel~='bookmark']").Select(el => el.GetAttribute("href")).ToArray();
            }
            

            List<string> result = new List<string>();

            foreach (var par in pars)
            {
                
                if(url== "https://будьвдвижении.рф/news" || url == "https://mmp38.ru" ||url== "https://myrosmol.ru/measures")
                {
                    result.Add(url + par);
                }
                else
                {
                    result.Add(par);
                }
            }

            return result;
        }
    }
}
