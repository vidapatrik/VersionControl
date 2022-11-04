using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using week06._2.Abstractions;
using week06._2.Entities;

namespace week06._2
{
    public partial class Form1 : Form
    {
        private List<Toy> _toys = new List<Toy>();
        private IToyFactory _factory;
        private Toy _nextToy;
        public IToyFactory Factory
        {
            get { return _factory; }
            set { _factory = value;
                DisplayNext();
            }
        }
        public Form1()
        {
            InitializeComponent();
            Factory = new CarFactory();
        }

        private void conveyorTimer_Tick(object sender, EventArgs e)
        {
            int Right = 0;
            foreach (var item in _toys)
            {
                item.MoveToy();
                if(item.Left> Right)
                { Right = item.Left; }

                if (Right > 1000)
                {
                    var oldestToy = _toys[0];
                    mainPanel.Controls.Remove(oldestToy);
                    _toys.Remove(oldestToy);
                }
            }
        }

        private void createTimer_Tick(object sender, EventArgs e)
        {
            var toy = Factory.CreateNew();
            _toys.Add(toy);
            toy.Left = -toy.Width;
            mainPanel.Controls.Add(toy);


        }

        private void button1_Click(object sender, EventArgs e)
        {
            Factory = new CarFactory();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Factory = new BallFactory
            { BallColor = colorButton.BackColor
            };
        }
        private void DisplayNext()
        {
            if(_nextToy != null)
            { Controls.Remove(_nextToy); }
            _nextToy = Factory.CreateNew();
            _nextToy.Top = label1.Top + label1.Height + 20;
            _nextToy.Left = label1.Left;
            Controls.Add(_nextToy);
        }

        private void colorButton_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var color = new ColorDialog();
            color.Color = button.BackColor;
            if (color.ShowDialog() != DialogResult.OK) return;
            button.BackColor = color.Color;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Factory = new PresentFactory();
        }
    }
}
