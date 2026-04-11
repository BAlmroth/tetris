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
    private Block _block = new Block(Shapes.T) { X = 2, Y = 0 };
    
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
            if (_block.Y + _block.Shape.GetLength(0) < _board.Height)
                _block.Y = _block.Y + 1;
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
        if (_block.Y + _block.Shape.GetLength(0) < _board.Height)
        {
            _block.Y = _block.Y + 1;
        }
        else
        {
            BottomBlock();
            _block = new Block(Shapes.T) { X = 2, Y = 0 };   
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

    void BottomBlock()
    {
        for (int row = 0; row < _block.Shape.GetLength(0); row++)
        {
            for (int col = 0; col < _block.Shape.GetLength(1); col++)
            {
                //gör om block till tiles i boarden
                if (_block.Shape[row, col] == 1)
                {
                    _board.Tiles[_block.X + col, _block.Y + row] = 1;
                }
            }
        }
    }
}