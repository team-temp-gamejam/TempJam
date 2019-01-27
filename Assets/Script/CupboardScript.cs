using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupboardScript : InteractItem
{
    private PlayerControl interactingPlayer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        interactingPlayer = collision.gameObject.GetComponent<PlayerControl>();
    }


    override public void Interact()
    {
        Cupboard parent = transform.parent.GetComponent<Cupboard>();
        if(!parent.isOpen)parent.SetOpen(!parent.isOpen);
        else if (!parent.vacant)
        {
            if (parent.insideThing.name == "Compass")
            {
                interactingPlayer.compassCollected = true;
            }
            if(parent.insideThing.tag == "InteractItem")
            {
                parent.GetComponent<InteractItem>().Interact();
            }
        }
        else if (parent.isOpen) parent.SetOpen(!parent.isOpen);


    }
}
