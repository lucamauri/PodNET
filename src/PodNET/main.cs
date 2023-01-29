//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading.Channels;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace SynthesisCode.PodNET
{
    public class PodRSSFeed
    {
        public XDocument CreateRssFeedOriginal(PodChannel Channel, IEnumerable<PodEpisode> items)
        {
            //xmlns:googleplay="http://www.google.com/schemas/play-podcasts/1.0"
            XNamespace NSGooglePlay = "http://www.google.com/schemas/play-podcasts/1.0";
            XNamespace NSiTunes = "http://www.itunes.com/dtds/podcast-1.0.dtd";
            //XNamespace NSContent = "content";

            var rss = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("rss",
                new XAttribute("version", "2.0"),
                new XAttribute(XNamespace.Xmlns + "itunes", NSiTunes),
                new XAttribute(XNamespace.Xmlns + "googleplay", NSGooglePlay),
                new XAttribute(XNamespace.Xmlns + "content", "http://purl.org/rss/1.0/modules/content/"),
                new XElement("channel",
                new XElement("title", Channel.ChannelTitle),
                new XElement("description", Channel.ChannelDescription),
                new XElement(NSiTunes + "image", Channel.ChannelImageURL),
                new XElement("language", Channel.ChannelLanguage),
                new XElement(NSiTunes + "category", Channel.ChannelCategory),
                new XElement(NSiTunes + "explicit", Channel.ChannelExplicit),
                items.Select(i =>
                    new XElement("item",
                    new XElement("title", i.Title),
                     new XElement("link", i.Link),
                     new XElement("description", i.Description),
                     new XElement("pubDate", i.PublishDate.ToString("r"))
                 )
             )
         )
     )
 );
            rss.Save("ExamplePod.xml");
            RSSFeed = rss;
            return rss;
        }

        public XDocument CreateRssFeed(PodChannel Channel, IEnumerable<PodEpisode> items)
        {
            XNamespace NSiTunes = "http://www.itunes.com/dtds/podcast-1.0.dtd";
            XNamespace NSGooglePlay = "http://www.google.com/schemas/play-podcasts/1.0";

            var XMLDeclaration = new XDeclaration("1.0", "utf-8", "yes");
            var XMLRoot = new XElement("rss",
                new XAttribute("version", "2.0"),
                new XAttribute(XNamespace.Xmlns + "itunes", NSiTunes),
                new XAttribute(XNamespace.Xmlns + "googleplay", NSGooglePlay),
                new XAttribute(XNamespace.Xmlns + "content", "http://purl.org/rss/1.0/modules/content/")
                );
            var XMLChannel = new XElement("channel",
                new XElement("title", Channel.ChannelTitle),
                new XElement("description", new XCData(Channel.ChannelDescription)),
                new XElement(NSiTunes + "image", Channel.ChannelImageURL),
                new XElement("language", Channel.ChannelLanguage),
                new XElement(NSiTunes + "category", Channel.ChannelCategory),
                new XElement(NSiTunes + "explicit", Channel.ChannelExplicit)
                );

            if ( Channel.ChannelAuthor != null && Channel.ChannelAuthor != "")
            {
                XMLChannel.Add(new XElement(NSiTunes + "author", Channel.ChannelAuthor));
            }
            if (Channel.ChannelHomepage != null)
            {
                XMLChannel.Add(new XElement( "link", Channel.ChannelHomepage));
            }
            if (Channel.ChannelOwnerName != null || Channel.ChannelOwnerEmail != null) {
                XMLChannel.Add(new XElement(NSiTunes + "owner",
                    new XElement(NSiTunes + "owner", Channel.ChannelOwnerName),
                    new XElement(NSiTunes + "owner", Channel.ChannelOwnerEmail))
                    );
            }
                var XMLEpisodes = items.Select(i => new XElement("item",
                 new XElement("title", i.Title),
                 new XElement("link", i.Link),
                 new XElement("description", i.Description),
                 new XElement("pubDate", i.PublishDate.ToString("r"))
                 )
             );
            XMLChannel.Add(XMLEpisodes);
            XMLRoot.Add(XMLChannel);

            var rss = new XDocument(XMLDeclaration, XMLRoot);
            rss.Save("ExamplePod.xml");
            RSSFeed = rss;
            return rss;
        }

        public XDocument? RSSFeed { get; private set; }
    }

    public class PodChannel
    {
        public PodChannel(string _ChannelTitle, string _ChannelDescription, string _ChannelImageURL, CultureInfo _ChannelLanguage, IEnumerable<string> _ChannelCategory, bool _ChannelExplicit)
        {
            ChannelTitle = _ChannelTitle;
            ChannelDescription = _ChannelDescription;
            ChannelImageURL = _ChannelImageURL;
            ChannelLanguage = _ChannelLanguage.TwoLetterISOLanguageName;
            ChannelCategory = _ChannelCategory;
            ChannelExplicit = _ChannelExplicit;
        }

        public string ChannelTitle { get; }
        public string ChannelDescription { get; }
        public string ChannelImageURL { get; }
        public string ChannelLanguage { get; }
        public IEnumerable<string> ChannelCategory { get; }
        public bool ChannelExplicit { get; }

        public string? ChannelAuthor { get; set; }
        public string? ChannelHomepage { get; set; }
        public string? ChannelOwnerEmail { get; set; }
        public string? ChannelOwnerName { get; set; }

    }

    public class PodEpisode
    {
        // Auto-implemented properties for trivial get and set
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public DateTime PublishDate { get; set; }

        // Constructor
        public PodEpisode(string _Title, string _Link, string _Description, DateTime _PublishDate)
        {
            Title = _Title;
            Link = _Link;
            Description = _Description;
            PublishDate = _PublishDate;
        }
    }
}