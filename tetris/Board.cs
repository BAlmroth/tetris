namespace tetris;

public class Board
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Width { get; set; } = 5;
    public int Height { get; set; } = 10;
    public int[,] Tiles { get; set; }


    public Board()
    {
        Tiles = new int[Width, Height];
    }

    public void Draw()
    {
        for (var row = 0; row < Height; row++)
        {
            for (var col = 0; col < Width; col++)
            {
                Console.SetCursorPosition(X + col, Y + row);

                if (Tiles[col, row] == 0)
                    Console.Write(".");
                else
                    Console.Write("X");
            }
        }
    }

}