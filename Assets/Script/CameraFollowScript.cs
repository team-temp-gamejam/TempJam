using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{

    [SerializeField]
    private float camSpeed;
    public GameObject player;
    public GameObject compass;
    private Vector2 RoomIndex; // row and column of room in array
    private Vector2 RoomPosition; // position in world coordinate
    
    private Vector2 cameraOffset;
    public  Vector2 AxisMultiply = new Vector2(1, 1);
    
    private Generator generator;

    private int mask;
    
    // Start is called before the first frame update
    void Start()
    {
        mask = this.gameObject.GetComponent<Camera>().cullingMask;
        this.gameObject.GetComponent<Camera>().cullingMask = 0;
        generator = GameObject.Find("Generator").GetComponent<Generator>();
        // compass.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        updateRoom();
        cameraOffset = new Vector2(player.transform.position.x - RoomPosition.x, player.transform.position.y - RoomPosition.y);
        transform.position = Vector2.Lerp(transform.position, RoomPosition + (cameraOffset * AxisMultiply), camSpeed);
        if(((Vector2)transform.position - (RoomPosition + (cameraOffset * AxisMultiply))).magnitude < 0.01)
        {
            this.gameObject.GetComponent<Camera>().cullingMask = mask;
        }
        // if (player.GetComponent<PlayerControl>().compassCollected)
        // {
        //     compass.SetActive(true);
        // }
        this.gameObject.transform.rotation = Quaternion.Euler(0, 0, player.GetComponent<PlayerControl>().orientation);
    }

    public void SetRoom(int column, int row) {
        RoomIndex = new Vector2(column, row);
        GameObject room = GameObject.Find("Generator").GetComponent<Generator>().GetRoom(RoomIndex);
        RoomPosition = new Vector2(room.transform.position.x, room.transform.position.y);
    }

    void updateRoom() {
        RoomIndex = player.GetComponent<PlayerControl>().GetCurrentRoom();
        GameObject room = generator.GetRoom(RoomIndex);
        RoomPosition = new Vector2(room.transform.position.x, room.transform.position.y);
    }

}
