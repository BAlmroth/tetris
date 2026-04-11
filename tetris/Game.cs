//
// This is the game event logic that you can customize and cannibalize
// as needed. You should try to write your game in a modular way, avoid
// making one huge Game class.
//

using tetris;

namespace tetris
{
    
}

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
        Console.WriteLine($"Player pressed key: {key}");
    }

    void Tick()
    {
        _block.Y = _block.Y + 1;
        _board.Draw();
        _block.Draw();
        ScheduleNextTick();
    }

    void ScheduleNextTick()
    {
        // the game will automatically update itself every half a second, adjust as needed
        _timer = new ScheduleTimer(800, Tick);
    }
}