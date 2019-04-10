using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

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
            if (string.IsNullOrEmpty(IdEntry.Text))
            {
                return;
            }

            Navigation.PushAsync(new ChapterListPage());
        }
    }
}
