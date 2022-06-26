using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

class GetInfo
{
    
    public void getInfo(string[] remoteUris)
    {
        foreach(string remoteUri in remoteUris)
        {
            WebClient myWebClient = new WebClient();
            Console.WriteLine("Downloading " + remoteUri);
            byte[] myDataBuffer = myWebClient.DownloadData(remoteUri);
            string download = Encoding.ASCII.GetString(myDataBuffer);
            Console.WriteLine("Download successful.");
            
            String [] Emails = GetEmailsFromWebContent(download);
            foreach(string Email in Emails)
            {
                Console.WriteLine(Email);
            }
            
            String [] Phones = GetPhonesFromWebContent(download);
            foreach(string Phone in Phones)
            {
                Console.WriteLine(Phone);
            }

            String [] Addresses = GetAddressesFromWebContent(download);
            foreach(string Address in Addresses)
            {
                Console.WriteLine(Address);
            }
        }
        Console.WriteLine("There is no more data to show");    
    }

    private static string[] GetEmailsFromWebContent(string webEmailContent)
    {
        MatchCollection collEmail = default(MatchCollection);
        int i = 0;
        collEmail = Regex.Matches(
            webEmailContent, "([a-zA-Z0-9_\\-\\.]+)@([a-zA-Z0-9_\\-\\.]+)\\.([a-zA-Z]{2,5})");
        string[] emailResults = new string[collEmail.Count];
        for (i = 0; i <= emailResults.Length - 1; i++)
        {
            emailResults[i] = collEmail[i].Value;
        }

        return emailResults;
    }


    private static string[] GetPhonesFromWebContent(string webPhoneContent)
    {
       var htmlDoc = new HtmlDocument();
       htmlDoc.LoadHtml(webPhoneContent);
       Regex phoneRegex = new Regex(@"(?:\+\d+\s+\(\d+\)\s+)?\d{4,5}\s+\d{3}\s+\d{3}");
       int i = 0;
       var collPhone = phoneRegex.Matches(htmlDoc.DocumentNode.InnerText);
       string [] phoneResults = new string[collPhone.Count];
       for (i=0; i <= phoneResults.Length - 1; i++)
       {
            phoneResults[i] = collPhone[i].Value;
       }
        return phoneResults;

    }
    
    private static string[] GetAddressesFromWebContent(string webAddressContent)
    {
       var htmlDoc = new HtmlDocument();
       htmlDoc.LoadHtml(webAddressContent);
       Regex AddressRegex = new Regex(@"([Gg][Ii][Rr] 0[Aa]{2})|((([A-Za-z][0-9]{1,2})|(([A-Za-z][A-Ha-hJ-Yj-y][0-9]{1,2})|(([A-Za-z][0-9][A-Za-z])|([A-Za-z][A-Ha-hJ-Yj-y][0-9][A-Za-z]?))))\s?[0-9][A-Za-z]{2})");
       int i = 0;
       var collAddress = AddressRegex.Matches(htmlDoc.DocumentNode.InnerText);
       string [] addressResults = new string[collAddress.Count];
       for (i=0; i <= addressResults.Length - 1; i++)
       {
            addressResults[i] = collAddress[i].Value;
       }
        return addressResults;

    }



}
