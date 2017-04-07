using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using WatTvdb.V1;

namespace WatTvdb.WP7Sample
{
    public partial class MainPage : PhoneApplicationPage
    {
        private TvdbMirrors tvdbMirrors;
        private string ApiKey { get; set; }

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            ApiKey = "apikey";
            Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            // load the mirror information
            var api = new Tvdb(ApiKey);
            api.GetMirrors(null, result =>
                {
                    tvdbMirrors = result.Data;
                });
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (tbSearch.Text != null)
            {
                Tvdb api = new Tvdb(ApiKey);
                api.SearchSeries(tbSearch.Text, null, result =>
                    {
                        if (result.Data != null)
                        {
                            listbox.ItemsSource = (from s in result.Data
                                                   select new Series
                                                   {
                                                       SeriesId = s.id,
                                                       Title = s.SeriesName,
                                                       Overview = s.Overview,
                                                       Banner = s.banner
                                                   }).ToList();
                        }
                        else
                            MessageBox.Show(api.Error, "Error", MessageBoxButton.OK);
                    });
            }
        }
    }
}