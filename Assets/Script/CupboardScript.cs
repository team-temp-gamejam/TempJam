using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupboardScript : InteractItem
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    override public void Interact()
    {
        Cupboard parent = transform.parent.GetComponent<Cupboard>();
        parent.SetOpen(!parent.isOpen);
    }
}
