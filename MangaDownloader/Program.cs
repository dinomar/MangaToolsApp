using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MangaDownloader
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Manga Downloader *****\n");
            //string chapterId = "382926";
            //Console.WriteLine(string.Format("ChapterId: {0}", chapterId));

            ////ChapterInfo newChapter = Downloader.GetChapter(chapterId);
            ////Console.WriteLine(newChapter.MangaId);

            //string mangaId = "21946";
            //MangaInfo manga = Downloader.GetManga(mangaId);
            //Console.WriteLine(manga.Title);

            Downloader dl = new Downloader();
            dl.OnMangaDownloaded += dl_OnMangaDownloaded;

            dl.GetMangaAsync("35939");
            Console.ReadLine();
        }

        private static void dl_OnMangaDownloaded(object sender, MangaDownloadedEventArgs e)
        {
            if (e.Error == null)
            {
                Console.WriteLine("OK");
            }
            else
            {
                Console.WriteLine(e.Error.Message);
            }
        }
    }
}
