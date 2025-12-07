using System.Collections.Generic;
using UnityEngine;

public enum Sound
{
    BGM,
    GunShots,
}
public class SFXManager : MonoBehaviour
{
    #region Singleton
    private static SFXManager _instance = null;
    public static SFXManager instance { get => _instance; }
    void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
    }
    #endregion

    #region Structural Setup
    enum SFXType
    {
        BGM, SFX, Voice
    }
    [System.Serializable]
    struct SoundDetails
    {
        public AudioSource audioSource;
        public SFXType SoundType;
    }
    #endregion

    #region Sounds References Taken Via Inspector Here
    [SerializeField] SoundDetails 
    BGM,
    GunShots;
    #endregion
    Dictionary<Sound, SoundDetails> SoundMap;
    
    #region Builtin Functions
    void Start()
    {
        SoundMap = new Dictionary<Sound, SoundDetails>()
        {
            {Sound.BGM , BGM},
        };
    }
    #endregion

    #region Functionality
    public void Play(Sound SoundType)
    {
        if(!SoundMap.ContainsKey(SoundType)) return;

        AdjustVolume(SoundMap[SoundType]);
        AudioSource AS = SoundMap[SoundType].audioSource;
        SFXType type = SoundMap[SoundType].SoundType;
        AS.Play();
    }
    public void UpdateAllVolumes()
    {
        foreach(SoundDetails soundDetails in SoundMap.Values)
            AdjustVolume(soundDetails);
    }
    void AdjustVolume(SoundDetails soundDetails)
    {
        AudioSource AS = soundDetails.audioSource;
        SFXType type = soundDetails.SoundType;
        SaveData saveDataValues = SaveData.instance;
        switch(type)
        {
            case SFXType.BGM:
            AS.volume = saveDataValues.BGM_Volume;
            break;
            case SFXType.SFX:
            AS.volume = saveDataValues.SFX_Volume;
            break;
            case SFXType.Voice:
            AS.volume = saveDataValues.Voice_Volume;
            break;
            default:
            break;
        }
    }
    #endregion
}
