using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompassBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject needle;
    [SerializeField]
    private GameObject world;
    private Vector2 dir;
    private Quaternion needleRotation;

    [SerializeField]
    private bool collected;

    //public bool collected;
    // Start is called before the first frame update
    void Awake()
    {
        //fix the neeedle rotation to the world map
        needleRotation = world.transform.rotation;
    }
    void Start()
    {
        collected = false;

        //not collected compass will be not shown
        GetComponent<Renderer>().enabled = collected;
    }
        // Update is called once per frame
    void Update()
    {
        //shown when it's been collected
        GetComponent<Renderer>().enabled = collected;
        //change when there are rotation of camera
        needle.transform.rotation = needleRotation;
    }

    //getter setter for collected
    public void setCollected(bool collected)
    {
        this.collected = collected;
    }
    public bool getCollected()
    {
        return collected;
    }
}
