using Microsoft.Data.SqlClient;
using Microsoft.Web.WebView2.Core;

namespace C_Project
{
    public partial class Form1 : Form
    {
        string connectionString = @"Server=(LocalDB)\MSSQLLocalDB;Initial Catalog=Appointments;Integrated Security=True;TrustServerCertificate=True";
        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
        }

        private void webView21_Click(object sender, EventArgs e)
        {

        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            webView21.Dock = DockStyle.Fill;
            this.WindowState = FormWindowState.Maximized;

            await webView21.EnsureCoreWebView2Async();

            string htmlPath = Path.Combine(Application.StartupPath, "index.html");
            webView21.CoreWebView2.Navigate(new Uri(htmlPath).AbsoluteUri);

            // Listen for the "window.chrome.webview.postMessage" from HTML
            webView21.WebMessageReceived += Message;
        }
        private async void Message(object sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            // Only proceed if the message sent from HTML is "save_booking"
            if (e.TryGetWebMessageAsString() == "save_booking")
            {
                try
                {
                    // Fetch values from the HTML inputs
                    string patient = await GetValueFromId("pname");
                    string physician = await GetValueFromId("doctor");
                    string date = await GetValueFromId("date");
                    string time = await GetValueFromId("time");
                    
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        await con.OpenAsync();
                        // Matching your DB columns: Physician, Date, Time, PatientName
                        string query = "INSERT INTO booking(PatientName, Physician, BookedDate, BookedTime) VALUES (@pname, @phys, @date, @time)";

                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@pname", patient);
                            cmd.Parameters.AddWithValue("@phys", physician);
                            cmd.Parameters.AddWithValue("@date", date);
                            cmd.Parameters.AddWithValue("@time", time);
                         
                            await cmd.ExecuteNonQueryAsync();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database Error: " + ex.Message);
                }
            }
        }

        // Helper to get input values and remove the extra JSON quotes returned by WebView2
        private async Task<string> GetValueFromId(string id)
        {
            string val = await webView21.CoreWebView2.ExecuteScriptAsync($"document.getElementById('{id}').value");

            // If the element is missing or empty, val might be "null" or "\"\""
            if (string.IsNullOrEmpty(val) || val == "null") return string.Empty;

            return val.Trim('"');
        }
    }
}
