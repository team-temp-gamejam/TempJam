using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{

    public List<GameObject> cupboards;
    public List<GameObject> doors;
    public List<GameObject> players;
    public GameObject ghost;

    void Start()
    {

    }

    public void ScanMap() {
        Transform map = GameObject.Find("Map").transform;
        if (map != null) {
            foreach (Transform room in map) {
                foreach (Transform item in room) {
                    if (item.gameObject.tag == "Player") {
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
}
