namespace Cours.function;

public class Grid
{
    public static void DisplayGrid(List<List<char>> grid, int taille)
    {
        string nbCase = "   ";
        string[] Letters = new[]
        {
            "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P",
            "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"
        };
        for (int i = 0; i < taille; i++)
        {
            nbCase += i + " ";
            if (i < 10)
            {
                nbCase += " ";
            }
        }

        Console.WriteLine(nbCase);
        for (int i = 0; i < taille; i++)
        {
            Console.Write(Letters[i] + "  ");
            for (int j = 0; j < taille; j++)
            {
                if (grid[i][j] == 'X')
                {
                    Console.Write("X  ");
                }
                else if (grid[i][j] == '0' || grid[i][j] == '1' || grid[i][j] == '2' || grid[i][j] == '3' || grid[i][j] == '4' || grid[i][j] == '5' || grid[i][j] == '6' || grid[i][j] == '7' || grid[i][j] == '8')
                {
                    Console.Write(grid[i][j] + "  ");
                }
                else
                {
                    Console.Write("X  ");
                }
            }

            Console.WriteLine("");
        }
    }
    
    public static void InitializeGrid(List<List<char>> grid, int taille)
    {
        for (int i = 0; i < taille; i++)
        {
            grid.Add(new List<char>());
            for (int j = 0; j < taille; j++)
            {
                grid[i].Add(' ');
            }
        }
    }
}