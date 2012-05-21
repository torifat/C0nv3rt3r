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
using Clarity.Phone.Controls;
using GalaSoft.MvvmLight.Command;

namespace C0nv3rt3r
{
    public partial class Time : AnimatedBasePage
    {
        public Time()
        {
            InitializeComponent();
            CreateTiles();
            AnimationContext = LayoutRoot;
        }


        private void CreateTiles()
        {
            ICommand tapCommand = new RelayCommand<string>(this.HubTileTapAction);
            List<TileItem> tileItems = new List<TileItem>()
            {
                new TileItem() {
                    GroupTag = "Time",
                    Title = "Hour",
                    Message = "0",
                    TapCommand = tapCommand
                },
                new TileItem() {
                    GroupTag = "Time",
                    Title = "Minute",
                    Notification = "0",
                    TapCommand = tapCommand
                },
                new TileItem() {
                    GroupTag = "Time",
                    Title = "Second",
                    Notification = "0",
                    TapCommand = tapCommand
                },
            };

            this.tileList.ItemsSource = tileItems;
        }

        private void HubTileTapAction(string param)
        {
            //Uri nextPage = new Uri("/View/" + param + ".xaml", UriKind.Relative);
            Uri nextPage = new Uri("/View/Time.xaml", UriKind.Relative);
            NavigationService.Navigate(nextPage);
        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            HubTileService.FreezeGroup("Time");
        }
    }
}