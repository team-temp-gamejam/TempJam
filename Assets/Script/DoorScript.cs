using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public GameObject spawnPoint;
    public Vector2Int nextRoomPos;
    private GameObject nextRoom;
    private GameObject nextDoor;

    private void Start() {
        // get local position, regardless of rotation
        //nextRoomPos is actually just direction vector to to other room 
        Vector2 positionFromParent = new Vector2 ((int)(transform.position.x - transform.parent.transform.position.x), (int)(transform.position.y - transform.parent.transform.position.y));
        if (positionFromParent.x != 0)
            nextRoomPos.x = positionFromParent.x > 0?1:-1;
        if (positionFromParent.y != 0)
            nextRoomPos.y = positionFromParent.y < 0?1:-1;
        
        //get room location on map array from MapManager
        GameObject mapManager = GameObject.Find("MapManager");      //position in map array is stored in MapManager
        Vector2 tilePos = transform.parent.gameObject.GetComponent<RoomScript>().getTilePosition();
        nextRoom = mapManager.GetComponent<MapManager>().GetRoom((int)(tilePos.y + nextRoomPos.y), (int)(tilePos.x + nextRoomPos.x));
        nextRoomPos *= -1;          //this is flipped to get more understanding of the door that player will be warped to
        
       
    }

    void OnTriggerEnter2D(Collider2D col) {

         //get object of corresponding door in adjacent room
        if (nextRoomPos.x != 0) {
            if (nextRoomPos.x > 0) {
                nextDoor = nextRoom.GetComponent<RoomScript>().rightDoor;
            }
            else {
                nextDoor = nextRoom.GetComponent<RoomScript>().leftDoor;
            }
        }
        if (nextRoomPos.y != 0) {
            if (nextRoomPos.y > 0) 
                nextDoor = nextRoom.GetComponent<RoomScript>().downDoor;
            else 
                nextDoor = nextRoom.GetComponent<RoomScript>().upDoor;
        }

        // move player to other room
        if (col.gameObject.tag == "Player")
            col.gameObject.transform.position =  nextDoor.GetComponent<DoorScript>().spawnPoint.transform.position;
    }

}
