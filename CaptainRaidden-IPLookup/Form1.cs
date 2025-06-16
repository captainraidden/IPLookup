using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IPLookup
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
        "IP Lookup Tool\n\n" +
        "Developed by: CaptainRaidden\n" +
        "Using: ip-api.com for IP Geolocation\n\n" +
        "Thanks for using our tool!",
        "Credits",
        MessageBoxButtons.OK,
        MessageBoxIcon.Information
        );

        }

        private async void guna2Button1_Click(object sender, EventArgs e)
        {
            string ip = txtIP.Text.Trim();
            if (string.IsNullOrEmpty(ip))
            {
                MessageBox.Show("Please enter an IP address.");
                return;
            }

            rtbResult.Text = "Loading...";
            await Task.Delay(3000);

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string url = $"http://ip-api.com/json/{ip}";
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();

                    JObject data = JObject.Parse(responseBody);

                    string result = $"IP Address: {data["query"]}\n" +
                                    $"Country: {data["country"]}\n" +
                                    $"Region: {data["regionName"]}\n" +
                                    $"City: {data["city"]}\n" +
                                    $"ZIP: {data["zip"]}\n" +
                                    $"ISP: {data["isp"]}\n" +
                                    $"Org: {data["org"]}\n" +
                                    $"AS: {data["as"]}\n" +
                                    $"Timezone: {data["timezone"]}\n" +
                                    $"Lat: {data["lat"]}, Lon: {data["lon"]}";

                    rtbResult.Text = result;
                }
            }
            catch (Exception ex)
            {
                rtbResult.Text = $"Error: {ex.Message}";
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            txtIP.Clear();
            rtbResult.Clear();
        }
    }
}
    

