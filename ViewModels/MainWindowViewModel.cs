﻿using System;
using System.Timers;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using DemoArkanoid.Models;
using System.Windows.Input;
using System.Collections.Generic;

namespace DemoArkanoid.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private Ball _ball = null!;
        private Paddle _paddle = null!;
        private Timer _gameTimer = null!;
        private double _speedX = 5; // Скорость движения по оси X
        private double _initialSpeedX = 5;
        private double _speedY = 5; // Скорость движения по оси Y
        private double _speedPaddle = 5;
        private bool _isBallMoving = false;
        private List<Brick> _bricks = new List<Brick>();
        private int _lives = 2; // Количество жизней
        private string _gameResult = string.Empty;

        public List<Brick> Bricks => _bricks;

        public MainWindowViewModel()
        {
            InitializeGame();
        }

        public bool PlayerUp { get; set; }
        public bool PlayerDown { get; set; }

        public int Lives
        {
            get { return _lives; }
            set
            {
                if (_lives != value)
                {
                    _lives = value;
                    RaisePropertyChanged(nameof(Lives));
                }
            }
        }

        public string GameResult
        {
            get { return _gameResult; }
            set
            {
                if (_gameResult != value)
                {
                    _gameResult = value;
                    RaisePropertyChanged(nameof(GameResult));
                }
            }
        }


        private void GameLoop(object sender, ElapsedEventArgs e)
        {
            // Логика движения мяча
            if (_isBallMoving)
            {
                _ball.X += _speedX;
                _ball.Y += _speedY;

                // Проверка столкновения мяча с нижней границей
                if (_ball.Y + _ball.Radius >= 600)
                {
                    // Отнимаем жизнь
                    Lives--;

                    // Проверяем, остались ли еще жизни
                    if (Lives > 0)
                    {
                        // Мяч останавливается
                        _isBallMoving = false;

                        // Мяч вставляется на середину ракетки
                        _ball.X = _paddle.X + _paddle.Width / 2 - _ball.Radius;
                        _ball.Y = _paddle.Y - _ball.Radius;
                    }
                    else
                    {
                        // Все стираем с экрана
                        _bricks.Clear();
                        _isBallMoving = false;

                        // Выводим сообщение о поражении
                        GameResult = "You lose!";
                    }
                }

                // Проверка столкновения мяча с боковыми границами
                if (_ball.X + _ball.Radius >= 800 || _ball.X - _ball.Radius <= 0)
                {
                    _speedX = -_speedX;
                }

                // Проверка столкновения мяча с верхней границей
                if (_ball.Y - _ball.Radius <= 0)
                {
                    _speedY = Math.Abs(_speedY);
                }
            }

            // Проверка столкновения мяча с кирпичами
            foreach (var brick in _bricks)
            {
                // Проверка столкновения мяча с неломанным кирпичом
                if (!brick.IsDestroyed && _ball.Y + _ball.Radius >= brick.Y && _ball.Y - _ball.Radius <= brick.Y + brick.Height && _ball.X + _ball.Radius >= brick.X && _ball.X - _ball.Radius <= brick.X + brick.Width)
                {
                    // Меняем направление движения мяча
                    _speedY = -_speedY;

                    // Ломаем кирпич
                    brick.IsDestroyed = true;

                    // Проверяем, остались ли еще неразрушенные кирпичи
                    bool allBricksDestroyed = true;
                    foreach (var b in _bricks)
                    {
                        if (!b.IsDestroyed)
                        {
                            allBricksDestroyed = false;
                            break;
                        }
                    }

                    // Если все кирпичи разрушены, выводим сообщение о победе
                    if (allBricksDestroyed)
                    {
                        // Все стираем с экрана
                        _bricks.Clear();
                        _isBallMoving = false;

                        // Выводим сообщение о победе
                        GameResult = "You win!";
                    }
                }
            }

            // Логика движения ракетки
            HandlePaddleInput();

            HandleBorderCollisions();

            // Обновляем интерфейс
            RaisePropertyChanged(nameof(BallX));
            RaisePropertyChanged(nameof(BallY));
            RaisePropertyChanged(nameof(PaddleX));
            RaisePropertyChanged(nameof(PaddleY));
            RaisePropertyChanged(nameof(GameResult));
        }

        private void InitializeGame()
        {
            _ball = new Ball(400, 300);
            _paddle = new Paddle(350, 550);
            _gameTimer = new Timer(16); // ~60 fps
            _gameTimer.Elapsed += GameLoop;
            _gameTimer.Start();

            _ball.X = _paddle.X + _paddle.Width / 2 - _ball.Radius;
            _ball.Y = _paddle.Y - _ball.Radius;

            // Создание кирпичей
            const int bricksPerRow = 4; // Количество кирпичей в ряду
            const int brickWidth = 100; // Ширина кирпича
            const int brickHeight = 30; // Высота кирпича
            const int gap = 10; // Расстояние между кирпичами

            int startX = (800 - (bricksPerRow * brickWidth + (bricksPerRow - 1) * gap)) / 2; // Вычисляем начальную координату X
            int startY = 50; // Начальная координата Y

            for (int row = 0; row < 3; row++) // 3 ряда кирпичей
            {
                for (int col = 0; col < bricksPerRow; col++)
                {
                    double brickX = startX + col * (brickWidth + gap);
                    double brickY = startY + row * (brickHeight + gap);
                    _bricks.Add(new Brick(brickX, brickY) { Width = brickWidth, Height = brickHeight });
                }
            }
        }



        private void HandlePaddleInput()
        {
            if (PlayerUp && _paddle.X - _speedPaddle >= 0)
                _paddle.X -= _speedPaddle;
            if (PlayerDown && _paddle.X + _paddle.Width + _speedPaddle <= 800)
                _paddle.X += _speedPaddle;
        }

        private void HandleBorderCollisions()
        {
            // Проверка столкновения мяча с ракеткой
            if (_ball.Y + _ball.Radius >= _paddle.Y && _ball.Y - _ball.Radius <= _paddle.Y && _ball.X + _ball.Radius >= _paddle.X && _ball.X - _ball.Radius <= _paddle.X + _paddle.Width)
            {
                // Вычисляем координаты границ ракетки
                double paddleLeft = _paddle.X;
                double paddleLeftCenter = _paddle.X + _paddle.Width * 0.25; // Левая часть, ближе к центру
                double paddleRightCenter = _paddle.X + _paddle.Width * 0.75; // Правая часть, ближе к центру
                double paddleRight = _paddle.X + _paddle.Width;
                double paddleCenter = _paddle.X + _paddle.Width / 2;

                // Определение угла отскока в зависимости от положения попадания на ракетку
                if (_ball.X >= paddleLeft && _ball.X <= paddleLeftCenter)
                {
                    // Левая часть, отправляет влево
                    _speedX = -Math.Abs(_initialSpeedX);
                    _speedY = -Math.Abs(_speedY);
                }
                else if (_ball.X <= paddleRight && _ball.X >= paddleRightCenter)
                {
                    // Правая часть, отправляет вправо
                    _speedX = Math.Abs(_initialSpeedX);
                    _speedY = -Math.Abs(_speedY);
                }
                else if (_ball.X > paddleLeftCenter && _ball.X < paddleCenter)
                {
                    // Центральная часть, отправляет вверх
                    _speedX = -Math.Abs(_initialSpeedX * 0.5);
                    _speedY = -Math.Abs(_speedY);
                }
                else if (_ball.X <= paddleRightCenter && _ball.X >= paddleCenter)
                {
                    // Правая часть, отправляет вправо
                    _speedX = Math.Abs(_initialSpeedX * 0.5);
                    _speedY = -Math.Abs(_speedY);
                }

                // Изменение позиции мяча, чтобы он не застревал в ракетке
                _ball.Y = _paddle.Y - _ball.Radius;
            }

            // Столкновение с правой границей
            if (_ball.X + _ball.Radius >= 800)
            {
                _ball.X = 800 - _ball.Radius;
                _speedX = -Math.Abs(_speedX);
            }

            // Столкновение с левой границей
            if (_ball.X - _ball.Radius <= 0)
            {
                _ball.X = _ball.Radius;
                _speedX = Math.Abs(_speedX);
            }

            // Столкновение с верхней границей
            if (_ball.Y - _ball.Radius <= 0)
            {
                _ball.Y = _ball.Radius;
                _speedY = Math.Abs(_speedY);
            }
        }

        // Свойства для привязки интерфейса
        public double BallX => _ball.X;
        public double BallY => _ball.Y;
        public double PaddleX => _paddle.X;
        public double PaddleY => _paddle.Y;
        public double PaddleWidth => _paddle.Width;
        public double PaddleHeight => _paddle.Height;
        public IBrush BallColor => Brushes.Yellow;
        public IBrush PaddleColor => Brushes.Green;
        public double BallRadius => _ball.Radius;



        // Обработчик события нажатия клавиши
        public void KeySpaceStart(KeyEventArgs e)
        {
            if (_ball.Y + _ball.Radius >= _paddle.Y && _ball.Y + _ball.Radius <= _paddle.Y && _ball.X + _ball.Radius >= _paddle.X && _ball.X - _ball.Radius <= _paddle.X + _paddle.Width)
            {
                _isBallMoving = true;
                _speedX = -Math.Abs(_speedX); // Устанавливаем скорость в противоположное значение
                _speedY = -Math.Abs(_speedY); // Устанавливаем скорость в противоположное значение
            }
        }
        public void KeyEscPressed(KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Environment.Exit(0);
            }
        }
    }
}