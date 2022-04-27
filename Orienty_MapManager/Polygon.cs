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
        public List<Point> points { get; set; } = new List<Point>();
        public bool isFinished { get; set; } = false;

        /// <summary>
        /// 
        /// </summary>
        /// <returns>true если стена была достроена</returns>
        public bool AddPointOfWall(Point mousePositon)
        {
            if (points.Count > 0)
            {
                if (IsNearPoints(mousePositon, points[0],20)) //end painting wall
                {
                    isFinished = true;
                    return true;
                }
            }

            points.Add(mousePositon);

            return false;
        }

        public static bool IsPointInPolygon(Point test, List<Point> polygon)
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


        public static void ChnageToNearPoint (ref Point mouse, List<Polygon> pavs, Polygon build)
        {
            Point nearest = new Point() { X = mouse.X, Y = mouse.Y };
            int distance;
            int mindistance= int.MaxValue;
            foreach(var pBuild in build.points)
            {
                distance = GetDistanceBetweenPoints(mouse, pBuild);
                if (distance < 15 && mindistance > distance)
                {
                    mindistance = distance;
                    nearest.X = pBuild.X;
                    nearest.Y = pBuild.Y;
                }
            }
            for(int i=0;i<pavs.Count;++i)
            {
                foreach(var point in pavs[i].points)
                { 
                    distance = GetDistanceBetweenPoints(mouse, point);
                    if(distance<15 && mindistance>distance)
                    {
                        mindistance = distance;
                        nearest.X = point.X;
                        nearest.Y = point.Y;
                    }
                }
            }
            mouse.X = nearest.X;
            mouse.Y = nearest.Y;
        }

        public static Point Intersection(Point A, Point B, Point C, Point D)
        {
            double xo = A.X, yo = A.Y;
            double p = B.X - A.X, q = B.Y - A.Y;

            double x1 = C.X, y1 = C.Y;
            double p1 = D.X - C.X, q1 = D.Y - C.Y;

            double x = (xo * q * p1 - x1 * q1 * p - yo * p * p1 + y1 * p * p1) /
                (q * p1 - q1 * p);
            double y = (yo * p * q1 - y1 * p1 * q - xo * q * q1 + x1 * q * q1) /
                (p * q1 - p1 * q);

            if (Double.IsNaN(x))
                x = int.MaxValue-1;
            if (Double.IsNaN(y))
                y = int.MaxValue - 1;

            return new Point(Convert.ToInt32(x), Convert.ToInt32(y));
        }

        public static bool IsBelongLine(Point a1, Point a2, Point b)
        {
            double x1 = a1.X;
            double y1 = a1.Y;
            double x2 = a2.X;
            double y2 = a2.Y;
            double x3 = b.X;
            double y3 = b.Y;
            /*return (Math.Abs((x3 - x1) * (y2 - y1) - (y3 - y1) * (x2 - x1)) <= 0.1) ||
                ((x1 < x3 && x3 < x2) || (x2 < x3 && x3 < x1));*/
            return (Math.Abs(x1 - x3) / Math.Abs(y1 - y3) - Math.Abs(x2 - x3) / Math.Abs(y2 - y3) >= 0.001);
            /*double a= (y1 - y2) / (x1 - x2);
            double c= ((y1 + y2) - a * (x1 + x2)) / 2;
            if ((y3 == a * x3 + c) && (x3 > x1) && (x3 < x2) || (y3 == a * x3 + c) && (x2 > x1) && (x3 < x1))
                return true;
            else
                returnn false;*/
            //return (Math.Abs(Math.Sqrt(Math.Pow(x1 - x3, 2) + Math.Pow(y1 - y3, 2)) + Math.Sqrt(Math.Pow(x2 - x3, 2) + Math.Pow(y2 - y3, 2)) - Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2))) < 0.1) &&

        }

        public static Point GetCorrectPoint(Point mouse, Polygon build, Point lastPoint)
        {
            Point pointPer;
            for (int i = 0; i < build.points.Count-1; i++)
            {
                pointPer = Intersection(build.points[i], build.points[i + 1], mouse, lastPoint);
                if(IsBelongLine(mouse, lastPoint, pointPer))
                {
                    return pointPer;
                }
                
            }
            pointPer = Intersection(build.points[0], build.points[build.points.Count-1], mouse, lastPoint);
            if (IsBelongLine(mouse, lastPoint, pointPer))
            {
                return pointPer;
            }
            return mouse;
        }

        private static int GetDistanceBetweenPoints(Point first, Point second)
        {
            int result = (second.X - first.X) * (second.X - first.X);
            result += (second.Y - first.Y) * (second.Y - first.Y);
            result = Convert.ToInt32(Math.Sqrt(result));

            return result;
        }

        private static bool IsNearPoints(Point first, Point second, int dist)
        {
            return GetDistanceBetweenPoints(first, second) < dist;
        }

        public void Reset()
        {
            points.Clear();
            isFinished = false;
        }
    }
}
