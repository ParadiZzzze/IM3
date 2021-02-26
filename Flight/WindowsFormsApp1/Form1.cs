using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        double dt = 0.1;
        double t;
        BusinessLogic name = new BusinessLogic();

        private void btStart_Click(object sender, EventArgs e)
        {
            t = 0;
            name.Calculating((double)edHeight.Value, (double)edSpeed.Value, (double)edAngle.Value, (double)edSquare.Value, (double)edWeight.Value);

            timer1.Start();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            t += dt;
            name.finalCountdown(t);
            label4.Text = "Time:" + Convert.ToString(t);
            labDistance.Text = "d= " + name.x;

            if (name.y <= 0) timer1.Stop();

        }

        /*Код писался с использованием скелета предыдущей (2ой) лабораторной
         был удален сам график и поиск "удобного" масштаба, т.к. с точки зрения физики, формулы 
         были бы неверны. Для расчетов использовались формулы, предоставленные Александром Николаевичем. Насколько помню из школьной программы физики, то
         при уменьшении массы тела, оно должно лететь дальше. По этим формулам происходит обратное.*/

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }
    }

    public class BusinessLogic
    {
        const double g = 9.81;
        const double C = 0.15;
        const double rho = 1.29;

        public double x = 0;
        public double y = 0;

        double t = 0;
        double y0;
        double v0;
        double angle;
        double S;
        double m;
        double vx;
        double vy;
        double k;
        double cosangle;
        double sinangle;

        public void Calculating(double a,double b, double c, double d, double e)
        {
            t = 0;
            x = 0;
            y = a;
            v0 = b;
            double an = c * Math.PI / 180;
            S = d;
            m = e;
            cosangle = (double)Math.Cos(an);
            sinangle = (double)Math.Sin(an);            
            k = 0.5 * C * rho * S / m;
            vx = v0 * cosangle;
            vy = v0 * sinangle;
        }

        public void finalCountdown(double dt)
        {
            double v = (double)Math.Sqrt((double)(vx * vx + vy * vy));
            vx = vx - k * vx * v * dt;
            vy = vy - (g + k * vy * v) * dt;
            x = x + vx * dt;
            y = y + vy * dt;


        }
    }
}
