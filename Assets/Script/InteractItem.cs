using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractItem : MonoBehaviour
{
    
    protected bool interactable;

    // Start is called before the first frame update
    void Start()
    {
        interactable = true;
        gameObject.tag = "Interactable";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void Interact();
}
