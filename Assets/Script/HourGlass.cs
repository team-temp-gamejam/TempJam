using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HourGlass : InteractItem
{
    [SerializeField]
    private Timer childrenTime;
    [SerializeField]
    private GhostMovement ghost;
    [SerializeField]
    private int timePlus;

    [SerializeField]
    private AudioSource stopTime;

    private float ghostOriginalSpeed;
    // Start is called before the first frame update
    void Start()
    {
        ghostOriginalSpeed = ghost.speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            //Debug.Log("inputGet");
            stopTime.Play();
            Interact();
            
        }
    }

    override public void Interact()
    {
       // Debug.Log("Coroutine start");
        StartCoroutine("PausedTime");
    }

    IEnumerator PausedTime()
    {
        childrenTime.timePaused = true;
        ghost.speed = 0;
       // Debug.Log(childrenTime.timePaused);
        yield return new WaitForSeconds(timePlus);
        
        childrenTime.timePaused = false;
       // Debug.Log(childrenTime.timePaused);
        ghost.speed = ghostOriginalSpeed;
    }
}
