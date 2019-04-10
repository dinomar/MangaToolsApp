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
    [Serializable]
    public class PageInfo
    {
        public string Url { get; }

        private string ext;
        public string Ext { get { return ext; } }

        private string name;
        public string Name { get { return name; } }

        public PageInfo(string url)
        {
            Url = url;
            name = GetImageName();
            ext = GetImageExtension();
        }

        private string GetImageExtension()
        {
            try
            {
                string[] tempUrl = Url.Split('.');
                string extension = tempUrl[tempUrl.Length - 1];
                return extension;
            }
            catch (IndexOutOfRangeException ex)
            {
                return string.Empty;
            }
        }

        private string GetImageName()
        {
            try
            {
                string[] tempUrl = Url.Split('/');
                return tempUrl[tempUrl.Length - 1].Split('.')[0];
            }
            catch (IndexOutOfRangeException ex)
            {
                return string.Empty;
            }
        }

        //*SaveImage
        //*SaveImage(filePath)
    }
}
