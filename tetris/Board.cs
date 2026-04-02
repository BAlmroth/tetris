namespace tetris;

public class Board
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Width { get; set; } //hårdkodat?
    public int Height { get; set; } //hårdkodat?
    public int[,] Tiles { get; set; }


    public Board()
    {
        Tiles = new int[Width, Height];
    }

    public void Draw()
    {
        
    }

}