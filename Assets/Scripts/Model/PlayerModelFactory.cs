using System.Collections.Generic;

public static class PlayerModelFactory
{
    public static List<PlayerModel> CreatePlayers(int totalPlayers, int playersCount = 1)
    {
        var result = new List<PlayerModel>();
        for (var i = 0; i < playersCount; i++)
        {
            var name = GlobalSettings.PlayerNames.Count > i ? GlobalSettings.PlayerNames[i] :i.ToString();
            result.Add(new PlayerModel(name, GlobalSettings.StartBalance, true));
        }
        for (var i = playersCount; i < totalPlayers; i++)
        {
            result.Add(new PlayerModel(i.ToString(), GlobalSettings.StartBalance, false));
        }
        return result;
    }
}
