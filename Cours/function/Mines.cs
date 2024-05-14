namespace Cours.function;

public class Mines
{
    public static void PlaceMinesRandomly(List<List<char>> grid, int taille, int nbMines)
    {
        Random rand = new Random();

        while (nbMines > 0)
        {
            int x = rand.Next(0, taille);
            int y = rand.Next(0, taille);

            if (grid[x][y] != 'X')
            {
                grid[x][y] = 'X';
                nbMines--;
            }
        }
    }
    
    public static int CountAdjacentMines(List<List<char>> grid, int taille, int ligne, int colonne)
    {
        int count = 0;

        for (int i = ligne - 1; i <= ligne + 1; i++)
        {
            for (int j = colonne - 1; j <= colonne + 1; j++)
            {
                if (i >= 0 && i < taille && j >= 0 && j < taille)
                {
                    if (grid[i][j] == 'X')
                    {
                        count++;
                    }
                }
            }
        }

        return count;
    }
    
    public static int CalculateNumberOfMines(int taille, int difficulte)
    {
        int nbMines = 0;
        if (difficulte == 1)
        {
            nbMines = taille * taille / 10;
        }
        else if (difficulte == 2)
        {
            nbMines = taille * taille / 5;
        }
        else if (difficulte == 3)
        {
            nbMines = taille * taille / 3;
        }
        return nbMines;
    }
    
}