using Microsoft.Web.WebView2.Core;
using System;
using System.IO;
using System.Windows;
using System.Threading.Tasks;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeWebView();
        }

        private async void InitializeWebView()
        {
            await InitializeWebViewAsync();
        }

        private async Task InitializeWebViewAsync()
        {
            var op = new CoreWebView2EnvironmentOptions("--disable-web-security");
            var env = await CoreWebView2Environment.CreateAsync(null, null, op);
            await webView.EnsureCoreWebView2Async(env);

            var currentDirectory = Directory.GetCurrentDirectory();
            var reactAppPath = Path.Combine(currentDirectory, "dist", "index.html");

            if (File.Exists(reactAppPath))
            {
                // Enable developer tools for debugging
                webView.CoreWebView2.OpenDevToolsWindow();

                // Navigate to your React app's index.html using file protocol
                webView.CoreWebView2.Navigate(new Uri(reactAppPath).AbsoluteUri);
            }
            else
            {
                MessageBox.Show("React app not found. Please build the React app and ensure the files are in the correct location.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
