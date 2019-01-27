using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cupboard : MonoBehaviour
{
    public bool isOpen;
    public SpriteRenderer sprite;
    public Sprite open;
    public Sprite closed;
<<<<<<< HEAD
    public bool vacant;
    public GameObject insideThing;
    public Vector2 currentRoom;

=======
    public AudioSource openSound, closeSound;
>>>>>>> 9b06287d3487b451a0bc1a1d99abfef3006dd838
    // Start is called before the first frame update
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        
        collision.gameObject.GetComponent<PlayerControl>().hiding();
        
    }

    public void SetOpen(bool isOpen)
    {
        this.isOpen = isOpen;
        if (isOpen)
        {
            openSound.Play();
        }
        else
        {
            closeSound.Play();
        }
    }
}
