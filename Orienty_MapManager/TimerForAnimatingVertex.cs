using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Orienty_MapManager
{
    public class TimerForAnimatingVertex : Timer
    {
        Vector2 from, to;
        float a = 0;
        Vertex vertex;
        float da = 0.02f;
        Form1 form1;
        SecondOrderDynamics secondOrderDynamics;
        Stopwatch stopWatch = new Stopwatch();


        public static List<TimerForAnimatingVertex> timers = new List<TimerForAnimatingVertex>();

        public static void StartAnimationForVertex(Point from, Point to, Vertex vertex, Form1 form1)
        {
            timers.Add(new TimerForAnimatingVertex(from, to, vertex, form1));
        }

        private TimerForAnimatingVertex(Point from, Point to, Vertex vertex, Form1 form1)
        {
            this.from = new Vector2(from.X, from.Y);
            this.to = new Vector2(to.X, to.Y);
            this.vertex = vertex;
            this.form1 = form1;

            Interval = 16;

            secondOrderDynamics = new SecondOrderDynamics(2f, 0.5f, 2f, this.to, this.from);

            Tick += DoTick;
            stopWatch.Start();
            Start();
        }

        private void DoTick(object sender, EventArgs e)
        {
            stopWatch.Stop();
            var DeltaTime = stopWatch.ElapsedMilliseconds;
            stopWatch.Restart();

            Vector2 temp = secondOrderDynamics.Update(DeltaTime, to);
            vertex.x = (int)temp.X;
            vertex.y = (int)temp.Y;

            if (a >= 1)
            {
                Stop();
                timers.Remove(this);
                vertex.x = (int)to.X;
                vertex.y = (int)to.Y;
            }

            a += da;

            form1.UpdateGraphImage();
        }
    }
}
