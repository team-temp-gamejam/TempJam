using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupboardScript : InteractItem
{
    [SerializeField]
    private PlayerControl interactingPlayer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            interactingPlayer = collision.gameObject.GetComponent<PlayerControl>();
        }
    }


    override public void Interact()
    {
        Cupboard parent = transform.parent.GetComponent<Cupboard>();
        parent.SetOpen(!parent.isOpen, interactingPlayer);
    }
}
