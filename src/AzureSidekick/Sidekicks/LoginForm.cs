using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Windows.Forms;
using Settings = AzureSidekick.Properties.Settings;

namespace AzureSidekick.Sidekicks
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            var isValid = CheckAzureDevOpsPAT(Settings.Default.AzureServer, Settings.Default.AccessToken );
            label3.Text =(isValid
                ? "PAT معتبر است ✅"
                : "PAT نامعتبر ❌");
        }

        static bool CheckAzureDevOpsPAT(string organizationUrl, string pat)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    // Authorization header با PAT
                    var authToken = Convert.ToBase64String(Encoding.ASCII.GetBytes($":{pat}"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);

                    // درخواست به endpoint پروژه‌ها
                    var url = $"{organizationUrl}/_apis/projects?api-version=7.1-preview.4";
                    var response = client.GetAsync(url).Result;

                    // اگر StatusCode 200 یا 2xx باشد، PAT معتبر است
                    return response.IsSuccessStatusCode;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
