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

        if (timeLeft == 0)
        {
            //lose the game
        }

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