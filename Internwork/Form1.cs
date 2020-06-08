using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using Newtonsoft.Json;

namespace Internwork
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string latitude = textBox1.Text;
            string longitude = textBox2.Text;
            string url = "https://api.sunrise-sunset.org/json?lat=" + latitude + "&lng=" + longitude + "&formatted=1&date=today";

            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync(url))
                {
                    using (HttpContent content = response.Content)
                    {
                        string json = await content.ReadAsStringAsync();
                        rootobject root = JsonConvert.DeserializeObject<rootobject>(json);
                        if (root.status != "OK")
                            MessageBox.Show("Enter Valid Input");
                        else
                        {
                            DateTime univd, locald;
                            string strtime = root.results.sunrise;
                            univd = DateTime.Parse(strtime);
                            locald = univd.ToLocalTime();
                            label3.Text = "Sunrise is at " + locald.ToLongTimeString();
                            DateTime locald2, univd2;
                            string strtime2 = root.results.sunset;
                            univd2 = DateTime.Parse(strtime2);
                            locald2 = univd2.ToLocalTime();
                            label4.Text = "Sunset is at " + locald2.ToLongTimeString();
                        }



                    }
                }
            }
        }
    }
}
