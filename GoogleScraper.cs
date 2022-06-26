using HtmlAgilityPack;

class GoogleScraper
{
    public List<SerpResult> ScrapeSerp(string query, int nPages)
    {
        var serpResults = new List<SerpResult>();
        int i = 0;
        //The i value dictates which page we start scraping from
        for(i = 2; i <= nPages; i++)
        {
            var url = "https://www.google.com/search?q=" + query + "&num=10&start=" + ((i - 1) * 10).ToString();
            HtmlWeb web = new HtmlWeb();
            web.UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7)" +
                            "AppleWebKit/537.36 (KHTML, like Gecko) Chrome/102.0.5005.61" +
                            " Safari/537.36";
            
            var htmldoc = web.Load(url);

            HtmlNodeCollection nodes = htmldoc.DocumentNode.SelectNodes("//div[@class='yuRUbf']");

            foreach (var tag in nodes)
            {
                var result = new SerpResult();

                result.url = tag.Descendants("a").FirstOrDefault().Attributes["href"].Value;
                serpResults.Add(result);
            }
        }


        return serpResults;
    }
    
    public class SerpResult
    {
        public string url { get; set; }
    }
}
