namespace Algorithms
{
    public class Point
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Point(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }
        public static bool operator==(Point obj1, Point obj2)
        {
            if (obj2.X != obj1.X || obj2.Y != obj1.Y) return false;
            else return true;
        }
        public static bool operator !=(Point obj1, Point obj2)
        {
            return !(obj1 == obj2);
        }

    }
}
