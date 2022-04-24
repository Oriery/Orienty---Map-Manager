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
    }
}
