using System.Collections.Generic;

public class GlobalSettings
{
    public static int MaxChipInBet { get; } = 10;
    public static int ChipSize { get; set; } = 50;
    public static List<string> PlayerNames { get; } = new List<string>() { "Player", "Player2", "Player3", "Player4", "Player5" };
    public static int StartBalance { get; set; } = 1000;
    public static float ChipSpeed { get; } = 30;
    public static float SpeedDispersion { get; } = 10f;

    public static int TotalPlayerCount { get; set; } = 5;
    public static int PlayerCounr { get; set; } = 1;
}
