using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveVisual : MonoBehaviour
{

    public GameObject stepWave, doorWave, cupboardWave;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Notify(Vector2 soundPosition, int type) {
        float distance = Vector2.Distance(soundPosition, transform.position);
        float angle = Vector2.SignedAngle( Vector2.up, soundPosition - new Vector2(transform.position.x, transform.position.y));
        GameObject sound;
        if (distance > 2 && distance < 13) {
            if (type == 2) {
                sound = doorWave;
            } else 
            if (type == 3) {
                sound = cupboardWave;
            } else 
            sound = stepWave;

            // GameObject wave = Instantiate(sound, transform.position, Quaternion.Euler(0, 0, angle));
            GameObject wave = Instantiate(sound, transform.position, Quaternion.Euler(0, 0, angle), transform.parent);
            Color current = wave.GetComponent<SpriteRenderer>().color;
            wave.GetComponent<SpriteRenderer>().color = new Color(current.r, current.g, current.b, 1-(distance/13));
        }
    }
}
