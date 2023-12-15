// Game.cs
using System;
using System.Collections.Generic;

class Game
{
    private Paddle? paddle;
    // Инициализация игры
    public void InitializeGame(Paddle paddle, Ball ball, List<Brick> bricks)
    {
        this.paddle = paddle;
        // Устанавливаем начальную позицию для ракетки
        paddle.SetPosition(Console.WindowWidth / 2 - paddle.Width / 2, Console.WindowHeight - 2);

        // Устанавливаем начальную позицию для мяча
        ball.SetPosition(Console.WindowWidth / 2, Console.WindowHeight - 3);

        // Создаем кирпичи и располагаем их на поле
        int brickRows = 5;
        int bricksPerRow = Console.WindowWidth / (Brick.Width + 1); // +1 для небольшого отступа между кирпичами

        for (int row = 0; row < brickRows; row++)
        {
            for (int col = 0; col < bricksPerRow; col++)
            {
                int brickX = col * (Brick.Width + 1);
                int brickY = row * (Brick.Height + 1);

                Brick brick = new Brick();
                brick.SetPosition(brickX, brickY);

                bricks.Add(brick);
            }
        }
    }

    // Отрисовка игры
    public void DrawGame(Paddle paddle, Ball ball, List<Brick> bricks)
    {
        // Отрисовка ракетки
        paddle.Draw();

        // Отрисовка мяча
        Console.SetCursorPosition(ball.X, ball.Y);
        Console.Write("O");

        // Отрисовка кирпичей
        foreach (var brick in bricks)
        {
            if (brick.IsIntact)
            {
                Console.SetCursorPosition(brick.X, brick.Y);
                Console.Write("#");
            }
        }
    }


    // Проверка на завершение игры
    public bool GameIsOver(List<Brick> bricks)
    {
        // Проверяем, остались ли еще целые кирпичи
        foreach (var brick in bricks)
        {
            if (brick.IsIntact)
            {
                return false; // Если хотя бы один кирпич цел, игра не завершена
            }
        }

        return true; // Все кирпичи разбиты, игра завершена
    }

    public void Update(Ball ball, List<Brick> bricks)
    {
        // Проверка на столкновение с ракеткой
        ball.BounceOffPaddle(paddle);

        // Обновление состояния мяча, передавая список кирпичей
        ball.Update(bricks);
    }

    // Метод для получения начальной позиции мяча
    public int[] GetInitialBallPosition()
    {
        // Возвращаем начальную позицию мяча, например, середина ракетки
        return new int[] { paddle!.X + paddle!.Width / 2, paddle!.Y - 1 };
    }

}
