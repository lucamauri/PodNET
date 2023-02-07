//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading.Channels;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;
using static System.Net.Mime.MediaTypeNames;

namespace SynthesisCode.Open.PodNET
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
                    new XElement("title", i.EpisodeTitle),
                     new XElement("link", i.EpisodeLink),
                     new XElement("description", i.EpisodeDescription),
                     new XElement("pubDate", i.EpisodePublishDate.ToString("r"))
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

            /* Specifications of the podcast RSS are available at:
             * https://help.apple.com/itc/podcasts_connect/
             * https://support.google.com/podcast-publishers/answer/9889544 
            */
            var XMLDeclaration = new XDeclaration("1.0", "utf-8", "yes");
            var XMLRoot = new XElement("rss",
                new XAttribute("version", "2.0"),
                new XAttribute(XNamespace.Xmlns + "itunes", NSiTunes),
                new XAttribute(XNamespace.Xmlns + "googleplay", NSGooglePlay),
                new XAttribute(XNamespace.Xmlns + "content", "http://purl.org/rss/1.0/modules/content/")
                );
            XElement XMLChannel = new XElement("channel",
                new XElement("title", Channel.ChannelTitle),
                new XElement(NSiTunes + "title", Channel.ChannelTitle),
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
            if (Channel.ChannelOwnerName != null && Channel.ChannelOwnerEmail != null) {
                XMLChannel.Add(new XElement(NSiTunes + "owner",
                    new XElement(NSiTunes + "name", Channel.ChannelOwnerName),
                    new XElement(NSiTunes + "email", Channel.ChannelOwnerEmail))
                    );
            }
            if (Channel.ChannelType != null)
            {
                XMLChannel.Add(new XElement(NSiTunes + "type", Channel.ChannelType));
            }
            
            XMLChannel.Add(Channel.ChannelCopyright != null ? new XElement("copyright", Channel?.ChannelCopyright) : null);

            if (Channel.ChannelBlock != null)
            {
                XMLChannel.Add(new XElement(NSiTunes + "block", Channel.ChannelBlock));
            }
            if (Channel.ChannelComplete != null)
            {
                XMLChannel.Add(new XElement(NSiTunes + "complete", Channel.ChannelComplete));
            }

            IEnumerable<XElement> XMLEpisodes = items.Select(i => new XElement("item", 
                new XElement("title", i.EpisodeTitle),
                new XElement("enclosure", 
                    new XAttribute("url", i.EpisodeURL),
                    new XAttribute ("length", i.EpisodeLength),
                    new XAttribute("type", i.EpisodeFileType)
                    )
                )
             );

            /*
             
                new XElement("link", i.EpisodeLink),
                new XElement("description", new XCData(i.EpisodeDescription)),
                new XElement("pubDate", i.EpisodePublishDate.ToString("r"))
             */
            XMLChannel.Add(XMLEpisodes);
            XMLRoot.Add(XMLChannel);

            var rss = new XDocument(XMLDeclaration, XMLRoot);
            rss.Save("ExamplePod.xml");
            RSSFeed = rss;
            return rss;
        }

        public XDocument? RSSFeed { get; private set; }
    }
}