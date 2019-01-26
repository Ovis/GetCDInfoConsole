using MetaBrainz.MusicBrainz.CoverArt;
using MetaBrainz.MusicBrainz.DiscId;
using System;
using System.Text;

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

            if(toc != null)
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
                        Console.WriteLine($" --- Offset: {150,6} ({Program.TwoSeconds,-16}) Length: {t.Offset - 150,6} ({t.StartTime.Subtract(Program.TwoSeconds),-16})");
                }
                foreach (var t in toc.Tracks)
                {
                    Console.Write($" {t.Number,2}. Offset: {t.Offset,6} ({t.StartTime,-16}) Length: {t.Length,6} ({t.Duration,-16})");
                    //ISRC出力
                    if ((features & DiscReadFeature.TrackIsrc) != 0)
                        Console.Write($" ISRC: {t.Isrc ?? "* not set *"}");
                    Console.WriteLine();
                }

                //CD情報取得
                var uri = new UriBuilder(TableOfContents.DefaultUrlScheme, TableOfContents.DefaultWebSite, TableOfContents.DefaultPort, "ws/2/discid/" + Uri.EscapeDataString(toc.DiscId), null);

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
                var cdDataUri= uri.Uri;

            }
        }
    }
}
