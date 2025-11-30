using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeUIManager : MonoBehaviour
{
    [SerializeField] GameObject MainMenu;
    [SerializeField] GameObject MainMenu_Buttons;
    [SerializeField] GameObject SettingsPanel;
    [SerializeField] GameObject LevelSelectPanel;
    #region Builtin Functions
    void Start()
    {
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
        Application.Quit();
    }
    public void LevelSelect(int LevelNumber)
    {
        //SceneManager.LoadScene($"Level{LevelNumber}");
        SceneManager.LoadScene("MainMap");
    }
    #endregion
}
