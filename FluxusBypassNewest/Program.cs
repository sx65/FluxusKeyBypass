using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web;
using System.Windows.Forms;
using HtmlAgilityPack;

public class Response
{
    public string Url { get; set; }
    public string Content { get; set; }

    public Response(string url, string content)
    {
        Url = url;
        Content = content;
    }
}

public class Program
{
    [STAThread]
    public static void Main()
    {
        hamhamnigger();
    }

    public static void hamhamnigger()
    {
        Console.Title = "Omg goofy ShowerHeadL";
        Console.Write("Enter the HWID: ");
        var hwid = Console.ReadLine();


        var urls = new List<string>
        {
            $"https://flux.li/windows/start.php?updated_browser=true&HWID={hwid}",
            "https://fluxteam.net/windows/checkpoint/check1.php",
            "https://fluxteam.net/windows/checkpoint/check2.php",
            "https://fluxteam.net/windows/checkpoint/main.php"
        };

        var client = new HttpClient();

        var responses = new List<Response>();

        foreach (var url in urls)
        {
            try
            {
                responses.Add(Request(client, url));
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error requesting URL: {url}");
            }
        }

        foreach (var response in responses)
        {
            Console.WriteLine("Bypassed");
        }

        var lastResponse = responses[responses.Count - 1];
        var document = new HtmlAgilityPack.HtmlDocument();
        document.LoadHtml(lastResponse.Content);
        var key = "";
        var keyNode = document.DocumentNode.SelectSingleNode("//main/code[2]");
        if (keyNode != null)
        {
            key = keyNode.InnerText;
        }
        else
        {
            key = "Error!";
        }

        Console.WriteLine($"Your key is: {key}");

        Clipboard.SetText(key);
        Console.WriteLine("Key copied to clipboard.");

        System.Environment.Exit(0);
    }

    private static Response Request(HttpClient client, string url)
    {
        var headers = new Dictionary<string, string>
        {
            { "User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/94.0.4606.81 Safari/537.36" },
            { "Referer", "https://linkvertise.com/" }
        };

        var request = new HttpRequestMessage(HttpMethod.Get, url);
        foreach (var header in headers)
        {
            request.Headers.Add(header.Key, header.Value);
        }

        var response = client.SendAsync(request).Result;
        var content = response.Content.ReadAsStringAsync().Result;

        return new Response(url, content);
    }
}
