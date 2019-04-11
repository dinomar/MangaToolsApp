using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MangaDownloader
{
    public enum LanguageFilter
    {
        English,
    }

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

        private List<string> chapters = new List<string>(); // Id's of chapters
        public List<string> Chapters { get { return chapters; } }
        public string this[int index] {
            get { return chapters[index]; }
        }

        public int Count { get { return chapters.Count; } }

        public MangaInfo(string json, LanguageFilter languageFilter = LanguageFilter.English)
        {
            parseJson(json, languageFilter);
        }

        // parseJson
        private void parseJson(string json, LanguageFilter languageFilter)
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

                    foreach (dynamic item in jsonObj.chapter)
                    {
                        try
                        {
                            switch (languageFilter)
                            {
                                case LanguageFilter.English:
                                    if (jsonObj.chapter[item.Name].lang_code == "gb")
                                    {
                                        chapters.Add(item.Name);
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException ex)
                        {
                            chapters.Add(item.Name);
                        }
                    }
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
