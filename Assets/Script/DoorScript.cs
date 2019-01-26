using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public GameObject spawnPoint;
    public Vector2Int nextRoomPos;
    private GameObject nextRoom;
    private GameObject nextDoor;
    private bool isLock;
    private Animator animator;
    public GameObject DoorBound;

    private void Start()
    {
        // get local position, regardless of rotation
        //nextRoomPos is actually just direction vector to to other room 
        Vector2 positionFromParent = new Vector2 ((int)(transform.position.x - transform.parent.transform.position.x), (int)(transform.position.y - transform.parent.transform.position.y));
        RoomScript currentRoom = transform.parent.GetComponent<RoomScript>();
        
        if (positionFromParent.x != 0)
            if (positionFromParent.x > 0) {
                nextRoomPos.x = 1;
                currentRoom.rightDoor = this.gameObject;
            }
            else {
                nextRoomPos.x = -1;
                currentRoom.leftDoor = this.gameObject;
            }
        if (positionFromParent.y != 0)
            if (positionFromParent.y > 0) {
                nextRoomPos.y = 1;
                currentRoom.upDoor = this.gameObject;
            }
            else {
                nextRoomPos.y = -1;
                currentRoom.downDoor = this.gameObject;
            }
        //get room location on map array from MapManager
        GameObject mapManager = GameObject.Find("MapManager");      //position in map array is stored in MapManager
        Vector2 tilePos = transform.parent.gameObject.GetComponent<RoomScript>().getTilePosition();
        nextRoom = mapManager.GetComponent<MapManager>().GetRoom((int)(tilePos.y + nextRoomPos.y), (int)(tilePos.x + nextRoomPos.x));
        nextRoomPos *= -1;          //this is flipped to get more understanding of the door that player will be warped to
       
        DoorBound.SetActive(isLock);
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {

         //get object of corresponding door in adjacent room
        if (nextDoor == null) SetNextDoor();

        // move player to other room
        if (col.gameObject.tag == "Player" && !isLock) {
            col.gameObject.GetComponent<PlayerControl>().SetCurrentRoom(nextRoom.GetComponent<RoomScript>().getTilePosition());
            col.gameObject.transform.position =  nextDoor.GetComponent<DoorScript>().spawnPoint.transform.position;
            DoorFlip();
            nextDoor.GetComponent<DoorScript>().DoorFlip();
        }
        // else if (col.gameObject.tag == "Ghost")
        // {
        //     col.gameObject.GetComponent<GhostMovement>().CrossRoom(nextDoor.GetComponent<DoorScript>().spawnPoint.transform.position);
        // }
    }

    public void DoorFlip() {
        animator.SetTrigger("PassDoor");
    }

    public void SetDoorLock(bool isLock) {
        if (nextDoor == null) SetNextDoor();
        if (this.isLock != isLock) {
            this.isLock = isLock;
            animator.SetBool("isLock", isLock);
            nextDoor.GetComponent<DoorScript>().SetDoorLock(isLock);
            DoorBound.SetActive(isLock);
        }
    }

    public bool isDoorLock() {
        return isLock;
    }

    private void SetNextDoor() {
         if (nextRoomPos.x != 0) 
         {
            if (nextRoomPos.x > 0) 
            {
                nextDoor = nextRoom.GetComponent<RoomScript>().rightDoor;
            }
            else
            {
                nextDoor = nextRoom.GetComponent<RoomScript>().leftDoor;
            }
        }
        if (nextRoomPos.y != 0) {
            if (nextRoomPos.y < 0) 
                nextDoor = nextRoom.GetComponent<RoomScript>().downDoor;
            else
                nextDoor = nextRoom.GetComponent<RoomScript>().upDoor;
        }


        
    }

}
