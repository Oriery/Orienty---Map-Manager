using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Orienty_MapManager
{
    public class Polygon
    {
        public List<Point> points = new List<Point>();
        public bool isFinished = false;

        /// <summary>
        /// 
        /// </summary>
        /// <returns>true если стена была достроена</returns>
        public bool AddPointOfWall(Point mousePositon)
        {
            if (points.Count > 0)
            {
                if (IsNearPoints(mousePositon, points[0])) //end painting wall
                {
                    isFinished = true;
                    return true;
                }
            }

            points.Add(mousePositon);

            return false;
        }

        public bool IsPointInPolygon(Point test, List<Point> polygon)
        {

             int[,] q_patt= { {0,1}, {3,2} };

             if (polygon.Count<3) return false;

            Point pred_pt = polygon[polygon.Count - 1];
            pred_pt.X-=test.X;
            pred_pt.Y-=test.Y;

            int pred_q = q_patt[pred_pt.Y < 0 ? 1 : 0, pred_pt.X < 0 ? 1 : 0];

            int w = 0;

            for(var iter=0;iter<polygon.Count;++iter)
            {
                Point cur_pt = polygon[iter];

                cur_pt.X-=test.X;
                cur_pt.Y-=test.Y;

                int q = q_patt[cur_pt.Y < 0 ? 1 : 0, cur_pt.X < 0 ? 1 : 0];

                switch (q-pred_q)
                {
                case -3:++w;break;
                case 3:--w;break;
                case -2:if(pred_pt.X* cur_pt.Y>=pred_pt.Y* cur_pt.X) ++w;break;
                case 2:if(!(pred_pt.X* cur_pt.Y>=pred_pt.Y* cur_pt.X)) --w;break;
                }

                pred_pt = cur_pt;
                pred_q = q;

            }

            return w != 0;

        }

        private int GetDistanceBetweenPoints(Point first, Point second)
        {
            int result = (second.X - first.X) * (second.X - first.X);
            result += (second.Y - first.Y) * (second.Y - first.Y);
            result = Convert.ToInt32(Math.Sqrt(result));

            return result;
        }

        private bool IsNearPoints(Point first, Point second)
        {
            return GetDistanceBetweenPoints(first, second) < 10;
        }

        public void Reset()
        {
            points.Clear();
            isFinished = false;
        }
    }
}
