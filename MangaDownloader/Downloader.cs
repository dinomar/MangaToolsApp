using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Drawing;

namespace MangaDownloader
{
    public class Downloader
    {
        private const string apiMangaUrl = "https://mangadex.org/api/manga/";
        private const string apiChapterUrl = "https://mangadex.org/api/chapter/";

        public event EventHandler<MangaDownloadedEventArgs> OnMangaDownloaded;
        private void onMangaDownloaded(MangaDownloadedEventArgs e)
        {
            OnMangaDownloaded?.Invoke(this, e);
        }

        public event EventHandler<ChapterDownloadedEventArgs> OnChapterDownloaded;
        private void onChapterDownloaded(ChapterDownloadedEventArgs e)
        {
            OnChapterDownloaded?.Invoke(this, e);
        }

        // Get MangaInfo
        public static MangaInfo GetManga(string mangaId)
        {
            Uri uri;
            try
            {
                string formatedUrl = apiMangaUrl + mangaId;
                Console.WriteLine(formatedUrl);
                if (Uri.TryCreate(formatedUrl, UriKind.RelativeOrAbsolute, out uri))
                {
                    // get json
                    string json = downloadInfo(uri);

                    // parse json
                    MangaInfo manga = new MangaInfo(json);

                    return manga;
                }
            }
            catch (IndexOutOfRangeException ex)
            {
                throw ex;
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
            catch (WebException ex)
            {
                throw ex;
            }
            return null;
        }

        // Get MangaInfo Async
        public async Task<MangaInfo> GetMangaAsync(string mangaId)
        {
            Uri uri;
            try
            {
                string formatedUrl = apiMangaUrl + mangaId;
                if (Uri.TryCreate(formatedUrl, UriKind.RelativeOrAbsolute, out uri))
                {
                    // get json
                    string json = await downloadInfoAsync(uri);

                    // parse json
                    MangaInfo manga = new MangaInfo(json);

                    // Fire OnMangaDownloadedEvent
                    onMangaDownloaded(new MangaDownloadedEventArgs(manga));

                    return manga;
                }
            }
            catch (IndexOutOfRangeException ex)
            {
                onMangaDownloaded(new MangaDownloadedEventArgs(ex));
            }
            catch (ArgumentException ex)
            {
                onMangaDownloaded(new MangaDownloadedEventArgs(ex));
            }
            catch (WebException ex)
            {
                onMangaDownloaded(new MangaDownloadedEventArgs(ex));
            }
            return null;
        }

        // Get ChapterInfo
        public static ChapterInfo GetChapter(string chapterId)
        {
            Uri uri;
            try
            {
                string formatedUrl = apiChapterUrl + chapterId;
                Console.WriteLine(formatedUrl);
                if (Uri.TryCreate(formatedUrl, UriKind.RelativeOrAbsolute, out uri))
                {
                    // get json
                    string json = downloadInfo(uri);

                    // parse json
                    ChapterInfo chapter = new ChapterInfo(json);

                    return chapter;
                }
            }
            catch (IndexOutOfRangeException ex)
            {
                throw ex;
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
            catch (WebException ex)
            {
                throw ex;
            }
            return null;
        }
        
        // Get ChapterInfo Async
        public async Task<ChapterInfo> GetChapterAsync(string chapterId)
        {
            Uri uri;
            try
            {
                string formatedUrl = apiChapterUrl + chapterId;
                if (Uri.TryCreate(formatedUrl, UriKind.RelativeOrAbsolute, out uri))
                {
                    // get json
                    string json = await downloadInfoAsync(uri);

                    // parse json
                    ChapterInfo chapter = new ChapterInfo(json);

                    // Fire OnChapterDownloaded Event
                    onChapterDownloaded(new ChapterDownloadedEventArgs(chapter));

                    return chapter;
                }
            }
            catch (IndexOutOfRangeException ex)
            {
                onChapterDownloaded(new ChapterDownloadedEventArgs(ex));
            }
            catch (ArgumentException ex)
            {
                onChapterDownloaded(new ChapterDownloadedEventArgs(ex));
            }
            catch (WebException ex)
            {
                onChapterDownloaded(new ChapterDownloadedEventArgs(ex));
            }
            return null;
        }

        // Download Chapter
        private static string downloadInfo(Uri uri)
        {
            try
            {
                using (WebClient wc = new WebClient())
                {
                    return wc.DownloadString(uri);
                }
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
            catch (WebException ex)
            {
                throw ex;
            }
        }

        // Download Chapter Async
        private async Task<string> downloadInfoAsync(Uri uri)
        {
            try
            {
                using (WebClient wc = new WebClient())
                {
                    return await wc.DownloadStringTaskAsync(uri);
                }
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
            catch (WebException ex)
            {
                throw ex;
            }
        }

        // Download Image
        public static Bitmap DownloadImage(PageInfo page)
        {
            Uri uri;
            if (!Uri.TryCreate(page.Url, UriKind.RelativeOrAbsolute, out uri))
            {
                return null;
            }

            try
            {
                using (WebClient wc = new WebClient())
                {
                    Stream stream = wc.OpenRead(uri);
                    Bitmap bitmap = new Bitmap(stream);
                    stream.Flush();
                    stream.Close();
                    
                    return bitmap;
                }
            }
            catch (WebException ex)
            {
                throw ex;
            }
        }

        // Download Image Async
        public async Task<Bitmap> DownloadImageAsync(PageInfo page)
        {
            Uri uri;
            if (!Uri.TryCreate(page.Url, UriKind.RelativeOrAbsolute, out uri))
            {
                return null;
            }

            try
            {
                using (WebClient wc = new WebClient())
                {
                    Stream stream = await wc.OpenReadTaskAsync(uri);
                    Bitmap bitmap = new Bitmap(stream);
                    stream.Flush();
                    stream.Close();
                    
                    return bitmap;
                }
            }
            catch (WebException ex)
            {
                throw ex;
            }
        }
    }
}

// Move mangainfo,chapterinfo download into class. add onEvents