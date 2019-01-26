using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{

    public int width, height;

    public GameObject[] roomPrefab;

    // private List<List<GameObject>> room;
    public List<GameObject> room;
    

    public GameObject GetRoom(int row, int column) {
        // return room[row][column];
        return room[row*width + column];
    }

    public GameObject GetRoom(Vector2 index) {
        return room[(int)index.x + (int)index.y*width];
    }
}
