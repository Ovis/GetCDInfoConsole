using Newtonsoft.Json;

namespace GetCDInfoConsole.Class
{
    public class ReleaseJson
    {
        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("country")]
        public string country { get; set; }

        [JsonProperty("title")]
        public string title { get; set; }

        [JsonProperty("asin")]
        public string asin { get; set; }

        [JsonProperty("text-representation")]
        public TextRepresentation textrepresentation { get; set; }

        [JsonProperty("label-info")]
        public LabelInfo[] labelinfo { get; set; }

        [JsonProperty("date")]
        public string date { get; set; }

        [JsonProperty("media")]
        public Medium[] media { get; set; }

        [JsonProperty("status-id")]
        public string statusid { get; set; }

        [JsonProperty("artist-credit")]
        public ArtistCredit[] artistcredit { get; set; }

        [JsonProperty("disambiguation")]
        public string disambiguation { get; set; }

        [JsonProperty("packaging")]
        public string packaging { get; set; }

        [JsonProperty("cover-art-archive")]
        public CoverArtArchive coverartarchive { get; set; }

        [JsonProperty("packaging-id")]
        public string packagingid { get; set; }

        [JsonProperty("quality")]
        public string quality { get; set; }

        [JsonProperty("release-events")]
        public ReleaseEvents[] releaseevents { get; set; }

        [JsonProperty("barcode")]
        public string barcode { get; set; }

        [JsonProperty("status")]
        public string status { get; set; }
    }

    public class TextRepresentation
    {
        [JsonProperty("script")]
        public string script { get; set; }

        [JsonProperty("language")]
        public string language { get; set; }
    }

    public class CoverArtArchive
    {
        [JsonProperty("front")]
        public bool front { get; set; }

        [JsonProperty("back")]
        public bool back { get; set; }

        [JsonProperty("darkened")]
        public bool darkened { get; set; }

        [JsonProperty("artwork")]
        public bool artwork { get; set; }

        [JsonProperty("count")]
        public int count { get; set; }
    }

    public class Label
    {
        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("sort-name")]
        public string sortname { get; set; }

        [JsonProperty("disambiguation")]
        public string disambiguation { get; set; }

        [JsonProperty("label-code")]
        public object labelcode { get; set; }
    }

    public class LabelInfo
    {  
        [JsonProperty("catalog-number")]
        public string catalognumber { get; set; }

        [JsonProperty("label")]
        public Label label { get; set; }
    }

    public class Medium
    {
        [JsonProperty("tracks")]
        public Track[] tracks { get; set; }

        [JsonProperty("title")]
        public string title { get; set; }

        [JsonProperty("format-id")]
        public string formatid { get; set; }

        [JsonProperty("format")]
        public string format { get; set; }

        [JsonProperty("track-count")]
        public int trackcount { get; set; }

        [JsonProperty("position")]
        public int position { get; set; }

        [JsonProperty("track-offset")]
        public int trackoffset { get; set; }
    }

    public class Track
    {
        [JsonProperty("recording")]
        public Recording recording { get; set; }

        [JsonProperty("title")]
        public string title { get; set; }

        [JsonProperty("artist-credit")]
        public ArtistCredit[] artistcredit { get; set; }

        [JsonProperty("number")]
        public string number { get; set; }

        [JsonProperty("position")]
        public int position { get; set; }

        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("length")]
        public int? length { get; set; }
    }

    public class Recording
    {
        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("length")]
        public int? length { get; set; }

        [JsonProperty("disambiguation")]
        public string disambiguation { get; set; }

        [JsonProperty("artist-credit")]
        public ArtistCredit[] artistcredit { get; set; }

        [JsonProperty("title")]
        public string title { get; set; }

        [JsonProperty("video")]
        public bool video { get; set; }
    }

    public class Artist
    {
        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("sort-name")]
        public string sortname { get; set; }

        [JsonProperty("disambiguation")]
        public string disambiguation { get; set; }
    }

    public class ArtistCredit
    {
        [JsonProperty("artist")]
        public Artist artist { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("joinphrase")]
        public string joinphrase { get; set; }
    }

    public class ReleaseEvents
    {
        [JsonProperty("date")]
        public string date { get; set; }

        [JsonProperty("area")]
        public Area area { get; set; }
    }

    public class Area
    {
        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("sort-name")]
        public string sortname { get; set; }

        [JsonProperty("disambiguation")]
        public string disambiguation { get; set; }

        [JsonProperty("iso-3166-1-codes")]
        public string[] iso31661codes { get; set; }
    }
}