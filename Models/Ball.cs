namespace DemoArkanoid.Models
{
    public class Ball
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Radius { get; } = 10; // Радиус мяча

        public Ball(double initialX, double initialY)
        {
            X = initialX;
            Y = initialY;
        }
    }
}
