﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    public float speed;

    private Vector2Int destination;
    private List<List<Room>> map;
    private Vector2Int position;
    private GameObject room;
    private Vector2[] doorPositions = new Vector2[4];
    public int direction;
    private Rigidbody2D rg;

    // Start is called before the first frame update
    void Start()
    {
        position = new Vector2Int();
        rg = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        rg.MovePosition(rg.position + (doorPositions[direction] - rg.position).normalized * speed * Time.deltaTime);
    }

    public void SetMap(List<List<Room>> map)
    {
        this.map = map;
        room = map[position.x][position.y].interior;
        GetDoors();
    }

    public void CrossRoom()
    {
        Vector2Int displacement = new Vector2Int();
        switch (direction)
        {
            case 0:
                displacement = new Vector2Int(0, 1);
                break;
            case 1:
                displacement = new Vector2Int(1, 0);
                break;
            case 2:
                displacement = new Vector2Int(0, -1);
                break;
            case 3:
                displacement = new Vector2Int(-1, 0);
                break;
            default:
                break;
        }
        transform.position += new Vector3(displacement.x * 4, displacement.y * 4);

        position += displacement;
        room = map[position.x][position.y].interior;
        GetDoors();
    }

    void GetDoors()
    {
        DoorDummy[] doors = room.GetComponentsInChildren<DoorDummy>();
        Room roomMap = map[position.x][position.y];

        Vector2 doorPos = doors[0].transform.position;
        if (roomMap.upActive)
        {
            foreach (DoorDummy door in doors)
            {
                if (door.transform.position.y > doorPos.y)
                {
                    doorPos = door.transform.position;
                }
            }
            doorPositions[0] = doorPos;
        }
        if (roomMap.rightActive)
        {
            foreach (DoorDummy door in doors)
            {
                if (door.transform.position.x > doorPos.x)
                {
                    doorPos = door.transform.position;
                }
            }
            doorPositions[1] = doorPos;
        }
        if (roomMap.downActive)
        {
            foreach (DoorDummy door in doors)
            {
                if (door.transform.position.y < doorPos.y)
                {
                    doorPos = door.transform.position;
                }
            }
            doorPositions[2] = doorPos;
        }
        if (roomMap.leftActive)
        {
            foreach (DoorDummy door in doors)
            {
                if (door.transform.position.x < doorPos.x)
                {
                    doorPos = door.transform.position;
                }
            }
            doorPositions[3] = doorPos;
        }
    }

    public void SetDestination(Vector2Int destination)
    {
        this.destination = destination;

    }

    // List<int> ShortestPath(Vector2Int source, Vector2Int destination) {

    // }
}
