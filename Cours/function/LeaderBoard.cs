using System.Text.Json;
using Cours.@class;

namespace Cours.function;

public class LeaderBoard
{
    public static void LeaderBoardAdd(int taille, DateTime startTime)
    {
        DateTime endTime = DateTime.Now;
        Console.WriteLine("Entrez votre nom pour le leaderboard : ");
        string? nom = Console.ReadLine();
        Leaderboard leaderboard = new Leaderboard();
        leaderboard.Nom = nom;
        leaderboard.duree = (int)(endTime - startTime).TotalSeconds;
        switch (taille)
        {
            case 10:
                leaderboard.dificulty = 1;
                break;
            case 20:
                leaderboard.dificulty = 2;
                break;
            case 26:
                leaderboard.dificulty = 3;
                break;
        }
        string jsonString = File.ReadAllText("leaderboard.json");
        List<Leaderboard>? leaderboardList = new List<Leaderboard>();
        if (!string.IsNullOrEmpty(jsonString) && jsonString.StartsWith("[") && jsonString.EndsWith("]"))
        {
            leaderboardList = JsonSerializer.Deserialize<List<Leaderboard>>(jsonString);
        }
        leaderboardList?.Add(leaderboard);
        jsonString = JsonSerializer.Serialize(leaderboardList);
        File.WriteAllText("leaderboard.json", jsonString);
        StartMenu.Menu();
    }
    
    public static void LeaderBoardShow()
    {
        int? loop = 0;
        string? jsonString = File.ReadAllText("leaderboard.json");
        List<Leaderboard>? leaderboardList = JsonSerializer.Deserialize<List<Leaderboard>>(jsonString);
        leaderboardList?.Sort((x, y) => x.duree.CompareTo(y.duree));
        foreach (Leaderboard? leader in leaderboardList)
        {
            loop++;
            Console.WriteLine($"Nom : {leader.Nom} | Durée : {leader.duree} | Difficulté : {leader.dificulty}");
            if (loop == 10)
            {
                break;
            }
        }
        Thread.Sleep(5000);
        StartMenu.Menu();
    }
}