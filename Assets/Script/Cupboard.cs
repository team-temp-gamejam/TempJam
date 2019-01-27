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

    void Start()
    {
        sprite = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpen)
        {
            sprite.sprite = open;
        }
        else
        {
            sprite.sprite = closed;
        }

        if (isOpen && Vacant())
        {
            transform.GetComponent<BoxCollider2D>().isTrigger = true;
        }
        else
        {
            transform.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        collision.gameObject.GetComponent<PlayerControl>().transform.position = transform.position;
        collision.gameObject.GetComponent<PlayerControl>().hiding();
        this.isOpen = false;
        transform.GetComponent<BoxCollider2D>().isTrigger = false;
    }

    public void SetOpen(bool isOpen, PlayerControl opener)
    {
        this.isOpen = isOpen;
        if (isOpen)
        {
            openSound.Play();
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
            }
        }
        else
        {
            closeSound.Play();
            if (!Vacant())
            {
                if (insideThing.GetComponent<Compass>() != null)
                {
                    insideThing.GetComponentInChildren<SpriteRenderer>().enabled = false;
                }
            }
        }
    }

    public bool Vacant()
    {
        return insideThing == null;
    }
}
