﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    public float speed;

    public Vector2Int destination;
    private List<List<Room>> map;
    private Vector2Int position;
    private GameObject room;
    private Vector2[] doorPositions = new Vector2[4];
    public List<int> direction = new List<int>();
    private Rigidbody2D rg;

    // Start is called before the first frame update
    void Start()
    {
        position = new Vector2Int();
        rg = GetComponent<Rigidbody2D>();
        //SetDestination(destination);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (direction.Count > 0)
        {
            rg.MovePosition(rg.position + (doorPositions[direction[0]] - rg.position).normalized * speed * Time.deltaTime);
        }
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
        switch (direction[0])
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
        direction.RemoveAt(0);
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
        direction = ShortestPath(map[position.x][position.y], map[destination.x][destination.y]);
    }

    List<int> ShortestPath(Room source, Room destination)
    {
        List<Room> traversed = new List<Room>();
        List<List<Room>> path = new List<List<Room>>();
        List<Room> resultPath = new List<Room>();
        traversed.Add(source);
        List<Room> firstPath = new List<Room>();
        firstPath.Add(source);
        path.Add(firstPath);

        List<Room> currentPath;
        Room currentRoom;
        Room currentUp;
        Room currentRight;
        Room currentDown;
        Room currentLeft;
        while (true)
        {
            currentPath = path[0];
            path.RemoveAt(0);
            currentRoom = currentPath[currentPath.Count - 1];
            currentPath.Add(new Room());
            if (currentRoom.upActive && !traversed.Contains(currentRoom.up))
            {
                currentUp = currentRoom.up;
                currentPath[currentPath.Count - 1] = currentUp;
                if (currentUp == destination)
                {
                    resultPath = new List<Room>(currentPath);
                    break;
                }
                path.Add(new List<Room>(currentPath));
            }
            if (currentRoom.rightActive && !traversed.Contains(currentRoom.right))
            {
                currentRight = currentRoom.right;
                currentPath[currentPath.Count - 1] = currentRight;
                if (currentRight == destination)
                {
                    resultPath = new List<Room>(currentPath);
                    break;
                }
                path.Add(new List<Room>(currentPath));
            }
            if (currentRoom.downActive && !traversed.Contains(currentRoom.down))
            {
                currentDown = currentRoom.down;
                currentPath[currentPath.Count - 1] = currentDown;
                if (currentDown == destination)
                {
                    resultPath = new List<Room>(currentPath);
                    break;
                }
                path.Add(new List<Room>(currentPath));
            }
            if (currentRoom.leftActive && !traversed.Contains(currentRoom.left))
            {
                currentLeft = currentRoom.left;
                currentPath[currentPath.Count - 1] = currentLeft;
                if (currentLeft == destination)
                {
                    resultPath = new List<Room>(currentPath);
                    break;
                }
                path.Add(new List<Room>(currentPath));
            }
        }

        List<int> result = new List<int>();
        Room current;
        Room previous;
        for (int i = 1; i < resultPath.Count; i++)
        {
            current = resultPath[i];
            previous = resultPath[i - 1];
            if (current.up == previous)
            {
                result.Add(2);
            }
            else if (current.right == previous)
            {
                result.Add(3);
            }
            else if (current.down == previous)
            {
                result.Add(0);
            }
            else if (current.left == previous)
            {
                result.Add(1);
            }
        }

        return result;
    }
}
