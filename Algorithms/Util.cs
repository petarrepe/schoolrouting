using System;

namespace Algorithms
{
    public static class Util
    {
        public static double EuclidianDistance(Point firstPoint, Point secondPoint)
        {
            return Math.Sqrt(Math.Pow(firstPoint.X - secondPoint.X, 2) + Math.Pow(secondPoint.Y - firstPoint.Y, 2));
        }
    }
}
