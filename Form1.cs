using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameAssistant
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static bool _started;
        private async void button1_Click(object sender, EventArgs e)
        {
            update_frequency = Convert.ToInt32(textBox2.Text);

            if (_started) return;

            _started = true;
            await Program.StartTask();
        }

        
        private async void button2_Click(object sender, EventArgs e)
        {
            _started = false;
            await Program.StopTask();
        }

        public static int update_frequency;
        private void button3_Click(object sender, EventArgs e)
        {
            textBox2.Text = textBox1.Text;
            update_frequency = Convert.ToInt32(textBox2.Text);
        }

        private bool _AHstarted;
        private async void Button4_Click(object sender, EventArgs e)
        {
            update_frequency = Convert.ToInt32(textBox2.Text);

            if (_AHstarted) return;

            _AHstarted = true;
            await Program.SnipeAH();
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            update_frequency = Convert.ToInt32(textBox2.Text);

            if (_started) return;

            _started = true;
            await Program.mainloop();
        }
    }
}
