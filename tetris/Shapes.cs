namespace tetris;

public class Shapes
{

    public static int[,] I = new int[,]
    {
        { 1, 1, 1, 1 }
    };

    public static int[,] O = new int[,]
    {
        { 1, 1 },
        { 1, 1 }
    };
    
    public static int[,] T = new int[,]
    {
        { 1, 1, 1 },
        { 0, 1, 0 }
    };
    
    public static int[,] L = new int[,]
    {
        { 1, 0 },
        { 1, 0 },
        { 1, 1 }
    };
    
    public static int[,] J = new int[,]
    {
        { 0, 1 },
        { 0, 1 },
        { 1, 1 }
    };
    
    public static int[,] S = new int[,]
    {
        { 0, 1, 1 },
        { 1, 1, 0 }
    };
    
    public static int[,] Z = new int[,]
    {
        { 1, 1, 0 },
        { 0, 1, 1 }
    };
}