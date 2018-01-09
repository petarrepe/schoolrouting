using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    internal static class ExtensionMethods
    {
        internal static double EuclidianDistance(this Point firstPoint, Point secondPoint)
        {
            return Math.Sqrt(Math.Pow(firstPoint.X - secondPoint.X, 2) + Math.Pow(secondPoint.Y - firstPoint.Y, 2));
        }
    }
}
