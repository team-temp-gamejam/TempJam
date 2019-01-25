using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{

    public int width, height;

    public GameObject[] roomPrefab;

    private List<List<GameObject>> room;

    public GameObject GetRoom(int row, int column) {
        return room[row][column];
    }
}
