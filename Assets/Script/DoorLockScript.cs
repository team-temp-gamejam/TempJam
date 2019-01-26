using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLockScript : InteractItem
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    override public void Interact() {
        DoorScript door = transform.parent.GetComponent<DoorScript>();
        door.SetDoorLock(!door.isDoorLock());
    }
}
