using System.ComponentModel;

namespace DemoArkanoid.Models
{
    public class Brick : INotifyPropertyChanged
    {
        private bool _isDestroyed = false;

        public double X { get; set; }
        public double Y { get; set; }
        private double _width = 100;
        private double _height = 20;

        public double Width
        {
            get { return _width; }
            set
            {
                if (_width != value)
                {
                    _width = value;
                    OnPropertyChanged(nameof(Width));
                }
            }
        }

        public double Height
        {
            get { return _height; }
            set
            {
                if (_height != value)
                {
                    _height = value;
                    OnPropertyChanged(nameof(Height));
                }
            }
        }
        public bool IsDestroyed
        {
            get { return _isDestroyed; }
            set
            {
                if (_isDestroyed != value)
                {
                    _isDestroyed = value;
                    OnPropertyChanged(nameof(IsDestroyed));
                    OnPropertyChanged(nameof(IsVisible));
                }
            }
        }

        public bool IsVisible => !_isDestroyed;
        public bool IsSpecialBrick { get; } = false;


        public Brick(double initialX, double initialY)
        {
            X = initialX;
            Y = initialY;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}