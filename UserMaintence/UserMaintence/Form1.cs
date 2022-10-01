using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            button1.Text = Resource1.Add;
            listBox1.DataSource = users;
            listBox1.ValueMember = "ID";
            listBox1.DisplayMember = "FullName";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var u = new Class1()
            {
                FullName = label1.Text,
               
            };
            users.Add(u);
        }
    }
}
