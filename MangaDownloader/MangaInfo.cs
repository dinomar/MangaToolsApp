using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MangaDownloader
{
    [Serializable]
    public class MangaInfo : IEnumerable
    {
        private string title;
        public string Title { get { return title; } }

        private string description;
        public string Description { get { return description; } }

        private string coverUrl;
        public string CoverUrl { get { return coverUrl; } }

        private string author;
        public string Author { get { return author; } }

        private string artist;
        public string Artist { get { return artist; } }

        private List<ChapterInfo> chapters = new List<ChapterInfo>();
        public ChapterInfo this[int index] {
            get { return chapters[index]; }
        }

        public int Count { get { return chapters.Count; } }

        public MangaInfo(string json)
        {
            parseJson(json);
        }

        //parseJson
        private void parseJson(string json)
        {
            try
            {
                dynamic jsonObj = JObject.Parse(json);
                if (jsonObj.status == "OK")
                {
                    title = jsonObj.manga.title;
                    description = jsonObj.manga.description;
                    coverUrl = jsonObj.manga.cover_url;
                    author = jsonObj.manga.author;
                    artist = jsonObj.manga.artist;

                    List<string> chapterIds = new List<string>();
                    foreach (dynamic item in jsonObj.chapter)
                    {
                        chapterIds.Add(item.Name);
                    }

                    Parallel.ForEach(chapterIds, async (id) =>
                    {
                        Console.WriteLine(id);
                        ChapterInfo newChapter = Downloader.GetChapter(id);
                        chapters.Add(newChapter);
                    });

                    //Parallel -> non async getChapter

                    //foreach (string id in chapterIds)
                    //{
                    //    ChapterInfo newChapter = await Downloader.GetChapterAsync(id);
                    //    chapters.Add(newChapter);
                    //}
                }
            }
            catch (JsonReaderException ex)
            {
                title = string.Empty;
                description = string.Empty;
                coverUrl = string.Empty;
                author = string.Empty;
                artist = string.Empty;
            }
        }

        public IEnumerator GetEnumerator()
        {
            return chapters.GetEnumerator();
        }
    }
}
