using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScript : MonoBehaviour
{
    [SerializeField]
    private int row, column;

    public GameObject LeftDoor, RightDoor, UpDoor, DownDoor;

    public void setTilePosition(int row, int column) {
        this.column = column;
        this.row = row;
    }

    public Vector2 getTilePosition() {
        return new Vector2(row, column);
    }

    
}
