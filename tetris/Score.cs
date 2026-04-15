namespace tetris;

public class Score
{
    public int Points { get; private set; }

    public void countPoints(int clearedRows)
    {
        if (clearedRows == 1) Points += 100;
        if (clearedRows == 2) Points += 300;
        if (clearedRows == 3) Points += 500;
        if (clearedRows == 4) Points += 800;
    }

    public void Draw(Board board)
    {
        Console.SetCursorPosition(board.X, board.Y + board.Height + 2);
        Console.WriteLine($"Score: {Points}");

        Console.SetCursorPosition(board.X, board.Y + board.Height + 3);
        Console.WriteLine("P = pause | Esc = terminate program | Space = harddrop");
    }
}