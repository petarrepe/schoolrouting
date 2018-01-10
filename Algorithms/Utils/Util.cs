using System;
using System.Collections.Generic;

namespace Algorithms
{
    public static class Util
    {
        public static double EuclidianDistance(Point firstPoint, Point secondPoint)
        {
            return Math.Sqrt(Math.Pow(firstPoint.X - secondPoint.X, 2) + Math.Pow(secondPoint.Y - firstPoint.Y, 2));
        }

        public static double[][] ListToArray(this List<Point> pointsList)
        {
            double[][] value = new double[pointsList.Count][];
            for (int i = 0; i < pointsList.Count; i++)
            {
                value[i]=new double[2];
                value[i][0] = pointsList[i].X;
                value[i][1] = pointsList[i].Y;
            }
            return value;
        }
    }
}
