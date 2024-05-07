namespace MemoryManagement
{
    struct Point
    {
        public double X, Y;

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        private static Point origin = new Point();
        public static ref readonly Point Origin => ref origin; 

        public void Reset()
        {
            X = 0; Y = 0;
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }

    class InParametersDemo
    {
        void changeMe(ref Point p)
        {
            p.X++;
        }

        double MeasureDistance(in Point p1, in Point p2)
        {
            //p1 = new Point();
            //changeMe(ref p2);

            p1.Reset();


            var dx = p1.X - p2.X;
            var dy = p1.Y - p2.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        double MeasureDistance(Point p1, Point p2)
        {
            return 0.0;
        }


        public InParametersDemo()
        {
            var p1 = new Point(1, 1);
            var p2 = new Point(4, 5);

            var distance = MeasureDistance(p1, p2);

            Console.WriteLine($"Distance between {p1} and {p2} is {distance}");

            var distanceFromOrigin = MeasureDistance(p1, Point.Origin);

            Point copyOfOrigin = Point.Origin; //by-value

            //ref var messWithOrigin = ref Point.Origin;
            ref readonly var originRef = ref Point.Origin;
            originRef++;
        }
        static void Demo(string[] args)
        {
            new InParametersDemo();
        }
    }
}
