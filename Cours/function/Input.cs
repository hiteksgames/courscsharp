namespace Cours.function;

public class Input
{
    public static void WaitInput(List<List<char>> grid, int taille, DateTime startTime, ref int casesRestantes)
    {
        Console.WriteLine("Choisissez une case (sous format A1) : ");
        string? choixCase = Console.ReadLine().ToUpper();
        if (choixCase == "SAVE")
        {
            Save.SaveGame(grid, taille, casesRestantes, startTime);
            Console.WriteLine("Continuer ? (O/N) : ");
            string continuer = Console.ReadLine().ToUpper();
            if (continuer == "O")
            {
                WaitInput(grid, taille, startTime, ref casesRestantes);
            }
            else
            {
                StartMenu.Menu();
            }
        }else if (choixCase == "WIN")
        {
            Console.WriteLine("Félicitations ! Vous avez gagné !");
            LeaderBoard.LeaderBoardAdd(taille, startTime);
        }
        int ligne = choixCase[0] - 'A';
        int colonne = Convert.ToInt32(choixCase.Substring(1));

        if (colonne < 0 || colonne >= taille || ligne < 0 || ligne >= taille)
        {
            Console.Clear();
            Console.WriteLine("Coordonnées invalides !");
            Grid.DisplayGrid(grid, taille);
            WaitInput(grid, taille, startTime, ref casesRestantes);
        }
        else
        {
            int adjMines = Mines.CountAdjacentMines(grid, taille, ligne, colonne);
            Console.WriteLine($"Vous avez choisi la case {choixCase}");
            if (grid[ligne][colonne] == 'X')
            {
                Console.WriteLine("BOUM ! Vous avez perdu !");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Vous avez survécu !");
                grid[ligne][colonne] = adjMines.ToString()[0];
                casesRestantes--;
                Grid.DisplayGrid(grid, taille);

                // Vérification de la victoire
                if (casesRestantes == 0)
                {
                    Console.WriteLine("Félicitations ! Vous avez gagné !");
                    LeaderBoard.LeaderBoardAdd(taille, startTime);
                }
                else
                {
                    WaitInput(grid, taille, startTime, ref casesRestantes);
                }
            }
        }
    }
}