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
    public class ChapterInfo : IEnumerable
    {
        private string id;
        public string Id { get { return id; } }

        private string hash;
        public string Hash { get { return hash; } }

        private int number; // Chapter nr
        public int Number { get { return number; } } 

        private string mangaId;
        public string MangaId { get { return mangaId; } }

        private string server;
        public string Server { get { return server; } }

        public List<PageInfo> pages = new List<PageInfo>();
        public PageInfo this[int index] {
            get
            {
                return pages[index];
            }
        }

        public int Count { get { return pages.Count; } }

        public ChapterInfo(string json)
        {
            parseJson(json);
        }

        private string getFullPath(string url)
        {
            if (pages == null || string.IsNullOrEmpty(Server) || string.IsNullOrEmpty(Hash))
            {
                return "";
            }

            return Server + Hash + "/" + url;
        }

        public void createPages(string[] pagesUrls)
        {
            foreach (string url in pagesUrls)
            {
                pages.Add(new PageInfo(getFullPath(url)));
            }
        }

        private void parseJson(string json)
        {
            try
            {
                dynamic jsonObj = JObject.Parse(json);
                if (jsonObj.status == "OK")
                {
                    id = jsonObj.id;
                    hash = jsonObj.hash;
                    number = jsonObj.chapter;
                    mangaId = jsonObj.manga_id;
                    server = jsonObj.server;
                    createPages(jsonObj.page_array.ToObject<string[]>());
                }
            }
            catch (JsonReaderException ex)
            {
                setDefaults();
            }
            catch(FormatException ex)
            {
                setDefaults();
            }
        }

        private void setDefaults()
        {
            id = string.Empty;
            hash = string.Empty;
            number = -1;
            mangaId = string.Empty;
            server = string.Empty;
        }

        public IEnumerator GetEnumerator()
        {
            return pages.GetEnumerator();
        }
    }
}
