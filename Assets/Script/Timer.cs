using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class Timer : MonoBehaviour
{
    [SerializeField]
    public int timeLeft; 
    public Text timerText;

    void Start()
    {
        StartCoroutine("TickTime");
        //Time.timeScale = 1; 
    }

    void Update()
    {
        timerText.text = ("" + timeLeft);
        
        //lose the game
        if(timeLeft == 0)
        {

        }

    }


    IEnumerator TickTime()
    {
        while (timeLeft>0)
        {
            yield return new WaitForSeconds(1);
            timeLeft--;
        }

    }
}