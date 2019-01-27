using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventRelay : MonoBehaviour
{
    // private List<GameObject> players;
    private GameObject ghost;
    // Start is called before the first frame update
    void Start()
    {
        // players = GetComponent<MapManager>().players;
        ghost = GetComponent<MapManager>().ghost;
    }

    public static void Notify(Vector3 pos, int type) {
        List<GameObject> players = GameObject.Find("MapManager").GetComponent<MapManager>().players;
        Vector2 pos2D = new Vector2(pos.x, pos.y);
        foreach (GameObject player in players) {
            player.GetComponent<PlayerControl>().soundVisual.GetComponent<WaveVisual>().Notify(pos2D, type);
        }
        // ghost.soundVisual.Notify(pos, type);
    }
}
