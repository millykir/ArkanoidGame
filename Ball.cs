using System.Data;

class Ball
{
  // Поля класса
  private int x; // Позиция по горизонтали
  private int y; // Позиция по вертикали
  private int speedX; // Скорость по горизонтали
  private int speedY; // Скорость по вертикали

  // Метод для обновления состояния мяча
  private int updateCount = 0;

  // Свойства для доступа к координатам мяча
  public int X { get { return x; } }
  public int Y { get { return y; } }

  // Конструктор класса
  public Ball()
  {
    // Начальная позиция мяча и его скорость
    x = 0;
    y = 0;
    speedX = 1;
    speedY = 1;
  }

  // Метод для установки начальной позиции мяча
  public void SetPosition(int initialX, int initialY)
  {
    x = initialX;
    y = initialY;
  }


  public void Update(List<Brick> bricks)
  {
    updateCount++;

    if (updateCount % 2 == 0)
    {
      // Обновление координат мяча в соответствии со скоростью
      x += speedX;
      y += speedY;

      // Проверка на столкновение с верхней стеной
      if (y <= 0)
      {
        // Инвертируем вертикальную скорость при столкновении
        speedY = -speedY;

        // Корректируем координату y, чтобы мяч не выходил за границы
        y = Math.Max(0, Math.Min(Console.WindowHeight - 1, y));
      }

      // Проверка на столкновение с левой или правой стеной
      if (x <= 0 || x >= Console.WindowWidth - 1)
      {
        // Инвертируем горизонтальную скорость при столкновении
        speedX = -speedX;

        // Корректируем координату x, чтобы мяч не выходил за границы
        x = Math.Max(0, Math.Min(Console.WindowWidth - 1, x));
      }

      // Проверка на столкновение с кирпичами
      foreach (var brick in bricks)
      {
        if (brick.IsIntact && IsCollisionWithBrick(brick))
        {
          BounceOffBrick(brick);
        }
      }
    }
  }

  private bool IsCollisionWithBrick(Brick brick)
  {
    return x >= brick.X && x <= brick.X + Brick.Width && y >= brick.Y && y <= brick.Y + Brick.Height;
  }

  private void BounceOffBrick(Brick brick)
  {
    // Инвертируем вертикальную и горизонтальную скорость при столкновении с кирпичом
    speedY = -speedY;
    speedX = -speedX;

    // Разрушаем кирпич
    brick.Break();
  }




  public void BounceOffPaddle(Paddle paddle)
  {
    // Определяем, в какую ячейку ракетки попал мяч
    int paddleCellWidth = paddle.Width / 10;
    int ballCell = (x - paddle.X) / paddleCellWidth;

    if (ballCell >= 0 && ballCell < 10 && y == paddle.Y - 1)
    {

      if (ballCell >= 0 && ballCell <= 2)
      {
        speedX = -2;
      }
      else if (ballCell >= 3 && ballCell <= 5)
      {
        speedX = -1;
      }
      else if (ballCell >= 6 && ballCell <= 7)
      {
        speedX = 1;
      }
      else if (ballCell >= 8 && ballCell <= 10)
      {
        speedX = 2;
      }

      speedY = -Math.Abs(speedY);

      // // Рассчитываем угол отскока в зависимости от ячейки
      // double angle = 145 - ballCell * 15;

      // // Преобразуем угол в радианы
      // double radians = angle * (Math.PI / 180);

      // // Вычисляем новые скорости мяча
      // double newDirectionX = Math.Cos(radians);
      // double newDirectionY = Math.Sin(radians);

      // // Присваиваем новые направления мячу
      // speedX = speedX + ((int)newDirectionX);
      // speedY = -Math.Abs(speedY) - Math.Abs((int)newDirectionY);
    }
  }

}
