using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using MangaDownloader;

namespace MangaToolsApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void SearchBtn_Clicked(object sender, EventArgs e)
        {
            Console.WriteLine("Search Btn Click");
            if (!string.IsNullOrEmpty(IdEntry.Text))
            {
                Navigation.PushAsync(new ChapterListPage(IdEntry.Text));
            }
        }
    }
}