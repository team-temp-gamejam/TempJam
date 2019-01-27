using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveDisplay : MonoBehaviour
{

    public float timeToLive;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, timeToLive);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
