namespace DemoArkanoid.Models
{
    public class Paddle
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; } = 100;
        public double Height { get; } = 20;

        public Paddle(double initialX, double initialY)
        {
            X = initialX;
            Y = initialY;
        }
    }
}
