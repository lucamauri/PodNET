/*
 * using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
*/

using System.Globalization;
using System.Xml.Linq;

namespace SynthesisCode.Open.PodNET
{

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
        public PodType? ChannelType { get; set; }
        public string? ChannelCopyright { get; set; }

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

    public enum PodType
    {
        // Values of <itunes:type>
        Episodic,
        Serial
    }

}