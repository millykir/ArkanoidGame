// Program.cs
using System;
using System.Collections.Generic;

Console.WindowWidth = 40; // Устанавливаем размер окна консоли
Console.WindowHeight = 20;

Game game = new Game();
Paddle paddle = new Paddle();
Ball ball = new Ball();
List<Brick> bricks = new List<Brick>();

// Инициализация игры
game.InitializeGame(paddle, ball, bricks);

// Основной цикл игры
while (true)
{
    Console.Clear(); // Очищаем консоль на каждом шаге для отрисовки нового состояния игры

    // Обновление состояния игры
    paddle.Update();

    // Проверка на столкновение с ракеткой
    game.Update(ball, bricks);

    // Проверка на достижение нижней границы
    if (ball.Y >= Console.WindowHeight - 1)
    {
        // Возвращаем мяч в центр ракетки и ждем нажатия пробела
        ball.SetPosition(paddle.X + paddle.Width / 2, paddle.Y - 1);

        // Ожидание нажатия пробела
        while (true)
        {
            Console.Clear();
            paddle.Update();
            game.DrawGame(paddle, ball, bricks);

            ConsoleKeyInfo key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Spacebar)
            {
                break;
            }

            System.Threading.Thread.Sleep(32);
        }
    }

    // Отрисовка игры
    game.DrawGame(paddle, ball, bricks);

    // Проверка на завершение игры
    if (game.GameIsOver(bricks))
    {
        Console.WriteLine("Game Over! You lost!");
        break;
    }

    // Задержка для контроля скорости игры
    System.Threading.Thread.Sleep(32);
}