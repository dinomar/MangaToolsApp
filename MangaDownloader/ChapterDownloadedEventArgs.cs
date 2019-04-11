using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaDownloader
{
    public class ChapterDownloadedEventArgs : EventArgs
    {
        public ChapterInfo Chapter { get; }
        public Exception Error { get; }

        public ChapterDownloadedEventArgs(ChapterInfo chapter)
        {
            Chapter = chapter;
        }

        public ChapterDownloadedEventArgs(Exception exception)
        {
            Error = exception;
        }
    }
}
