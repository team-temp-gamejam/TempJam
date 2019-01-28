using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cupboard : MonoBehaviour
{
    public bool isOpen = false;
    public SpriteRenderer sprite;
    public Sprite open;
    public Sprite closed;
    public GameObject insideThing;
    public Vector2 currentRoom;
    public AudioSource openSound, closeSound;

    private float colliderSizeY;

    void Start()
    {
        sprite = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // if (isOpen)
        // {
        //     sprite.sprite = open;
        // }
        // else
        // {
        //     sprite.sprite = closed;
        // }

        // if (isOpen && Vacant())
        // {
        //     transform.GetComponent<BoxCollider2D>().isTrigger = true;
        // }
        // else
        // {
        //     transform.GetComponent<BoxCollider2D>().isTrigger = false;
        // }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player") {
            col.gameObject.transform.position = transform.position;
            col.gameObject.GetComponent<PlayerControl>().hiding(gameObject);
            // this.isOpen = false;
            // transform.GetComponent<BoxCollider2D>().isTrigger = false;
            SetOpen(false, null);
        }
    }

    public void SetOpen(bool isOpen, PlayerControl opener)
    {
        this.isOpen = isOpen;
        if (isOpen)
        {
            openSound.Play();
            sprite.sprite = open;
            EventRelay.Notify(transform.position, 3);
            if (!Vacant())
            {
                //Debug.Log("Something");
                if(insideThing.GetComponent<Compass>() != null)
                {
                    //Debug.Log("Compass Found");
                    if (!opener.compassCollected)
                    {
                        opener.compassCollected = true;
                        Destroy(insideThing);
                        GetComponent<BoxCollider2D>().isTrigger = true;
                        //Debug.Log("Compass Lost");
                    }
                    else
                    {
                        // Alert

                        insideThing.GetComponentInChildren<SpriteRenderer>().enabled = true;
                    }
                }
                if (insideThing.GetComponent<HourGlass>() != null)
                {

                }
                if (insideThing.GetComponent<PlayerControl>() != null)
                {

                }
                
            // }
            // if (!Vacant()) {
            //     transform.GetComponent<BoxCollider2D>().isTrigger = false;
            } else {
                transform.GetComponent<BoxCollider2D>().isTrigger = true;
            }
            // colliderSizeY = GetComponent<BoxCollider2D>().size.y;
            GetComponent<BoxCollider2D>().size -= (new Vector2(0, 0.2f));

        }
        else
        {
            closeSound.Play();
            sprite.sprite = closed;
            transform.GetComponent<BoxCollider2D>().isTrigger = false;
            if (!Vacant())
            {
                if (insideThing.GetComponent<Compass>() != null)
                {
                    insideThing.GetComponentInChildren<SpriteRenderer>().enabled = false;
                }
            }
            GetComponent<BoxCollider2D>().size +=  (new Vector2(0, 0.2f));
        }
    }

    public bool Vacant()
    {
        return insideThing == null;
    }
}
