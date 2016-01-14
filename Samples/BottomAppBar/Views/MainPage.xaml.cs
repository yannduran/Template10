﻿using Template10.Controls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Sample.Views
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }


private void SampleClick(object sender, RoutedEventArgs e)
{
    Shell.SetBusy(true, "Please wait...");
    BottomAppBar.IsEnabled = false;
    Template10.Common.WindowWrapper.Current().Dispatcher.Dispatch(() =>
    {
        Shell.SetBusy(false);
        BottomAppBar.IsEnabled = true;
    }, 3000);
}
    }
}
