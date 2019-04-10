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
    public static class Downloader
    {
        private const string apiMangaUrl = "https://mangadex.org/api/manga/";
        private const string apiChapterUrl = "https://mangadex.org/api/chapter/";

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
        public static async Task<MangaInfo> GetMangaAsync(string mangaId)
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
        public static async Task<ChapterInfo> GetChapterAsync(string chapterId)
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
        private static async Task<string> downloadInfoAsync(Uri uri)
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
        public static async Task<Bitmap> DownloadImageAsync(PageInfo page)
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

        //private static string formatUrl(string url)
        //{
        //    try
        //    {
        //        string[] tempUrl = url.Split('/');

        //        string chapterId = tempUrl[tempUrl.Length - 2];
        //        return string.Format(apiUrl, chapterId);
        //    }
        //    catch (IndexOutOfRangeException ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}