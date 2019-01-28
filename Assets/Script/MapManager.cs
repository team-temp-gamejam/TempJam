using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{

    public static MapManager instance;

    public List<GameObject> cupboards;
    public List<GameObject> doors;
    public List<GameObject> players;
    public GameObject ghost;

    void Awake()
    {
        if (instance == null) {
            instance = this;
        } else {
            Destroy (gameObject);
            return;
        }
        
    }

    public void ScanMap()
    {
        Transform map = GameObject.Find("Map").transform;
        if (map != null)
        {
            foreach (Transform room in map)
            {
                foreach (Transform item in room)
                {
                    if (item.gameObject.tag == "Player")
                    {
                        players.Add(item.gameObject);
                    }
                    if (item.gameObject.tag == "Cupboard")
                    {
                        cupboards.Add(item.gameObject);
                    }
                    if (item.gameObject.tag == "Door")
                    {
                        doors.Add(item.gameObject);
                    }
                    if (item.gameObject.tag == "Ghost")
                    {
                        ghost = item.gameObject;
                    }
                }
            }
        }
    }

    public bool CheckWinCondition()
    {
        bool win = true;
        Vector2 roomPosition = players[0].GetComponent<PlayerControl>().GetCurrentRoom();
        foreach (GameObject player in players)
        {
            if (roomPosition != player.GetComponent<PlayerControl>().GetCurrentRoom())
            {
                win = false;
                break;
            }
        }
        return win;
    }
}
