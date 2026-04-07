namespace tetris;

public class Block
{
    public int X { get; set; }
    public int Y { get; set; }
    public int[,] Shape { get; set; }

    public Block()
    {
        Shape = new int[,]
        {
            { 1, 1, 1 },
            { 0, 1, 0 }
        };
    }

    public void Draw()
    {
        for (int row = 0; row < Shape.GetLength(0); row++)
        {
            for (int col = 0; col < Shape.GetLength(1); col++)
            {
                if (Shape[row, col] == 1)
                {
                    Console.SetCursorPosition(X + col, Y + row);
                    Console.Write("X");
                }
            }
        }
    }
}