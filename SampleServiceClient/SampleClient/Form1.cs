using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SampleServiceClient;
using SampleServiceClient.ServiceReference1;
using System.ServiceModel.Security;

namespace SampleClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs ea)
        {
            var client = new SampleServiceClient.ServiceReference1.PersonManagerSyncPortTypeClient();
            client.ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.None;
            client.ClientCredentials.UserName.UserName = "user1";
            client.ClientCredentials.UserName.Password = "tratata";

            // Create person
            var sourceId = Guid.NewGuid();
            var hdr = new imsx_RequestHeaderInfoType();
            var req = new createPersonRequest();
            req.personRecord = UserMapper.MapToRecord(new User { SourceId = sourceId.ToString(), FirstName = "Vlad", LastName = "Ogay", Language = "en" });

            textBox1.Text += "=== CreatePerson ===";
            try
            {
                client.createPerson(hdr, req);
                textBox1.Text += Environment.NewLine;
                textBox1.Text += "OK";
            }
            catch (Exception e)
            {
                textBox1.Text += Environment.NewLine;
                textBox1.Text += e.Message;
                if (e.InnerException != null)
                {
                    textBox1.Text += Environment.NewLine;
                    textBox1.Text += e.InnerException.Message;
                    if (e.InnerException.InnerException != null)
                    {
                        textBox1.Text += Environment.NewLine;
                        textBox1.Text += e.InnerException.InnerException.Message;
                    }
                }
            }

            // Read person that we've just created
            var req2 = new readPersonRequest { sourcedId = sourceId.ToString() };
            readPersonResponse resp2;

            textBox1.Text += Environment.NewLine;
            textBox1.Text += Environment.NewLine;
            textBox1.Text += "=== ReadPerson ===";
            try
            {
                client.readPerson(hdr, req2, out resp2);
                textBox1.Text += Environment.NewLine;
                textBox1.Text += "OK";
            }
            catch (Exception e2)
            {
                textBox1.Text += Environment.NewLine;
                textBox1.Text += e2.Message;
                if (e2.InnerException != null)
                {
                    textBox1.Text += Environment.NewLine;
                    textBox1.Text += e2.InnerException.Message;
                }
            }
        }
    }
}
