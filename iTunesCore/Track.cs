using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTunesCore
{
    public class Track
    {
        public int TrackId { get; set; }
        public string Name { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public string Genre { get; set; }
        public string Kind { get; set; }
        public int Size { get; set; }
        public int TotalTime { get; set; }
        public int Year { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateAdded { get; set; }
        public int BitRate { get; set; }
        public int SampleRate { get; set; }
        public string Comments { get; set; }
        public int PlayCount { get; set; }
        public Int64 PlayDate { get; set; }
        public DateTime PlayDateUtc { get; set; }
        public int SkipCount { get; set; }
        public DateTime SkipDate { get; set; }
        public int Rating { get; set; }
        public int AlbumRating { get; set; }
        public int TrackNumber { get; set; }
    }
}
