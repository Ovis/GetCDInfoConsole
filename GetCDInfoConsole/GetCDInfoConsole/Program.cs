using GetCDInfoConsole.Class;
using MetaBrainz.MusicBrainz.DiscId;
using Newtonsoft.Json;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GetCDInfoConsole
{
    class Program
    {
        private static readonly TimeSpan TwoSeconds = new TimeSpan(0, 0, 2);

        static void Main(string[] args)
        {
            string device = null;

            var features = TableOfContents.AvailableFeatures;

            var toc = TableOfContents.ReadDisc(device, features);

            if (toc != null)
            {
                //ディスク情報
                Console.WriteLine($"CD Device Used      : {toc.DeviceName}");
                Console.WriteLine($"Features Requested  : {features}");
                Console.WriteLine();
                if ((features & DiscReadFeature.MediaCatalogNumber) != 0)
                    Console.WriteLine($"Media Catalog Number: {toc.MediaCatalogNumber ?? "* not set *"}");
                Console.WriteLine($"MusicBrainz Disc ID : {toc.DiscId}");
                Console.WriteLine($"FreeDB Disc ID      : {toc.FreeDbId}");
                Console.WriteLine($"Submission URL      : {toc.SubmissionUrl}");
                Console.WriteLine();

                //Track情報
                Console.WriteLine("Tracks:");
                { // Check for a "hidden" pre-gap track
                    var t = toc.Tracks[toc.FirstTrack];
                    if (t.StartTime > Program.TwoSeconds)
                        Console.WriteLine($" --- Offset: {150,6} ({TwoSeconds,-16}) Length: {t.Offset - 150,6} ({t.StartTime.Subtract(TwoSeconds),-16})");
                }
                foreach (var t in toc.Tracks)
                {
                    Console.Write($" {t.Number,2}. Offset: {t.Offset,6} ({t.StartTime,-16}) Length: {t.Length,6} ({t.Duration,-16})");
                    //ISRC出力
                    if ((features & DiscReadFeature.TrackIsrc) != 0)
                        Console.Write($" ISRC: {t.Isrc ?? "* not set *"}");
                    Console.WriteLine();
                }

                //ここまでMetaBrainzのライブラリのデモプログラムから流用
                Console.WriteLine();

                //CD情報取得
                var uriBase = new UriBuilder(TableOfContents.DefaultUrlScheme, TableOfContents.DefaultWebSite, TableOfContents.DefaultPort);
                var uri = uriBase;
                uri.Path = "ws/2/discid/" + Uri.EscapeDataString(toc.DiscId);
                var query = new StringBuilder();

                query.Append("toc=");

                query.Append(toc.FirstTrack).Append("+").Append(toc.LastTrack).Append("+").Append(toc.Length);
                for (var i = toc.FirstTrack; i <= toc.LastTrack; ++i)
                {
                    query.Append("+").Append(toc.Tracks[i].Offset);
                }

                //JSON形式で取得
                query.Append("&fmt=json");

                uri.Query = query.ToString();

                var json = GetJsonString(uri.Uri).Result;

                if (json == null)
                {
                    Console.WriteLine("Failed to get Json.");
                    return;
                }

                var discIdJsonData = JsonConvert.DeserializeObject<DiscIdJson>(json);

                //取得されたJSONからMBIDを取得
                var mbId = discIdJsonData.releases[0].id;


                //MBIDをもとにデータを取得
                uri = uriBase;
                uri.Path = "ws/2/release/" + mbId;

                query = new StringBuilder();

                query.Append("inc=recordings+labels+artists+artist-credits");
                //JSON形式で取得
                query.Append("&fmt=json");

                uri.Query = query.ToString();

                json = GetJsonString(uri.Uri).Result;

                if (json == null)
                {
                    Console.WriteLine("Failed to get Json.");
                    return;
                }

                var releaseData = JsonConvert.DeserializeObject<ReleaseJson>(json);

                Console.WriteLine("Title       : " + releaseData.title);
                Console.WriteLine("Album Artist: " + releaseData.artistcredit[0].name);
                Console.WriteLine("Label       : " + releaseData.labelinfo[0].label.name);
                Console.WriteLine("Release Date: " + releaseData.date);
                Console.WriteLine("ASIN        : " + releaseData.asin);
                Console.WriteLine("BarCode     : " + releaseData.barcode);
                Console.WriteLine("MBID        : " + mbId);

                Console.WriteLine("続行するには何かキーを押してください．．．");
                // Console.ReadKey();

                //アルバムアート取得のテスト
                var covArt = new MetaBrainz.MusicBrainz.CoverArt.CoverArt("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36 OPR/55.0.2994.61");

                Image imgData = covArt.FetchFrontAsync(new Guid(mbId)).Result.Decode();
                using (Bitmap saveImg = new Bitmap(imgData.Width, imgData.Height))
                {
                    imgData.Save("D:\\AlbumArt.jpg", ImageFormat.Jpeg);
                }

                Console.ReadKey();

            }
        }

        /// <summary>
        /// APIデータ取得処理
        /// </summary>
        /// <param name="uri">取得対象APIURL</param>
        /// <returns>取得値文字列</returns>
        public static async Task<string> GetJsonString(Uri uri)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    //RateLimitingがあるため、User-Agentを明示的に指定(User-Agentの値は暫定)
                    //https://musicbrainz.org/doc/XML_Web_Service/Rate_Limiting
                    httpClient.DefaultRequestHeaders.Add("Accept-Language", "ja-JP");
                    httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36 OPR/55.0.2994.61");

                    return await httpClient.GetStringAsync(uri);
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.Message);

                return null;
            }
        }
    }
}
