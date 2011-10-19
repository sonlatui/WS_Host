using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace WS_Host
{
    public partial class Form1 : Form
    {
        ServiceHost httpHost;
        ServiceHost netHost;
        ServiceHost wsHost;

        [ServiceContract]
        public interface ILib
        {
            [OperationContract]
            string GetTacGia(string TacPham);
            
        }
        public class Lib : ILib
        {
            public string GetTacGia(string TacPham)
            {
                if (TacPham == "Nhat Ky Trong Tu")

                    return "Ho Chi Minh";
                if (TacPham == "So Do")

                    return "Vu Trong Phung";
                if (TacPham == "Tat Den")

                    return "Ngo Tat To";
                return "Khong Biet =.=!";

            }
        }   
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            status.Text = "";
            Type contractType = typeof(ILib);
            Type instanceType = typeof(Lib);
            if (checkBox2.Checked)
            {
                httpHost = new ServiceHost(instanceType, new Uri[] { new Uri(textBox1.Text) });
                try
                {
                    httpHost.AddServiceEndpoint(contractType, new BasicHttpBinding(), textBox2.Text);
                    status.Text += "BasicHTTP Ready\n";
                    //System.Diagnostics.Process.Start(textBox1.Text);
                    if (checkBox1.Checked)
                    {
                        ServiceMetadataBehavior behavior = new ServiceMetadataBehavior();
                        behavior.HttpGetEnabled = true;
                        httpHost.Description.Behaviors.Add(behavior);
                        httpHost.AddServiceEndpoint(typeof(IMetadataExchange), new BasicHttpBinding(), "MEX");
                    }
                    httpHost.Open();
                }
                catch (Exception ex)
                {
                    status.Text = ex.Message;
                    httpHost.Abort();
                }
            }
            if (checkBox3.Checked)
            {
                
                try
                {
                    NetTcpBinding A = new NetTcpBinding(SecurityMode.Transport);
                    netHost = new ServiceHost(instanceType, new Uri[] { new Uri(textBox3.Text) });
                    netHost.AddServiceEndpoint(contractType, A, textBox3.Text);
                    netHost.Open();
                    status.Text += "NetTcp Ready\n";
                    //System.Diagnostics.Process.Start(textBox3.Text); 
                }
                catch (Exception ex)
                {
                    status.Text = ex.Message;
                    netHost.Abort();
                }
            }
            if (checkBox4.Checked)
            {
                wsHost = new ServiceHost(instanceType, new Uri[] { new Uri(textBox4.Text) });
                try
                {
                    wsHost.AddServiceEndpoint(contractType, new WSHttpBinding(), textBox4.Text);
                    if (checkBox1.Checked)
                    {
                        ServiceMetadataBehavior behavior = new ServiceMetadataBehavior();
                        behavior.HttpGetEnabled = true;
                        wsHost.Description.Behaviors.Add(behavior);
                        wsHost.AddServiceEndpoint(typeof(IMetadataExchange), new WSHttpBinding(), "MEX");
                    }
                    wsHost.Open();
                    status.Text += "WsHTTP Ready";
                    //System.Diagnostics.Process.Start(textBox4.Text);
                }
                catch (Exception ex)
                {
                    status.Text = ex.Message;
                    wsHost.Abort();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                httpHost.Abort();
            }
            if (checkBox3.Checked)
            {
                netHost.Abort();
            }
            if (checkBox4.Checked)
            {
                wsHost.Abort();
            }
        }
    }
}
