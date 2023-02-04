using AngleSharp;
using AngleSharp.Dom;
using static System.Net.WebRequestMethods;

namespace Lift_application.Services
{
    public class AngleParse
    {
        public async Task<List<string>> ParseLink(string url)
        {
            var config = Configuration.Default.WithDefaultLoader();
            using var context = BrowsingContext.New(config);

            List<string> result = new List<string>();

            using var doc = await context.OpenAsync(url);




            var pars = doc.QuerySelectorAll("a").Select(el => el.GetAttribute("href")).ToArray();



            if (url == "https://myrosmol.ru/measures")
            {
                pars = doc.QuerySelectorAll("a.event").Select(el => el.GetAttribute("href")).ToArray();
            }
            else if (url == "https://роскультцентр.рф")
            {
                pars = doc.QuerySelectorAll("a.js-product-link").Select(el => el.GetAttribute("href")).ToArray();
            }
            
            else if (url == "https://vsekonkursy.ru")
            {
                pars = doc.QuerySelectorAll("a[rel~='bookmark']").Select(el => el.GetAttribute("href")).ToArray();
            }

            else if(url == "https://bvbinfo.ru/catalog-news")
            {
                pars = doc.QuerySelectorAll("a.news-card").Select(el => el.GetAttribute("href")).ToArray();
            }
            
            else if(url== "https://mmp38.ru")
            {
                pars = doc.QuerySelectorAll("h3 a").Select(el => el.GetAttribute("href")).ToArray();
            }




                foreach (var par in pars)
            {
                
                if(url == "https://mmp38.ru")
                {
                    result.Add(url + par);
                }
                else if(url== "https://myrosmol.ru/measures")
                {
                    var urls = "https://myrosmol.ru";
                    result.Add(urls + par);
                }
                else if (url == "https://bvbinfo.ru/catalog-news")
                {
                    var urls = "https://bvbinfo.ru";
                    result.Add(urls + par);
                }
               
                else
                {
                    result.Add(par);
                }
            }

            return result;
        }


        public async Task<string> ParseTitle(string url)
        {
            var config = Configuration.Default.WithDefaultLoader();
            using var context = BrowsingContext.New(config);

            string result = "";

            using var doc = await context.OpenAsync(url);


            

            if (url != null)
            {                             
                    var pars = doc.QuerySelectorAll("h1");
                    foreach (var par in pars)
                    {

                        result += par.Text().Trim();

                    }
                
                
            }

            return result;
        }
        public async Task<string> ParseTitleH2(string url)
        {
            var config = Configuration.Default.WithDefaultLoader();
            using var context = BrowsingContext.New(config);

            string result = "";

            using var doc = await context.OpenAsync(url);




            var pars = doc.QuerySelectorAll("h2");




            foreach (var par in pars)
            {

                result += par.Text().Trim();

            }
            return result;
        }


        public async Task<string> ParseText(string url)
        {
            var config = Configuration.Default.WithDefaultLoader();
            using var context = BrowsingContext.New(config);

            string result = "";

            using var doc = await context.OpenAsync(url);


            var pars = doc.QuerySelectorAll("p");
            foreach (var par in pars)
            {

                result += par.Text().Trim();

            }
            
            


            

            
            return result;
        }

        public async Task<string> ParseTextRosCult(string url)
        {
            var config = Configuration.Default.WithDefaultLoader();
            using var context = BrowsingContext.New(config);

            string result = "";

            using var doc = await context.OpenAsync(url);




            var pars = doc.QuerySelectorAll("div.t119__preface");




            foreach (var par in pars)
            {
                result += par.Text().Trim();
            }
            return result;
        }

        public async Task<string> ParseTitleRosCult(string url)
        {
            var config = Configuration.Default.WithDefaultLoader();
            using var context = BrowsingContext.New(config);

            string result = "";

            using var doc = await context.OpenAsync(url);




            var pars = doc.QuerySelectorAll("div.t182__title");




            foreach (var par in pars)
            {
                result += par.Text().Trim();
            }
            return result;
        }

                

        public async Task<string> ParseTitleH2MM(string url)
        {
            var config = Configuration.Default.WithDefaultLoader();
            using var context = BrowsingContext.New(config);

            string result = "";

            using var doc = await context.OpenAsync(url);




            var pars = doc.QuerySelectorAll("div.title h2");




            foreach (var par in pars)
            {

                result += par.Text().Trim();

            }
            return result;
        }


        public async Task<string> ParseTextMM(string url)
        {
            var config = Configuration.Default.WithDefaultLoader();
            using var context = BrowsingContext.New(config);

            string result = "";

            using var doc = await context.OpenAsync(url);




            var pars = doc.QuerySelectorAll("div.news-detail p");




            foreach (var par in pars)
            {

                result += par.Text().Trim();

            }
            return result;
        }



        public async Task<string> ParseTextAisYoung(string url)
        {
            var config = Configuration.Default.WithDefaultLoader();
            using var context = BrowsingContext.New(config);

            string result = "";

            using var doc = await context.OpenAsync(url);




            var pars = doc.QuerySelectorAll("div.event__description p");




            foreach (var par in pars)
            {

                result += par.Text().Trim();

            }
            return result;
        }
    }
}
