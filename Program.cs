using HtmlAgilityPack;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to Google Scraper");
        Console.WriteLine("What do you want to search for?");
        string userQuery = Console.ReadLine();

        //The number we enter is currently positional to the i value in GoogleScraper
        Console.WriteLine("How many pages do you want (each page is 10 results)?");
        int userNPages = Convert.ToInt32(Console.ReadLine());

        GoogleScraper newSearch = new GoogleScraper();
        var results = newSearch.ScrapeSerp(userQuery, userNPages);

        //Here we convert List<T> to a string Array - can turn this into a function
        List<string> listedResults = new List<string>();
        foreach (var result in results)
        {
            string strResult = Convert.ToString(result.url);
            listedResults.Add(strResult);
        }
        
        String[] resultsArray = listedResults.ToArray();

        GetInfo newRequest = new GetInfo();
        newRequest.getInfo(resultsArray);

    }

}
