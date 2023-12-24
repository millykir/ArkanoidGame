using System.ComponentModel;

namespace DemoArkanoid.Models
{
    public class Brick : INotifyPropertyChanged
    {
        private bool _isDestroyed = false;

        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; } = 60;
        public double Height { get; } = 20;
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