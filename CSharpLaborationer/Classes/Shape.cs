namespace Classes
{
    public abstract class Shape
    {
        double _width;
        double _length;

        public double Width { get; set; }
        public double Length { get; set; }

        protected Shape(double l, double w) { }
        public abstract double Area { get; }
        public abstract double Perimeter { get; }

        public override string ToString() { return ""; }
    }
}
