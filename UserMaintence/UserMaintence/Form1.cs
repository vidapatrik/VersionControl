using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserMaintence.Entities;

namespace UserMaintence
{
    public partial class Form1 : Form
    {

        BindingList<Class1> users = new BindingList<Class1>();

        public Form1()
        {
            InitializeComponent();
            label1.Text = Resource1.FullName;
            button2.Text = Resource1.button2;
            button1.Text = Resource1.button1;
            listBox1.DataSource = users;
            listBox1.ValueMember = "ID";
            listBox1.DisplayMember = "FullName";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var u = new Class1()
            {
                FullName = label1.Text+label2.Text,

            };
            users.Add(u);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog() == DialogResult.OK) //!a StreamWriter villanyos!!
            {
                StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.UTF8);
                foreach (Class1 s in users)
                {
                    sw.Write(s.ID);
                    sw.Write(';');
                    sw.Write(s.FullName);
                }

                sw.Close();

            }
        }
    }
}
