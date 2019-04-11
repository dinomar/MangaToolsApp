using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MangaDownloader;

namespace MangaToolsApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ChapterListPage : ContentPage
	{
        private string mangaSearchId;
        private Downloader dl;
        private MangaInfo mangaInfo;
        private List<ChapterInfo> chapters;

        public ChapterListPage(string mangaId)
		{
			InitializeComponent ();

            mangaSearchId = mangaId;
            dl = new Downloader();

            // Link Events
            dl.OnMangaDownloaded += dl_OnMangaDownloaded;
            dl.OnChapterDownloaded += dl_OnChapterDownloaded;

            // Load Manga
            dl.GetMangaAsync(mangaSearchId);
        }

        // Manga Loaded.
        private void dl_OnMangaDownloaded(object sender, MangaDownloadedEventArgs e)
        {
            if (e.Error == null)
            {
                mangaInfo = e.Manga;
                loadChapters();
            }
            else
            {
                DisplayAlert("Error", e.Error.Message, "Ok");
                Console.WriteLine(e.Error.Message);
            }
        }

        // Chapter Loaded.
        private void dl_OnChapterDownloaded(object sender, ChapterDownloadedEventArgs e)
        {
            if (e.Error == null)
            {
                chapters.Add(e.Chapter);
                renderListView();
            }
            else
            {
                DisplayAlert("Error", e.Error.Message, "Ok");
                Console.WriteLine(e.Error.Message);
            }
        }

        private void loadChapters()
        {
            List<string> chapterIds = mangaInfo.Chapters;
            //foreach (string id in chapterIds)
            //{
            //    dl.GetChapterAsync(id);
            //}
            Parallel.ForEach(chapterIds, (id) =>
            {
                dl.GetChapterAsync(id);
            });
        }

        private void renderListView()
        {
            chapterListView.ItemsSource = chapters;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}