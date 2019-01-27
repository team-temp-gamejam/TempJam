using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cupboard : MonoBehaviour
{
    public bool isOpen;
    public SpriteRenderer sprite;
    public Sprite open;
    public Sprite closed;
    public bool vacant;
    public GameObject insideThing;
    public Vector2 currentRoom;
    public AudioSource openSound, closeSound;

    void Start()
    {
        sprite = this.GetComponent<SpriteRenderer>();

        //test
        vacant = true;
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

        if (isOpen && vacant)
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
        
        
        collision.gameObject.GetComponent<PlayerControl>().hiding();
        
    }

    public void SetOpen(bool isOpen, PlayerControl opener)
    {
        this.isOpen = isOpen;
        if (isOpen)
        {
            openSound.Play();
            if (!vacant)
            {
                if(insideThing.GetComponent<Compass>() != null)
                {
                    if (!opener.compassCollected)
                    {
                        opener.compassCollected = true;
                    }
                    else
                    {
                        
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
        }
    }
}
