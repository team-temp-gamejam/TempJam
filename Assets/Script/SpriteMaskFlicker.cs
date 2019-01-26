using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteMaskFlicker : MonoBehaviour
{
    // Start is called before the first frame update
    SpriteMask mask;
    public float timeToChange;
    private float counter;
    private int current = 1;

    public Sprite mk1, mk2, mk3;

    void Start()
    {
        mask = GetComponent<SpriteMask>();
    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
        if (counter > timeToChange) {
            counter = 0;
            current = (current+1)%2;
            if (current == 0 ) mask.sprite = mk1;
            if (current == 1 ) mask.sprite = mk2;
            if (current == 2 ) mask.sprite = mk3;
        }
    }
}
