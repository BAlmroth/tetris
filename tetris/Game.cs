//
// This is the game event logic that you can customize and cannibalize
// as needed. You should try to write your game in a modular way, avoid
// making one huge Game class.
//

using tetris;

class Game
{
    ScheduleTimer? _timer;

    public bool Paused { get; private set; }
    public bool GameOver { get; private set; }

    private Board _board = new Board() { X = 0, Y = 0 };
    private Block _block = new Block(Shapes.I) { X = 2, Y = 0 };
    
    public void Start()
    {
        Console.WriteLine("Start");
        _board.Draw();
        _block.Draw();
        ScheduleNextTick();
    }

    public void Pause()
    {
        Console.WriteLine("Pause");
        Paused = true;
        _timer!.Pause();
    }

    public void Resume()
    {
        Console.WriteLine("Resume");
        Paused = false;
        _timer!.Resume();
    }

    public void Stop()
    {
        Console.WriteLine("Stop");
        GameOver = true;
    }

    public void Input(ConsoleKey key)
    {
        if (key == ConsoleKey.LeftArrow)
        {
            if (_block.X > 0)
                _block.X = _block.X - 1;
        }

        if (key == ConsoleKey.RightArrow)
        {
            if (_block.X + _block.Shape.GetLength(1) < _board.Width)
                _block.X = _block.X + 1;
        }

        if (key == ConsoleKey.Spacebar)
        {
            while (CanMoveDown())
                _block.Y = _block.Y + 1;

            BottomBlock();
            DeleteRows();
            _block = new Block(Shapes.I) { X = 2, Y = 0 };
        }

        if (key == ConsoleKey.UpArrow)
        {
            _block.Rotate();
        }

        _board.Draw();
        _block.Draw();
    }

    void Tick()
    {
        if (CanMoveDown())
        {
            _block.Y = _block.Y + 1;
        }
        else
        {
            BottomBlock();
            DeleteRows();
            _block = new Block(Shapes.I) { X = 2, Y = 0 };
        }
        _board.Draw();
        _block.Draw();
        ScheduleNextTick();
    } 

    void ScheduleNextTick()
    {
        // the game will automatically update itself every half a second, adjust as needed
        _timer = new ScheduleTimer(800, Tick);
    }

    //
    void BottomBlock()
    {
        for (int row = 0; row < _block.Shape.GetLength(0); row++)
        {
            for (int col = 0; col < _block.Shape.GetLength(1); col++)
            {
                if (_block.Shape[row, col] == 1)
                {
                    int tileX = _block.X + col;
                    int tileY = _block.Y + row;

                    if (tileX >= 0 && tileX < _board.Width && tileY >= 0 && tileY < _board.Height)
                        _board.Tiles[tileX, tileY] = 1;
                }
            }
        }
    }

    bool IsRowFull(int row)
    {
        for (int col = 0; col < _board.Width; col++)
        {
            if (_board.Tiles[col, row] == 0)
                return false;
        }

        return true;
    }

    void ClearRow(int row)
    {
        for (int col = 0; col < _board.Width; col++)
        {
            _board.Tiles[col, row] = 0;
        }
    }

    void MoveRowDown(int row, int numRows)
    {
        for (int col = 0; col < _board.Width; col++)
        {
            _board.Tiles[col, row + numRows] = _board.Tiles[col, row];
            _board.Tiles[col, row] = 0;
        }
    }

    int DeleteRows()
    {
        int cleared = 0;

        for (int row = _board.Height - 1; row >= 0; row--)
        {
            if (IsRowFull(row))
            {
                ClearRow(row);
                cleared++;
            }
            else if (cleared > 0)
            {
                MoveRowDown(row, cleared);
            }
        }

        return cleared;
    }
    
    bool CanMoveDown()
    {
        for (int row = 0; row < _block.Shape.GetLength(0); row++)
        {
            for (int col = 0; col < _block.Shape.GetLength(1); col++)
            {
                if (_block.Shape[row, col] == 1)
                {
                    int nextX = _block.X + col;
                    int nextY = _block.Y + row + 1;

                    // check bottom of board
                    if (nextY >= _board.Height)
                        return false;

                    // check left and right bounds
                    if (nextX < 0 || nextX >= _board.Width)
                        return false;

                    // check if tile below is already filled
                    if (_board.Tiles[nextX, nextY] == 1)
                        return false;
                }
            }
        }
        return true;
    }
}