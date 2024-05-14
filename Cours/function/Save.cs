using System.Text.Json;
using Cours.@class;

namespace Cours.function;

public class Save
{
    public static void LoadSaveGame()
    {
        if (File.Exists("saved_game.json"))
        {
            string jsonString = File.ReadAllText("saved_game.json");
            GameSaveData? saveData = JsonSerializer.Deserialize<GameSaveData>(jsonString);
            int taille = saveData.Taille;
            List<List<char>>? grid = saveData.Grid;
            int casesRestantes = saveData.CasesRestantes;
            DateTime startTime = saveData.StartTime;
            Grid.DisplayGrid(grid, taille);
            Input.WaitInput(grid, taille, startTime, ref casesRestantes);
        }
        else
        {
            Console.WriteLine("Aucune partie sauvegardée trouvée !");
            StartMenu.Menu();
        }
    }

    public static void SaveGame(List<List<char>> grid, int taille, int casesRestantes, DateTime startTime)
    {
        GameSaveData saveData = new GameSaveData();
        saveData.Taille = taille;
        saveData.Grid = grid;
        saveData.CasesRestantes = casesRestantes;
        saveData.StartTime = startTime;
        string jsonString = JsonSerializer.Serialize(saveData);
        File.WriteAllText("saved_game.json", jsonString);
        Console.WriteLine("Le jeu a été sauvegardé avec succès !");
    }
}