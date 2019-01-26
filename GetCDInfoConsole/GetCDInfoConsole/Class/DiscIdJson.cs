using Newtonsoft.Json;

namespace GetCDInfoConsole.Class
{
    class DiscIdJson
    {
        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("offsets")]
        public int[] offsets { get; set; }

        [JsonProperty("sectors")]
        public int sectors { get; set; }

        [JsonProperty("releases")]
        public Release[] releases { get; set; }

        [JsonProperty("offset-count")]
        public int offsetcount { get; set; }

        public class Release
        {
            [JsonProperty("text-representation")]
            public TextRepresentation textrepresentation { get; set; }

            [JsonProperty("country")]
            public string country { get; set; }

            [JsonProperty("quality")]
            public string quality { get; set; }

            [JsonProperty("barcode")]
            public string barcode { get; set; }

            [JsonProperty("disambiguation")]
            public string disambiguation { get; set; }

            [JsonProperty("date")]
            public string date { get; set; }

            [JsonProperty("packaging")]
            public string packaging { get; set; }

            [JsonProperty("cover-art-archive")]
            public CoverArtArchive coverartarchive { get; set; }

            [JsonProperty("status-id")]
            public string statusid { get; set; }

            [JsonProperty("id")]
            public string id { get; set; }

            [JsonProperty("title")]
            public string title { get; set; }

            [JsonProperty("packaging-id")]
            public string packagingid { get; set; }

            [JsonProperty("status")]
            public string status { get; set; }

            [JsonProperty("media")]
            public Medium[] media { get; set; }

            [JsonProperty("release-events")]
            public ReleaseEvents[] releaseevents { get; set; }

            [JsonProperty("asin")]
            public object asin { get; set; }
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

            [JsonProperty("darkened")]
            public bool darkened { get; set; }

            [JsonProperty("back")]
            public bool back { get; set; }

            [JsonProperty("count")]
            public int count { get; set; }

            [JsonProperty("artwork")]
            public bool artwork { get; set; }
        }

        public class Medium
        {
            [JsonProperty("format")]
            public string format { get; set; }

            [JsonProperty("title")]
            public string title { get; set; }

            [JsonProperty("track-count")]
            public int trackcount { get; set; }

            [JsonProperty("discs")]
            public Disc[] discs { get; set; }

            [JsonProperty("position")]
            public int position { get; set; }

            [JsonProperty("format-id")]
            public string formatid { get; set; }
        }

        public class Disc
        {
            [JsonProperty("offset-count")]
            public int offsetcount { get; set; }

            [JsonProperty("id")]
            public string id { get; set; }

            [JsonProperty("offsets")]
            public int[] offsets { get; set; }

            [JsonProperty("sectors")]
            public int sectors { get; set; }
        }

        public class ReleaseEvents
        {
            [JsonProperty("area")]
            public Area area { get; set; }

            [JsonProperty("date")]
            public string date { get; set; }
        }

        public class Area
        {
            [JsonProperty("id")]
            public string id { get; set; }

            [JsonProperty("sort-name")]
            public string sortname { get; set; }

            [JsonProperty("iso-3166-1-codes")]
            public string[] iso31661codes { get; set; }

            [JsonProperty("disambiguation")]
            public string disambiguation { get; set; }

            [JsonProperty("name")]
            public string name { get; set; }
        }

    }
}