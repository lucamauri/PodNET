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
        public PodBlock? ChannelBlock { get; set; }
        public PodComplete? ChannelComplete { get; set; }
    }

    public class PodEpisode
    {
        /// <summary>
        /// A string containing a clear, concise name for your episode.
        /// </summary>
        public string EpisodeTitle { get; set; }
        /// <summary>
        /// URL to the episode media file
        /// </summary>
        public Uri EpisodeURL { get; set; }
        /// <summary>
        /// File size in bytes.
        /// </summary>
        public int EpisodeLength { get; set; }
        /// <summary>
        /// File format
        /// </summary>
        public string EpisodeFileType { get; set; }
        /// <summary>
        /// Permanent globally unique identifier for the episode
        /// </summary>
        public Guid EpisodeGUID { get; set; }
        /// <summary>
        /// The date and time when an episode was released
        /// </summary>
        public DateTime EpisodePublishDate { get; set; }
        /// <summary>
        /// Episode description. Basic HTML allowed
        /// </summary>
        public string EpisodeDescription { get; set; }
        /// <summary>
        /// Duration in seconds
        /// </summary>
        public int EpisodeDuration { get; set; }
        /// <summary>
        /// Link to the episode's HTML page, if any
        /// </summary>
        public Uri EpisodeLink { get; set; }
        /// <summary>
        /// URL of the image for the episode, in PNG or JPEG format
        /// </summary>
        public Uri EpisodeImage { get; set; }
        /// <summary>
        /// Indicate whether a parental advisory is necessary
        /// </summary>
        public bool EpisodeEXplict { get; set; }
        /// <summary>
        /// Mandatory number for "serial" podcasts
        /// </summary>
        public int EpisodeNumber { get; set; }
        /// <summary>
        /// Number of the episode's season
        /// </summary>
        public int EpisodeSeason { get; set; }
        /// <summary>
        /// Type of the episode: Full, Trailer or Bonus
        /// </summary>
        public string EpisodeType { get; set; }
        /// <summary>
        /// Set to "yes" to remove the episode from platform
        /// </summary>
        public EpisodeBlock EpisodeRemove { get; set; }
        
        /// <summary>
        /// Generate the Episode object
        /// </summary>
        /// <param name="_Title">Name of the episode</param>
        /// <param name="_URL">URL to the episode</param>
        /// <param name="_Length">File length in byte</param>
        /// <param name="_Type">File type</param>
        public PodEpisode(string _Title, Uri _URL, int _Length, string _Type)
        {
            EpisodeTitle = _Title;
            EpisodeURL = _URL;
            EpisodeLength = _Length;
            EpisodeFileType = _Type;
            /*
            Link = _Link;
            Description = _Description;
            PublishDate = _PublishDate;
            */
        }
    }

    public enum PodType
    {
        // Values of <itunes:type>
        Episodic,
        Serial
    }

    public enum EpisodeType
    {
        Full,
        Trailer,
        Bonus
    }

    public enum PodBlock
    {
        Yes,
        No
    }

    public enum EpisodeBlock
    {
        Yes,
        No
    }

    public enum PodComplete
    {
        Yes,
        No
    }

    public static class EpisodeFileType
    {
        public const string AudioM4A = "audio/x-m4a";
        public const string AudioMPEG = "audio/mpeg";
        public const string VideoQT = "video/quicktime";
        public const string VideoMP4 = "video/mp4";
        public const string VideoM4V = "video/x-m4v";
        public const string PDF = "application/pdf";
    }

}