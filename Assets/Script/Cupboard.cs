using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cupboard : MonoBehaviour
{
    public bool isOpen;
    public SpriteRenderer sprite;
    public Sprite open;
    public Sprite closed;
    public AudioSource openSound, closeSound;
    // Start is called before the first frame update
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
