using Laba2.Interfaces;
using Laba2.XmlAnalyzers;
using System.Text;
using System.Text.Encodings.Web;

namespace Laba2.UI
{
    public partial class MainPage : ContentPage
    {
        private string _selectedXmlFilePath;
        
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnPickFileClicked(object sender, EventArgs e)
        {
            try
            {
                var result = await FilePicker.PickAsync(PickOptions.Default);

                if (result != null)
                {
                    _selectedXmlFilePath = result.FullPath;
                    StatusLabel.Text = $"Вибрано файл: {result.FileName}";

                    GenerateHtmlButton.IsEnabled = true;
                    SearchEntry.IsEnabled = true;
                    SearchButton.IsEnabled = true;
                }
                else
                {
                    StatusLabel.Text = "Файл не вибрано.";
                }
            }
            catch (Exception ex)
            {
                StatusLabel.Text = $"Помилка: {ex.Message}";
            }
        }

        private async void OnGenerateHtmlClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedXmlFilePath))
            {
                StatusLabel.Text = "Спочатку виберіть XML файл!";
                return;
            }

            try
            {
                var xmlAnalyzer = new ScheduleLinqAnalyzer(); 
                var htmlGenerator = new SchedulerHtmlGenerator(xmlAnalyzer);

                string htmlFilePath = await Task.Run(() => htmlGenerator.GenerateHtmlSchedule(_selectedXmlFilePath));

                StatusLabel.Text = "Генерація завершена!";
                FilePathLabel.Text = $"HTML збережено: {htmlFilePath}";

                HtmlWebView.Source = new Uri(htmlFilePath);
            }
            catch (Exception ex)
            {
                StatusLabel.Text = $"Помилка: {ex.Message}";
            }
        }

        private async void OnSearchClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedXmlFilePath))
            {
                StatusLabel.Text = "Спочатку виберіть XML файл!";
                return;
            }

            try
            {
                var xmlAnalyzer = new ScheduleLinqAnalyzer();
                var htmlGenerator = new SchedulerHtmlGenerator(xmlAnalyzer);
                
                string htmlFilePath = await Task.Run(() => htmlGenerator.GenerateHtmlSchedule(_selectedXmlFilePath, SearchEntry.Text));

                ClearSearchButton.IsEnabled = true;
                HtmlWebView.Source = new Uri(htmlFilePath);
            }
            catch (Exception ex)
            {
                StatusLabel.Text = $"Помилка: {ex.Message}";
            }
        }
        
        private async void OnClearSearchClicked(object sender, EventArgs e)
        {
            try
            {
                SearchEntry.Text = string.Empty;

                var xmlAnalyzer = new ScheduleLinqAnalyzer();
                var htmlGenerator = new SchedulerHtmlGenerator(xmlAnalyzer);

                string htmlFilePath = await Task.Run(() => htmlGenerator.GenerateHtmlSchedule(_selectedXmlFilePath));

                HtmlWebView.Source = new Uri(htmlFilePath);
            }
            catch (Exception ex)
            {
                StatusLabel.Text = $"Помилка: {ex.Message}";
            }
        }

    }
}