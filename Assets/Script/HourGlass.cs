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
            interacted();
            
        }
    }

    override public void interacted()
    {
       // Debug.Log("Coroutine start");
        StartCoroutine("PausedTime");
    }

    IEnumerator PausedTime()
    {
        childrenTime.timePaused = true;
        ghost.speed = 0;
       // Debug.Log(childrenTime.timePaused);
        yield return new WaitForSeconds(1);
        
        childrenTime.timePaused = false;
       // Debug.Log(childrenTime.timePaused);
        ghost.speed = ghostOriginalSpeed;
    }
}
