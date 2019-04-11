using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaDownloader
{
    public class MangaDownloadedEventArgs : EventArgs
    {
        public MangaInfo Manga { get; }
        public Exception Error { get; }

        public MangaDownloadedEventArgs(MangaInfo manga)
        {
            Manga = manga;
        }

        public MangaDownloadedEventArgs(Exception exception)
        {
            Error = exception;
        }
    }
}
