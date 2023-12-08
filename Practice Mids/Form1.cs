using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Schema;

namespace Practice_Mids
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Random obj = new Random();
            int num = 100;
            //int min = int.Parse(textBox1.Text);
            //int max = int.Parse(textBox2.Text);
            double temp;
            double sum = 0;

            for (int i = 0; i < num; i++)
            {
                temp = obj.Next(1, 7);
                sum = sum + temp;
                textBox3.Text = textBox3.Text + "\r\t " + (temp);
            }
            textBox4.Text = textBox4.Text + (sum / num);
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double delx, sigma, x, xmin = -2, xmax = 2, D = 1, t = 0.005;
            int n = 1000;
            delx = (xmax - xmin) / (n - 1);
            sigma = Math.Sqrt(2 * D * t);

            double[] rho = new double[n];
            x = xmin;

            Graphics gg = CreateGraphics();
            SolidBrush sb = new SolidBrush(Color.Red);

            for (int i = 0; i < n; i++)
            {
                x = xmin + i * delx;
                rho[i] = 1 / sigma * Math.Exp((-x * x) / (2 * sigma * sigma));
                gg.FillEllipse(sb, 400 + (float)x * 50, 300 - (float)rho[i] * 20, 5, 5);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            float x1 = 50, x2 = 100;
            float n = 1000, n1 = 0, xmin = x1 - 10, xmax = x2 + 10;
            double r;

            float actualDistance = x2 - x1;
            float randomDistance;

            Random obj = new Random();
            Graphics gg = CreateGraphics();
            SolidBrush sb = new SolidBrush(Color.Red);
            SolidBrush sb1 = new SolidBrush(Color.Green);

            for (int i = 0; i < n; i++)
            {
                r = xmin + (xmax - xmin) * obj.NextDouble();
                if(r >= x1 && r <= x2)
                {
                    n1++;
                    gg.FillEllipse(sb, 200 + (float)r, 400, 5, 5);
                }
                else
                {
                    gg.FillEllipse(sb1, 200 + (float)r, 400, 5, 5);
                }
            }
            randomDistance = (float)n1 / (float)n * (xmax - xmin);
            float error = actualDistance - randomDistance;
            MessageBox.Show("actualDistance is " + actualDistance +
                "\nrandomDistance is " + randomDistance + 
                "\nThe absolute error is " + Math.Abs(error));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int n = 1000, n1 = 0;
            float xmin = 0, xmax = 20, ymin = 0, ymax = 20;
            float areaSquare = (ymax - ymin) / (xmax - xmin);
            float areaCircle = 0;
            double x, y;

            Random obj = new Random();
            Graphics gg = CreateGraphics();
            SolidBrush sb = new SolidBrush(Color.Red);
            SolidBrush sb1 = new SolidBrush(Color.Green);

            for (int i = 0; i < n; i++)
            {
                x = xmin + (xmax - xmin) * obj.NextDouble();
                y = ymin + (ymax - ymin) * obj.NextDouble();
                double xtemp = x - (xmax / 2);
                double ytemp = y - (ymax / 2);
                if (((xtemp * xtemp) + (ytemp * ytemp)) <= ((xmax / 2) * (ymax / 2)))
                {
                    n1++;
                    gg.FillEllipse(sb, 200 + (float)x * 10, 300 - (float)y * 10, 5, 5);
                }
                else
                {
                    gg.FillEllipse(sb1, 200 + (float)x * 10, 300 - (float)y * 10, 5, 5);
                }
            }
            areaCircle = (float)n1 / (float)n * areaSquare;
            double error = Math.Abs(areaSquare - areaCircle);
            MessageBox.Show("Area of Circle is " + areaCircle +
                "\nArea of Squre is " + areaSquare +
                "\nAbsolute Error is " + error);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Refresh();

            int nsteps = int.Parse(textBox1.Text);
            int nwalks = int.Parse(textBox2.Text);

            float[] xavg = new float[nsteps];
            float x, delx = 3;

            Random obj = new Random();
            Graphics gg = CreateGraphics();
            SolidBrush sb = new SolidBrush(Color.Red);
            SolidBrush sb1 = new SolidBrush(Color.Green);

            for (int w = 0; w < nwalks; w++)
            {
                x = 0;
                double prob;
                for (int s = 0; s < nsteps; s++)
                {
                    prob = obj.NextDouble();
                    if(prob <= 0.5)
                    {
                        x = x + delx;
                    }
                    else
                    {
                        x = x - delx;
                    }
                    xavg[s] = xavg[s] + x * x;
                }
            }
            for (int s = 0; s < nsteps; s++)
            {
                xavg[s] = xavg[s] / nsteps;
                gg.FillEllipse(sb, 300 + s * 10, 300 - (float)xavg[s], 5, 5);
                gg.FillEllipse(sb1, 300 + s * 10, 300 - (float)Math.Sqrt(xavg[s]), 5, 5);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Refresh();

            int nwalks = int.Parse(textBox1.Text);
            int nsteps = int.Parse(textBox2.Text);

            float x, y, delx = 3, dely = 3;
            float[] xavg = new float[nsteps];

            Random obj = new Random();
            Graphics gg = CreateGraphics();
            SolidBrush sb = new SolidBrush(Color.Red);
            SolidBrush sb1 = new SolidBrush(Color.Green);

            for (int w = 0; w < nwalks; w++)
            {
                x = 0;
                y = 0;
                double prob;
                for (int s = 0; s < nsteps; s++)
                {
                    prob = obj.NextDouble();
                    if (prob <= 0.25)
                    {
                        x = x + delx;
                    }
                    if (prob >= 0.25 && prob <0.50)
                    {
                        x = x - delx;
                    }
                    if (prob >= 0.5 && prob < 0.75)
                    {
                        y = y + dely;
                    }
                    else
                    {
                        y = y - dely;
                    }
                    xavg[s] = xavg[s] + x * x + y * y;
                    gg.FillEllipse(sb, 400 + x, 400 - y, 5, 5);
                    Thread.Sleep(2);
                    this.Refresh();
                }
            }
            for (int s = 0; s < nsteps; s++)
            {
                xavg[s] = xavg[s] / nsteps;
                gg.FillEllipse(sb, 300 + (float)s, 400 - (float)xavg[s], 4, 4);
                gg.FillEllipse(sb1, 300 + (float)s, 400 - (float)Math.Sqrt(xavg[s]), 4, 4);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            EdenCluster cluster = new EdenCluster(this);
            cluster.Growth(false);
            this.Refresh();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int flag = int.Parse(textBox1.Text);
            DLACluster cluster = new DLACluster(this, flag);
            cluster.Growth(false);
            this.Refresh();
        }
    }
}
