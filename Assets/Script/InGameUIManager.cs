using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameUIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject pausedGamePanel, optionPanel, exitToMainMenuPanel, exitGamePanel;


    [SerializeField]
    private Button resumeButton, optionButton, exitToMainMenuButton, exitGameButton, backOptionButton, yesExitMainButton, noExitMainButton, yesExitGameButton, noExitGameButton;

    [SerializeField]
    private GameObject p1Alert, p2Alert, p3Alert, p4Alert;

    [SerializeField]
    private Text p1AlertText, p2AlertText, p3AlertText, p4AlertText;

    [SerializeField]
    private PlayerControl p1, p2, p3, p4;

    private bool gameIsPaused;
    public bool p1Alerting, p2Alerting, p3Alerting, p4Alerting;

    // Start is called before the first frame update
    void Start()
    {
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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gamePaused();
        }

        if (p1Alerting && Input.GetButtonDown("p1Action"))
        {
            p1Alert.SetActive(false);
        }

        if (p2Alerting && Input.GetButtonDown("p2Action"))
        {
            p2Alert.SetActive(false);
        }

        if (p3Alerting && Input.GetButtonDown("p3Action"))
        {
            p3Alert.SetActive(false);
        }

        if (p4Alerting && Input.GetButtonDown("p4Action"))
        {
            p4Alert.SetActive(false);
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
        if (player == 1)
        {
            p1AlertText.text = "You already have a compass!";
            p1Alert.SetActive(true);
            p1Alerting = true;
        }
        if (player == 2)
        {
            p2AlertText.text = "You already have a compass!";
            p2Alert.SetActive(true);
            p2Alerting = true;
        }
        if (player == 3)
        {
            p3AlertText.text = "You already have a compass!";
            p3Alert.SetActive(true);
            p3Alerting = true;
        }
        if (player == 4)
        {
            p4AlertText.text = "You already have a compass!";
            p4Alert.SetActive(true);
            p4Alerting = true;
        }
    }

    private void BustedAlert(int player)
    {
        if(player == 1)
        {
            p1AlertText.text = "You are busted by a ghost.";
            p1Alert.SetActive(true);
            p1Alerting = true;
        }
        if(player == 2)
        {
            p2AlertText.text = "You are busted by a ghost.";
            p2Alert.SetActive(true);
            p2Alerting = true;
        }
        if (player == 3)
        {
            p3AlertText.text = "You are busted by a ghost.";
            p3Alert.SetActive(true);
            p3Alerting = true;
        }
        if (player == 4)
        {
            p4AlertText.text = "You are busted by a ghost.";
            p4Alert.SetActive(true);
            p4Alerting = true;
        }
    }
}
