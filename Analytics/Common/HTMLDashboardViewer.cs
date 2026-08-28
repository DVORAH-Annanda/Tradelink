using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;

namespace Analytics.Common
{
    public class HtmlDashboardViewer : UserControl
    {
        private readonly WebView2 _webView;
        private Task _initializationTask;

        public HtmlDashboardViewer()
        {
            _webView = new WebView2
            {
                Dock = DockStyle.Fill
            };

            Controls.Add(_webView);
        }

        public WebView2 WebView
        {
            get { return _webView; }
        }

        public Task InitializeAsync()
        {
            if (_initializationTask == null)
            {
                _initializationTask = InitializeWebViewAsync();
            }

            return _initializationTask;
        }

        private async Task InitializeWebViewAsync()
        {
            string userDataFolder = Path.Combine(
                Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData),
                "TTI2",
                "WebView2"
            );

            Directory.CreateDirectory(userDataFolder);

            CoreWebView2Environment environment =
                await CoreWebView2Environment.CreateAsync(
                    null,
                    userDataFolder
                );

            await _webView.EnsureCoreWebView2Async(environment);

            _webView.CoreWebView2.Settings.IsScriptEnabled = true;

#if DEBUG
            _webView.CoreWebView2.Settings.AreDevToolsEnabled = true;
#else
            _webView.CoreWebView2.Settings.AreDevToolsEnabled = false;
#endif
        }

        public async Task ShowHtmlAsync(string html)
        {
            await InitializeAsync();

            _webView.NavigateToString(html);
        }

        public async Task NavigateToFileAsync(string htmlFilePath)
        {
            await InitializeAsync();

            if (!File.Exists(htmlFilePath))
            {
                throw new FileNotFoundException(
                    "Dashboard HTML file could not be found.",
                    htmlFilePath
                );
            }

            _webView.Source = new Uri(htmlFilePath);
        }

        public async Task ExecuteScriptAsync(string javascript)
        {
            await InitializeAsync();

            await _webView.CoreWebView2.ExecuteScriptAsync(javascript);
        }
    }
}