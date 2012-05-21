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
using GalaSoft.MvvmLight.Command;
using Clarity.Phone.Controls.Animations;
using Clarity.Phone.Controls;

namespace C0nv3rt3r
{
    public partial class MainPage : AnimatedBasePage
    {
        // Constructor
        public MainPage()
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
                    Title = "Time",
                    Message = "Hour, Minutes, Seconds",
                    IconUri = "/Images/time.png",
                    TapCommand = tapCommand
                },
                new TileItem() {
                    Title = "Length",
                    Notification = "Distance",
                    IconUri = "/Images/distance.png",
                    TapCommand = tapCommand
                },
                new TileItem() {
                    Title = "Speed",
                    Notification = "Acceleration",
                    IconUri = "/Images/acceleration.png",
                    TapCommand = tapCommand
                },
                new TileItem() {
                    Title = "Heat",
                    Notification = "Temperature",
                    IconUri = "/Images/temperature.png",
                    TapCommand = tapCommand
                },
                new TileItem() {
                    Title = "Density",
                    Notification = "Volume",
                    IconUri = "/Images/density.png",
                    TapCommand = tapCommand
                }
            };

            this.tileList.ItemsSource = tileItems;
        }

        private void HubTileTapAction(string param)
        {
            //Uri nextPage = new Uri("/View/" + param + ".xaml", UriKind.Relative);
            Uri nextPage = new Uri("/View/Time.xaml", UriKind.Relative);
            NavigationService.Navigate(nextPage);
        }

        protected override Clarity.Phone.Controls.Animations.AnimatorHelperBase GetAnimation(AnimationType animationType, Uri toOrFrom)
        {

            //you could factor this into an intermediate base page to have some other defaults
            //such as always continuuming to a pivot page or rultes based on the page, direction and where you are goign to/coming from

            if (toOrFrom != null)
            {
                if (animationType == AnimationType.NavigateForwardOut)
                    return new TurnstileFeatherForwardOutAnimator() { ListBox = tileList, RootElement = LayoutRoot };
                else
                    return new TurnstileFeatherBackwardInAnimator() { ListBox = tileList, RootElement = LayoutRoot };
            }

            return base.GetAnimation(animationType, toOrFrom);
        }

        protected override void AnimationsComplete(AnimationType animationType)
        {
            switch (animationType)
            {
                case AnimationType.NavigateForwardIn:
                    //Add code to set data context and bind data
                    //you really only need to do that on forward in. on backward in everything
                    //will be there that existed on forward out
                    break;

                case AnimationType.NavigateBackwardIn:
                    //reset list so you can select the same element again
                    tileList.SelectedIndex = -1;
                    break;
            }


            base.AnimationsComplete(animationType);
        }

    }
}