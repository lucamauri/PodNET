// See https://aka.ms/new-console-template for more information
using System;
using System.Xml.Linq;
using SynthesisCode.PodNET;
using System.Xml;
using System.Globalization;

internal class Program
{
    private static void Main(string[] args)
    {
        List<PodEpisode> Episodes = new List<PodEpisode>();
        List<string> Categories = new List<string>();

        Categories.Add("Fiction");
        PodChannel Channel = new PodChannel("My Podcast", "A dummy podcast with link www.microsoft.com", "https://wikitrek.org/wt/resources/assets/DeltaDiscovery.svg?4b0ba", new CultureInfo("it-IT", false),Categories, false);
        Channel.ChannelAuthor = "LucaMauri Company";

        PodEpisode Episode = new PodEpisode("Episodio", "www.lucamauri.com/episodio1.flac", "Episodio1", DateTime.Now);
        Episodes.Add(Episode);

        PodRSSFeed Feeder = new PodRSSFeed();
        XDocument RSS = new XDocument();
                
        RSS = Feeder.CreateRssFeed(Channel, Episodes);
        Console.WriteLine("Hello, World!");
        Console.WriteLine(RSS.ToString());
    }
}