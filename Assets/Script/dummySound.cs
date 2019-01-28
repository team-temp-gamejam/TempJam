using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dummySound : MonoBehaviour
{

    public float delay;
    float counter;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
        if (counter > delay) {
            Debug.Log("broadcast sound");
            counter = 0;
            EventRelay.Notify(transform.position, 1);
        }
    }

}
