using System.Data;

namespace tetris;

public class Block
{
    public int X { get; set; }
    public int Y { get; set; }
    public int[,] Shape { get; set; }

    public Block(int[,] shape)
    {
        Shape = shape;
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
                    Console.Write("█");
                }
            }
        }
    }

    public void Rotate()
    {
        int rows = Shape.GetLength(0);
        int cols = Shape.GetLength(1);
        
        //byt plats på row och col 90 grader rotation
        int[,] rotated = new int[cols, rows];

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                rotated[col, rows - 1 - row] = Shape[row, col];
            }
        }

        Shape = rotated;
    }
}