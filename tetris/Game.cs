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
    private Block _block;
    private Random _random = new Random();
    private Score _score = new Score();

    public void Start()
    {
        _block = nextBlock();
        _board.Draw();
        _block.Draw();
        ScheduleNextTick();
    }

    public void Pause()
    {
        Log("Game Paused");
        Paused = true;
        _timer!.Pause();
    }

    public void Resume()
    {
        Log(" ");
        Paused = false;
        _timer!.Resume();
    }

    public void Stop()
    {
        Log("Game Terminated");
        GameOver = true;

        _timer?.Dispose();
        _timer = null;
    }

    public void Input(ConsoleKey key)
    {
        if (key == ConsoleKey.LeftArrow)
        {
            if (IsValidPosition(_block.Shape, _block.X - 1, _block.Y))
                _block.X--;
        }

        if (key == ConsoleKey.RightArrow)
        {
            if (IsValidPosition(_block.Shape, _block.X + 1, _block.Y))
                _block.X++;
        }

        if (key == ConsoleKey.Spacebar)
        {
            while (IsValidPosition(_block.Shape, _block.X, _block.Y + 1))
                _block.Y++;

            BottomBlock();
            _score.countPoints(DeleteRows());
            SpawnNextBlock();
        }

        if (key == ConsoleKey.UpArrow)
        {
            var oldShape = _block.Shape;

            _block.Rotate();

            if (!IsValidPosition(_block.Shape, _block.X, _block.Y))
                _block.Shape = oldShape;
        }

        _board.Draw();
        _block.Draw();
    }

    void Tick()
    {
        if (IsValidPosition(_block.Shape, _block.X, _block.Y + 1))
        {
            _block.Y++;
        }
        else
        {
            BottomBlock();
            _score.countPoints(DeleteRows());
            SpawnNextBlock();
        }
        _score.Draw(_board);
        _board.Draw();
        _block.Draw();
        ScheduleNextTick();
    }

    void ScheduleNextTick()
    {
        // the game will automatically update itself every half a second, adjust as needed
        _timer = new ScheduleTimer(800, Tick);
    }

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

    bool IsRowEmpty(int row)
    {
        for (int col = 0; col < _board.Width; col++)
        {
            if (_board.Tiles[col, row] == 1)
                return false;
        }
        return true;
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

    Block nextBlock()
    {
        var shapes = new int[][,]
        {
            Shapes.I, Shapes.O, Shapes.T, Shapes.L, Shapes.J, Shapes.S, Shapes.Z
        };

        var colors = new ConsoleColor[]
        {
            ConsoleColor.Cyan,
            ConsoleColor.Yellow,
            ConsoleColor.Magenta,
            ConsoleColor.Blue,
            ConsoleColor.DarkRed,
            ConsoleColor.Green,
            ConsoleColor.Red
        };

        int index = _random.Next(shapes.Length);
        var shape = shapes[index];
        return new Block(shape, colors[index]) { X = 2, Y = 0 };
    }

    bool IsGameOver()
    {
        return !(IsRowEmpty(0) && IsRowEmpty(1));
    }

    void SpawnNextBlock()
    {
        if (IsGameOver())
        {
            GameOver = true;

            _timer?.Dispose();
            _timer = null;

            Console.SetCursorPosition(0, 22);
            Log("GAME OVER! - Press R to restart");
            return;
        }

        _block = nextBlock();
    }

    bool IsValidPosition(int[,] shape, int x, int y)
    {
        for (int row = 0; row < shape.GetLength(0); row++)
        {
            for (int col = 0; col < shape.GetLength(1); col++)
            {
                if (shape[row, col] != 1)
                    continue;

                int bx = x + col;
                int by = y + row;

                if (bx < 0 || bx >= _board.Width)
                    return false;

                if (by < 0 || by >= _board.Height)
                    return false;

                if (_board.Tiles[bx, by] == 1)
                    return false;
            }
        }

        return true;
    }

    public void Restart()
    {
        _timer?.Dispose();
        _timer = null;

        _board = new Board() { X = 0, Y = 0 };
        _score = new Score();
        _block = nextBlock();

        GameOver = false;
        Paused = false;

        Console.Clear();
        _board.Draw();
        _block.Draw();

        ScheduleNextTick();
    }

    void Log(string message)
    {
        Console.SetCursorPosition(_board.X, _board.Y + _board.Height + 4);
        Console.WriteLine(new string(' ', 40));
        Console.SetCursorPosition(_board.X, _board.Y + _board.Height + 4);
        Console.WriteLine(message);
    }
}