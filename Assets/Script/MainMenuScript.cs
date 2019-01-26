using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField]
    private GameObject exitGamePanel;
    [SerializeField]
    private GameObject optionPanel;
    [SerializeField]
    private GameObject gameSettingPanel;
    [SerializeField]
    private Button startButton, optionButton, exitButton, exitConfirmButton, exitCancelButton, optionBackButton, backFromGameSettingButton, gameStartButton;
    [SerializeField]
    private Slider sfxSlider, bgmSlider;

    // Start is called before the first frame update
    void Start()
    {
        
        startButton.onClick.AddListener(openGameSetting);
        backFromGameSettingButton.onClick.AddListener(backFromGameSetting);
        gameStartButton.onClick.AddListener(startGame);
        optionButton.onClick.AddListener(option);
        optionBackButton.onClick.AddListener(backOption);
        exitButton.onClick.AddListener(exitGame);
        exitConfirmButton.onClick.AddListener(exitConfirm);
        exitCancelButton.onClick.AddListener(exitCancel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openGameSetting()
    {
        gameSettingPanel.SetActive(true);
    }

    public void backFromGameSetting()
    {
        gameSettingPanel.SetActive(false);
    }

    public void startGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //open option
    public void option()
    {
        optionPanel.SetActive(true);
    }

    public void applyVolume(float volume)
    {
        volume = sfxSlider.value;
        Debug.Log(volume);
    }

    //close option
    public void backOption()
    {
        optionPanel.SetActive(false);
    }

    //open exit confirmation
    public void exitGame()
    {
        exitGamePanel.SetActive(true);
    }

    //exit game
    public void exitConfirm()
    {
        Application.Quit();
    }

    //back to game
    public void exitCancel() 
    {
        exitGamePanel.SetActive(false);
    }
}
