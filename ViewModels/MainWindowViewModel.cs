using System;
using System.Timers;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using DemoArkanoid.Models;
using System.Windows.Input;

namespace DemoArkanoid.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private Ball _ball = null!;
        private Paddle _paddle = null!;
        private Timer _gameTimer = null!;
        private double _speed = 5;
        private bool _isBallMoving = false;

        public MainWindowViewModel()
        {
            InitializeGame();
        }

        public bool PlayerUp { get; set; }
        public bool PlayerDown { get; set; }

        private void InitializeGame()
        {
            _ball = new Ball(400, 300);
            _paddle = new Paddle(350, 550);
            _gameTimer = new Timer(16); // ~60 fps
            _gameTimer.Elapsed += GameLoop;
            _gameTimer.Start();

            _ball.X = _paddle.X + _paddle.Width / 2 - _ball.Radius;
            _ball.Y = _paddle.Y - _ball.Radius - 10;
        }

        private void GameLoop(object sender, ElapsedEventArgs e)
        {
            // Логика движения мяча
            if (_isBallMoving)
            {
                _ball.X += _speed;
                _ball.Y += _speed;

                // Проверка столкновения мяча с нижней границей
                if (_ball.Y + _ball.Radius >= 600)
                {
                    // Мяч останавливается
                    _isBallMoving = false;

                    // Мяч вставляется на середину ракетки
                    _ball.X = _paddle.X + _paddle.Width / 2 - _ball.Radius;
                    _ball.Y = _paddle.Y - _ball.Radius - 10;
                }
            }

            // Логика движения ракетки
            HandlePaddleInput();

            // Обработка столкновений с границами окна
            HandleBorderCollisions();

            // Обновляем интерфейс
            RaisePropertyChanged(nameof(BallX));
            RaisePropertyChanged(nameof(BallY));
            RaisePropertyChanged(nameof(PaddleX));
            RaisePropertyChanged(nameof(PaddleY));
        }

        private void HandlePaddleInput()
        {
            if (PlayerUp)
                _paddle.X -= _speed;
            if (PlayerDown)
                _paddle.X += _speed;
        }

        private void HandleBorderCollisions()
        {
            // Столкновение с правой границей
            if (_ball.X + _ball.Radius >= 800)
            {
                _ball.X = 800 - _ball.Radius;
                _speed = -Math.Abs(_speed);
            }

            // Столкновение с левой границей
            if (_ball.X - _ball.Radius <= 0)
            {
                _ball.X = _ball.Radius;
                _speed = Math.Abs(_speed);
            }

            // Столкновение с верхней границей
            if (_ball.Y - _ball.Radius <= 0)
            {
                _ball.Y = _ball.Radius;
                _speed = Math.Abs(_speed);
            }
        }

        // Свойства для привязки интерфейса
        public double BallX => _ball.X;
        public double BallY => _ball.Y;
        public double PaddleX => _paddle.X;
        public double PaddleY => _paddle.Y;
        public double PaddleWidth => _paddle.Width;
        public double PaddleHeight => _paddle.Height;
        public IBrush BallColor => Brushes.Blue;
        public IBrush PaddleColor => Brushes.Green;

        // Обработчик события нажатия клавиши
        public void KeySpaceStart(KeyEventArgs e)
        {

            _isBallMoving = true;
            _speed = -Math.Abs(_speed); // Устанавливаем скорость в противоположное значение
            _ball.X = _paddle.X + _paddle.Width / 2 - _ball.Radius;
            _ball.Y = _paddle.Y - _ball.Radius - 10;

        }
    }
}
