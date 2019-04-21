namespace DataStructures
{
    class Interval
    {
        public int index;
        public double a;
        public double b;
        public Interval(int index, double a, double b)
        {
            this.index = index;
            this.a = a;
            this.b = b;
        }
        public bool Contains(double c)
        {
            return (c > a && c <= b);
        }
    }
}
