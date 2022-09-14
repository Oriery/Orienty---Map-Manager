using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Orienty_MapManager
{
    public class TimerForAnimatingVertex : Timer
    {
        Point from, to;
        float a = 0;
        Vertex vertex;
        float da;
        Form1 form1;

        public TimerForAnimatingVertex(Point from, Point to, Vertex vertex, float timeInMillisec, Form1 form1)
        {
            this.from = from;
            this.to = to;
            this.vertex = vertex;
            this.form1 = form1;

            Interval = 20;
            da = Interval / timeInMillisec;

            Tick += DoTick;
            Start();
        }

        private void DoTick(object sender, EventArgs e)
        {
            float b = -1 * (a - 1) * (a - 1) + 1;
            Point temp = Lerp(b, from, to);
            vertex.x = temp.X;
            vertex.y = temp.Y;

            if (a >= 1)
            {
                Stop();
                form1.timers.Remove(this);
            }

            a += da;

            form1.UpdateGraphImage();
        }

        private Point Lerp(float alpha, Point a, Point b)
        {
            int dx = b.X - a.X;
            int dy = b.Y - a.Y;

            return new Point(a.X + (int)(dx * alpha), a.Y + (int)(dy * alpha));
        }
    }
}
