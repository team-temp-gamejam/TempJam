using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dummySound : MonoBehaviour
{
    GameObject mapManager;

    public float delay;
    float counter;

    // Start is called before the first frame update
    void Start()
    {
        mapManager = GameObject.Find("MapManager");
    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
        if (counter > delay) {
            Debug.Log("broadcast sound");
            counter = 0;
            mapManager.GetComponent<EventRelay>().Notify(transform.position, 1);
        }
    }

}
