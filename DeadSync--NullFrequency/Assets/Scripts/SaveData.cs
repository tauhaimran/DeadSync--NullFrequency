using UnityEngine;

public class SaveData
{
    #region Singleton
    private static SaveData _instance = null;
    public static SaveData instance
    {
        get
        {
            if (_instance == null)
                _instance = new SaveData();
            return _instance;
        }
    }
    private SaveData() { }
    #endregion

    public float BGM_Volume
    {
        get => PlayerPrefs.GetFloat("BGM_Volume", 1);
        set => PlayerPrefs.SetFloat("BGM_Volume", value);
    }
    public float SFX_Volume
    {
        get => PlayerPrefs.GetFloat("SFX_Volume", 1);
        set => PlayerPrefs.SetFloat("SFX_Volume", value);
    }
    public float Voice_Volume
    {
        get => PlayerPrefs.GetFloat("Voice_Volume", 1);
        set => PlayerPrefs.SetFloat("Voice_Volume", value);
    }

    public void Save()
    {
        PlayerPrefs.Save();
    }
}
