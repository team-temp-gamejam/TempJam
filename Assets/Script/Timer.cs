using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class Timer : MonoBehaviour
{
    [SerializeField]
    public float timeLeft; 
    public Text timerText;
    public bool timePaused;
    [SerializeField]
    private InGameUIManager gameUI;

    void Start()
    {
        timePaused = false;
        //Time.timeScale = 1; 
    }

    void Update()
    {
        timerText.text = ("" + (int)timeLeft);
        TickTime();
        //Debug.Log(timePaused);

        if ((int)timeLeft == 0)
        {
            //lose the game
            gameUI.losingScreen();
        }
        //if(timeLeft>0 && win condition)
        //{
        //gameUI.winning.SetActive(true);

        //}
    }


    void TickTime()
    {
        if (timeLeft>0 && !timePaused)
        {
           // yield return new WaitForSeconds(1);
            timeLeft -= Time.deltaTime;
            
        }

    }
}