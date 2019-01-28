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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            interactingPlayer = collision.gameObject.GetComponent<PlayerControl>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            if(collision.gameObject.GetComponent<PlayerControl>() == interactingPlayer)
                interactingPlayer = null;
        }
    }


    override public void Interact()
    {
        Cupboard parent = transform.parent.GetComponent<Cupboard>();
        parent.SetOpen(!parent.isOpen, interactingPlayer);
        // Vector2 backstep = (Vector2)transform.parent.position - (Vector2)interactingPlayer.gameObject.transform.position;
        // backstep = backstep.normalized/10;
        // Vector3 playerPosition = interactingPlayer.gameObject.transform.position;
        // interactingPlayer.gameObject.transform.position = new Vector3(playerPosition.x - backstep.x, playerPosition.y - backstep.y, playerPosition.z);
    }
}
