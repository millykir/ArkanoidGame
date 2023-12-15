// Paddle.cs
class Paddle
{
    // Поля класса
    private int x; // Позиция по горизонтали
    private int y; // Позиция по вертикали
    private int width; // Ширина ракетки

    // Свойства для доступа к координатам ракетки и её ширине
    public int X { get { return x; } }
    public int Y { get { return y; } }
    public int Width { get { return width; } }

    // Конструктор класса
    public Paddle()
    {
        // Начальная позиция ракетки и её ширина
        x = 0;
        y = 0;
        width = 10; // Вы можете установить любую ширину по вашему усмотрению
    }

    // Метод для установки начальной позиции ракетки
    public void SetPosition(int initialX, int initialY)
    {
        x = initialX;
        y = initialY;
    }

    // Метод для обновления состояния ракетки (например, обработка ввода)
    public void Update()
    {
        // Здесь вы можете добавить логику для обработки ввода, например, управление клавишами
        HandleInput();
    }


    // Метод для отрисовки ракетки
    public void Draw()
    {
        Console.SetCursorPosition(x, y);
        Console.Write(new string('=', width));
    }

    // Метод для обработки ввода
    public void HandleInput()
    {
        if (Console.KeyAvailable)
        {
            ConsoleKeyInfo key = Console.ReadKey(true); // Клавиша не отоброжается на экране

            // Здесь вы можете добавить логику для обработки различных клавиш
            // Например, управление ракеткой с помощью стрелок или клавиш A и D
            switch (key.Key)
            {
                case ConsoleKey.LeftArrow:
                    MoveLeft();
                    break;
                case ConsoleKey.RightArrow:
                    MoveRight();
                    break;
                // Другие обработки клавиш
            }
        }
    }

    // Метод для перемещения ракетки влево
    private void MoveLeft()
    {
        if (x > 0)
        {
            x--;
        }
    }

    // Метод для перемещения ракетки вправо
    private void MoveRight()
    {
        if (x + width < Console.WindowWidth)
        {
            x++;
        }
    }

}
