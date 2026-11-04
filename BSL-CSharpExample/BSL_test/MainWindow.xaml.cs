using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BSL_test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string BSL_SID = "";
        private static readonly HttpClient client = new HttpClient();

        public MainWindow()
        {
            InitializeComponent();
        }


        private async void bt_login_Click(object sender, RoutedEventArgs e)
        {
            // Connect via user and password
            string responseString;
            var values = new Dictionary<string, string>
                        {
                            { "User", bsl_user.Text },
                            { "Pwd", bsl_pwd.Text }
                        };
            var content = new StringContent(JsonSerializer.Serialize(values), Encoding.UTF8, "application/json");
            var url = bsl_url.Text + "/Login";
           
            PrintMessage(result, "Sending command: " + url, Colors.Green);
            // sned to server
            try
            {
                var responseMessage = await client.PostAsync(url, content);
                responseString = await responseMessage.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                responseString = string.Format("RestHttpClient.SendRequest failed: {0}", ex);
            }

            PrintMessage(result, responseString, Colors.Red);

            // Read and save Session Id
            var options = new JsonDocumentOptions { AllowTrailingCommas = true };
            try
            {
                JsonDocument document = JsonDocument.Parse(responseString, options);
                BSL_SID = document.RootElement.GetProperty("beas-sessionid").GetString();

                client.DefaultRequestHeaders.Remove("Cookie");
                client.DefaultRequestHeaders.Add("Cookie", "beas-sessionid=" + BSL_SID);
            }
            catch (Exception ex)
            {
                BSL_SID = "";
            }
        }
        private async void bt_logout_Click(object sender, RoutedEventArgs e)
        {
            string responseString;
            var values = new Dictionary<string, string>
                        {
                        };
            var content = new StringContent(JsonSerializer.Serialize(values), Encoding.UTF8, "application/json");
            var url = bsl_url.Text + "/Logout";

            PrintMessage(result, "Sending command: " + url, Colors.Green);
            try
            {
                var responseMessage = await client.PostAsync(url, content);
                responseString = await responseMessage.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                responseString = string.Format("RestHttpClient.SendRequest failed: {0}", ex);
            }
            client.DefaultRequestHeaders.Remove("Cookie");
            PrintMessage(result,responseString, Colors.Red);
        }
        private async void bt_get_Click(object sender, RoutedEventArgs e)
        {
            string responseString;
            var url = bsl_url.Text + get_resource.Text;

            PrintMessage(result, "Sending command: " + url, Colors.Green);
            try
            {
                responseString = await client.GetStringAsync(url);
            }
            catch (Exception ex)
            {
                responseString = string.Format("RestHttpClient.SendRequest failed: {0}", ex);
            }

            PrintMessage(result,responseString, Colors.Red);
        }
        private async void bt_post_Click(object sender, RoutedEventArgs e)
        {
            string responseString;

            TextRange textRange = new TextRange(post_json.Document.ContentStart,post_json.Document.ContentEnd);
            var content = new StringContent(textRange.Text, Encoding.UTF8, "application/json");
            var url = bsl_url.Text + post_resource.Text;

            PrintMessage(result, "Sending command: " + url, Colors.Green);
            try
            {
                var responseMessage = await client.PostAsync(url, content);
                responseString = await responseMessage.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                responseString = string.Format("RestHttpClient.SendRequest failed: {0}", ex);
            }

            PrintMessage(result, responseString, Colors.Red);
        }



        private void PrintMessage(RichTextBox box, string message, Color color)
        {
            AppendText(box, "[" + DateTime.Now.ToShortTimeString() + "]", Colors.LightGray);
            box.AppendText(" \t ");
            AppendText(box, message, color);
            box.AppendText(Environment.NewLine);
            box.ScrollToEnd();
        }
        void AppendText(RichTextBox box, string text, Color color)
        {
            box.AppendText(text);
        }

    }
}
