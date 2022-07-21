﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace timerFormMain
{
    public partial class Form1 : Form
    {


        private bool mouseDown;
        private Point lastLocation;








        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point((this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        /// <summary>
        /// </summary>
        int countdown = 0;
        int original = 1;
        bool countdownEnabled = true;

        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            updateTimer();
        }

        private void add30secs_Click(object sender, EventArgs e)
        {
            addTime(add30secs);
        }

        private void add1min_Click(object sender, EventArgs e)
        {
            addTime(add1min);
        }

        private void add5mins_Click(object sender, EventArgs e)
        {
            addTime(add5mins);
        }

        private void stopStart_Click(object sender, EventArgs e)
        {
            startStopFunc();
        }

        private void reset_Click(object sender, EventArgs e)
        {
            resetTimer();
        }

        private void add3mins_Click(object sender, EventArgs e)
        {
            addTime(add3mins);
        }

        private void startStopFunc()
        {
            if (countdown > 0 && countdownEnabled)
            {
                if (timer1.Enabled)
                {
                    timer1.Enabled = false;
                    StartStop.Text = "▶ Start";
                }
                else
                {
                    timer1.Enabled = true;
                    StartStop.Text = "■ Stop";
                    original = countdown;
                }
            }
            else
            {
                if (timer1.Enabled)
                {
                    timer1.Enabled = false;
                    StartStop.Text = "▶ Start";
                }
                else
                {
                    timer1.Enabled = true;
                    StartStop.Text = "■ Stop";
                }
            }
        }

        private void updateTimer()
        {
            if (countdownEnabled) countdown -= 1;
            else countdown += 1;
            
            if (countdownEnabled) timerBox.Text = (countdown / 10 / 60).ToString("00") + ":" + (countdown / 10 % 60).ToString("00");
            else timerBox.Text = (countdown / 10 / 60).ToString("00") + ":" + (countdown / 10 % 60).ToString("00") + "." + (countdown % 10);
            if (countdown == 0f)
            {
                timer1.Enabled = false;
                StartStop.Text = "▶ Start";
            }
            Console.WriteLine((countdown / 60).ToString("00") + ":" + (countdown % 60).ToString("00"));
            Refresh();
        }


        private void addTime(Button sender)
        {
            int amount;
            if (sender == add30secs) amount = 30;
            else amount = Convert.ToInt32(sender.Text.Replace(" secs", "").Replace(" mins", "").Replace(" min", "").Replace("+", ""))*60;
            countdown += amount*10;
            timerBox.Text = (countdown / 600).ToString("00") + ":" + (countdown /10 % 60).ToString("00");
            original += amount*10;
        }

        private void resetTimer()
        {
            timer1.Enabled = false;
            countdown = 0;
            timerBox.Text = "00:00";
            StartStop.Text = "▶ Start";
            Refresh();
        }

        private void timerBox_KeyDown(object sender, KeyEventArgs e)
        {
            enterValue(e);
        }

        private void enterValue(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try { countdown = Convert.ToInt32(timerBox.Text) * 6000; startStopFunc(); }
                catch { }
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            drawCircle(e);
        }

        private void drawCircle(PaintEventArgs e)
        {
            if (countdown == 0) e.Graphics.DrawArc(new Pen(Color.FromArgb(255, 203, 112), 5), new Rectangle(25, 25, 250, 250), 0, 360);
            Console.WriteLine(360 * ((float)countdown / (float)original));
            e.Graphics.DrawArc(new Pen(Color.FromArgb(255, 203, 112), 5), new Rectangle(25, 25, 250, 250), -90, 360 * ((float)countdown / (float)original));// Console.WriteLine(360 * (countdown / original)); }
        }

        private void switchMode_Click(object sender, EventArgs e)
        {
            if (countdownEnabled)
            {
                countdownEnabled = false;
                add1min.Enabled = false;
                add30secs.Enabled = false;
                add3mins.Enabled = false;
                add5mins.Enabled = false;
                pictureBox1.Hide();
            }
            else
            {
                countdownEnabled = true;
                add1min.Enabled = true;
                add30secs.Enabled = true;
                add3mins.Enabled = true;
                add5mins.Enabled = true;
                pictureBox1.Show();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
