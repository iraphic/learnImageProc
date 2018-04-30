using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DirectShowLib;
using Emgu.Util;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.CV.UI;

namespace deteksiii
{
    public partial class Form1 : Form
    {
        private VideoCapture cap = null;
        Mat frame = new Mat();

        double lowThr;
        double highThr;
        double accThr;
        double dist,
               param,
               minRad,
               maxRad,
               rad1;
        int radiusThr,
            thick,
            blue,
            green,
            red,
            gb;

        public Form1()
        { 
            InitializeComponent();
        }

        private void imageBox2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            cap = new VideoCapture(0);
            cap.ImageGrabbed += processFrame;
            cap.Start();
            timer1.Start();
        }

        private void processFrame(object sender, EventArgs e)
        {
            cap.Retrieve(frame, 0);

            Mat convertClr = new Mat();
            Mat gBlur = new Mat();

            CvInvoke.CvtColor(frame, convertClr, ColorConversion.Bgr2Gray);

            Mat copy = frame.Clone();

            CvInvoke.GaussianBlur(convertClr, gBlur, new Size(9, 9), 0, 0);

            CircleF[] circles = CvInvoke.HoughCircles(gBlur, HoughType.Gradient, minRad, maxRad, lowThr, accThr, 5);

            foreach (CircleF circle in circles)
            {
                CvInvoke.Circle(copy, new Point((int)circle.Center.X, (int)circle.Center.Y), radiusThr, new MCvScalar(blue, green, red), thick, LineType.EightConnected, 0);
            }

            imageBox1.Image = frame;
            imageBox2.Image = convertClr;
            imageBox3.Image = copy;
            imageBox4.Image = gBlur;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lowThr = trackThr.Value;
            accThr = accThrBar.Value;
            radiusThr = radiusBar.Value;
            thick = thickBar.Value;
            minRad = minRadiusBar.Value;
            maxRad = maxRadiusBar.Value;
            blue = blueBar.Value;
            green = greenBar.Value;
            red = redBar.Value;

            label12.Text = lowThr.ToString();
            label13.Text = accThr.ToString();
            label14.Text = radiusThr.ToString();
            label15.Text = thick.ToString();
            label10.Text = minRad.ToString();
            label11.Text = maxRad.ToString();
            label16.Text = blue.ToString();
            label17.Text = green.ToString();
            label18.Text = red.ToString();
        }


    }
}
