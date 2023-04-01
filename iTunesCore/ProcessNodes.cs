using System.Text;
using System.Xml;

namespace iTunesCore
{
    public class ProcessNodes
    {
        public Task GetXmlDocument(Stream file, StringBuilder output)
        {
            var settings = new XmlReaderSettings { IgnoreComments = true, IgnoreWhitespace = true };
            settings.IgnoreComments = true;
            settings.ValidationType = ValidationType.None;
            settings.DtdProcessing = DtdProcessing.Ignore;

            using (var reader = XmlReader.Create(file, settings))
            {
                var ws = new XmlWriterSettings { Indent = true };

                using (var writer = XmlWriter.Create(output, ws))
                {
                    while (reader.Read())
                    {
                        switch (reader.NodeType)
                        {
                            case XmlNodeType.Element:
                                writer.WriteStartElement(reader.Name);
                                break;
                            case XmlNodeType.Text:
                                writer.WriteString(reader.Value);
                                break;
                            case XmlNodeType.XmlDeclaration:
                            case XmlNodeType.ProcessingInstruction:
                                writer.WriteProcessingInstruction(reader.Name, reader.Value);
                                break;
                            case XmlNodeType.Comment:
                                writer.WriteComment(reader.Value);
                                break;
                            case XmlNodeType.EndElement:
                                writer.WriteFullEndElement();
                                break;
                        }
                    }
                }
            }

            return Task.CompletedTask;
        }

        public Track ParseTrack(XmlNode node)
        {
            var mover = -1;
            var kvp = new Dictionary<string, string>();

            foreach (XmlNode n in node)
            {
                if (n.Name == "key")
                    kvp.Add(node.ChildNodes[++mover].InnerText, node.ChildNodes[++mover].InnerText);
            }

            var track = new Track();

            track.TrackId = kvp.ContainsKey("Track ID") ? int.Parse(kvp["Track ID"]) : 0;
            track.Name = kvp.ContainsKey("Name") ? kvp["Name"] : string.Empty;
            track.Artist = kvp.ContainsKey("Artist") ? kvp["Artist"] : string.Empty;
            track.Album = kvp.ContainsKey("Album") ? kvp["Album"] : string.Empty;
            track.Genre = kvp.ContainsKey("Genre") ? kvp["Genre"] : string.Empty;
            track.Kind = kvp.ContainsKey("Kind") ? kvp["Kind"] : string.Empty;
            track.Size = kvp.ContainsKey("Size") ? int.Parse(kvp["Size"]) : 0;
            track.TotalTime = kvp.ContainsKey("Total Time") ? int.Parse(kvp["Total Time"]) : 0;
            track.Year = kvp.ContainsKey("Year") ? int.Parse(kvp["Year"]) : 0;
            track.DateModified = kvp.ContainsKey("Date Modified") ? DateTime.Parse(kvp["Date Modified"]) : new DateTime();
            track.DateAdded = kvp.ContainsKey("Date Added") ? DateTime.Parse(kvp["Date Added"]) : new DateTime();
            track.BitRate = kvp.ContainsKey("Bit Rate") ? int.Parse(kvp["Bit Rate"]) : 0;
            track.SampleRate = kvp.ContainsKey("Sample Rate") ? int.Parse(kvp["Sample Rate"]) : 0;
            track.Comments = kvp.ContainsKey("Comments") ? kvp["Comments"] : string.Empty;
            track.PlayCount = kvp.ContainsKey("Play Count") ? int.Parse(kvp["Play Count"]) : 0;
            track.PlayDate = kvp.ContainsKey("Play Date") ? Int64.Parse(kvp["Play Date"]) : 0;
            track.PlayDateUtc = kvp.ContainsKey("Play Date UTC") ? DateTime.Parse(kvp["Play Date UTC"]) : new DateTime();
            track.SkipCount = kvp.ContainsKey("Skip Count") ? int.Parse(kvp["Skip Count"]) : 0;
            track.SkipDate = kvp.ContainsKey("Skip Date") ? DateTime.Parse(kvp["Skip Date"]) : new DateTime();
            track.Rating = kvp.ContainsKey("Rating") ? int.Parse(kvp["Rating"]) : 0;
            track.AlbumRating = kvp.ContainsKey("Album Rating") ? int.Parse(kvp["Album Rating"]) : 0;
            track.TrackNumber = kvp.ContainsKey("Track Number") ? int.Parse(kvp["Track Number"]) : 0;

            return track;
        }
    }
}
