using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeUIManager : MonoBehaviour
{
    [SerializeField] GameObject MainMenu;
    [SerializeField] GameObject MainMenu_Buttons;
    [SerializeField] GameObject SettingsPanel;
    [SerializeField] GameObject LevelSelectPanel;

    [SerializeField] Slider BGM_VolumeSlider, SFX_VolumeSlider, Voice_VolumeSlider;
    #region Builtin Functions
    void Start()
    {
        SFXManager.instance.Play(Sound.BGM);

        SaveData saveData = SaveData.instance;

        BGM_VolumeSlider.value = saveData.BGM_Volume;
        SFX_VolumeSlider.value = saveData.SFX_Volume;
        Voice_VolumeSlider.value = saveData.Voice_Volume;

        MainMenu.SetActive(true);
        MainMenu_Buttons.SetActive(true);
        SettingsPanel.SetActive(false);
        LevelSelectPanel.SetActive(false);
    }
    #endregion
    #region MainMenu Button Functionality
    public void OnClick_Play()
    {
        MainMenu_Buttons.SetActive(false);
        LevelSelectPanel.SetActive(true);
    }
    public void OnClick_LevelSelectBack()
    {
        MainMenu_Buttons.SetActive(true);
        LevelSelectPanel.SetActive(false);
    }
    public void OnClick_SettingsBack()
    {
        MainMenu_Buttons.SetActive(true);
        SettingsPanel.SetActive(false);
    }
    public void OnCLick_Settings()
    {
        MainMenu_Buttons.SetActive(false);
        SettingsPanel.SetActive(true);
    }
    public void OnClick_Quit()
    {
        SaveData.instance.Save();
        Application.Quit();
    }
    public void LevelSelect(int LevelNumber)
    {
        //SceneManager.LoadScene($"Level{LevelNumber}");
        SceneManager.LoadScene("MainMap");
    }
    #endregion
    #region Volume Functionality
    public void OnVolumeChange()
    {
        SaveData saveData = SaveData.instance;

        saveData.BGM_Volume = BGM_VolumeSlider.value;
        saveData.SFX_Volume = SFX_VolumeSlider.value;
        saveData.Voice_Volume = Voice_VolumeSlider.value;

        SFXManager.instance.UpdateAllVolumes();
    }
    #endregion
}
