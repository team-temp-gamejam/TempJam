using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScript : MonoBehaviour
{
    [SerializeField]
    private int row, column;

    public GameObject leftDoor, rightDoor, upDoor, downDoor;

    
    public void Start()
    {
        // at the start of the game, doors will be re-arrange to the correct direction according to rotation
        float roomRotation = (transform.rotation.eulerAngles.z % 360);
        GameObject temp;
        while (roomRotation > 0) {
            temp = leftDoor;
            leftDoor = upDoor;
            upDoor = rightDoor;
            rightDoor = downDoor;
            downDoor = temp;
            roomRotation -= 90;
        }
    }

    public void setTilePosition(int row, int column) {
        this.column = column;
        this.row = row;
    }

    public Vector2 getTilePosition() {
        return new Vector2(column, row);
    }



    
}
