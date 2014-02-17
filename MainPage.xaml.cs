using System;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml.Controls;

namespace JayBeavers.ChronoZoom
{
    public sealed partial class MainPage
    {

        private Uri _currentUri;
 
        public MainPage()
        {
            InitializeComponent();

            ChronoZoomWebView.NavigationCompleted += ChronoZoomWebView_NavigationCompleted;
 
            DataTransferManager.GetForCurrentView().DataRequested += MainPage_DataRequested;
        }

        void MainPage_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            if (_currentUri == null) return;

            var request = args.Request;
            request.Data.Properties.Title = "I'd like to share this timeline '" + ChronoZoomWebView.DocumentTitle + "' with you...";
            request.Data.Properties.Description = "Share timeline '" + ChronoZoomWebView.DocumentTitle + "'";
            request.Data.SetWebLink(_currentUri);
            request.Data.Properties.ContentSourceWebLink = _currentUri;
        }

        void ChronoZoomWebView_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            _currentUri = args.Uri;
        }
    }
}