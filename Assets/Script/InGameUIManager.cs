using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class InGameUIManager : MonoBehaviour
{
    [SerializeField]
    private AudioMixer masterMixer;

  [SerializeField]
    private GameObject pausedGamePanel, optionPanel, exitToMainMenuPanel, exitGamePanel;


    [SerializeField]
    private Button resumeButton, optionButton, exitToMainMenuButton, exitGameButton, backOptionButton, yesExitMainButton, noExitMainButton, yesExitGameButton, noExitGameButton;

 
    [SerializeField]
    private Slider SFXSlider, BGMSlider;



    [SerializeField]
    public GameObject winning, losing;

    [SerializeField]
    private Button retry, toMainLose, playAgain, toMainWin;

    private Scene thisScene;
    private bool gameIsPaused;
    public bool p1Alerting, p2Alerting, p3Alerting, p4Alerting;

    // Start is called before the first frame update
    void Start()
    {
        thisScene = SceneManager.GetActiveScene();
        gameIsPaused = false;
        resumeButton.onClick.AddListener(gamePaused);
        optionButton.onClick.AddListener(openOption);
        backOptionButton.onClick.AddListener(closeOption);
        exitToMainMenuButton.onClick.AddListener(openExitToMainMenu);
        noExitMainButton.onClick.AddListener(closeExitToMainMenu);
        yesExitMainButton.onClick.AddListener(exitToMainMenu);
        exitGameButton.onClick.AddListener(openExitGame);
        noExitGameButton.onClick.AddListener(closeExitGame);
        yesExitGameButton.onClick.AddListener(exitGame);
        SFXSlider.onValueChanged.AddListener(SFXAdjust);
        BGMSlider.onValueChanged.AddListener(BGMAdjust);
        toMainLose.onClick.AddListener(exitToMainMenu);
        toMainWin.onClick.AddListener(exitToMainMenu);
        retry.onClick.AddListener(playGameAgain);
        playAgain.onClick.AddListener(playGameAgain);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gamePaused();
        }


        //if player collect another compass

        //if player is busted
    }

    void gamePaused()
    {
        
        pausedGamePanel.SetActive(!gameIsPaused);
        if (!gameIsPaused) Time.timeScale = 0;
        else Time.timeScale = 1;

        gameIsPaused = !gameIsPaused;
    }

    void openOption()
    {
        optionPanel.SetActive(true);
        changeToAnotherMenu();
    }

    void closeOption()
    {
        optionPanel.SetActive(false);
        changeToAnotherMenu();
    }

    void SFXAdjust(float soundLevel) 
    {
        masterMixer.SetFloat("SFX", soundLevel);
    }
    void BGMAdjust(float soundLevel) 
    {
        masterMixer.SetFloat("BGM", soundLevel);
    }

    void openExitToMainMenu()
    {
        exitToMainMenuPanel.SetActive(true);
        changeToAnotherMenu();
    }

    void exitToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -2);
    }

    void closeExitToMainMenu()
    {
        exitToMainMenuPanel.SetActive(false);
        changeToAnotherMenu();
    }

    void openExitGame()
    {
        exitGamePanel.SetActive(true);
        changeToAnotherMenu();
    }

    void exitGame()
    {
        Application.Quit();
    }

    void closeExitGame()
    {
        exitGamePanel.SetActive(false);
        changeToAnotherMenu();
    }

    private void changeToAnotherMenu()
    {
        if (pausedGamePanel.activeInHierarchy) pausedGamePanel.SetActive(false);
        else pausedGamePanel.SetActive(true);
    }

    private void Alert(int player)
    {
       
    }

    private void BustedAlert(int player)
    {
        
       
    }
    private void playGameAgain()
    {
        Application.LoadLevel(thisScene.name);
    }
    public void losingScreen()
    {
        losing.SetActive(true);
    }
    public void winningScreen()
    {
        winning.SetActive(true);
    }
}
