// Brick.cs
class Brick
{
    // Поля класса
    private int x; // Позиция по горизонтали
    private int y; // Позиция по вертикали
    private bool isIntact; // Флаг, показывающий, целый ли кирпич

    // Свойство для доступа к координатам кирпича
    public int X { get { return x; } }
    public int Y { get { return y; } }

    // Добавляем свойства Width и Height
    public static int Width { get; } = 2; // Замените значение на необходимое
    public static int Height { get; } = 1;

    // Свойство, показывающее, целый ли кирпич
    public bool IsIntact { get { return isIntact; } }

    // Конструктор класса
    public Brick()
    {
        // Начальная позиция кирпича и его целостность
        x = 0;
        y = 0;
        isIntact = true;
    }

    // Метод для установки начальной позиции кирпича
    public void SetPosition(int initialX, int initialY)
    {
        x = initialX;
        y = initialY;
    }

    // Метод для "разрушения" кирпича
    public void Break()
    {
        isIntact = false;
    }
}