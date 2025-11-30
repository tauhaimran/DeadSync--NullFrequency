public static class SaveGame
{
    public static int Coins
    {
        get => PlayerPrefs.GetInt("coins",0);
        set => PlayerPrefs.SetInt("coins",value);
    }
}
